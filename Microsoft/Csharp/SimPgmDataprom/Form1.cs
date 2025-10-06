using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;




using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Asn1.X9;
using System.IO.Ports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Crypto.Digests;
using System.Net.Sockets;
using System.Security.Cryptography;
using Org.BouncyCastle.Utilities;

namespace SimPgmDataprom
{
    public partial class Form1 : Form
    {
        static AsymmetricCipherKeyPair parChavesProgramador = null, parChavesAntares = null;
        static ECPublicKeyParameters chavePublicaControlador = null, chavePublicaControladorAnt = null;        
        byte[] SegredoCompartilhado = null, SegredoCompartilhadoAnt = null;
        byte[] IKM = null, IKMAnt;        
        UInt64 contadorMensagens = 0, contadorMensagensAnt = 0;
        private const int MaxFrameSize = 4096; // 4KB
        NetworkStream stream = null;
        Byte[] aeskey = null, aeskeyAnt = null;
        Task trataCliente = null;



        public Form1()
        {
            InitializeComponent();            
        }

        

        static AsymmetricCipherKeyPair GenerateKeyPair()
        {
            var curve = ECNamedCurveTable.GetByName("secp256r1");
            var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
            var keyGenParams = new ECKeyGenerationParameters(domainParams, new SecureRandom());

            var keyGen = new ECKeyPairGenerator();
            keyGen.Init(keyGenParams);
            return keyGen.GenerateKeyPair();
        }

        private void btConectar_Click(object sender, EventArgs e)
        {
            if (btConectar.Text == "Conectar")
            {     
                //CONECTAR
                if (comboBox1.SelectedIndex >= 0)
                {
                    try
                    {
                        serialPort1.PortName = comboBox1.SelectedItem.ToString();
                        serialPort1.Open();
                        btPubKey.Enabled = true;
                        btDataHora.Enabled = true;
                    }
                    catch
                    {
                        MessageBox.Show("Erro Abrindo a porta escolhida!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Escolha uma porta COM");
                    return;
                }

                btConectar.Text = "Desconectar";
                comboBox1.Enabled = false; 
                    
                tsslStatus.Text = "TX QUADRO NÃO SEGURO[BD]: - Solicitou Informações do controlador pela porta " + comboBox1.SelectedItem.ToString();                

                Byte[] enderecoBytes = DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY,0,0); //63 eh um dummy do protocolo DP
                DatapromFrame frame = DatapromFrame.ConstroiFrameQNS(enderecoBytes, OpcodesDP.ENVIA_IDENTIFICACAO_8D, null); //Constroi Quadro Nao Seguro
                Byte[] frameVetorizado = DatapromFrame.VetorizaQuadro(frame);
                serialPort1.Write(frameVetorizado, 0, frameVetorizado.Length);

                BeginInvoke(new Action(() => {                    

                    tbIterRX.Text = "";
                    tbQtRX.Text = "";
                    tbSessaoRX.Text = "";
                    tbTagRX.Text = "";
                    cbCriptoRX.Enabled = false;
                    tbPacotesTX.Text += "Serial: " + BitConverter.ToString(frameVetorizado).Replace("-", " ") + Environment.NewLine;

                    tbIterTX.Text = "-";
                    tbQtTX.Text = "0";
                    tbSessaoTX.Text = "-";
                    tbTagTX.Text = "-";
                    cbCriptoTX.Enabled = false;


                    
                })); // Atualizar o TextBox na thread principal

            }
            else
            {
                //DESCONECTAR
                parChavesProgramador = null;
                SegredoCompartilhado = null;
                IKM = null;
                contadorMensagens = 0;                
                comboBox1.Enabled=true;
                btPubKey.Enabled = false;
                btDataHora.Enabled = false;
                tsslStatus.Text = "Desconectado";
                btConectar.Text = "Conectar";
                aeskey = null;
                //DatapromFrame.aesKey = null;

                BeginInvoke(new Action(() =>    {
                    tbChaveLocalPriv.Text = "";
                    tbChaveLocalPub.Text = "";                    

                    tbIterRX.Text = ""; 
                    tbQtRX.Text = "";
                    tbSessaoRX.Text = "";
                    tbTagRX.Text = "";
                    cbCriptoRX.Enabled = false;

                    tbIterTX.Text = "";
                    tbQtTX.Text = "";
                    tbSessaoTX.Text = "";
                    tbTagTX.Text = "";
                    cbCriptoTX.Enabled = false;

                    tbChaveRemPub.Text = ""; 
                    tbSegredo.Text = "";
                    tbIKM.Text = "";                    
                    tbAESkey.Text = "";

                    tbCodigo.Text = "";                    
                    tbArea.Text = "";
                    tbRede.Text = "";
                    tbTabela.Text = "";
                    tbProtocolo.Text = "";
                    tbSw.Text = "";
                    tbDescricao.Text = "";

                })); // Atualizar o TextBox na thread principal
                                                      
                serialPort1.Close();
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(ports);
        }

        private List<byte> buffer = new List<byte>();
        
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int bytesToRead = sp.BytesToRead;
            byte[] tempBuffer = new byte[bytesToRead];
            sp.Read(tempBuffer, 0, bytesToRead);

            // Armazena os bytes recebidos no buffer contínuo
            buffer.AddRange(tempBuffer);

            while (true)
            {
                int startIdx = buffer.IndexOf(0x02); // Início do quadro
                int endIdx = buffer.IndexOf(0x03, startIdx + 1); // Fim do quadro (busca após o SOF)

                int ackIdx = buffer.IndexOf(0x06, startIdx + 1); // Fim do quadro (busca após o SOF)

                if(ackIdx != -1)
                {
                    BeginInvoke(new Action(() => { tbPacotesRX.Text += "Serial: RX_ACK_0x06 " + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                    break;
                }


                // Verifica se um quadro completo foi encontrado
                if (startIdx != -1 && endIdx != -1 && endIdx > startIdx)
                {
                    int length = endIdx - startIdx + 1;
                    byte[] frame = buffer.GetRange(startIdx, length).ToArray();

                    if (length > MaxFrameSize) //Se o comprimento excedeu limite, deve haver um erro no quadro
                    {
                        Console.WriteLine($"Quadro excedeu o tamanho máximo de {MaxFrameSize} bytes. Descartando...");
                        buffer.RemoveRange(0, endIdx + 1);
                        continue;
                    }

                    // Processa o quadro completo (incluindo os marcadores)
                    ProcessFrame(frame, 0);

                    // Remove os dados já processados do buffer
                    buffer.RemoveRange(0, endIdx + 1);
                }
                else
                {
                    // Se não há quadro completo, aguarda mais dados
                    break;
                }
            }            
        }

        private void ProcessFrame(byte[] dadosRecebidos, int iface)
        {

            DatapromFrame frame = null;
            string hex = BitConverter.ToString(dadosRecebidos).Replace("-", " ");

            if (iface == 0)
            {
                BeginInvoke(new Action(() => { tbPacotesRX.Text += "Serial: " + hex + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                frame = DatapromFrame.ObtemFrameDoVetor(dadosRecebidos, IKM, ref aeskey, 0);
                if(frame  != null) {
                    Byte[] ack = new Byte[1] { 0x06 };
                    serialPort1.Write(ack, 0, ack.Length); // Manda ack
                    BeginInvoke(new Action(() => { tbPacotesTX.Text += "Serial: TX_ACK_0x06 " + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                }                    
            }                
            else
            {
                BeginInvoke(new Action(() => { tbPacotesRX.Text += "TCP: " + hex + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                frame = DatapromFrame.ObtemFrameDoVetor(dadosRecebidos, IKMAnt, ref aeskeyAnt, 1);
            }                            
            
            if (frame != null)
            {
                switch (frame.op)
                {
                    case OpcodesDP.MENSAGEM_INICIAL_GSM_80:
                        {
                            //Retira os dados do frame recebido e monta um quadro de reposta

                            byte codigo = DatapromFrame.ObtemCodigoControladorDoVetor(frame.endereco);
                            byte area = DatapromFrame.ObtemAreaControladorDoVetor(frame.endereco);
                            byte rede = DatapromFrame.ObtemRedeControladorDoVetor(frame.endereco);
                            

                            BeginInvoke(new Action(() => { 
                                tbCodigoAnt.Text = codigo.ToString(); 
                                tbAreaAnt.Text = area.ToString(); 
                                tbRedeAnt.Text = rede.ToString();
                                tbSWAnt.Text = Encoding.ASCII.GetString(frame.dados,3,4);
                                tbTabelaAnt.Text = Encoding.ASCII.GetString(frame.dados, 7, 4);
                                tbProtocoloAnt.Text = Encoding.ASCII.GetString(frame.dados, 11, 4);
                            })); // Atualizar o TextBox na thread principal

                            Byte[] dados = { };
                            DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(frame.endereco, OpcodesDP.MENSAGEM_INICIAL_GSM_80, dados); //63 eh um dummy do protocolo DP
                            Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                            
                            if(iface == 0) //Responde via porta serial
                                serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); 
                            else //Responde via ethernet
                            {
                                if(stream != null)
                                {
                                    stream.WriteAsync(frame2Bytes, 0, frame2Bytes.Length);
                                    BeginInvoke(new Action(() => { tbPacotesTX.Text += "TCP: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal



                                    if (cbAutoAnt.Checked)
                                    {
                                        Thread.Sleep(3000); //Espera um pouco para não encavalar os comandos

                                        parChavesAntares = GenerateKeyPair();
                                        var alicePublicKey = parChavesAntares.Public as ECPublicKeyParameters;
                                        var alicePrivateKey = parChavesAntares.Private as ECPrivateKeyParameters;

                                        ////Envia chave publica local 
                                        byte[] publicKeyBytes = alicePublicKey.Q.GetEncoded(false); // false para descompactada
                                        byte[] privateKeyBytes = alicePrivateKey.D.ToByteArray(); // false para descompactada

                                        byte[] encodedPublicKeyBytes = Base64Code.EncodeToBase64Bytes(publicKeyBytes);
                                                                                                                       

                                        if (iface == 0) {
                                            //Serial
                                            frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY,0,0), OpcodesDP.TROCA_CHAVES_PUBLICA_B6, encodedPublicKeyBytes); //Constroi Quadro Nao Seguro
                                            frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                                            BeginInvoke(new Action(() => { tbPacotesTX.Text += "Serial: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                                            serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
                                        }
                                            
                                        else  {
                                            //ETH
                                            if (stream != null)
                                            {
                                                frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(Convert.ToByte(tbCodigoAnt.Text), Convert.ToByte(tbRedeAnt.Text), Convert.ToByte(tbAreaAnt.Text)), OpcodesDP.TROCA_CHAVES_PUBLICA_B6, encodedPublicKeyBytes); //Constroi Quadro Nao Seguro
                                                frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                                                stream.WriteAsync(frame2Bytes, 0, frame2Bytes.Length);
                                                BeginInvoke(new Action(() => { tbPacotesTX.Text += "TCP: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                                            }
                                                
                                        }

                                        BeginInvoke(new Action(() =>
                                        {
                                            tbChaveLocalPubAnt.Text = BitConverter.ToString(publicKeyBytes).Replace("-", " ");
                                            tbChaveLocalPrivAnt.Text = BitConverter.ToString(privateKeyBytes).Replace("-", " ");
                                            btPubKeyAnt.Enabled = false;
                                            tsslStatus.Text = tsslStatus.Text + " e Solicitada chave Pública do controlador...";

                                            tbQtRXAnt.Text = frame2.dados.Length.ToString();
                                            tbIterRXAnt.Text = "";
                                            tbSessaoRXAnt.Text = "";
                                            tbTagRXAnt.Text = "";
                                            cbCriptoRXAnt.Checked = false;
                                            cbCriptoTXAnt.Checked = false;

                                        })); // Atualizar o TextBox na thread principal  
                                    }
                                }
                            }
                            btPubKeyAnt.Enabled = true;
                            btDataHoraAnt.Enabled = true;
                            tsslStatus.Text = "TX QUADRO NÃO SEGURO[80]: - Respondeu Mensagem Inicial";
                            break;
                        }
                    case OpcodesDP.ENVIA_IDENTIFICACAO_8D:
                        {
                            //Tamanho e offset dos campos do comando
                            int offsetVersaoSW = 0, lenVersaoSW = 4;
                            int offsetAreaControlador = offsetVersaoSW + lenVersaoSW, lenAreaControlador = 2;
                            int offsetRedeControlador = offsetAreaControlador + lenAreaControlador, lenRedeControlador = 2;
                            int offsetCodigoControlador = offsetRedeControlador + lenRedeControlador, lenCodigoControlador = 2;
                            int offsetDescricao = offsetCodigoControlador + lenCodigoControlador, lenDescricao = 128;
                            int offsetVersaoTabelaH = offsetDescricao + lenDescricao, lenVersaoTabelaH = 1;
                            int offsetVersaoTabelaL = offsetVersaoTabelaH + lenVersaoTabelaH, lenVersaoTabelaL = 1;
                            int offsetVersaoProtocoloH = offsetVersaoTabelaL + lenVersaoTabelaL, lenVersaoProtocoloH = 1;
                            int offsetVersaoProtocoloL = offsetVersaoProtocoloH + lenVersaoProtocoloH;

                            //Pega Versao FW
                            string versaoSW = Encoding.ASCII.GetString(frame.dados, offsetVersaoSW, lenVersaoSW);
                            
                            //Pega Identificacao
                            string areaControlador = Encoding.ASCII.GetString(frame.dados, offsetAreaControlador, lenAreaControlador); //LSB
                            string redeControlador = Encoding.ASCII.GetString(frame.dados, offsetRedeControlador, lenRedeControlador);
                            string codigoControlador = Encoding.ASCII.GetString(frame.dados, offsetCodigoControlador, lenCodigoControlador); //MSB
                              
                            //Pega Descricao
                            string descricao = Encoding.ASCII.GetString(frame.dados, 4 + 6, 128);

                            //Pega Versao Tabela
                            Byte[] versaoTabela = new Byte[4];
                            versaoTabela[0] = (byte)((frame.dados[offsetVersaoTabelaH]) >> 4); //High
                            versaoTabela[1] = (byte)((frame.dados[offsetVersaoTabelaL]) & 0x0F); //Low
                            string strVersaoTabela = $"{versaoTabela[0]}.{versaoTabela[1]}";

                            //Pega Versao Protocolo
                            Byte[] versaoProtocolo = new Byte[4];
                            versaoProtocolo[0] = (byte)(frame.dados[offsetVersaoProtocoloH] >> 4);//High
                            versaoProtocolo[1] = (byte)(frame.dados[offsetVersaoProtocoloL] & 0x0F);//Low
                            string strVersaoProtocolo = $"{versaoProtocolo[0]}.{versaoProtocolo[1]}";                                                       


                            BeginInvoke(new Action(() => 
                            { 
                                tbSw.Text = versaoSW;
                                tbTabela.Text = strVersaoTabela;
                                tbProtocolo.Text = strVersaoProtocolo;

                                tbCodigo.Text = codigoControlador;
                                tbRede.Text = redeControlador;
                                tbArea.Text = areaControlador;

                                tbDescricao.Text = descricao; 
                                
                            })); // Atualizar o TextBox na thread principal                                                                
                            
                            tsslStatus.Text = "RX QUADRO NÃO SEGURO[BD]: - Recebidas Informações do Controlador";

                            BeginInvoke(new Action(() =>
                            {
                                tbQtRX.Text = frame.dados.Length.ToString();
                                tbIterRX.Text = "-";
                                tbSessaoRX.Text = "-";
                                tbTagRX.Text = "-";
                                cbCriptoRX.Checked = false;
                            })); // Atualizar o TextBox na thread principal


                            if ((cbAuto.Checked && iface == 0) || (cbAutoAnt.Checked && iface == 1))
                            {
                                parChavesProgramador = GenerateKeyPair();
                                var alicePublicKey = parChavesProgramador.Public as ECPublicKeyParameters;
                                var alicePrivateKey = parChavesProgramador.Private as ECPrivateKeyParameters;

                                ////Envia chave publica local 
                                byte[] publicKeyBytes = alicePublicKey.Q.GetEncoded(false); // false para descompactada
                                byte[] privateKeyBytes = alicePrivateKey.D.ToByteArray(); // false para descompactada

                                byte[] encodedPublicKeyBytes = Base64Code.EncodeToBase64Bytes(publicKeyBytes);




                                DatapromFrame frame2 = null;
                                if (iface == 0)
                                {
                                    frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY,0,0), OpcodesDP.TROCA_CHAVES_PUBLICA_B6, encodedPublicKeyBytes); //Constroi Quadro Nao Seguro
                                    Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                                    BeginInvoke(new Action(() => { tbPacotesTX.Text += "Serial: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                                    serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
                                    BeginInvoke(new Action(() =>
                                    {
                                        tbChaveLocalPub.Text = BitConverter.ToString(publicKeyBytes).Replace("-", " ");
                                        tbChaveLocalPriv.Text = BitConverter.ToString(privateKeyBytes).Replace("-", " ");
                                        btPubKey.Enabled = false;
                                        tsslStatus.Text = tsslStatus.Text + " e Solicitada chave Pública do controlador...";

                                        tbQtRX.Text = frame2.dados.Length.ToString();
                                        tbIterRX.Text = "";
                                        tbSessaoRX.Text = "";
                                        tbTagRX.Text = "";
                                        cbCriptoRX.Checked = false;

                                    })); // Atualizar o TextBox na thread principal  
                                }

                                else
                                {
                                    if (stream != null)
                                    {
                                        frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(Convert.ToByte(tbCodigoAnt.Text), Convert.ToByte(tbRedeAnt.Text), Convert.ToByte(tbAreaAnt.Text)), OpcodesDP.TROCA_CHAVES_PUBLICA_B6, encodedPublicKeyBytes); //Constroi Quadro Nao Seguro
                                        Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                                        stream.WriteAsync(frame2Bytes, 0, frame2Bytes.Length);
                                        BeginInvoke(new Action(() => { tbPacotesTX.Text += "TCP: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                                        BeginInvoke(new Action(() =>
                                        {
                                            tbChaveLocalPubAnt.Text = BitConverter.ToString(publicKeyBytes).Replace("-", " ");
                                            tbChaveLocalPrivAnt.Text = BitConverter.ToString(privateKeyBytes).Replace("-", " ");
                                            btPubKeyAnt.Enabled = false;
                                            tsslStatus.Text = tsslStatus.Text + " e Solicitada chave Pública do controlador...";

                                            tbQtRXAnt.Text = frame2.dados.Length.ToString();
                                            tbIterRXAnt.Text = "";
                                            tbSessaoRXAnt.Text = "";
                                            tbTagRXAnt.Text = "";
                                            cbCriptoRXAnt.Checked = false;

                                        })); // Atualizar o TextBox na thread principal  
                                    }

                                }                                
                            }

                            break;
                        }
                    case OpcodesDP.TROCA_DADOS_SEGUROS_B5:
                        {
                            

                            if (frame.dados[0] == OpcodesDP.SOLICITA_DATA_E_HORA_86)
                            {
                                tsslStatus.Text = "RX QUADRO SEGURO[B5-86]: - Recebeu Data e Hora!";
                                if(iface == 0) {

                                    contadorMensagens = frame.iterador > contadorMensagens ? frame.iterador : contadorMensagens;

                                    BeginInvoke(new Action(() =>
                                    {


                                        tbQtRX.Text = frame.dados.Length.ToString();
                                        tbIterRX.Text = frame.iterador.ToString();
                                        tbSessaoRX.Text = Convert.ToString(frame.iterador / DatapromFrame.MSG_BY_SESSION);
                                        tbTagRX.Text = BitConverter.ToString(frame.tag).Replace("-", " ");
                                        cbCriptoRX.Checked = true;
                                        tbAESkey.Text = aeskey != null ? BitConverter.ToString(aeskey).Replace("-", " ") : "ERRO";
                                        //tbAESkey.Text = DatapromFrame.aesKey != null ? BitConverter.ToString(DatapromFrame.aesKey).Replace("-", " ") : "ERRO";
                                    }));
                                }
                                else {

                                    contadorMensagensAnt = frame.iterador > contadorMensagensAnt ? frame.iterador : contadorMensagensAnt;

                                    BeginInvoke(new Action(() =>
                                    {
                                        tbQtRXAnt.Text = frame.dados.Length.ToString();
                                        tbIterRXAnt.Text = frame.iterador.ToString();
                                        tbSessaoRXAnt.Text = Convert.ToString(frame.iterador / DatapromFrame.MSG_BY_SESSION);
                                        tbTagRXAnt.Text = BitConverter.ToString(frame.tag).Replace("-", " ");
                                        cbCriptoRXAnt.Checked = true;
                                        tbAESkeyAnt.Text = aeskeyAnt != null ? BitConverter.ToString(aeskeyAnt).Replace("-", " ") : "ERRO";
                                        //tbAESkeyAnt.Text = DatapromFrame.aesKey != null ? BitConverter.ToString(DatapromFrame.aesKey).Replace("-", " ") : "ERRO";
                                    }));
                                }
                                

                                if ((cbRepete.Checked && iface == 0) || (cbRepeteAnt.Checked && iface == 1))
                                {
                                    Thread.Sleep(250);
                                    Byte[] quadro = new Byte[1];
                                    quadro[0] = OpcodesDP.SOLICITA_DATA_E_HORA_86; //Solicitação que será criptografada
                                    DatapromFrame frame2 = null;
                                    //DatapromFrame frame2 = DatapromFrame.ConstroiFrameQS(DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY), quadro, ref contadorMensagens, IKM); //63 eh um dummy do protocolo DP
                                    //Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);

                                    if (iface == 0) //Serial
                                    {
                                        frame2 = DatapromFrame.ConstroiFrameQS(DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY,0,0), quadro, ref contadorMensagens, IKM, ref aeskey,0); //63 eh um dummy do protocolo DP
                                        Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                                        BeginInvoke(new Action(() => { tbPacotesTX.Text += "Serial: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                                        serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
                                    }

                                    else //ETH
                                    {
                                        if (stream != null)
                                        {
                                            frame2 = DatapromFrame.ConstroiFrameQS(DatapromFrame.VetorizaIdDoControlador(Convert.ToByte(tbCodigoAnt.Text), Convert.ToByte(tbRedeAnt.Text), Convert.ToByte(tbAreaAnt.Text)), quadro, ref contadorMensagensAnt, IKMAnt, ref aeskeyAnt, 1); //63 eh um dummy do protocolo DP
                                            Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                                            stream.WriteAsync(frame2Bytes, 0, frame2Bytes.Length);
                                            BeginInvoke(new Action(() => { tbPacotesTX.Text += "TCP: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                                        }
                                    }

                                    tsslStatus.Text = "TX QUADRO SEGURO[B5-86]: - Solicitou data e hora do controlador";
                                    if (iface == 0)
                                    {
                                        BeginInvoke(new Action(() =>
                                        {
                                            tbQtTX.Text = frame2.dados.Length.ToString();
                                            tbIterTX.Text = frame2.iterador.ToString();
                                            tbSessaoTX.Text = Convert.ToString(frame2.iterador / DatapromFrame.MSG_BY_SESSION);
                                            tbTagTX.Text = BitConverter.ToString(frame2.tag).Replace("-", " ");
                                            cbCriptoTX.Checked = true;
                                        })); // Atualizar o TextBox na thread principal
                                    }
                                    else
                                    {
                                        BeginInvoke(new Action(() =>
                                        {
                                            tbQtTXAnt.Text = frame2.dados.Length.ToString();
                                            tbIterTXAnt.Text = frame2.iterador.ToString();
                                            tbSessaoTXAnt.Text = Convert.ToString(frame2.iterador / DatapromFrame.MSG_BY_SESSION);
                                            tbTagTXAnt.Text = BitConverter.ToString(frame2.tag).Replace("-", " ");
                                            cbCriptoTXAnt.Checked = true;
                                        })); // Atualizar o TextBox na thread principal
                                    }
                                    
                                }                                                                              
                            }
                            break;
                        }
                    case OpcodesDP.TROCA_CHAVES_PUBLICA_B6:
                        {
                            //Antes de guardar chave publica remota é necessario decodificar do B64
                            byte[] decodedRemotePublicKeyBytes = Base64Code.DecodeFromBase64Bytes(frame.dados);

                            var curve = ECNamedCurveTable.GetByName("secp256r1");
                            var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);

                            try
                            {
                                var point = curve.Curve.DecodePoint(decodedRemotePublicKeyBytes);                                
                                

                                string hexString1 = BitConverter.ToString(decodedRemotePublicKeyBytes).Replace("-", " "); // Converter para HEX                
                                if(iface == 0) {
                                    //Recebeu chave Serial (Programador)

                                    chavePublicaControlador = new ECPublicKeyParameters(point, domainParams);

                                    SegredoCompartilhado = null;
                                    SegredoCompartilhado = GenerateSharedSecret(parChavesProgramador.Private as ECPrivateKeyParameters, chavePublicaControlador);
                                    IKM = new byte[65];
                                    PSKLib.Get_PSK_IKM(0, SegredoCompartilhado, out IKM);

                                    BeginInvoke(new Action(() =>
                                    {
                                        tbChaveRemPub.Text = hexString1;
                                        tbSegredo.Text = BitConverter.ToString(SegredoCompartilhado).Replace("-", " ");
                                        tbIKM.Text = BitConverter.ToString(IKM).Replace("-", " ");
                                        tsslStatus.Text = "RX QUADRO NÃO SEGURO[B6]: - Recebeu Chave Pública";
                                        btPubKey.Enabled = true;

                                        tbQtRX.Text = frame.dados.Length.ToString();
                                        tbIterRX.Text = "-";
                                        tbSessaoRX.Text = "-";
                                        tbTagRX.Text = "-";
                                        cbCriptoRX.Checked = false;

                                    }));
                                }
                                else {
                                    chavePublicaControladorAnt = new ECPublicKeyParameters(point, domainParams);

                                    SegredoCompartilhadoAnt = null;
                                    SegredoCompartilhadoAnt = GenerateSharedSecret(parChavesAntares.Private as ECPrivateKeyParameters, chavePublicaControladorAnt);
                                    IKMAnt = new byte[65];                                    
                                    PSKLib.Get_PSK_IKM(Convert.ToByte(tbCodigoAnt.Text)%10, SegredoCompartilhadoAnt, out IKMAnt);

                                    tbChaveRemPubAnt.Text = hexString1;
                                    tbSegredoAnt.Text = BitConverter.ToString(SegredoCompartilhadoAnt).Replace("-", " ");
                                    tbIKMAnt.Text = BitConverter.ToString(IKMAnt).Replace("-", " ");
                                    tsslStatus.Text = "RX QUADRO NÃO SEGURO[B6]: - Recebeu Chave Pública";
                                    btPubKeyAnt.Enabled = true;

                                    tbQtRXAnt.Text = frame.dados.Length.ToString();
                                    tbIterRXAnt.Text = "-";
                                    tbSessaoRXAnt.Text = "-";
                                    tbTagRXAnt.Text = "-";
                                    cbCriptoRXAnt.Checked = false;
                                }
                                

                                if ((cbAuto.Checked && iface == 0) || (cbAutoAnt.Checked && iface == 1))
                                {
                                    BeginInvoke(new Action(() =>
                                    {
                                        tbQtRX.Text = "";
                                        tbIterRX.Text = "";
                                        tbSessaoRX.Text = "";
                                        tbTagRX.Text = "";
                                        cbCriptoRX.Checked = false;
                                    })); // Limpa TB recepção

                                    if ((IKM != null && iface == 0) || (IKMAnt != null && iface == 1)) {
                                        // Se IKM já cadastrada para a interface utilizada, então envia pode enviar QS

                                        Byte[] quadro = new Byte[1];
                                        quadro[0] = OpcodesDP.SOLICITA_DATA_E_HORA_86; //Solicitação que será criptografada
                                        DatapromFrame frame2 = null;


                                        if (iface == 0) //Serial
                                        {
                                            frame2 = DatapromFrame.ConstroiFrameQS(DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY, 0, 0), quadro, ref contadorMensagens, IKM, ref aeskey, 0); //63 eh um dummy do protocolo DP
                                            Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);

                                            BeginInvoke(new Action(() => { tbPacotesTX.Text += "Serial: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                                            serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
                                        }

                                        else //ETH
                                        {
                                            if (stream != null)
                                            {
                                                frame2 = DatapromFrame.ConstroiFrameQS(DatapromFrame.VetorizaIdDoControlador(Convert.ToByte(tbCodigoAnt.Text), Convert.ToByte(tbRedeAnt.Text), Convert.ToByte(tbAreaAnt.Text)), quadro, ref contadorMensagensAnt, IKMAnt, ref aeskeyAnt,1); //63 eh um dummy do protocolo DP
                                                Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);

                                                stream.WriteAsync(frame2Bytes, 0, frame2Bytes.Length);
                                                BeginInvoke(new Action(() => { tbPacotesTX.Text += "TCP: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                                            }
                                        }
                                        tsslStatus.Text = "TX QUADRO SEGURO[B5-86]: - Solicitou data e hora do controlador";
                                        if(iface == 0)
                                        {
                                            BeginInvoke(new Action(() =>
                                            {
                                                tbQtTX.Text = frame2.dados.Length.ToString();
                                                tbIterTX.Text = frame2.iterador.ToString();
                                                tbSessaoTX.Text = Convert.ToString(frame2.iterador / DatapromFrame.MSG_BY_SESSION);
                                                tbTagTX.Text = BitConverter.ToString(frame2.tag).Replace("-", " ");
                                                cbCriptoTX.Checked = true;
                                            })); // Atualizar o TextBox na thread principal
                                        }
                                        else
                                        {
                                            BeginInvoke(new Action(() =>
                                            {
                                                tbQtTXAnt.Text = frame2.dados.Length.ToString();
                                                tbIterTXAnt.Text = frame2.iterador.ToString();
                                                tbSessaoTXAnt.Text = Convert.ToString(frame2.iterador / DatapromFrame.MSG_BY_SESSION);
                                                tbTagTXAnt.Text = BitConverter.ToString(frame2.tag).Replace("-", " ");
                                                cbCriptoTXAnt.Checked = true;
                                            })); // Atualizar o TextBox na thread principal
                                        }
                                        
                                    }
                                    else
                                    {
                                        //Sem IKM, envia QNS
                                        Byte[] dados = null;
                                        
                                        if (iface == 0)
                                        {
                                            DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY, 0, 0), OpcodesDP.SOLICITA_DATA_E_HORA_86, dados); //63 eh um dummy do protocolo DP
                                            Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                                            BeginInvoke(new Action(() => { tbPacotesTX.Text += "Serial: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                                            serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
                                            BeginInvoke(new Action(() =>
                                            {
                                                tbQtTX.Text = "0";
                                                tbIterTX.Text = "-";
                                                tbSessaoTX.Text = "-";
                                                tbTagTX.Text = "-";
                                                cbCriptoTX.Checked = false;
                                            })); // Atualizar o TextBox na thread principal
                                        }

                                        else
                                        {
                                            if (stream != null)
                                            {
                                                DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(Convert.ToByte(tbCodigoAnt.Text), Convert.ToByte(tbRedeAnt.Text), Convert.ToByte(tbAreaAnt.Text)), OpcodesDP.SOLICITA_DATA_E_HORA_86, dados); //63 eh um dummy do protocolo DP
                                                Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                                                stream.WriteAsync(frame2Bytes, 0, frame2Bytes.Length);
                                                BeginInvoke(new Action(() => { tbPacotesTX.Text += "TCP: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                                                BeginInvoke(new Action(() =>
                                                {
                                                    tbQtTXAnt.Text = "0";
                                                    tbIterTXAnt.Text = "-";
                                                    tbSessaoTXAnt.Text = "-";
                                                    tbTagTXAnt.Text = "-";
                                                    cbCriptoTXAnt.Checked = false;
                                                })); // Atualizar o TextBox na thread principal
                                            }
                                        }
                                        tsslStatus.Text = "TX QUADRO NÃO SEGURO[86]: - Solicitou data e hora do controlador";
                                       
                                    }
                                }

                                if ((cbRepete.Checked && iface == 0) || (cbRepeteAnt.Checked && iface == 1))
                                {
                                    Thread.Sleep(500);
                                    if (parChavesProgramador == null)
                                    {
                                        parChavesProgramador = GenerateKeyPair();
                                    }
                                                                                                          

                                    if (iface == 0) {
                                        // Serial
                                        var alicePublicKey = parChavesProgramador.Public as ECPublicKeyParameters;
                                        var alicePrivateKey = parChavesProgramador.Private as ECPrivateKeyParameters;

                                        ////Envia chave publica local pela SERIAL
                                        byte[] publicKeyBytes = alicePublicKey.Q.GetEncoded(false); // false para descompactada
                                        byte[] privateKeyBytes = alicePrivateKey.D.ToByteArray(); // false para descompactada

                                        byte[] encodedPublicKeyBytes = Base64Code.EncodeToBase64Bytes(publicKeyBytes);

                                        BeginInvoke(new Action(() => { tbChaveLocalPriv.Text = BitConverter.ToString(privateKeyBytes).Replace("-", " "); })); // Atualizar o TextBox na thread principal
                                        BeginInvoke(new Action(() => { tbChaveLocalPub.Text = BitConverter.ToString(publicKeyBytes).Replace("-", " "); })); // Atualizar o TextBox na thread principal
                                        BeginInvoke(new Action(() => { tbSegredo.Text = ""; tbIKM.Text = ""; tbChaveRemPub.Text = ""; })); // Atualizar o TextBox na thread principal

                                        DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY, 0, 0), OpcodesDP.TROCA_CHAVES_PUBLICA_B6, encodedPublicKeyBytes); //Constroi Quadro Nao Seguro
                                        Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);


                                        BeginInvoke(new Action(() => { tbPacotesTX.Text += "Serial: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                                        serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
                                        BeginInvoke(new Action(() =>
                                        {
                                            tbQtTX.Text = frame2.dados.Length.ToString();
                                            tbIterTX.Text = "";
                                            tbSessaoTX.Text = "";
                                            tbTagTX.Text = "";
                                            cbCriptoTX.Checked = false;
                                        })); // Atualizar o TextBox na thread principal
                                    }

                                    else  {
                                        //TCP
                                        if (stream != null) {
                                            //Stream valido

                                            var alicePublicKey = parChavesAntares.Public as ECPublicKeyParameters;
                                            var alicePrivateKey = parChavesAntares.Private as ECPrivateKeyParameters;

                                            ////Envia chave publica local pela SERIAL
                                            byte[] publicKeyBytes = alicePublicKey.Q.GetEncoded(false); // false para descompactada
                                            byte[] privateKeyBytes = alicePrivateKey.D.ToByteArray(); // false para descompactada

                                            byte[] encodedPublicKeyBytes = Base64Code.EncodeToBase64Bytes(publicKeyBytes);

                                            BeginInvoke(new Action(() => { tbChaveLocalPrivAnt.Text = BitConverter.ToString(privateKeyBytes).Replace("-", " "); })); // Atualizar o TextBox na thread principal
                                            BeginInvoke(new Action(() => { tbChaveLocalPubAnt.Text = BitConverter.ToString(publicKeyBytes).Replace("-", " "); })); // Atualizar o TextBox na thread principal
                                            BeginInvoke(new Action(() => { tbSegredoAnt.Text = ""; tbIKMAnt.Text = ""; tbChaveRemPubAnt.Text = ""; })); // Atualizar o TextBox na thread principal

                                            DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(Convert.ToByte(tbCodigoAnt.Text), Convert.ToByte(tbRedeAnt.Text), Convert.ToByte(tbAreaAnt.Text)), OpcodesDP.TROCA_CHAVES_PUBLICA_B6, encodedPublicKeyBytes); //Constroi Quadro Nao Seguro
                                            Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);

                                            stream.WriteAsync(frame2Bytes, 0, frame2Bytes.Length);
                                            BeginInvoke(new Action(() => { tbPacotesTX.Text += "TCP: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                                            BeginInvoke(new Action(() =>
                                            {
                                                tbQtTXAnt.Text = frame2.dados.Length.ToString();
                                                tbIterTXAnt.Text = "";
                                                tbSessaoTXAnt.Text = "";
                                                tbTagTXAnt.Text = "";
                                                cbCriptoTXAnt.Checked = false;
                                            })); // Atualizar o TextBox na thread principal
                                        }
                                    }
                                    tsslStatus.Text = "TX QUADRO NÃO SEGURO[B6]: - Solicitou Chave Pública do controlador";
                                    btPubKey.Enabled = false;                                    
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Chave Pública Inválida!");
                            }
                            break;
                        }
                    case OpcodesDP.SOLICITA_DATA_E_HORA_86:
                        {
                            tsslStatus.Text = "RX QUADRO NÃO SEGURO[86]: - Recebeu Data e Hora!";

                            BeginInvoke(new Action(() =>
                            {
                                tbQtRX.Text = frame.dados.Length.ToString();
                                tbIterRX.Text = "-";
                                tbSessaoRX.Text = "-";
                                tbTagRX.Text = "-";
                                cbCriptoRX.Checked = false;
                            }));

                            if((cbRepete.Checked && iface == 0) || (cbRepeteAnt.Checked && iface == 1))
                            {
                                Thread.Sleep(250);
                                

                                //Sem IKM, envia QNS
                                Byte[] dados = null;
                                
                                if (iface == 0) //Serial
                                {
                                    DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY, 0, 0), OpcodesDP.SOLICITA_DATA_E_HORA_86, dados); //63 eh um dummy do protocolo DP
                                    Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);

                                    BeginInvoke(new Action(() =>
                                    {
                                        tbQtRX.Text = "";
                                        tbIterRX.Text = "";
                                        tbSessaoRX.Text = "";
                                        tbTagRX.Text = "";
                                        cbCriptoRX.Checked = false;
                                        tbPacotesTX.Text += "Serial: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine;
                                    })); //Limpa RX                                    
                                    
                                    BeginInvoke(new Action(() =>
                                    {
                                        tbQtTX.Text = "0";
                                        tbIterTX.Text = "-";
                                        tbSessaoTX.Text = "-";
                                        tbTagTX.Text = "-";
                                        cbCriptoTX.Checked = false;
                                    })); // Atualizar o TextBox na thread principal

                                    serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita SOLICITA_DATA_E_HORA_86
                                }

                                else //ETH
                                {
                                    if (stream != null)
                                    {
                                        DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY, 0, 0), OpcodesDP.SOLICITA_DATA_E_HORA_86, dados); //63 eh um dummy do protocolo DP
                                        Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);

                                        BeginInvoke(new Action(() =>
                                        {
                                            tbQtRXAnt.Text = "";
                                            tbIterRXAnt.Text = "";
                                            tbSessaoRXAnt.Text = "";
                                            tbTagRXAnt.Text = "";
                                            cbCriptoRXAnt.Checked = false;
                                            tbPacotesTX.Text += "TCP: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine;
                                        })); //Limpa RX 
                                                                  
                                        BeginInvoke(new Action(() =>
                                        {
                                            tbQtTXAnt.Text = "0";
                                            tbIterTXAnt.Text = "-";
                                            tbSessaoTXAnt.Text = "-";
                                            tbTagTXAnt.Text = "-";
                                            cbCriptoTXAnt.Checked = false;
                                        })); // Atualizar o TextBox na thread principal

                                        stream.WriteAsync(frame2Bytes, 0, frame2Bytes.Length); // Solicita SOLICITA_DATA_E_HORA_86
                                    }
                                }
                                tsslStatus.Text = "TX QUADRO NÃO SEGURO[86]: - Solicitou data e hora do controlador"; 
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        } 

        private void Form1_Load(object sender, EventArgs e)
        {
                
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.cts.Cancel();
        }

        static byte[] GenerateSharedSecret(ECPrivateKeyParameters privateKey, ECPublicKeyParameters publicKey)
        { 
            var ecDomain = privateKey.Parameters;
            var q = publicKey.Q.Multiply(privateKey.D).Normalize();
            var encodedPoint = q.GetEncoded(false); // false → formato não compactado, inclui 0x04
            return encodedPoint;
        }        

        private void btDataHora_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                tbQtRX.Text = "";
                tbIterRX.Text = "";
                tbSessaoRX.Text = "";
                tbTagRX.Text = "";
                cbCriptoRX.Checked = false;
            })); 

            if (IKM != null) {
                // IKM já cadastrada então envia pode enviar QS
                Byte[] quadro = new Byte[1];
                quadro[0] = OpcodesDP.SOLICITA_DATA_E_HORA_86; //Solicitação que será criptografada
                DatapromFrame frame2 = DatapromFrame.ConstroiFrameQS(DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY, 0, 0), quadro, ref contadorMensagens, IKM, ref aeskey, 0); //63 eh um dummy do protocolo DP
                Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);               
                serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica                
                BeginInvoke(new Action(() => { tbPacotesTX.Text += "Serial: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                tsslStatus.Text = "TX QUADRO SEGURO[B5-86]: - Solicitou data e hora do controlador";
                BeginInvoke(new Action(() =>
                {
                    tbQtTX.Text = frame2.dados.Length.ToString();
                    tbIterTX.Text = frame2.iterador.ToString();
                    tbSessaoTX.Text = Convert.ToString(frame2.iterador / DatapromFrame.MSG_BY_SESSION);
                    tbTagTX.Text = BitConverter.ToString(frame2.tag).Replace("-", " ");
                    //tbAESkey.Text = DatapromFrame.aesKey != null ? BitConverter.ToString(DatapromFrame.aesKey).Replace("-", " "): "ERRO";
                    tbAESkey.Text = aeskey != null ? BitConverter.ToString(aeskey).Replace("-", " ") : "ERRO";
                    cbCriptoTX.Checked = true;
                })); // Atualizar o TextBox na thread principal
            }
            else {
                //Sem IKM, envia QNS
                Byte[] dados = null;
                DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY, 0, 0), OpcodesDP.SOLICITA_DATA_E_HORA_86, dados); //63 eh um dummy do protocolo DP
                Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                BeginInvoke(new Action(() => { tbPacotesTX.Text += "Serial: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
                tsslStatus.Text = "TX QUADRO NÃO SEGURO[86]: - Solicitou data e hora do controlador";
                BeginInvoke(new Action(() => 
                {
                    tbQtTX.Text = "0";
                    tbIterTX.Text = "-";                     
                    tbSessaoTX.Text = "-"; 
                    tbTagTX.Text = "-"; 
                    cbCriptoTX.Checked = false;
                })); // Atualizar o TextBox na thread principal

            }            
        }      

        private void btPubKey_Click(object sender, EventArgs e)
        {
            if(parChavesProgramador == null)
            {
                parChavesProgramador = GenerateKeyPair();
            }
            
            
            var alicePublicKey = parChavesProgramador.Public as ECPublicKeyParameters;
            var alicePrivateKey = parChavesProgramador.Private as ECPrivateKeyParameters;

            ////Envia chave publica local pela SERIAL
            byte[] publicKeyBytes = alicePublicKey.Q.GetEncoded(false); // false para descompactada
            byte[] privateKeyBytes = alicePrivateKey.D.ToByteArray(); // false para descompactada

            byte[] encodedPublicKeyBytes = Base64Code.EncodeToBase64Bytes(publicKeyBytes);

            BeginInvoke(new Action(() => { tbChaveLocalPriv.Text = BitConverter.ToString(privateKeyBytes).Replace("-", " "); })); // Atualizar o TextBox na thread principal
            BeginInvoke(new Action(() => { tbChaveLocalPub.Text = BitConverter.ToString(publicKeyBytes).Replace("-", " "); })); // Atualizar o TextBox na thread principal
            BeginInvoke(new Action(() => { tbSegredo.Text = ""; tbIKM.Text = ""; tbChaveRemPub.Text = ""; })); // Atualizar o TextBox na thread principal

            DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(OpcodesDP.END_DUMMY, 0, 0), OpcodesDP.TROCA_CHAVES_PUBLICA_B6, encodedPublicKeyBytes); //Constroi Quadro Nao Seguro
            Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
            serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
            BeginInvoke(new Action(() => { tbPacotesTX.Text += "Serial: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal

            tsslStatus.Text = "TX QUADRO NÃO SEGURO[B6]: - Solicitou Chave Pública do controlador";
            btPubKey.Enabled = false;

            BeginInvoke(new Action(() =>
            {
                tbQtTX.Text = frame2.dados.Length.ToString();
                tbIterTX.Text = "-";
                tbSessaoTX.Text = "-";
                tbTagTX.Text = "-";
                cbCriptoTX.Checked = false;
            })); // Atualizar o TextBox na thread principal

        }

        private void cbAuto_CheckedChanged(object sender, EventArgs e)
        {   
            if (cbAuto.Checked)
            {
                cbRepete.Enabled = false;
                cbRepete.Checked = false;
            }
            else
            {
                cbRepete.Enabled = true;
            }
        }

        private void cbAutoAnt_CheckedChanged(object sender, EventArgs e)
        {
            
            if (cbAutoAnt.Checked)
            {
                cbRepeteAnt.Enabled = false;
                cbRepeteAnt.Checked = false;
            }
            else
            {
                cbRepeteAnt.Enabled = true;
            }
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => { tbPacotesRX.Text = ""; tbPacotesTX.Text = ""; })); // Atualizar o TextBox na thread principal
        }

        private void btPubKeyAnt_Click(object sender, EventArgs e)
        {
            if (parChavesAntares == null)
            {
                parChavesAntares = GenerateKeyPair();
            }


            var alicePublicKey = parChavesAntares.Public as ECPublicKeyParameters;
            var alicePrivateKey = parChavesAntares.Private as ECPrivateKeyParameters;

            ////Envia chave publica local pela SERIAL
            byte[] publicKeyBytes = alicePublicKey.Q.GetEncoded(false); // false para descompactada
            byte[] privateKeyBytes = alicePrivateKey.D.ToByteArray(); // false para descompactada

            byte[] encodedPublicKeyBytes = Base64Code.EncodeToBase64Bytes(publicKeyBytes);

            BeginInvoke(new Action(() => { tbChaveLocalPrivAnt.Text = BitConverter.ToString(privateKeyBytes).Replace("-", " "); })); // Atualizar o TextBox na thread principal
            BeginInvoke(new Action(() => { tbChaveLocalPubAnt.Text = BitConverter.ToString(publicKeyBytes).Replace("-", " "); })); // Atualizar o TextBox na thread principal
            BeginInvoke(new Action(() => { tbSegredoAnt.Text = ""; tbIKMAnt.Text = ""; tbChaveRemPubAnt.Text = ""; })); // Atualizar o TextBox na thread principal

            DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(Convert.ToByte(tbCodigoAnt.Text), Convert.ToByte(tbRedeAnt.Text), Convert.ToByte(tbAreaAnt.Text)), OpcodesDP.TROCA_CHAVES_PUBLICA_B6, encodedPublicKeyBytes); //Constroi Quadro Nao Seguro
            Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);

            if (stream != null)
            {
                stream.WriteAsync(frame2Bytes, 0, frame2Bytes.Length);
                BeginInvoke(new Action(() => { tbPacotesTX.Text += "TCP: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
            }

            tsslStatus.Text = "TX QUADRO NÃO SEGURO[B6]: - Solicitou Chave Pública do controlador";
            btPubKeyAnt.Enabled = false;

            BeginInvoke(new Action(() =>
            {
                tbQtTXAnt.Text = frame2.dados.Length.ToString();
                tbIterTXAnt.Text = "-";
                tbSessaoTXAnt.Text = "-";
                tbTagTXAnt.Text = "-";
                cbCriptoTXAnt.Checked = false;
            })); // Atualizar o TextBox na thread principal
        }

        private void btDataHoraAnt_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                tbQtRXAnt.Text = "";
                tbIterRXAnt.Text = "";
                tbSessaoRXAnt.Text = "";
                tbTagRXAnt.Text = "";
                cbCriptoRXAnt.Checked = false;
            }));

            if (IKMAnt != null)
            {
                // IKM já cadastrada então envia pode enviar QS
                Byte[] quadro = new Byte[1];
                quadro[0] = OpcodesDP.SOLICITA_DATA_E_HORA_86; //Solicitação que será criptografada
                DatapromFrame frame2 = DatapromFrame.ConstroiFrameQS(DatapromFrame.VetorizaIdDoControlador(Convert.ToByte(tbCodigoAnt.Text), Convert.ToByte(tbRedeAnt.Text), Convert.ToByte(tbAreaAnt.Text)), quadro, ref contadorMensagensAnt, IKMAnt, ref aeskeyAnt, 1); //63 eh um dummy do protocolo DP
                Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                if (stream != null)
                {
                    stream.WriteAsync(frame2Bytes, 0, frame2Bytes.Length);
                    BeginInvoke(new Action(() => { tbPacotesTX.Text += "TCP: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                }

                tsslStatus.Text = "TX QUADRO SEGURO[B5-86]: - Solicitou data e hora do controlador";
                BeginInvoke(new Action(() =>
                {
                    tbQtTXAnt.Text = frame2.dados.Length.ToString();
                    tbIterTXAnt.Text = frame2.iterador.ToString();
                    tbSessaoTXAnt.Text = Convert.ToString(frame2.iterador / DatapromFrame.MSG_BY_SESSION);
                    tbTagTXAnt.Text = BitConverter.ToString(frame2.tag).Replace("-", " ");
                    tbAESkeyAnt.Text = aeskeyAnt != null ? BitConverter.ToString(aeskeyAnt).Replace("-", " ") : "ERRO";
                    cbCriptoTXAnt.Checked = true;
                })); // Atualizar o TextBox na thread principal
            }
            else
            {
                //Sem IKM, envia QNS
                Byte[] dados = null;
                DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaIdDoControlador(Convert.ToByte(tbCodigoAnt.Text), Convert.ToByte(tbRedeAnt.Text), Convert.ToByte(tbAreaAnt.Text)), OpcodesDP.SOLICITA_DATA_E_HORA_86, dados); //63 eh um dummy do protocolo DP
                Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);

                if (stream != null)
                {
                    stream.WriteAsync(frame2Bytes, 0, frame2Bytes.Length);
                    BeginInvoke(new Action(() => { tbPacotesTX.Text += "TCP: " + BitConverter.ToString(frame2Bytes).Replace("-", " ") + Environment.NewLine; })); // Atualizar o TextBox na thread principal
                }
                tsslStatus.Text = "TX QUADRO NÃO SEGURO[86]: - Solicitou data e hora do controlador";
                BeginInvoke(new Action(() =>
                {
                    tbQtTXAnt.Text = "0";
                    tbIterTXAnt.Text = "-";
                    tbSessaoTXAnt.Text = "-";
                    tbTagTXAnt.Text = "-";
                    cbCriptoTXAnt.Checked = false;
                })); // Atualizar o TextBox na thread principal
            }
        }

        async void InicializaServidorTCP()
        {
            int porta = Convert.ToInt16(tbTcpPort.Text);
            TcpListener servidor = new TcpListener(IPAddress.Any, porta);
            servidor.Start();
            Console.WriteLine($"Servidor assíncrono iniciado na porta {porta}...");

            while (true)
            {
                TcpClient cliente = await servidor.AcceptTcpClientAsync();
                BeginInvoke(new Action(() => { tsslStatus.Text = "Conectado TCP/IP"; })); // Atualizar o TextBox na thread principal
                // Trata o cliente de forma assíncrona
                trataCliente = TratarClienteAsync(cliente);
            }
        }

        async Task TratarClienteAsync(TcpClient cliente)
        {
            List<byte> bufferTCP = new List<byte>();
            stream = cliente.GetStream();

            byte[] tempBuffer = new byte[1];
            try
            {
                while (true)
                {

                    int bytesRead = await stream.ReadAsync(tempBuffer, 0, 1);

                    if (bytesRead == 0)
                        break; // ERRO                           

                    // Armazena os bytes recebidos no buffer contínuo
                    bufferTCP.AddRange(tempBuffer);

                    while (true)
                    {
                        int startIdx = bufferTCP.IndexOf(OpcodesDP.STX); // Início do quadro
                        int endIdx = bufferTCP.IndexOf(OpcodesDP.ETX, startIdx + 1); // Fim do quadro (busca após o SOF)

                        // Verifica se um quadro completo foi encontrado
                        if (startIdx != -1 && endIdx != -1 && endIdx > startIdx)
                        {
                            int length = endIdx - startIdx + 1;
                            byte[] frame = bufferTCP.GetRange(startIdx, length).ToArray();

                            if (length > MaxFrameSize) //Se o comprimento excedeu limite, deve haver um erro no quadro
                            {
                                Console.WriteLine($"Quadro excedeu o tamanho máximo de {MaxFrameSize} bytes. Descartando...");
                                bufferTCP.RemoveRange(0, endIdx + 1);
                                continue;
                            }

                            // Processa o quadro completo (incluindo os marcadores)
                            ProcessFrame(frame, 1);

                            // Remove os dados já processados do buffer
                            bufferTCP.RemoveRange(0, endIdx + 1);
                        }
                        else
                        {
                            // Se não há quadro completo, aguarda mais dados
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ou desconexão inesperada: {ex.Message}");
            }
            finally
            {
                if (stream != null)
                    stream.Close();

                stream = null;

                cliente.Close();
                BeginInvoke(new Action(() => {
                    tbChaveLocalPrivAnt.Text = "";
                    tbChaveLocalPubAnt.Text = "";

                    tbIterRXAnt.Text = "";
                    tbQtRXAnt.Text = "";
                    tbSessaoRXAnt.Text = "";
                    tbTagRXAnt.Text = "";
                    cbCriptoRXAnt.Enabled = false;

                    tbIterTXAnt.Text = "";
                    tbQtTXAnt.Text = "";
                    tbSessaoTXAnt.Text = "";
                    tbTagTXAnt.Text = "";
                    cbCriptoTXAnt.Enabled = false;

                    tbChaveRemPubAnt.Text = "";
                    tbSegredoAnt.Text = "";
                    tbIKMAnt.Text = "";
                    tbAESkeyAnt.Text = "";

                    tbCodigoAnt.Text = "";
                    tbAreaAnt.Text = "";
                    tbRedeAnt.Text = "";
                    tbTabelaAnt.Text = "";
                    tbProtocoloAnt.Text = "";
                    tbSWAnt.Text = "";
                    tbDescricaoAnt.Text = "";
                    tsslStatus.Text = "Conexão TCP/IP Encerrada";

                    btPubKeyAnt.Enabled = false;
                    btDataHoraAnt.Enabled = false;

                })); // Atualizar o TextBox na thread principal

                parChavesAntares = null;
                IKMAnt = null;
                SegredoCompartilhadoAnt = null;
                contadorMensagensAnt = 0;
            }
        }


        private void btIniciar_Click(object sender, EventArgs e)
        {
            if (btIniciar.Text == "Iniciar")
            {
                btIniciar.Text = "Iniciado!";
                btIniciar.Enabled = false;
                InicializaServidorTCP();
                BeginInvoke(new Action(() =>
                {
                    tbTcpPort.Enabled = false;
                    tsslStatus.Text = "Aguardando Conexão TCP/IP...";
                }));
            }            
        }
    }
}



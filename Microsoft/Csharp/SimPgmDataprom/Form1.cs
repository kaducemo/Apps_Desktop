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
        static AsymmetricCipherKeyPair parChavesProgramador = null;
        static ECPublicKeyParameters chavePublicaControlador = null;
        byte[] SegredoCompartilhado = null;
        byte[] IKM = null;        
        UInt64 contadorMensagens = 0;
        
        public Form1()
        {
            InitializeComponent();
            InicializaServidorTCP();
        }

        async void InicializaServidorTCP()
        {
            int porta = 5000;
            TcpListener servidor = new TcpListener(IPAddress.Any, porta);
            servidor.Start();
            Console.WriteLine($"Servidor assíncrono iniciado na porta {porta}...");

            while (true)
            {
                TcpClient cliente = await servidor.AcceptTcpClientAsync();
                BeginInvoke(new Action(() => { tsslStatus.Text = "Conectado TCP/IP"; btConectar.Enabled = false; })); // Atualizar o TextBox na thread principal
                // Trata o cliente de forma assíncrona
                _ = TratarClienteAsync(cliente);
            }
        }

        async Task TratarClienteAsync(TcpClient cliente)
        {
            NetworkStream stream = null;
            try
            {
                stream = cliente.GetStream();
                while (true)
                {                    
                        byte[] buffer = new byte[1024];
                        int bytesLidos = await stream.ReadAsync(buffer, 0, buffer.Length);
                        string mensagemRecebida = Encoding.UTF8.GetString(buffer, 0, bytesLidos);                        

                        string resposta = "Mensagem recebida com sucesso!" + mensagemRecebida;
                        byte[] dadosResposta = Encoding.UTF8.GetBytes(resposta);

                        BeginInvoke(new Action(() => { tbIterRX.Text = mensagemRecebida; })); // Atualizar o TextBox na thread principal

                        await stream.WriteAsync(dadosResposta, 0, dadosResposta.Length);
                    
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

                cliente.Close();
                BeginInvoke(new Action(() => { tsslStatus.Text = "Conexão TCP/IP Encerrada"; btConectar.Enabled = true; })); // Atualizar o TextBox na thread principal
                
            }            
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

                Byte[] enderecoBytes = DatapromFrame.VetorizaCodigoDoControlador(OpcodesDP.END_DUMMY); //63 eh um dummy do protocolo DP
                DatapromFrame frame = DatapromFrame.ConstroiFrameQNS(enderecoBytes, OpcodesDP.ENVIA_IDENTIFICACAO_8D, null); //Constroi Quadro Nao Seguro
                Byte[] frameVetorizado = DatapromFrame.VetorizaQuadro(frame);
                serialPort1.Write(frameVetorizado, 0, frameVetorizado.Length);

                BeginInvoke(new Action(() => {                    

                    tbIterRX.Text = "";
                    tbQtRX.Text = "";
                    tbSessaoRX.Text = "";
                    tbTagRX.Text = "";
                    cbCriptoRX.Enabled = false;

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
                DatapromFrame.aesKey = null;

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
                    tbIterTX.Text = "";
                    tbTabela.Text = "";
                    tbProtocolo.Text = "";
                    tbDescricao.Text = "";
                    tbCodigo.Text = "";
                    tbDescricao.Text = "";
                    tbAESkey.Text = "";
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
        private const int MaxFrameSize = 4096; // 4KB
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
                    ProcessFrame(frame);

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

        private void ProcessFrame(byte[] dadosRecebidos)
        {
            DatapromFrame frame = DatapromFrame.ObtemFrameDoVetor(dadosRecebidos, IKM);
            if (frame != null)
            {
                switch (frame.op)
                {
                    case OpcodesDP.MENSAGEM_INICIAL_GSM_80:
                        {
                            break;
                        }
                    case OpcodesDP.ENVIA_IDENTIFICACAO_8D:
                        {
                            string versaoSW = Encoding.ASCII.GetString(frame.dados, 0, 4);
                            string codigoControlador = Encoding.ASCII.GetString(frame.dados, 4, 6);
                            string descricao = Encoding.ASCII.GetString(frame.dados, 4 + 6, 128);
                            Byte[] versaoTabela = new Byte[4];
                            versaoTabela[0] = (byte)((frame.dados[frame.dados.Length - 4]) >> 4); //High
                            versaoTabela[1] = (byte)((frame.dados[frame.dados.Length - 3]) & 0x0F); //Low
                            string strVersaoTabela = $"{versaoTabela[0]}.{versaoTabela[1]}";

                            Byte[] versaoProtocolo = new Byte[4];
                            versaoProtocolo[0] = (byte)(frame.dados[frame.dados.Length - 2] >> 4);//High
                            versaoProtocolo[1] = (byte)(frame.dados[frame.dados.Length - 1] & 0x0F);//Low
                            string strVersaoProtocolo = $"{versaoProtocolo[0]}.{versaoProtocolo[1]}";


                            BeginInvoke(new Action(() => { tbSw.Text = versaoSW; tbCodigo.Text = codigoControlador; tbDescricao.Text = descricao; tbTabela.Text = strVersaoTabela; tbProtocolo.Text = strVersaoProtocolo; })); // Atualizar o TextBox na thread principal                                                                
                            tsslStatus.Text = "RX QUADRO NÃO SEGURO[BD]: - Recebidas Informações do Controlador";

                            BeginInvoke(new Action(() =>
                            {
                                tbQtRX.Text = frame.dados.Length.ToString();
                                tbIterRX.Text = "-";
                                tbSessaoRX.Text = "-";
                                tbTagRX.Text = "-";
                                cbCriptoRX.Checked = false;
                            })); // Atualizar o TextBox na thread principal


                            if (cbAuto.Checked)
                            {
                                parChavesProgramador = GenerateKeyPair();
                                var alicePublicKey = parChavesProgramador.Public as ECPublicKeyParameters;
                                var alicePrivateKey = parChavesProgramador.Private as ECPrivateKeyParameters;

                                ////Envia chave publica local pela SERIAL
                                byte[] publicKeyBytes = alicePublicKey.Q.GetEncoded(false); // false para descompactada
                                byte[] privateKeyBytes = alicePrivateKey.D.ToByteArray(); // false para descompactada

                                byte[] encodedPublicKeyBytes = Base64Code.EncodeToBase64Bytes(publicKeyBytes);

                                

                                DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaCodigoDoControlador(63), OpcodesDP.TROCA_CHAVES_PUBLICA_B6, encodedPublicKeyBytes); //Constroi Quadro Nao Seguro
                                Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);

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

                            break;
                        }
                    case OpcodesDP.TROCA_DADOS_SEGUROS_B5:
                        {
                            contadorMensagens = frame.iterador >contadorMensagens? frame.iterador:contadorMensagens; 

                            if (frame.dados[0] == OpcodesDP.SOLICITA_DATA_E_HORA_86)
                            {
                                tsslStatus.Text = "RX QUADRO SEGURO[B5-86]: - Recebeu Data e Hora!";
                                BeginInvoke(new Action(() =>
                                {
                                    
                                    
                                        tbQtRX.Text = frame.dados.Length.ToString();
                                        tbIterRX.Text = frame.iterador.ToString();
                                        tbSessaoRX.Text = Convert.ToString(frame.iterador / DatapromFrame.MSG_BY_SESSION);
                                        tbTagRX.Text = BitConverter.ToString(frame.tag).Replace("-", " ");
                                        cbCriptoRX.Checked = true;
                                        tbAESkey.Text = DatapromFrame.aesKey != null ? BitConverter.ToString(DatapromFrame.aesKey).Replace("-", " ") : "ERRO";
                                }));

                                if (cbRepete.Checked)
                                {
                                    Thread.Sleep(250);
                                    Byte[] quadro = new Byte[1];
                                    quadro[0] = OpcodesDP.SOLICITA_DATA_E_HORA_86; //Solicitação que será criptografada
                                    DatapromFrame frame2 = DatapromFrame.ConstroiFrameQS(DatapromFrame.VetorizaCodigoDoControlador(OpcodesDP.END_DUMMY), quadro, ref contadorMensagens, IKM); //63 eh um dummy do protocolo DP
                                    Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                                    serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
                                    tsslStatus.Text = "TX QUADRO SEGURO[B5-86]: - Solicitou data e hora do controlador";
                                    BeginInvoke(new Action(() =>
                                    {
                                        tbQtTX.Text = frame2.dados.Length.ToString();
                                        tbIterTX.Text = frame2.iterador.ToString();
                                        tbSessaoTX.Text = Convert.ToString(frame2.iterador / DatapromFrame.MSG_BY_SESSION);
                                        tbTagTX.Text = BitConverter.ToString(frame2.tag).Replace("-", " ");
                                        cbCriptoTX.Checked = true;
                                    })); // Atualizar o TextBox na thread principal
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
                                chavePublicaControlador = new ECPublicKeyParameters(point, domainParams);

                                SegredoCompartilhado = null;
                                SegredoCompartilhado = GenerateSharedSecret(parChavesProgramador.Private as ECPrivateKeyParameters, chavePublicaControlador);
                                IKM = new byte[65];
                                PSKLib.Get_PSK_IKM(1, SegredoCompartilhado, out IKM);

                                string hexString1 = BitConverter.ToString(decodedRemotePublicKeyBytes).Replace("-", " "); // Converter para HEX                

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

                                if (cbAuto.Checked)
                                {
                                    BeginInvoke(new Action(() =>
                                    {
                                        tbQtRX.Text = "";
                                        tbIterRX.Text = "";
                                        tbSessaoRX.Text = "";
                                        tbTagRX.Text = "";
                                        cbCriptoRX.Checked = false;
                                    })); // Limpa TB recepção

                                    if (IKM != null)
                                    {
                                        // IKM já cadastrada então envia pode enviar QS
                                        Byte[] quadro = new Byte[1];
                                        quadro[0] = OpcodesDP.SOLICITA_DATA_E_HORA_86; //Solicitação que será criptografada
                                        DatapromFrame frame2 = DatapromFrame.ConstroiFrameQS(DatapromFrame.VetorizaCodigoDoControlador(OpcodesDP.END_DUMMY), quadro, ref contadorMensagens, IKM); //63 eh um dummy do protocolo DP
                                        Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                                        serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
                                        tsslStatus.Text = "TX QUADRO SEGURO[B5-86]: - Solicitou data e hora do controlador";
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
                                        //Sem IKM, envia QNS
                                        Byte[] dados = null;
                                        DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaCodigoDoControlador(OpcodesDP.END_DUMMY), OpcodesDP.SOLICITA_DATA_E_HORA_86, dados); //63 eh um dummy do protocolo DP
                                        Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
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

                                if (cbRepete.Checked)
                                {
                                    Thread.Sleep(250);
                                    if (parChavesProgramador == null)
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

                                    DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaCodigoDoControlador(OpcodesDP.END_DUMMY), OpcodesDP.TROCA_CHAVES_PUBLICA_B6, encodedPublicKeyBytes); //Constroi Quadro Nao Seguro
                                    Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);

                                    serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
                                    tsslStatus.Text = "TX QUADRO NÃO SEGURO[B6]: - Solicitou Chave Pública do controlador";
                                    btPubKey.Enabled = false;

                                    BeginInvoke(new Action(() =>
                                    {
                                        tbQtTX.Text = frame2.dados.Length.ToString();
                                        tbIterTX.Text = "";
                                        tbSessaoTX.Text = "";
                                        tbTagTX.Text = "";
                                        cbCriptoTX.Checked = false;
                                    })); // Atualizar o TextBox na thread principal
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
                            if(cbRepete.Checked)
                            {
                                Thread.Sleep(250);
                                BeginInvoke(new Action(() =>
                                {
                                    tbQtRX.Text = "";
                                    tbIterRX.Text = "";
                                    tbSessaoRX.Text = "";
                                    tbTagRX.Text = "";
                                    cbCriptoRX.Checked = false;
                                })); //Limpa RX

                                //Sem IKM, envia QNS
                                Byte[] dados = null;
                                DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaCodigoDoControlador(OpcodesDP.END_DUMMY), OpcodesDP.SOLICITA_DATA_E_HORA_86, dados); //63 eh um dummy do protocolo DP
                                Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
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
                DatapromFrame frame2 = DatapromFrame.ConstroiFrameQS(DatapromFrame.VetorizaCodigoDoControlador(OpcodesDP.END_DUMMY), quadro, ref contadorMensagens, IKM); //63 eh um dummy do protocolo DP
                Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
                serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
                tsslStatus.Text = "TX QUADRO SEGURO[B5-86]: - Solicitou data e hora do controlador";
                BeginInvoke(new Action(() =>
                {
                    tbQtTX.Text = frame2.dados.Length.ToString();
                    tbIterTX.Text = frame2.iterador.ToString();
                    tbSessaoTX.Text = Convert.ToString(frame2.iterador / DatapromFrame.MSG_BY_SESSION);
                    tbTagTX.Text = BitConverter.ToString(frame2.tag).Replace("-", " ");
                    tbAESkey.Text = DatapromFrame.aesKey != null ? BitConverter.ToString(DatapromFrame.aesKey).Replace("-", " "): "ERRO";
                    cbCriptoTX.Checked = true;
                })); // Atualizar o TextBox na thread principal
            }
            else {
                //Sem IKM, envia QNS
                Byte[] dados = null;
                DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaCodigoDoControlador(OpcodesDP.END_DUMMY), OpcodesDP.SOLICITA_DATA_E_HORA_86, dados); //63 eh um dummy do protocolo DP
                Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);
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

            DatapromFrame frame2 = DatapromFrame.ConstroiFrameQNS(DatapromFrame.VetorizaCodigoDoControlador(OpcodesDP.END_DUMMY), OpcodesDP.TROCA_CHAVES_PUBLICA_B6, encodedPublicKeyBytes); //Constroi Quadro Nao Seguro
            Byte[] frame2Bytes = DatapromFrame.VetorizaQuadro(frame2);

            serialPort1.Write(frame2Bytes, 0, frame2Bytes.Length); // Solicita chave Publica
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
            
            cbRepete.Enabled = !cbAuto.Checked;
            if (cbAuto.Checked)
            {
                cbRepete.Checked = false;
            }
        }
    }
}

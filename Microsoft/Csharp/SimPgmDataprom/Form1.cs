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
        //Constantes
        const int TAG_LEN = 16;
        const int CONTADOR_LEN = 8;

        static AsymmetricCipherKeyPair parChavesProgramador;
        static ECPublicKeyParameters chavePublicaControlador;
        byte[] SegredoCompartilhado;
        byte[] IKM = null;
        byte[] desafioCriptografado = null;
        byte[] aesKey = new byte[32]; //Chave para AES256
        byte[] iv = new byte[12]; //Tabela IV
        bool chavePublicaRemotaRecebida = false;
        byte[] salt = Encoding.ASCII.GetBytes("DATAPROM_SALT");
        byte[] info = new byte[18] { (byte)'D', (byte)'A', (byte)'T', (byte)'A', (byte)'S', (byte)'E', (byte)'C', (byte)'R', (byte)'E', (byte)'T',
                                            0,          0,          0,        0,         0,         0,         0,       0};
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
                    //using (NetworkStream stream = cliente.GetStream())
                    //{
                        byte[] buffer = new byte[1024];
                        int bytesLidos = await stream.ReadAsync(buffer, 0, buffer.Length);
                        string mensagemRecebida = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

                        //Console.WriteLine($"Mensagem recebida: {mensagemRecebida}");

                        string resposta = "Mensagem recebida com sucesso!" + mensagemRecebida;
                        byte[] dadosResposta = Encoding.UTF8.GetBytes(resposta);

                        BeginInvoke(new Action(() => { tbOutput.Text = mensagemRecebida; })); // Atualizar o TextBox na thread principal


                        await stream.WriteAsync(dadosResposta, 0, dadosResposta.Length);
                    //}
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
            if (maquinaEstados == ME.None || maquinaEstados == ME.Desconectado)
            {
                chavePublicaRemotaRecebida = false;
                comboBox1.Enabled = false;
                tsslStatus.Text = "Enviando PBK...";
                btConectar.Text = "Desconectar";
                maquinaEstados = ME.Envia_PBK_Local;
                serialPort1.PortName = comboBox1.SelectedItem.ToString();
                serialPort1.Open();

                parChavesProgramador = GenerateKeyPair();
                var alicePublicKey = parChavesProgramador.Public as ECPublicKeyParameters;
                var alicePrivateKey = parChavesProgramador.Private as ECPrivateKeyParameters;

                //Envia chave publica local pela SERIAL
                byte[] publicKeyBytes = alicePublicKey.Q.GetEncoded(false); // false para descompactada
                byte[] privateKeyBytes = alicePrivateKey.D.ToByteArray(); // false para descompactada

                int tam1 = 0;
                byte[] dadosEmpacotados = EmpacotaDadosProtocolo(publicKeyBytes, out tam1);

                serialPort1.Write(dadosEmpacotados, 0, dadosEmpacotados.Length);
                maquinaEstados = ME.Recebe_PBK_Remota;                

                BeginInvoke(new Action(() => { tbChaveLocalPriv.Text = BitConverter.ToString(privateKeyBytes).Replace("-", " "); })); // Atualizar o TextBox na thread principal
                BeginInvoke(new Action(() => { tbChaveLocalPub.Text = BitConverter.ToString(publicKeyBytes).Replace("-", " "); })); // Atualizar o TextBox na thread principal

            }
            else
            {
                chavePublicaRemotaRecebida = false;
                comboBox1.Enabled=true;
                tsslStatus.Text = "Desconectado";
                btConectar.Text = "Conectar";
                BeginInvoke(new Action(() =>    {
                                                    tbChaveLocalPriv.Text = "";
                                                    tbChaveLocalPub.Text = "";
                                                    tbOutput.Text = ""; 
                                                    tbQt.Text = ""; 
                                                    tbChaveRemPub.Text = ""; 
                                                    tbSegredo.Text = "";
                                                    tbDesafio.Text = "";
                                                })); // Atualizar o TextBox na thread principal
                                                     
                maquinaEstados = ME.Desconectado;
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
        /*Processa um frame recebido*/
        {
            if (maquinaEstados == ME.Recebe_PBK_Remota)
            {                
                    chavePublicaRemotaRecebida = true;                    

                    //Retira header
                    byte[] dadosRecebidosSemHeader = new byte[dadosRecebidos.Length - 2];
                    Array.Copy(dadosRecebidos, 1, dadosRecebidosSemHeader, 0, dadosRecebidosSemHeader.Length);

                    int tamPacote = 0;
                    var chavePublicaRem = DesempacotaDadosProtocolo(dadosRecebidosSemHeader, out tamPacote);

                    var curve = ECNamedCurveTable.GetByName("secp256r1");
                    var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
                    var point = curve.Curve.DecodePoint(chavePublicaRem);
                    chavePublicaControlador = new ECPublicKeyParameters(point, domainParams);
                    SegredoCompartilhado = null;
                    SegredoCompartilhado = GenerateSharedSecret(parChavesProgramador.Private as ECPrivateKeyParameters, chavePublicaControlador);
                    IKM = new byte[65];
                    PSKLib.Get_PSK_IKM(1, SegredoCompartilhado,out IKM);

                    string hexString = BitConverter.ToString(chavePublicaRem).Replace("-", " "); // Converter para HEX                
                    BeginInvoke(new Action(() => { tbOutput.Text = hexString; tbQt.Text = chavePublicaRem.Length.ToString(); tbChaveRemPub.Text = hexString; tbSegredo.Text = BitConverter.ToString(SegredoCompartilhado).Replace("-", " "); })); // Atualizar o TextBox na thread principal                
                    maquinaEstados = ME.Recebe_Desafio;
                    tsslStatus.Text = "Segredo ECDH criado";

            }
            else if (maquinaEstados == ME.Recebe_Desafio) // Recebendo desafio criptografado
            {               
                byte[] dadosRecebidosSemHeader = new byte[dadosRecebidos.Length - 2];
                Array.Copy(dadosRecebidos, 1, dadosRecebidosSemHeader, 0, dadosRecebidosSemHeader.Length);
                
                int tamPacote = 0;
                byte[] desafioCriptografado = DesempacotaDadosProtocolo(dadosRecebidosSemHeader, out tamPacote);

                contadorMensagens = BitConverter.ToUInt64(desafioCriptografado.Take(8).ToArray(), 0); // Atualiza contador com o índice atual da mensagem
                
                Buffer.BlockCopy(desafioCriptografado, 0, info, info.Length - CONTADOR_LEN, CONTADOR_LEN); // Copia o contador para o final do info

                string hexString = BitConverter.ToString(desafioCriptografado).Replace("-", " "); // Converte os dados recebidos p/HEX e salva para ser impresso em uma Textbox de RX
                                
                var hkdfKey = new HkdfBytesGenerator(new Sha256Digest()); //Cria um gerador HKDF para chave
                hkdfKey.Init(new HkdfParameters(IKM, salt, info)); 
                hkdfKey.GenerateBytes(aesKey, 0, aesKey.Length); // Gera chave AES256

                var hkdfIV = new HkdfBytesGenerator(new Sha256Digest()); //Cria um novo gerador HKDF pra IV
                hkdfIV.Init(new HkdfParameters(IKM, salt, info)); 
                hkdfIV.GenerateBytes(iv, 0, iv.Length); // Gera IV

                // Inicializa o AES-GCM
                var gcm = new GcmBlockCipher(new AesEngine());
                var aeadParams = new AeadParameters(new KeyParameter(aesKey), TAG_LEN * 8, iv);
                gcm.Init(false, aeadParams); // false = modo decifrar

                byte[] entrada = desafioCriptografado.Skip(8).ToArray();                
                byte[] resultado = new byte[gcm.GetOutputSize(entrada.Length)];

                try
                {
                    int len = gcm.ProcessBytes(entrada, 0, entrada.Length, resultado, 0);
                    len += gcm.DoFinal(resultado, len);

                    string desafioDecifrado = Encoding.UTF8.GetString(resultado, 0, len);
                    BeginInvoke(new Action(() => { tbDesafio.Text = desafioDecifrado; })); // Atualizar o TextBox na thread principal 
                }
                catch (InvalidCipherTextException)
                {
                    BeginInvoke(new Action(() => { tbDesafio.Text = "Desafio Inválido"; })); // Atualizar o TextBox na thread principal 
                }                

                BeginInvoke(new Action(() => { tbOutput.Text = hexString; tbQt.Text = desafioCriptografado.Length.ToString(); }));

                maquinaEstados = ME.Envia_Solucao;                
                
                byte[] solucaoBytes = System.Text.Encoding.ASCII.GetBytes(tbDesafioLocal.Text); // Transforma a solução em Bytes
                
                //Criptografar dados
                contadorMensagens += 1;

                byte[] contadorMensagensBytes = BitConverter.GetBytes(contadorMensagens); //Transforma contadorMensagens em bytes
                if (BitConverter.IsLittleEndian == false) Array.Reverse(contadorMensagensBytes); // garante little-endian se necessário                
                Buffer.BlockCopy(contadorMensagensBytes, 0, info, info.Length - CONTADOR_LEN, contadorMensagensBytes.Length); //Atualiza o info para criar uma nova IV

                hkdfKey = new HkdfBytesGenerator(new Sha256Digest()); //Cria um gerador HKDF para chave
                hkdfKey.Init(new HkdfParameters(IKM, salt, info));
                hkdfKey.GenerateBytes(aesKey, 0, aesKey.Length); // Gera chave AES256

                hkdfIV = new HkdfBytesGenerator(new Sha256Digest());
                hkdfIV.Init(new HkdfParameters(IKM, salt, info));
                hkdfIV.GenerateBytes(iv, 0, iv.Length); // Atualiza IV usando o novo INFO

                // Criptografa com AES-GCM
                var gcmEnc = new GcmBlockCipher(new AesEngine());
                var aeadParamsEnc = new AeadParameters(new KeyParameter(aesKey), TAG_LEN * 8, iv); // 128 = TAG de 16 bytes
                gcmEnc.Init(true, aeadParamsEnc); // true = encriptação

                byte[] cifra = new byte[gcmEnc.GetOutputSize(solucaoBytes.Length)];
                int lenEnc = gcmEnc.ProcessBytes(solucaoBytes, 0, solucaoBytes.Length, cifra, 0);
                lenEnc += gcmEnc.DoFinal(cifra, lenEnc); // cifra contém dados + tag

                // Monta quadro: [contador | cifra (dados) |  tag]
                byte[] quadroPayload = new byte[contadorMensagensBytes.Length + lenEnc];
                Buffer.BlockCopy(contadorMensagensBytes, 0, quadroPayload, 0, contadorMensagensBytes.Length);
                Buffer.BlockCopy(cifra, 0, quadroPayload, contadorMensagensBytes.Length, lenEnc);

                // Aplica protocolo de empacotamento
                int tamQuadroTX = 0;
                var quadroTX = EmpacotaDadosProtocolo(quadroPayload, out tamQuadroTX);
                
                // Envia pela serial
                serialPort1.Write(quadroTX, 0, quadroTX.Length);

                // Atualiza status
                tsslStatus.Text = "Desafio Remoto Recebido. Solução Enviada...";
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
        public static byte[] DesempacotaDadosProtocolo(byte[] input, out int lenOut)
        {
            if (input == null || input.Length == 0)
            {
                lenOut = 0;
                return null;
            }

            int totalBits = input.Length * 7;
            lenOut = totalBits / 8;
            byte[] output = new byte[lenOut];

            int inIndex = input.Length - 1;
            int outIndex = lenOut - 1;
            int shift = 1;            

            for (outIndex = lenOut - 1; outIndex >= 0; outIndex--)
            {
                output[outIndex] = (byte)(input[inIndex] << shift);

                output[outIndex] = ClearLSBNBits(output[outIndex], shift);                
                output[outIndex] |= (byte)((input[inIndex - 1] & 0b01111111) >> (7 - shift));
                inIndex--;

                if ((shift + 1) % 8 != 0)
                {
                    shift++;

                }
                else
                {
                    shift = 1;
                    inIndex--;
                }
            }

            return output;            
        }        

        public static byte[] EmpacotaDadosProtocolo(byte[] input, out int lenOut)
            /*Empacota dados em 7 bits*/
        {    
            int totalBits = input.Length * 8;
            lenOut = (totalBits % 7 != 0) ? (totalBits / 7) + 1 : (totalBits / 7);
            byte[] output = new byte[lenOut];

            int j = lenOut - 1;
            int shift = 1;
            byte msb = 0, lsb = 0;               


            for (int i = input.Length - 1; i >= 0; i--)
            {
                msb = input[i];
                msb >>= shift;
                lsb = input[i];

                lsb = ClearMSBNBits(lsb, 8 - shift);
                lsb <<= (7 - shift);


                output[j] |= 0x80;
                output[j] |= msb;
                j--;
                output[j] |= 0x80;
                output[j] |= lsb;

                if ((shift + 1) % 8 != 0)
                {
                    shift++;

                }
                else
                {
                    shift = 1;
                    j--;
                }
            }

            byte[] dadosEmpacotadoscomHeader = new byte[output.Length + 2];

            dadosEmpacotadoscomHeader[0] = 0x02; // início
            Array.Copy(output, 0, dadosEmpacotadoscomHeader, 1, output.Length);
            dadosEmpacotadoscomHeader[dadosEmpacotadoscomHeader.Length - 1] = 0x03; // final

            return dadosEmpacotadoscomHeader;            
        }

        private static byte ClearLSBNBits(byte b, int n)
        {
            return (byte)(b & (~((1 << n) - 1)));
        }

        private static byte ClearMSBNBits(byte b, int n)
        {
            return (byte)(b & ((1 << (8 - n)) - 1));
        }
    }
}

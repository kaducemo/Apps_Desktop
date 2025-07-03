using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



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

namespace SimPgmDataprom
{
    public partial class Form1 : Form
    {

        static AsymmetricCipherKeyPair parChavesProgramador;
        static ECPublicKeyParameters chavePublicaControlador;
        byte[] SegredoCompartilhado;
        byte[] desafioCriptografado = null;
        byte[] aesKey = new byte[32]; //Chave para AES256
        byte[] iv = new byte[16]; //Tabela IV
        bool chavePublicaRemotaRecebida = false;
        byte[] salt = Encoding.ASCII.GetBytes("DATAPROM_SALT");
        byte[] info = new byte[18] { (byte)'D', (byte)'A', (byte)'T', (byte)'A', (byte)'S', (byte)'E', (byte)'C', (byte)'R', (byte)'E', (byte)'T',
                                            0,          0,          0,        0,         0,         0,         0,       0};

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
                                
                byte[] dadosEmpacotadoscomHeader = new byte[dadosEmpacotados.Length + 2];

                dadosEmpacotadoscomHeader[0] = 0x02; // início
                Array.Copy(dadosEmpacotados, 0, dadosEmpacotadoscomHeader, 1, dadosEmpacotados.Length);
                dadosEmpacotadoscomHeader[dadosEmpacotadoscomHeader.Length - 1] = 0x03; // final

                serialPort1.Write(dadosEmpacotadoscomHeader, 0, dadosEmpacotadoscomHeader.Length);
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

                    string hexString = BitConverter.ToString(chavePublicaRem).Replace("-", " "); // Converter para HEX                
                    BeginInvoke(new Action(() => { tbOutput.Text = hexString; tbQt.Text = chavePublicaRem.Length.ToString(); tbChaveRemPub.Text = hexString; tbSegredo.Text = BitConverter.ToString(SegredoCompartilhado).Replace("-", " "); })); // Atualizar o TextBox na thread principal                
                    maquinaEstados = ME.Recebe_Desafio;

            }
            else if (maquinaEstados == ME.Recebe_Desafio) // Recebendo desafio criptografado
            {               
                byte[] dadosRecebidosSemHeader = new byte[dadosRecebidos.Length - 2];
                Array.Copy(dadosRecebidos, 1, dadosRecebidosSemHeader, 0, dadosRecebidosSemHeader.Length);
                
                int tamPacote = 0;
                var desafioCriptografado = DesempacotaDadosProtocolo(dadosRecebidosSemHeader, out tamPacote);
                
                Buffer.BlockCopy(desafioCriptografado, 0, info, info.Length - 8, 8);

                string hexString = BitConverter.ToString(desafioCriptografado).Replace("-", " "); // Converter para HEX
                                
                var hkdfKey = new HkdfBytesGenerator(new Sha256Digest());
                hkdfKey.Init(new HkdfParameters(SegredoCompartilhado, salt, info.Take(10).ToArray()));
                hkdfKey.GenerateBytes(aesKey, 0, aesKey.Length);

                var hkdfIV = new HkdfBytesGenerator(new Sha256Digest());
                hkdfIV.Init(new HkdfParameters(SegredoCompartilhado, salt, info));
                hkdfIV.GenerateBytes(iv, 0, iv.Length);                

                using (Aes aes = Aes.Create())
                {
                    aes.Mode = CipherMode.CBC;                    
                    aes.Padding = PaddingMode.PKCS7; 
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    aes.Key = aesKey;
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor())
                    {
                        byte[] textoDescriptografado = decryptor.TransformFinalBlock(desafioCriptografado.Skip(8).ToArray(), 0, desafioCriptografado.Length-8);
                        BeginInvoke(new Action(() => { tbDesafio.Text = System.Text.Encoding.UTF8.GetString(textoDescriptografado); })); // Atualizar o TextBox na thread principal                
                    }
                }

                BeginInvoke(new Action(() => { tbOutput.Text = hexString; tbQt.Text = desafioCriptografado.Length.ToString(); }));
                
            }
        }

        static byte[] DeriveFromHKDF(byte[] secret, byte[] salt, string infoText, int outputLength)
            /*Deriva IV a partir de um contexto e um numero de iterações*/
        {
            byte[] info = Encoding.ASCII.GetBytes(infoText);
            var hkdf = new HkdfBytesGenerator(new Sha256Digest());
            hkdf.Init(new HkdfParameters(secret, salt, info));
            byte[] output = new byte[outputLength];
            hkdf.GenerateBytes(output, 0, outputLength);
            return output;
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
            
            return output;            
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

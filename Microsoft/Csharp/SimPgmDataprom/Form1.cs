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
        byte[] salt = Encoding.ASCII.GetBytes("DATAPROM");
        byte[] info = Encoding.ASCII.GetBytes("SECRETCOMM");
        //string info = "SECRETCOMM";




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
                tsslStatus.Text = "Conectando...";
                btConectar.Text = "Desconectar";
                maquinaEstados = ME.Conectado;
                serialPort1.PortName = comboBox1.SelectedItem.ToString();
                serialPort1.Open();

                parChavesProgramador = GenerateKeyPair();
                var alicePublicKey = parChavesProgramador.Public as ECPublicKeyParameters;
                var alicePrivateKey = parChavesProgramador.Private as ECPrivateKeyParameters;

                //Envia chave publica local pela SERIAL
                byte[] publicKeyBytes = alicePublicKey.Q.GetEncoded(false); // false para descompactada
                byte[] privateKeyBytes = alicePrivateKey.D.ToByteArray(); // false para descompactada
                serialPort1.Write(publicKeyBytes, 0, publicKeyBytes.Length);

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
        
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {   
            if(chavePublicaRemotaRecebida == false)
            {
                if (serialPort1.BytesToRead >= 65)
                {
                    chavePublicaRemotaRecebida = true;
                    byte[] dadosRecebidos = new byte[65];
                    serialPort1.Read(dadosRecebidos, 0, 65);

                    var curve = ECNamedCurveTable.GetByName("secp256r1");
                    var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
                    var point = curve.Curve.DecodePoint(dadosRecebidos);
                    chavePublicaControlador = new ECPublicKeyParameters(point, domainParams);
                    SegredoCompartilhado = null;
                    SegredoCompartilhado = GenerateSharedSecret(parChavesProgramador.Private as ECPrivateKeyParameters, chavePublicaControlador);

                    string hexString = BitConverter.ToString(dadosRecebidos).Replace("-", " "); // Converter para HEX                
                    BeginInvoke(new Action(() => { tbOutput.Text = hexString; tbQt.Text = dadosRecebidos.Length.ToString(); tbChaveRemPub.Text = hexString; tbSegredo.Text = BitConverter.ToString(SegredoCompartilhado).Replace("-", " "); })); // Atualizar o TextBox na thread principal                
                }                
            }
            else // Recebendo mensagem criptografada
            {
                int qt = serialPort1.BytesToRead;
                //if (serialPort1.BytesToRead >= 16)
                //{
                    desafioCriptografado = new byte[serialPort1.BytesToRead];
                    serialPort1.Read(desafioCriptografado, 0, serialPort1.BytesToRead);
                    string hexString = BitConverter.ToString(desafioCriptografado).Replace("-", " "); // Converter para HEX

                    //byte[] salt = Encoding.ASCII.GetBytes("DATAPROM");
                    //byte[] info = Encoding.ASCII.GetBytes("SECRETCOMM");
                    var hkdf = new HkdfBytesGenerator(new Sha256Digest());
                    hkdf.Init(new HkdfParameters(SegredoCompartilhado, salt, info));
                    hkdf.GenerateBytes(aesKey, 0, aesKey.Length);
                    hkdf.GenerateBytes(iv, 0, iv.Length);

                    using (Aes aes = Aes.Create())
                    {
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.None; // ou PaddingMode.PKCS7 se houver padding
                        aes.KeySize = 256;
                        aes.BlockSize = 128;
                        aes.Key = aesKey;
                        aes.IV = iv;

                        using (var decryptor = aes.CreateDecryptor())
                        {
                            byte[] textoDescriptografado = decryptor.TransformFinalBlock(desafioCriptografado, 0, desafioCriptografado.Length);
                            BeginInvoke(new Action(() => { tbDesafio.Text = System.Text.Encoding.UTF8.GetString(textoDescriptografado); })); // Atualizar o TextBox na thread principal                
                        }
                    }                 

                    BeginInvoke(new Action(() => { tbOutput.Text = hexString; tbQt.Text = desafioCriptografado.Length.ToString(); }));
                //}
            }
        }

        static byte[] DeriveFromHKDF(byte[] secret, byte[] salt, string infoText, int outputLength)
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
            //var agreement = new ECDHBasicAgreement();
            //agreement.Init(privateKey);
            //var sharedSecret = agreement.CalculateAgreement(publicKey);
            ////return sharedSecret.ToByteArray().Skip(1).ToArray();
            //return sharedSecret.ToByteArray();

            var ecDomain = privateKey.Parameters;
            var q = publicKey.Q.Multiply(privateKey.D).Normalize();
            var encodedPoint = q.GetEncoded(false); // false → formato não compactado, inclui 0x04
            return encodedPoint;
        }

    }
}

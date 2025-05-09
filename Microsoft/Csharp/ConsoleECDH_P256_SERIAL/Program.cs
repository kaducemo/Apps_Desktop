using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Ports;


using System.Text;
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



namespace ConsoleECDH_B163
{

    internal class Program
    {
        static SerialPort serialPort;
        static AsymmetricCipherKeyPair aliceKeyPair;
        static ECPublicKeyParameters bobPublicKey;
        static byte[] segredo;
        static int SM = 0;
        static string decodificada;

        static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (SM == 0)
            {
                SerialPort sp = (SerialPort)sender;
                byte[] bobPublicKeyBytes = new byte[sp.BytesToRead];
                sp.Read(bobPublicKeyBytes, 0, bobPublicKeyBytes.Length);

                var curve = ECNamedCurveTable.GetByName("secp256r1");
                var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
                var point = curve.Curve.DecodePoint(bobPublicKeyBytes);
                bobPublicKey = new ECPublicKeyParameters(point, domainParams);

                Console.WriteLine("Chave pública de Bob recebida.");

                // Geração de chaves para Alice
                aliceKeyPair = GenerateKeyPair();
                var alicePublicKey = aliceKeyPair.Public as ECPublicKeyParameters;
                var alicePrivateKey = aliceKeyPair.Private as ECPrivateKeyParameters;

                //Envia chave publica local pela SERIAL
                byte[] publicKeyBytes = alicePublicKey.Q.GetEncoded(false); // false para descompactada
                serialPort.Write(publicKeyBytes, 0, publicKeyBytes.Length);

                // Derivar segredo entre a chave Privada Local e a Chave Publica Remota
                segredo = GenerateSharedSecret(aliceKeyPair.Private as ECPrivateKeyParameters, bobPublicKey);
                Console.WriteLine($"Chave compartilhada derivada: {BitConverter.ToString(segredo)}");

                SM = 1;
            }
            else if (SM == 1)
                //Recebe segredo
            {   
                SerialPort sp = (SerialPort)sender;
                byte[] msg = new byte[sp.BytesToRead];
                sp.Read(msg, 0, msg.Length);
                Console.WriteLine("Recebeu a segredo!");
                SM = 2;

            }
            else if(SM == 2)
            {

                // Recebe mensagem e descriptografa a mensagem
                

                SerialPort sp = (SerialPort)sender;
                byte[] mensagem = new byte[sp.BytesToRead];
                sp.Read(mensagem, 0, mensagem.Length);
                decodificada = DecryptMessage(segredo.Take(16).ToArray(), mensagem);
                SM = 3;
                //Console.WriteLine("Pressione uma tecla para ver a mensagem recebida...");

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

        static byte[] GenerateSharedSecret(ECPrivateKeyParameters privateKey, ECPublicKeyParameters publicKey)
        {
            var agreement = new ECDHBasicAgreement();
            agreement.Init(privateKey);
            var sharedSecret = agreement.CalculateAgreement(publicKey);
            return sharedSecret.ToByteArray().Skip(1).ToArray();
        }

        static byte[] EncryptMessage(byte[] key, string message)
        {
            var cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(new AesEngine()));
            var keyParam = new KeyParameter(key.Take(16).ToArray());
            var iv = new byte[16];
            new SecureRandom().NextBytes(iv);
            var parameters = new ParametersWithIV(keyParam, iv);

            cipher.Init(true, parameters);
            var input = Encoding.UTF8.GetBytes(message);
            var output = new byte[cipher.GetOutputSize(input.Length)];
            var length = cipher.ProcessBytes(input, 0, input.Length, output, 0);
            cipher.DoFinal(output, length);

            return iv.Concat(output).ToArray();
        }

        static string DecryptMessage(byte[] key, byte[] encryptedMessage)
        {

            //var cipher = new PaddedBufferedBlockCipher(new EcbBlockCipher(new AesEngine()));
            var cipher = new BufferedBlockCipher(new EcbBlockCipher(new AesEngine())); // << USE BufferedBlockCipher
            var keyParam = new KeyParameter(key.Take(16).ToArray());

            cipher.Init(false, keyParam);
            var output = new byte[cipher.GetOutputSize(encryptedMessage.Length)];
            var length = cipher.ProcessBytes(encryptedMessage, 0, encryptedMessage.Length, output, 0);
            cipher.DoFinal(output, length);

            string tmp = Encoding.ASCII.GetString(output).TrimEnd('\0');
            return tmp;

        }

        static void Main(string[] args)
        {
            serialPort = new SerialPort("COM15", 115200, Parity.None, 8, StopBits.One);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            serialPort.Open();
            
            Console.WriteLine("Espere receber as chaves ou pressione uma tecla pra continuar...");

            while (Console.ReadKey(true) == null) ; // Espera aqui até que as chaves sejam trocadas ou uma tecla seja pressionada

            serialPort.Write("A");



            // Manter o console aberto
            
            while (Console.ReadKey(true) == null) ; // Espera aqui até que as chaves sejam trocadas ou uma tecla seja pressionada
            Console.WriteLine("Recebeu a mensagem: " + decodificada);
            Console.WriteLine("Pressione uma tecla para sair...");
            Console.ReadKey(true);
            
            //Console.ReadLine();

        }
    }
}

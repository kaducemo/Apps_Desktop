using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimPgmDataprom
{
    public class DatapromFrame
    {
        private const int AES_KEY_LEN = 16;
        private const int AES_IV_LEN = 12;
        private const int ADD_LEN = 3; // Tamanho da TAG GCM  (tamanho do endereco)
        private const int GCM_TAG_LEN = 16; // Tamanho da TAG GCM   (decodificado do B64)     
        private const int CONTADOR_LEN = 8; // Tamanho do contador (decodificado do B64)
        public const int MSG_BY_SESSION = 10; // Numero de mensagens por sessao
        private const int END_OFFSET = 1; // Offset do endereco
        private const int OP_OFFSET = 4; // Offset do OPCODE    
        private const int RES_OFFSET = 5; //Offset do RES de um quadro seguro
        private const int B64_ITERADOR_OFFSET = 6; //Offset do ITERADOR de um quadro SEGURO
        private const int B64_ITERADOR_LEN = 12; //Comprimento do ITERADOR de um quadro SEGURO em B64
        private const int B64_TAG_OFFSET = 18; //Offset do TAG-GCM de um quadro SEGURO
        private const int B64_TAG_LEN = 24; //Comprimento do TAG-GCM de um quadro SEGURO em B64
        private const int B64_DATA_OFFSET = 42; //Offset dos dados de um quadro SEGURO
        private const int LEN_MINIMAL_QNS = 7; //Tamanho do quadro nao seguro minimo (sem dados)
        private const int LEN_MINIMAL_QS = 44; //Tamanho do quadro SEGURO minimo (sem dados)

        private static byte[] salt = new byte[11] { (byte)'D', (byte)'A', (byte)'T', (byte)'A', (byte)'S', (byte)'A', (byte)'L', (byte)'T', 0, 0, 0};

        private static byte[] info = new byte[16] { (byte)'D', (byte)'A', (byte)'T', (byte)'A', (byte)'I', (byte)'N', (byte)'F', (byte)'O',
                                            0,          0,          0,        0,         0,         0,         0,       0};

        //public static byte[] aesKey = null;


        public Byte[] endereco = new Byte[ADD_LEN]; // Endereco Controlador (AREA/CONTROLADOR/SUBCONTROLADOR)
        public Byte op; //OPCODE NAO SEGURO

        // Utilizado apenas em Quadros Seguros (QS)
        public Byte res; // Reservado
        public UInt64 iterador; // Cotador de pacotes
        public Byte[] tag = new Byte[GCM_TAG_LEN]; // Tag GCM

        // Utilizado apenas em Quadros Nao Seguros (QNS)
        public Byte[] dados;   // Dados (criptografados ou não)        

        static Byte GeraChecksum(Byte[] input, UInt16 offset, UInt16 q)
        {
            UInt16 i = 0, j = 0;
            Byte checksum = 0;

            if (input != null && offset < input.Length && q > 0 && ((offset + q) <= input.Length))
            {
                for (checksum = 0, i = offset, j = 0; j < q; j++)
                    checksum ^= input[i + j];
            }                

            return (Byte)((~checksum) | 0x80);
        }

        static void LimpaMSB(byte[] vet, UInt16 offset, UInt16 q)
        {            
            if (vet != null && offset <vet.Length && q > 0 && ((offset + q) <= vet.Length))
            {
                for (int i = offset, j = 0; j < q; j++)                
                    vet[i+j] &= 0x7F;                
            }
        }

        void SetaMSB(Byte[] vet, UInt16 offset, UInt16 q)
        {
            if (vet != null && offset < vet.Length && q > 0 && ((offset + q) <= vet.Length))
            {
                for (int i = offset, j = 0; j < q; j++)
                    vet[i+j] |= 0x80;
            }
        }

        public static Byte ObtemCodigoControladorDoVetor(Byte[] input)
        {
            Byte codigo = 0;
            codigo |= (Byte)((input[1] << 4) & 0x30);
            codigo |= (Byte)((input[2] >> 3) & 0x0F);
            return codigo;            
        }

        public static Byte ObtemAreaControladorDoVetor(Byte[] input)
        {
            Byte area = 0;
            area |= (Byte)((input[0] >> 1) & 0x3F);
            return area;
        }

        public static Byte ObtemRedeControladorDoVetor(Byte[] input)
        {
            Byte rede = 0;
            rede |= (Byte)((input[0] << 5) & 0x20);
            rede |= (Byte)((input[1] >> 2) & 0x1F);
            return rede;
        }

        static public Byte[] VetorizaIdDoControlador(Byte cod, Byte rede, Byte area)
        {
            Byte[] output = new Byte[3];

            //Codigo
            output[0] = 0;
            output[1] = (Byte)((cod >> 4) & 0x03);
            output[2] = (Byte)((cod << 3) & 0x78);

            //Rede
            output[0] |= (Byte)((rede >> 5) & 0x01);
            output[1] |= (Byte)((rede << 2) & 0x7C);

            //Area
            output[0] |= (Byte)((area << 1) & 0x7E);

            output[0] |= 0x80;
            output[1] |= 0x80;
            output[2] |= 0x80;

            return output;
        }

        static public DatapromFrame ObtemFrameDoVetor(Byte[] vet, Byte[] ikm, ref Byte[] aesKey, int salt_id)
        {
            DatapromFrame ret = null;
            

            //Verifica Compatibilidade/Integridade do vetor
            if ((vet[0] == OpcodesDP.STX) && (vet[vet.Length - 1] == OpcodesDP.ETX) && (vet[vet.Length - 2] == GeraChecksum(vet,1, (UInt16)(vet.Length - 3))))
            {

                ret = new DatapromFrame();                 

                LimpaMSB(vet,END_OFFSET, 3);
                ret.endereco[0] = vet[END_OFFSET + 0];
                ret.endereco[1] = vet[END_OFFSET + 1];
                ret.endereco[2] = vet[END_OFFSET + 2];

                ret.op = vet[OP_OFFSET];

                if (ret.op == OpcodesDP.TROCA_DADOS_SEGUROS_B5)
                { 
                    // Quadro seguro (possui campos adicionais)
                    if (vet.Length > LEN_MINIMAL_QS && ikm != null)
                    {
                        aesKey = new byte[AES_KEY_LEN]; //Chave para AES
                        byte[] iv = new byte[AES_IV_LEN]; //Tabela IV                                                                       
                        
                        // Decodifica iterador
                        LimpaMSB(vet, B64_ITERADOR_OFFSET, B64_ITERADOR_LEN);
                        Byte[] iteradorTMP = Base64Code.DecodeFromBase64Bytes(vet.Skip(B64_ITERADOR_OFFSET).Take(12).ToArray()); //Decodifica B64
                        ret.iterador = BitConverter.ToUInt64(iteradorTMP, 0); //Salva na classe em UINT64
                                                

                        // Decodifica TAG
                        LimpaMSB(vet, B64_TAG_OFFSET, B64_TAG_LEN);
                        ret.tag = Base64Code.DecodeFromBase64Bytes(vet.Skip(B64_TAG_OFFSET).Take(24).ToArray());

                        // Retira RES
                        LimpaMSB(vet,RES_OFFSET, 1);
                        ret.res = vet[RES_OFFSET];

                        //Entao existe dados (obrigatorio)
                        UInt16 tamDadosB64 = (UInt16)((vet.Length - 2) - B64_DATA_OFFSET);

                        //Decodifica Dados(Mantem Criptografado) e anexa TAG no final
                        LimpaMSB(vet, B64_DATA_OFFSET, tamDadosB64);
                        Byte[] dadosDecodificados = Base64Code.DecodeFromBase64Bytes(vet.Skip(B64_DATA_OFFSET).Take(tamDadosB64).ToArray());
                        Byte[] dadosDecodTAG = new byte[dadosDecodificados.Length + ret.tag.Length];                        
                        Buffer.BlockCopy(dadosDecodificados, 0, dadosDecodTAG, 0, dadosDecodificados.Length);
                        Buffer.BlockCopy(ret.tag, 0, dadosDecodTAG, dadosDecodificados.Length, ret.tag.Length);

                        // Inicia processo de Descriptografia
                        Byte[] iteradorBytes = BitConverter.GetBytes(ret.iterador); //Utilizado no INFO da IV
                        Byte[] sessaoBytes   = BitConverter.GetBytes(ret.iterador / MSG_BY_SESSION); //Utilizado no INFO da CHAVE

                        Byte[] infoIV = new Byte[info.Length];
                        Byte[] infoKey = new Byte[info.Length];
                        Byte[] saltWithId = new Byte[salt.Length];

                        Buffer.BlockCopy(info, 0, infoIV, 0, info.Length); // LABEL INFO PARA IVS
                        Buffer.BlockCopy(info, 0, infoKey, 0, info.Length); // LABEL INFO PARA KEY
                        Buffer.BlockCopy(salt, 0, saltWithId, 0, salt.Length); // LABEL SALT

                        if(salt_id == 1)
                        {
                            Byte[] auxSaltId = new byte[3];                            
                            auxSaltId[0] = ObtemAreaControladorDoVetor(vet.Skip(1).Take(3).ToArray());
                            auxSaltId[1] = ObtemRedeControladorDoVetor(vet.Skip(1).Take(3).ToArray());
                            auxSaltId[2] = ObtemCodigoControladorDoVetor(vet.Skip(1).Take(3).ToArray());
                            Buffer.BlockCopy(auxSaltId, 0, saltWithId, 8, auxSaltId.Length);
                        }


                        Buffer.BlockCopy(iteradorBytes, 0, infoIV, infoIV.Length - CONTADOR_LEN, CONTADOR_LEN); // Copia o contador para o final do infoIV
                        Buffer.BlockCopy(sessaoBytes, 0, infoKey, infoKey.Length - CONTADOR_LEN, CONTADOR_LEN); // Copia o contador para o final do infoKEY

                        var hkdfKey = new HkdfBytesGenerator(new Sha256Digest()); //Cria um gerador HKDF para chave
                        hkdfKey.Init(new HkdfParameters(ikm, saltWithId, infoKey));
                        hkdfKey.GenerateBytes(aesKey, 0, aesKey.Length); // Gera chave AES128

                        var hkdfIV = new HkdfBytesGenerator(new Sha256Digest()); //Cria um novo gerador HKDF pra IV
                        hkdfIV.Init(new HkdfParameters(ikm, saltWithId, infoIV));
                        hkdfIV.GenerateBytes(iv, 0, iv.Length); // Gera IV

                        // Inicializa o AES-GCM
                        var gcm = new GcmBlockCipher(new AesEngine());
                        var aeadParams = new AeadParameters(new KeyParameter(aesKey), GCM_TAG_LEN * 8, iv);
                        gcm.Init(false, aeadParams); // false = modo decifrar

                        //  Local onde dados serão descriptografados
                        ret.dados = new byte[gcm.GetOutputSize(dadosDecodTAG.Length)];

                        try {
                            int len = gcm.ProcessBytes(dadosDecodTAG, 0, dadosDecodTAG.Length, ret.dados, 0);
                            len += gcm.DoFinal(ret.dados, len); //Checa TAG                            
                        }
                        catch (InvalidCipherTextException) {
                            //TAG não bateu.
                            ret = null;
                        } 
                    }
                    else
                    {                        
                        ret = null;
                    }
                }
                else if (ret.op >= OpcodesDP.MENSAGEM_INICIAL_GSM_80 && ret.op <= OpcodesDP.TROCA_CHAVES_PUBLICA_B6) {
                    // Quadro nao seguro
                    if (vet.Length > LEN_MINIMAL_QNS) {
                        //Entao existe dados
                        ret.dados = vet.Skip(OP_OFFSET + 1).Take((vet.Length - 1) - (OP_OFFSET + 2)).ToArray();
                        LimpaMSB(ret.dados, 0, (UInt16)ret.dados.Length);
                    }
                }
            }
            return ret;
        }

        static public DatapromFrame ConstroiFrameQNS(Byte[] end, Byte op, Byte[] dados) {
            /* Constroi Quadro Nao Seguro
             * end = 3bytes de endereco do quadro padrao da Dataprom
             * op = Opcode
             * dados = dados relacionados ao quadro que se deseja transmitir
             */
            DatapromFrame ret = null;
            if (end != null && (op >= OpcodesDP.MENSAGEM_INICIAL_GSM_80 && op <= OpcodesDP.TROCA_CHAVES_PUBLICA_B6) && op != OpcodesDP.TROCA_DADOS_SEGUROS_B5)
            // Verifica se endereco e opcode sao validos
            {
                ret = new DatapromFrame();                

                ret.endereco[0] = end[0];
                ret.endereco[1] = end[1];
                ret.endereco[2] = end[2];
                ret.op = op;
                ret.res = 0;
                
                if (dados != null)
                {
                    //Passa o conteudo dos dados para dentro do quadro
                    ret.dados = new Byte[dados.Length];
                    Buffer.BlockCopy(dados, 0, ret.dados, 0, dados.Length);                    
                }
                else
                {
                    ret.dados = null;
                }
                
            }
            return ret;
        }

        static public DatapromFrame ConstroiFrameQS(Byte[] end, Byte[] dados, ref UInt64 iterador, Byte[] ikm, ref Byte[] aeskey, int salt_id)
        { // Constroi Quadro Seguro

            DatapromFrame ret = null;

            if (end != null && dados != null && ikm != null)
            {
                ret = new DatapromFrame();
                ret.endereco[0] = end[0];
                ret.endereco[1] = end[1];
                ret.endereco[2] = end[2];
                ret.op = OpcodesDP.TROCA_DADOS_SEGUROS_B5;
                iterador += 1;
                ret.iterador = iterador;
                ret.res = 0;
                

                Byte[] contadorMensagensBytes = BitConverter.GetBytes(ret.iterador); //Transforma contadorMensagens em bytes
                Byte[] contadorSessoesBytes = BitConverter.GetBytes(ret.iterador / MSG_BY_SESSION); //Transforma contadorMensagens em bytes

                Byte[] infoIV = new Byte[info.Length];
                Byte[] infoKey = new Byte[info.Length];
                Byte[] saltWithId = new Byte[salt.Length];

                Buffer.BlockCopy(info, 0, infoIV, 0, info.Length); // LABEL INFO PARA IVS
                Buffer.BlockCopy(info, 0, infoKey, 0, info.Length); // LABEL INFO PARA KEY
                Buffer.BlockCopy(salt, 0, saltWithId, 0, salt.Length); // LABEL PARA SALT 

                Buffer.BlockCopy(contadorMensagensBytes, 0, infoIV, infoIV.Length - CONTADOR_LEN, CONTADOR_LEN); // INFO PARA IVS
                Buffer.BlockCopy(contadorSessoesBytes, 0, infoKey, infoKey.Length - CONTADOR_LEN, CONTADOR_LEN); // INFO PARA KEYS

                if (salt_id == 1)
                {
                    Byte[] auxSaltId = new byte[3];
                    auxSaltId[0] = ObtemAreaControladorDoVetor(end);
                    auxSaltId[1] = ObtemRedeControladorDoVetor(end);
                    auxSaltId[2] = ObtemCodigoControladorDoVetor(end);
                    Buffer.BlockCopy(auxSaltId, 0, saltWithId, 8, auxSaltId.Length);
                }

                aeskey = new byte[AES_KEY_LEN]; //Chave para AES
                byte[] iv = new byte[12]; //Tabela IV                

                var hkdfKey = new HkdfBytesGenerator(new Sha256Digest()); //Cria um gerador HKDF para chave
                hkdfKey.Init(new HkdfParameters(ikm, saltWithId, infoKey));
                hkdfKey.GenerateBytes(aeskey, 0, aeskey.Length); // Gera chave AES128

                var hkdfIV = new HkdfBytesGenerator(new Sha256Digest()); //Cria um novo gerador HKDF pra IV
                hkdfIV.Init(new HkdfParameters(ikm, saltWithId, infoIV));
                hkdfIV.GenerateBytes(iv, 0, iv.Length); // Gera IV

                // Inicializa o AES-GCM
                var gcm = new GcmBlockCipher(new AesEngine());
                var aeadParams = new AeadParameters(new KeyParameter(aeskey), GCM_TAG_LEN * 8, iv);
                gcm.Init(true, aeadParams); // // true = encriptação

                byte[] cifra = new byte[gcm.GetOutputSize(dados.Length)];
                int lenEnc = gcm.ProcessBytes(dados, 0, dados.Length, cifra, 0);
                lenEnc += gcm.DoFinal(cifra, lenEnc); // cifra contém dados(N) + tag(16)

                ret.dados = new Byte[cifra.Length - GCM_TAG_LEN];
                ret.dados = cifra.Take(cifra.Length - GCM_TAG_LEN).ToArray(); //Salva dados criptografados
                ret.tag = cifra.Skip(cifra.Length - GCM_TAG_LEN).Take(GCM_TAG_LEN).ToArray(); //Salva Tag                 
            }

            //Envia um dado criptografado
            return ret;
        }

        //void DestrutorFrames(DP_Frame_t** frame);
        static public Byte[] VetorizaQuadro(DatapromFrame frame)
        {
            Byte[] output = null;
            int i = 0;

            if (frame != null)
            {
                if (frame.op == OpcodesDP.TROCA_DADOS_SEGUROS_B5) {

                    //Codificar campos para B64
                    Byte[] dadosB64 = Base64Code.EncodeToBase64Bytes(frame.dados); //dados ja estao criptografados
                    Byte[] iteradorB64 = Base64Code.EncodeToBase64Bytes(BitConverter.GetBytes(frame.iterador));
                    Byte[] tagB64 = Base64Code.EncodeToBase64Bytes(frame.tag);
                    output = new Byte[dadosB64.Length + LEN_MINIMAL_QS];

                    // Atualiza vetor de saida com campos nao condificados em B64
                    output[0] = OpcodesDP.STX;
                    output[END_OFFSET] = (Byte)(frame.endereco[0] | 0x80);
                    output[END_OFFSET + 1] = (Byte)(frame.endereco[1] | 0x80);
                    output[END_OFFSET + 2] = (Byte)(frame.endereco[2] | 0x80);
                    output[OP_OFFSET] = frame.op;
                    output[RES_OFFSET] = (Byte)(frame.res | 0x080);
                    output[output.Length-1] = OpcodesDP.ETX;

                    //Copia dados codificados em B64
                    Buffer.BlockCopy(iteradorB64,   0, output, B64_ITERADOR_OFFSET  , iteradorB64.Length);
                    Buffer.BlockCopy(tagB64,        0, output, B64_TAG_OFFSET       , tagB64.Length);
                    Buffer.BlockCopy(dadosB64,      0, output, B64_DATA_OFFSET      , dadosB64.Length);

                    //Atualizado CKS
                    output[output.Length - 2] = GeraChecksum(output, 1, (UInt16)(output.Length - 3));


                }
                else if (frame.op != 0) {
                    // OPCODEs de quadro nao seguro (QNS)
                    Byte cks = 0;

                    output = (frame.dados == null) ? new Byte[LEN_MINIMAL_QNS] : new Byte[frame.dados.Length + LEN_MINIMAL_QNS];                    

			        //Insere marcadores de quadro
			        output[i++] = OpcodesDP.STX;
			        output[output.Length - 1] = OpcodesDP.ETX;

			        //Insere endereco setando o primeiro bit
			        output[i] = (Byte)(frame.endereco[0] | 0x80);
                    cks ^= output[i++];
			        output[i] = (Byte)(frame.endereco[1] | 0x80);
                    cks ^= output[i++];
		            output[i] = (Byte)(frame.endereco[2] | 0x80);
                    cks ^= output[i++];

			        output[i] = frame.op;
                    cks ^= output[i++];

                    if (frame.dados != null)
                    {
                        //Existem dados a serem transmitidos
                        int j = 0; //Indice do vetor de dados
                        while (j < frame.dados.Length)
                        {
				            output[i + j] = (Byte)(frame.dados[j] | 0x80);
                            cks ^= output[i+j];
                            j++;
                        }
                        i += j; //Adiciona o deslocamento dos dados
                    }

			        output[i++] = (Byte)((~cks) | 0x80);
                    
                }
                else
                {

                }
            }

            return output;
        }

    }
}

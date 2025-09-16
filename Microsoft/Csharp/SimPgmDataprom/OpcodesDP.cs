using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimPgmDataprom
{
    public class OpcodesDP
    {
        //Endereços Especiais
        public const Byte END_DUMMY = 63;

        //Marcadores de quadro
        public const Byte STX = 0x02;
        public const Byte ETX = 0x03;

        //OPCodes
        public const Byte MENSAGEM_INICIAL_GSM_80 = 0x80;
        public const Byte SOLICITA_DATA_E_HORA_86 = 0x86;
        public const Byte ENVIA_IDENTIFICACAO_8D = 0x8D;
        public const Byte TROCA_DADOS_SEGUROS_B5 = 0xB5;
        public const Byte TROCA_CHAVES_PUBLICA_B6 = 0xB6;

    }
}

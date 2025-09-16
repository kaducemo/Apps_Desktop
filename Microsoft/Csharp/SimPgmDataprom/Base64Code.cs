using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimPgmDataprom
{
    public class Base64Code
    {
        public static byte[] EncodeToBase64Bytes(byte[] input)
        {
            string base64 = Convert.ToBase64String(input);
            return System.Text.Encoding.ASCII.GetBytes(base64);
        }

        public static byte[] DecodeFromBase64Bytes(byte[] base64Bytes)
        {
            string base64 = System.Text.Encoding.ASCII.GetString(base64Bytes);
            byte[] decoded = Convert.FromBase64String(base64);
            return decoded;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DeviceSM1.Models
{
    public class EncodeString
    {
        public static string MD5HashCryptography(string string2MD5)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            byte[] dataMd5 = md5.ComputeHash(Encoding.Default.GetBytes(string2MD5));
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dataMd5.Length; i++)
            {
                sb.AppendFormat("{0x:x2}", dataMd5[i]);
            }

            string2MD5 = sb.ToString();

            return string2MD5;

        }

        public static string EncodeTo64(string text)
        {
            byte[] toEncode = Encoding.Unicode.GetBytes(text);
            string returnEncode = Convert.ToBase64String(toEncode);

            return returnEncode;
        }

        public static string DecodeFrom64(string text)
        {
            byte[] toDecode = Convert.FromBase64String(text);
            string returnDecode = Encoding.Unicode.GetString(toDecode);
            return returnDecode;
        }
    }
}

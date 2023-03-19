using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Personnel
{
    public class Methods
    {
        public static string StringToHash(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] b = Encoding.UTF8.GetBytes(str);
            byte[] hash = md5.ComputeHash(b);
            StringBuilder sb = new StringBuilder();
            foreach (var a in hash) sb.Append(a.ToString("X2"));
            return sb.ToString();
        }
        public static string EncodeDecrypt(string str)
        {
            var ch = str.ToArray();
            string newStr = "";
            foreach (var c in ch) newStr += TopSecret(c);
            return newStr;
        }
        public static char TopSecret(char character)
        {
            ushort secretKey = 0x0088;
            character = (char)(character ^ secretKey);
            return character;
        }
    }
}

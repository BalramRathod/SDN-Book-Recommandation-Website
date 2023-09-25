using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Domain.HelperModel
{
    public class EncryptionHelper
    {
        public static string key = "@5599Balram";

        public static string Encrypt(string String)
        {

            if (String == null)
            {
                return "";
            }
            else
            {
                String = String + key;
                var StringBytes = System.Text.Encoding.UTF8.GetBytes(String);
                return Convert.ToBase64String(StringBytes);
            }

        }

        public static string Decrypt(string String)
        {
            if (String == null)
            {
                return "";
            }
            else
            {
                var StringBytes = Convert.FromBase64String(String);
                var actualString = Encoding.UTF8.GetString(StringBytes);
                actualString = actualString.Substring(0, actualString.Length - key.Length);
                return actualString;
            }
        }
    }
}

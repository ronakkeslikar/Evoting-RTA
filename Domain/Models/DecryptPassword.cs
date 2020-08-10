using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Domain.Models
{
    public class DecryptPassword
    {

        public static string Decrypt_Password(string encrypt_Password)
        {

            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encrypt_Password);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}

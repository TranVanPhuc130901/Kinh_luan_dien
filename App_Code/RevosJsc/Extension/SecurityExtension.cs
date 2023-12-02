using System;
using System.Security.Cryptography;
using System.Text;

namespace RevosJsc.Extension
{
    /// <summary>
    /// Thực hiện các công việc liên quan đến vấn đề bảo mật
    /// </summary>
    public class SecurityExtension
    {
        /// <summary>
        /// Trả về chuỗi đã được mã hoá MD5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string BuildPassword(string input)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes("revos" + input + "171006"));
            var sBuilder = new StringBuilder();
            foreach (var i in data)
            {
                sBuilder.Append(i.ToString("x2"));
            }
            return sBuilder.ToString();
        }
        /// <summary>
        /// Mã hoá chuỗi dạng đơn giản. Để giải mã cần dùng phương thức Decode
        /// </summary>
        /// <param name="value">Chuỗi cần mã hóa</param>
        /// <returns></returns>
        public static string Encode(string value)
        {
            var str1 = "";
            var random = new Random();
            foreach (var t in value)
            {
                var str2 = str1;
                var ch = (char)(t + 1U);
                var str3 = ch.ToString();
                ch = (char)random.Next(97, 122);
                var str4 = ch.ToString();
                str1 = str2 + str3 + str4;
            }
            return str1;
        }

        /// <summary>Giải mã chuỗi được mã hoá bởi phương thức Encode</summary>
        /// <param name="value">Chuỗi cần giải mã</param>
        /// <returns></returns>
        public static string Decode(string value)
        {
            var str = "";
            for (var index = 0; index < value.Length; index += 2)
                str += ((char)(value[index] - 1U)).ToString();
            return str;
        }
    }
}
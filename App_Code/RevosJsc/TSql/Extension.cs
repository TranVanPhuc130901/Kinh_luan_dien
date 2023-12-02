using System;
using System.Text.RegularExpressions;

namespace RevosJsc.TSql
{
    public class Extension
    {
        /// <summary>
        /// Loại bỏ các ký tự có thể gây ra lỗi SQL Injection
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string RemoveSqlInjectionChars(string inputString)
        {
            //Liệt kê danh sách các ký tự bị cấm
            string[] notAllowChars = { "'", "--", "create", "insert", "update", "delete", "drop", "truncate", "alter", "execute", "exec", "backup", "restore" };

            //Loại bỏ các ký tự bị cấm
            foreach (var chars in notAllowChars)
            {
                while (inputString.ToLower().IndexOf(chars.ToLower(), StringComparison.Ordinal) > -1) inputString = Regex.Replace(inputString, chars, "", RegexOptions.IgnoreCase);
            }

            return inputString;
        }
    }
}

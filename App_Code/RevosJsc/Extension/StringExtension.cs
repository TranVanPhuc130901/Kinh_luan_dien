using System;
using System.Text.RegularExpressions;

namespace RevosJsc.Extension
{
    public class StringExtension
    {
        public class SpecialCharactersKeyword
        {
            public const string ParamsSpilitItems = "*!<=*_*=>*!";
            public const string ParamsSpilitRole = ",";
        }

        /// <summary>
        /// Cắt lấy một số ký tự đầu trong chuỗi
        /// </summary>
        /// <param name="numberChar">Số ký tự cần lấy</param>
        /// <param name="sourceString">Chuỗi nguồn</param>
        /// <param name="addSomeChar">true: thêm dấu ... vào kết quả nếu chuỗi được cắt</param>
        /// <returns></returns>
        public static string GetCharInDesc(int numberChar, string sourceString, bool addSomeChar)
        {
            var s = "";
            if (sourceString.Length > numberChar)
            {
                s += sourceString.Substring(0, numberChar);
                if (addSomeChar) s += "...";
            }
            else s = sourceString;
            return s;
        }

        /// <summary>
        /// Định dạng menu theo level. VD: level 1 -> kết quả ..., level 2 -> kết quả ......
        /// </summary>
        /// <param name="iglevel">level, điền số tự nhiên</param>
        /// <returns></returns>
        public static string FormatLinkMenu(string iglevel)
        {
            var s = "";
            int level = Convert.ToInt16(iglevel);
            for (var i = 0; i < level; i++) s += "...";
            return s;
        }

        /// <summary>
        /// Ghép các chuỗi lại, kết quả trả về dạng chuoiPhanCach + chuoi 1 + chuoiPhanCach +...+ chuoi n + chuoiPhanCach. Chú ý: nếu chuỗi cần ghép là chuỗi rỗng thì nó vẫn được ghép.
        /// </summary>
        /// <param name="chuoiPhanCach">Chuỗi dùng đề phân cách các chuỗi được ghép. Truyền vào chuỗi rỗng nếu muốn sử dụng Keyword.Keyword.ParamsSpilitItems làm chuỗi phân cách</param>
        /// <param name="cacChuoi">Danh sách các chuỗi được ghép, truyền vào theo kiểu mảng string hoặc từng biến kiểu string cách nhau bởi dấu ,</param>
        /// <returns></returns>
        public static string GhepChuoi(string chuoiPhanCach, params string[] cacChuoi)
        {
            if (chuoiPhanCach.Length < 1) chuoiPhanCach = SpecialCharactersKeyword.ParamsSpilitItems;
            var s = chuoiPhanCach;
            foreach (var chuoi in cacChuoi) s += chuoi + chuoiPhanCach;
            return s;
        }

        /// <summary>
        /// Trả về chuỗi được chia theo chuỗi phân cách. VD: -:-Chuoi1-:-Chuoi2-:- , vị trí 1 -> Chuoi 1
        /// </summary>
        /// <param name="chuoiNguon">Chuỗi chứa dữ liệu cần lấy</param>
        /// <param name="chuoiPhanCach">Chuỗi để chia. Truyền vào chuỗi rỗng nếu muốn sử dụng Keyword.Dominet.Website.Keyword.SpecialCharacter.ParamsSpilitItems làm chuỗi để chia</param>
        /// <param name="viTriCanLay">Vị trí cần lấy</param>
        /// <returns></returns>
        public static string LayChuoi(string chuoiNguon, string chuoiPhanCach, int viTriCanLay)
        {
            if (chuoiPhanCach.Length < 1) chuoiPhanCach = SpecialCharactersKeyword.ParamsSpilitItems;
            var s = "";
            var strTemp = chuoiNguon.Split(new[] { chuoiPhanCach }, StringSplitOptions.None);
            if (strTemp.Length > viTriCanLay) s = strTemp[viTriCanLay];
            return s;
        }

        /// <summary>
        /// Kiểm tra xem quyền có thuộc danh sách quyền hay không, vd quyền 1, ds quyền ,1,2,3, -> trả về true
        /// </summary>
        /// <param name="role">quyền (vd: 1)</param>
        /// <param name="listRoles">danh sách quyền (vd: ,1,2,3,)</param>
        /// <returns></returns>
        public static bool RoleInListRoles(string role, string listRoles)
        {
            return listRoles.IndexOf(SpecialCharactersKeyword.ParamsSpilitRole + role + SpecialCharactersKeyword.ParamsSpilitRole, StringComparison.Ordinal) > -1;
        }

        /// <summary>
        /// Loại bỏ các ký tự có thể gây ra lỗi SQL Injection
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string RemoveSqlInjectionChars(string inputString)
        {
            //Liệt kê danh sách các ký tự bị cấm
            string[] notAllowChars = {"'", "--", "create", "insert", "update", "delete", "drop", "truncate", "alter", "execute", "exec", "backup", "restore"};

            //Loại bỏ các ký tự bị cấm
            foreach (var chars in notAllowChars)
            {
                while (inputString.ToLower().IndexOf(chars.ToLower(), StringComparison.Ordinal) > -1) inputString = Regex.Replace(inputString, chars, "", RegexOptions.IgnoreCase);
            }

            return inputString;
        }
        /// <summary>
        /// Loại bỏ các ký tự khỏi số điện thoại
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string RemoveCharsInPhoneNumber(string inputString)
        {
            //Liệt kê danh sách các ký tự bị cấm
            string[] notAllowChars = { "-", "(", ")", " ", ".", "[", "]" };

            //Loại bỏ các ký tự bị cấm
            foreach (var chars in notAllowChars)
            {
                while (inputString.ToLower().IndexOf(chars.ToLower(), StringComparison.Ordinal) > -1) inputString = inputString.Replace(chars, "");
            }

            return inputString;
        }

        /// <summary>
        /// Thay thế chuỗi, chuỗi trả về là chuỗi tiếng Việt không dấu chỉ chứa các ký tự A-Z,a-z các số 0-9, dấu _ và dấu -
        /// </summary>
        /// <param name="text">Chuỗi cần thay thế</param>
        /// <returns></returns>
        public static string ReplateTitle(string text)
        {
            text = text.Trim();//Xoá các khoảng trắng ở hai đầu

            //Thay thế các kí tự thường có dấu bởi các kí tự thường không dấu
            text = Regex.Replace(text, "(ä|à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ)", "a");
            text = Regex.Replace(text, "ç", "c");
            text = Regex.Replace(text, "(è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ)", "e");
            text = Regex.Replace(text, "(ì|í|î|ị|ỉ|ĩ)", "i");
            text = Regex.Replace(text, "(ö|ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ)", "o");
            text = Regex.Replace(text, "(ü|ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ)", "u");
            text = Regex.Replace(text, "(ỳ|ý|ỵ|ỷ|ỹ)", "y");
            text = Regex.Replace(text, "(đ)", "d");

            //Thay thế các kí tự viết hoa có dấu bởi các kí tự viết hoa không dấu
            text = Regex.Replace(text, "(Ä|À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ)", "A");
            text = Regex.Replace(text, "Ç", "C");
            text = Regex.Replace(text, "(È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ)", "E");
            text = Regex.Replace(text, "(Ì|Í|Ị|Ỉ|Ĩ)", "I");
            text = Regex.Replace(text, "(Ö|Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ)", "O");
            text = Regex.Replace(text, "(Ü|Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ)", "U");
            text = Regex.Replace(text, "(Ỳ|Ý|Ỵ|Ỷ|Ỹ)", "Y");
            text = Regex.Replace(text, "(Đ)", "D");

            //Thay thế các khoảng trắng bởi một dấu -
            text = Regex.Replace(text, "[ \t\r\n\v\f]", "-");

            //Thay một hoặc nhiều kí tự trống bới một dấu -
            text = Regex.Replace(text, "( )+", "-");

            //Chỉ giữ lại các chữ cái từ A-Z, từ a-z, các số từ 0-9, dấu _ và dấu -
            text = Regex.Replace(text, "[^A-Za-z0-9_-]", "-");

            //Thay một hoặc nhiều dấu _ bởi một đấu _
            text = Regex.Replace(text, "(_)+", "_");

            //Thay một hoặc nhiều dấu - bởi một dấu -
            text = Regex.Replace(text, "(-)+", "-");

            //Xoá những ký tự _ đầu và cuối
            text = text.Trim('_');

            //Xoá những ký tự - đầu và cuối
            text = text.Trim('-');

            return text.ToLower();
        }
    }
}
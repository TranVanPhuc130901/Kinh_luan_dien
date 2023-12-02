using System;
using System.Web;
using RevosJsc.Extension;

namespace Developer.Extension
{
    /// <summary>
    /// Hỗ trợ các thao tác liên quan đến menu
    /// </summary>
    public class MenuExtension
    {
        /// <summary>
        /// Trả về " target='_blank' " hoặc ""
        /// </summary>
        /// <param name="vgparrams"></param>
        /// <returns></returns>
        public static string GetTarget(string vgparrams)
        {
            return StringExtension.LayChuoi(vgparrams, StringExtension.SpecialCharactersKeyword.ParamsSpilitItems, 2) == "1" ? " target='_blank' " : "";
        }

        public static string GetIgidInVgdesc(string vgdesc)
        {
            var igidParrent = "";
            var index1 = vgdesc.IndexOf("igid=", StringComparison.Ordinal);
            if (index1 > -1)
            {
                index1 += "igid=".Length;
                var index2 = vgdesc.IndexOf("&", index1, StringComparison.Ordinal);
                igidParrent = index2 > -1 ? vgdesc.Substring(index1, index2 - index1) : vgdesc.Substring(index1);
            }
            if (igidParrent.Length < 1) igidParrent = "0";
            return igidParrent.Replace("#", "");
        }

        /// <summary>
        /// Trích thông tin ra theo kiểu QueryString
        /// </summary>
        /// <param name="info">Chuỗi chứa các QueryString</param>
        /// <param name="name">Tên QueryString cần lấy</param>
        /// <returns></returns>
        public static string GetQueryString(string info, string name)
        {
            var s = "";

            #region Trích thông tin ra theo kiểu QueryString

            //Lấy tất cả parram được post lên từ máy khách
            var myUrl = info;
            myUrl = HttpUtility.HtmlDecode(myUrl);
            //Chuyển về kiểu QueryString
            var values = HttpUtility.ParseQueryString(myUrl);
            //Lấy ra giá trị theo tên QueryString
            //var t = values["ExpiryDate"];
            var temp = "";
            if (values[name] == null) return s;
            temp = values[name];
            s = temp;

            #endregion

            return s;
        }


        public static string GetUrl()
        {
            var s = "";
            s = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.RawUrl).ToLower();
            if (s.EndsWith("default.aspx?")) s = s.Remove(s.Length - "default.aspx?".Length);
            if (s.EndsWith("default.aspx")) s = s.Remove(s.Length - "default.aspx".Length);
            return s;
        }

    }
}
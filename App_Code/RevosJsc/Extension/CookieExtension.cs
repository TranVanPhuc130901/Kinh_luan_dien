using System;
using System.Linq;
using System.Web;

namespace RevosJsc.Extension
{
    /// <summary>
    /// Thực thiện các công việc như lưu, lấy cookies...
    /// </summary>
    public class CookieExtension
    {
        /// <summary>
        /// Lưu tên trường đang được chọn để sắp xếp
        /// </summary>
        public const string SortField = "field";

        /// <summary>
        /// Lưu tên cách sắp xếp (asc hay desc)
        /// </summary>
        public const string SortMode = "mode";

        #region Cookies sắp xếp trong admin

        /// <summary>
        /// Lưu cookies trường sắp xếp dữ liệu trong admin. Trả về chuỗi sắp xếp vừa được lưu
        /// </summary>
        /// <param name="field">Tên trường(vd: Dominet.Columns.GroupsColumns.VgnameColumn)</param>
        /// <param name="sortCookiesName">Tên cookies(vd: CookieKeyword.SortNewCategory)</param>
        public static string SetCookiesSort(string field, string sortCookiesName)
        {
            var mode = "";
            HttpContext.Current.Response.Cookies[sortCookiesName][SortField] = field;
            switch (HttpContext.Current.Request.Cookies[sortCookiesName][SortMode])
            {
                case null:
                    HttpContext.Current.Response.Cookies[sortCookiesName][SortMode] = " ASC";
                    mode = " ASC";
                    break;
                case " ASC":
                    HttpContext.Current.Response.Cookies[sortCookiesName][SortMode] = " DESC";
                    mode = " DESC";
                    break;
                default:
                    HttpContext.Current.Response.Cookies[sortCookiesName][SortMode] = " ASC";
                    mode = " ASC";
                    break;
            }
            HttpContext.Current.Response.Cookies[sortCookiesName].Expires = DateTime.MaxValue;
            return field + mode;
        }

        /// <summary>
        /// Lấy điều kiện sắp xếp trong admin từ coookies (vd: một kết quả trả về có thể là "vgName asc")
        /// </summary>
        /// <param name="sortCookiesName">Tên cookies(vd: CookieKeyword.SortNewCategory)</param>
        /// <returns></returns>
        public static string GetCookiesSort(string sortCookiesName)
        {
            var s = "";
            if (HttpContext.Current.Request.Cookies[sortCookiesName] == null) return s;
            if (HttpContext.Current.Request.Cookies[sortCookiesName][SortField] == null || HttpContext.Current.Request.Cookies[sortCookiesName][SortMode] == null) return s;
            var orderField = HttpContext.Current.Request.Cookies[sortCookiesName][SortField];
            var orderMode = HttpContext.Current.Request.Cookies[sortCookiesName][SortMode];
            s = orderField + " " + orderMode;
            return s;
        }

        #endregion Cookies sắp xếp trong admin

        #region Xử lý cookies theo kiểu có mã hoá - cookies đăng nhập

        /// <summary>
        /// Lưu một cookies, có mã hoá giá trị. Để kiểm tra cookies này có hợp lệ hay không cần dùng phương thức CheckValidCookies.
        /// Để lấy giá trị cookies đã được lưu bởi phương thức này cần dùng phương thức GetCookies
        /// </summary>
        /// <param name="cookiesName">Tên cookies cần lưu</param>
        /// <param name="cookiesValue">Giá trị cần lưu</param>
        public static void SaveCookies(string cookiesName, string cookiesValue)
        {
            #region Get Computer Name

            //Get Computer Name
            var strClientName = System.Net.Dns.GetHostName();

            #endregion

            #region Get domain

            var domain = HttpContext.Current.Request.Url.Host.ToLower();

            #endregion

            #region Time

            var timeValue = DateTime.Now.Ticks.ToString();
            var timeCookie = new HttpCookie("1103" + cookiesName) {Value = timeValue};
            HttpContext.Current.Response.Cookies.Add(timeCookie);

            #endregion

            #region Mã hoá và lưu giá trị cookies

            //Mã hóa giá trị
            var valuesCheck = SecurityExtension.BuildPassword(strClientName + domain + timeValue);
            var valueCookies = new HttpCookie(cookiesName) {Value = "a" + valuesCheck + EncodeSimple(cookiesValue) + "z"};
            //Gán chữ cái vào đầu để lưu được trên Safari
            HttpContext.Current.Response.Cookies.Add(valueCookies);

            #endregion
        }

        /// <summary>
        /// Xoá cookies được lưu bởi phương thức SaveCookies
        /// </summary>
        /// <param name="cookiesName">Tên cookies</param>
        public static void ClearCookies(string cookiesName)
        {
            if (HttpContext.Current.Request.Cookies["1103" + cookiesName] != null)
            {
                #region Xoá cookies Time

                var timeCookie = new HttpCookie("1103" + cookiesName) {Expires = DateTime.Now.AddDays(-1)};
                HttpContext.Current.Response.Cookies.Add(timeCookie);

                #endregion
            }
            if (HttpContext.Current.Request.Cookies[cookiesName] == null) return;

            #region Xoá cookies chính

            //Mã hóa giá trị
            var valueCookies = new HttpCookie(cookiesName) {Expires = DateTime.Now.AddDays(-1)};
            HttpContext.Current.Response.Cookies.Add(valueCookies);

            #endregion
        }

        /// <summary>
        /// Kiểm tra xem cookies được tạo bởi phương thức SaveCookies có hợp lệ hay không.
        /// </summary>
        /// <param name="cookiesName">Tên cookies cần kiểm tra</param>
        /// <returns></returns>
        public static bool CheckValidCookies(string cookiesName)
        {
            var kq = false;

            #region Get Computer Name

            //Get Computer Name
            var strClientName = "";
            strClientName = System.Net.Dns.GetHostName();

            #endregion Get Computer Name

            #region Get domain

            var domain = HttpContext.Current.Request.Url.Host.ToLower();

            #endregion

            var valueSaved = "";
            if (HttpContext.Current.Request.Cookies[cookiesName] != null) valueSaved = HttpContext.Current.Request.Cookies[cookiesName].Value;
            var timeSaved = "";
            if (HttpContext.Current.Request.Cookies["1103" + cookiesName] != null) timeSaved = HttpContext.Current.Request.Cookies["1103" + cookiesName].Value;
            var newValuew = "a" + SecurityExtension.BuildPassword(strClientName + domain + timeSaved);

            if (valueSaved.IndexOf(newValuew, StringComparison.Ordinal) > -1) kq = true;
            return kq;
        }

        /// <summary>
        /// Lấy giá trị cookies được lưu bởi phương thức SaveCookies
        /// </summary>
        /// <param name="cookiesName">Tên cookies</param>
        /// <returns></returns>
        public static string GetCookies(string cookiesName)
        {
            #region Get Computer Name

            //Get Computer Name
            var strClientName = "";
            strClientName = System.Net.Dns.GetHostName();

            #endregion Get Computer Name

            #region Get domain

            var domain = HttpContext.Current.Request.Url.Host.ToLower();

            #endregion

            var valueSaved = "";
            if (HttpContext.Current.Request.Cookies[cookiesName] != null) valueSaved = HttpContext.Current.Request.Cookies[cookiesName].Value;
            var timeSaved = "";
            if (HttpContext.Current.Request.Cookies["1103" + cookiesName] != null) timeSaved = HttpContext.Current.Request.Cookies["1103" + cookiesName].Value;

            var newValuew = "a" + SecurityExtension.BuildPassword(strClientName + domain + timeSaved);

            var encodeValue = valueSaved.Replace(newValuew, "");
            if (encodeValue.Length > 0) encodeValue = encodeValue.Remove(encodeValue.Length - 1);
            encodeValue = DecodeSimple(encodeValue);

            return encodeValue;
        }

        #region Mã hoá và giải mã cookies

        /// <summary>
        /// Mã hoá chuỗi dạng đơn giản. Để giải mã cần dùng phương thức DecodeSimple
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EncodeSimple(string value)
        {
            var rd = new Random();
            return value.Aggregate("", (current, c) => current + ((char) (c + 1) + ((char) rd.Next('a', 'z')).ToString()));
        }

        /// <summary>
        /// Giải mã chuỗi được mã hoá bởi phương thức EncodeSimple
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecodeSimple(string value)
        {
            var s = "";
            for (var i = 0; i < value.Length; i += 2)
            {
                s += ((char)(value[i] - 1)).ToString();
            }
            return s;
        }

        #endregion Mã hoá và giải mã cookies

        #endregion Xử lý cookies theo kiểu có mã hoá - cookies đăng nhập
    }
}
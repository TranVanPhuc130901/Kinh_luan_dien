using System.Web;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

namespace RevosJsc.LanguageControl
{
    public class Cookie
    {
        public static string LanguageIdAdmin = "fc4649d44a90703e1a071ea7bad0089f";
        public static string LanguageIdDisplay = "2468c3f921527fa4a6729f14dbc05bdd";

        public static string GetLanguageValueAdmin()
        {
            if (HttpContext.Current.Request.Cookies[LanguageIdAdmin] != null) return StringExtension.RemoveSqlInjectionChars(HttpContext.Current.Request.Cookies[LanguageIdAdmin].Value);
            var dt = LanguageNational.GetData("1", LanguageNationalColumns.IlnId,  LanguageNationalTSql.GetByStatus("1"), LanguageNationalColumns.IlnSortOrder);
            if (dt.Rows.Count < 1) HttpContext.Current.Response.Redirect("/admin/wizard");
            return dt.Rows.Count > 0 ? dt.Rows[0][LanguageNationalColumns.IlnId].ToString() : StringExtension.RemoveSqlInjectionChars(HttpContext.Current.Request.Cookies[LanguageIdAdmin].Value);
        }

        public static string GetLanguageValueDisplay()
        {
            if (HttpContext.Current.Request.Cookies[LanguageIdDisplay] != null) return StringExtension.RemoveSqlInjectionChars(HttpContext.Current.Request.Cookies[LanguageIdDisplay].Value);
            var dt = LanguageNational.GetData("1", LanguageNationalColumns.IlnId, LanguageNationalTSql.GetByStatus("1"), LanguageNationalColumns.IlnSortOrder);
            if (dt.Rows.Count < 1) HttpContext.Current.Response.Redirect("/admin/wizard");
            return dt.Rows.Count > 0 ? dt.Rows[0][LanguageNationalColumns.IlnId].ToString() : StringExtension.RemoveSqlInjectionChars(HttpContext.Current.Request.Cookies[LanguageIdDisplay].Value);
        }
        /// <summary>
        /// Lưu id của language hiện tại vào cookie - Cho trang admin
        /// </summary>
        /// <param name="languageId">id của ngôn ngữ hiện tại</param>
        public static void SetLanguageValueAdmin(string languageId)
        {
            if (languageId == "") return;
            var languageIdAdmin = new HttpCookie(LanguageIdAdmin) { Value = languageId };
            HttpContext.Current.Response.Cookies.Add(languageIdAdmin);
        }
        /// <summary>
        /// Lưu id của language hiện tại vào cookie - Cho trang hiển thị
        /// </summary>
        /// <param name="languageId">id của ngôn ngữ hiện tại</param>
        public static void SetLanguageValueDisplay(string languageId)
        {
            if (languageId == "") return;
            var languageIdDisplay = new HttpCookie(LanguageIdDisplay) { Value = languageId };
            HttpContext.Current.Response.Cookies.Add(languageIdDisplay);
        }
    }
}

using System.Web;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.LanguageControl;
using RevosJsc.TSql;

namespace RevosJsc
{
    public class LanguageExtension
    {
        /// <summary>
        /// Dịch từ khóa theo ngôn ngữ hiển thị.
        /// </summary>
        /// <param name="keyword">Từ khóa - tối đa 128 ký tự</param>
        /// <returns></returns>
        public static string TranslateKeyword(string keyword)
        {
            var languageValueDisplay = Cookie.GetLanguageValueDisplay();
            var flag = HttpContext.Current.Cache["Translate-" + languageValueDisplay + "-" + keyword] == null;
            if (!flag) return HttpContext.Current.Cache["Translate-" + languageValueDisplay + "-" + keyword].ToString();
            var condition = DataExtension.AndConditon(
                "ikId IN (SELECT ikId FROM dbo.Keywords WHERE vkTitle = N'" + keyword.Replace("\'", "''") + "')", 
                TranslateTSql.GetByLang(languageValueDisplay)
            );
            var dt = Translate.GetData("1", TranslateColumns.VtValue, condition, "");
            return dt.Rows.Count > 0 ? dt.Rows[0][TranslateColumns.VtValue].ToString() : keyword;
        }
        /// <summary>
        /// Dịch từ khóa theo ngôn ngữ truyền vào.
        /// </summary>
        /// <param name="keyword">Từ khóa - tối đa 128 ký tự</param>
        /// <param name="languageValueDisplay">ID ngôn ngữ hiển thị</param>
        /// <returns></returns>
        public static string TranslateKeyword(string keyword, string languageValueDisplay)
        {
            var flag = HttpContext.Current.Cache["Translate-" + languageValueDisplay + "-" + keyword] == null;
            if (!flag) return HttpContext.Current.Cache["Translate-" + languageValueDisplay + "-" + keyword].ToString();
            var condition = DataExtension.AndConditon(
                "ikId IN (SELECT ikId FROM dbo.Keywords WHERE vkTitle=N'" + keyword.Replace("\'", "''") + "')",
                TranslateTSql.GetByLang(languageValueDisplay)
            );
            var dt = Translate.GetData("1", TranslateColumns.VtValue, condition, "");
            return dt.Rows.Count > 0 ? dt.Rows[0][TranslateColumns.VtValue].ToString() : keyword;
        }
    }
}

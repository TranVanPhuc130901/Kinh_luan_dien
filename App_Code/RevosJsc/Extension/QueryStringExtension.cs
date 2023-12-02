using System.Web;

namespace RevosJsc.Extension
{
    public class QueryStringExtension
    {
        public static string GetQueryString(string name)
        {
            return HttpContext.Current.Request[name] == null ? "" : StringExtension.RemoveSqlInjectionChars(HttpContext.Current.Request[name]);
        }
    }
}

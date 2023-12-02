
namespace RevosJsc.TSql
{
    public class SettingsTSql
    {
        public static string GetBykey(string vsKey)
        {
            return " vsKey = N'" + Extension.RemoveSqlInjectionChars(vsKey) + "' ";
        }

        public static string GetByValue(string vsValue)
        {
            return " vsValue = N'" + Extension.RemoveSqlInjectionChars(vsValue) + "' ";
        }

        public static string GetByLang(string vsLang)
        {
            return " vsLang = N'" + Extension.RemoveSqlInjectionChars(vsLang) + "' ";
        }
    }
}

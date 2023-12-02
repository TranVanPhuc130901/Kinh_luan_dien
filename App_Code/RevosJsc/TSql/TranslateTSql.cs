namespace RevosJsc.TSql
{
    public class TranslateTSql
    {
        public static string GetById(string itId)
        {
            return " itId = N'" + Extension.RemoveSqlInjectionChars(itId) + "' ";
        }
        public static string GetByLang(string vtLang)
        {
            return " vtLang = N'" + Extension.RemoveSqlInjectionChars(vtLang) + "' ";
        }
        public static string GetByikId(string ikId)
        {
            return " ikId = N'" + Extension.RemoveSqlInjectionChars(ikId) + "' ";
        }
        public static string GetByValue(string vtValue)
        {
            return " vtValue = N'" + Extension.RemoveSqlInjectionChars(vtValue) + "' ";
        }
    }
}

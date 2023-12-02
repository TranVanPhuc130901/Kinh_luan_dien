namespace RevosJsc.TSql
{
    public class KeywordsTSql
    {
        public static string GetById(string ikId)
        {
            return " ikId = N'" + Extension.RemoveSqlInjectionChars(ikId) + "' ";
        }

        public static string GetByTitle(string vkTitle)
        {
            return " vkTitle = N'" + Extension.RemoveSqlInjectionChars(vkTitle) + "' ";
        }

        public static string GetByDescription(string vkDescription)
        {
            return " vkDescription = N'" + Extension.RemoveSqlInjectionChars(vkDescription) + "' ";
        }
    }
}

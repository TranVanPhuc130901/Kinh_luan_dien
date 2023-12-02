namespace RevosJsc.TSql
{
    public class LanguageNationalTSql
    {
        public static string GetById(string ilnId)
        {
            return " ilnId = N'" + Extension.RemoveSqlInjectionChars(ilnId) + "' ";
        }

        public static string GetByName(string vlnName)
        {
            return " vlnName = N'" + Extension.RemoveSqlInjectionChars(vlnName) + "' ";
        }

        public static string GetByFlag(string vlnFlag)
        {
            return " vlnFlag = N'" + Extension.RemoveSqlInjectionChars(vlnFlag) + "' ";
        }

        public static string GetBySortOrder(string ilnSortOrder)
        {
            return " ilnSortOrder = N'" + Extension.RemoveSqlInjectionChars(ilnSortOrder) + "' ";
        }

        public static string GetByStatus(string ilnStatus)
        {
            return " ilnStatus = N'" + Extension.RemoveSqlInjectionChars(ilnStatus) + "' ";
        }
    }
}

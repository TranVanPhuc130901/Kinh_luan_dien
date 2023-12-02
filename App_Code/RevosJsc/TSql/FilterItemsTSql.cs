namespace RevosJsc.TSql
{
    public class FilterItemsTSql
    {
        public static string GetById(string ifiId)
        {
            return " ifiId = N'" + Extension.RemoveSqlInjectionChars(ifiId) + "' ";
        }
        public static string GetByItemsId(string iiId)
        {
            return " iiId = N'" + Extension.RemoveSqlInjectionChars(iiId) + "' ";
        }

        public static string GetByParam(string vfiParam)
        {
            return " vfiParam = N'" + Extension.RemoveSqlInjectionChars(vfiParam) + "' ";
        }
    }
}

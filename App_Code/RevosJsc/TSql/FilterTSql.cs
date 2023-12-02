namespace RevosJsc.TSql
{
    public class FilterTSql
    {
        public static string GetById(string ifId)
        {
            return " ifId = N'" + Extension.RemoveSqlInjectionChars(ifId) + "' ";
        }

        public static string GetByLang(string vfLang)
        {
            return " vfLang = N'" + Extension.RemoveSqlInjectionChars(vfLang) + "' ";
        }

        public static string GetByLevel(string ifLevel)
        {
            return " ifLevel = N'" + Extension.RemoveSqlInjectionChars(ifLevel) + "' ";
        }

        public static string GetBySortOrder(string ifSortOrder)
        {
            return " ifSortOrder = N'" + Extension.RemoveSqlInjectionChars(ifSortOrder) + "' ";
        }

        public static string GetByParentId(string ifParentId)
        {
            return " ifParentId = N'" + Extension.RemoveSqlInjectionChars(ifParentId) + "' ";
        }

        public static string GetByGenealogy(string vfGenealogy)
        {
            return " vfGenealogy = N'" + Extension.RemoveSqlInjectionChars(vfGenealogy) + "' ";
        }

        public static string GetByName(string vfName)
        {
            return " vfName = N'" + Extension.RemoveSqlInjectionChars(vfName) + "' ";
        }

        public static string GetByDescription(string vfDescription)
        {
            return " vfDescription = N'" + Extension.RemoveSqlInjectionChars(vfDescription) + "' ";
        }

        public static string GetByImage(string vfImage)
        {
            return " vfImage = N'" + Extension.RemoveSqlInjectionChars(vfImage) + "' ";
        }
        public static string GetByParam(string vfParam)
        {
            return " vfParam = N'" + Extension.RemoveSqlInjectionChars(vfParam) + "' ";
        }

        public static string GetByDateCreated(string dfDateCreated)
        {
            return " dfDateCreated = N'" + Extension.RemoveSqlInjectionChars(dfDateCreated) + "' ";
        }

        public static string GetByDateModified(string dfDateModified)
        {
            return " dfDateModified = N'" + Extension.RemoveSqlInjectionChars(dfDateModified) + "' ";
        }

        public static string GetByType(string vfType)
        {
            return " vfType = N'" + Extension.RemoveSqlInjectionChars(vfType) + "' ";
        }
        public static string GetByStatus(string ifStatus)
        {
            return " ifStatus = N'" + Extension.RemoveSqlInjectionChars(ifStatus) + "' ";
        }
    }
}

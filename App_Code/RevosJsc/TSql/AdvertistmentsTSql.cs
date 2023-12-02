namespace RevosJsc.TSql
{
    public class AdvertistmentsTSql
    {
        public static string GetById(string iaId)
        {
            return " [Advertistments].iaId = N'" + Extension.RemoveSqlInjectionChars(iaId) + "' ";
        }
        public static string GetByLang(string vaLang)
        {
            return " iaLang = N'" + Extension.RemoveSqlInjectionChars(vaLang) + "' ";
        }
        public static string GetByPositionId(string iapId)
        {
            return " iapId = N'" + Extension.RemoveSqlInjectionChars(iapId) + "' ";
        }
        public static string GetByTitle(string vaTitle)
        {
            return " vaTitle = N'" + Extension.RemoveSqlInjectionChars(vaTitle) + "' ";
        }
        public static string GetByLink(string vaLink)
        {
            return " vaLink = N'" + Extension.RemoveSqlInjectionChars(vaLink) + "' ";
        }
        public static string GetByTarget(string iaTarget)
        {
            return " iaTarget = N'" + Extension.RemoveSqlInjectionChars(iaTarget) + "' ";
        }
        public static string GetByDescription(string vaDescription)
        {
            return " vaDescription = N'" + Extension.RemoveSqlInjectionChars(vaDescription) + "' ";
        }
        public static string GetByImage(string vaImage)
        {
            return " vaImage = N'" + Extension.RemoveSqlInjectionChars(vaImage) + "' ";
        }
        public static string GetByDateCreated(string daDateCreated)
        {
            return " daDateCreated = N'" + Extension.RemoveSqlInjectionChars(daDateCreated) + "' ";
        }
        public static string GetByDateModifield(string daDateModifield)
        {
            return " daDateModifield = N'" + Extension.RemoveSqlInjectionChars(daDateModifield) + "' ";
        }
        public static string GetByParam(string vaParam)
        {
            return " vaParam = N'" + Extension.RemoveSqlInjectionChars(vaParam) + "' ";
        }
        public static string GetBySortOrder(string iaSortOrder)
        {
            return " iaSortOrder = N'" + Extension.RemoveSqlInjectionChars(iaSortOrder) + "' ";
        }
        public static string GetByStatus(string iaStatus)
        {
            return " iaStatus = N'" + Extension.RemoveSqlInjectionChars(iaStatus) + "' ";
        }
    }
}
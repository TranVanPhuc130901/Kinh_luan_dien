namespace RevosJsc.TSql
{
    public class AdvertistmentPositionsTSql
    {
        public static string GetById(string iapId)
        {
            return " [AdvertistmentPositions].iapId = N'" + Extension.RemoveSqlInjectionChars(iapId) + "' ";
        }
        public static string GetByLang(string vapLang)
        {
            return " iapLang = N'" + Extension.RemoveSqlInjectionChars(vapLang) + "' ";
        }
        public static string GetByPosition(string iaPosition)
        {
            return " iaPosition = N'" + Extension.RemoveSqlInjectionChars(iaPosition) + "' ";
        }
        public static string GetByName(string vapName)
        {
            return " vapName = N'" + Extension.RemoveSqlInjectionChars(vapName) + "' ";
        }
        public static string GetByDescription(string vapDescription)
        {
            return " vapDescription = N'" + Extension.RemoveSqlInjectionChars(vapDescription) + "' ";
        }
        public static string GetByImage(string vapImage)
        {
            return " vapImage = N'" + Extension.RemoveSqlInjectionChars(vapImage) + "' ";
        }
        public static string GetByStatus(string iapStatus)
        {
            return " iapStatus = N'" + Extension.RemoveSqlInjectionChars(iapStatus) + "' ";
        }
        public static string GetBySortOrder(string iapSortOrder)
        {
            return " iapSortOrder = N'" + Extension.RemoveSqlInjectionChars(iapSortOrder) + "' ";
        }
        public static string GetByParam(string vapParam)
        {
            return " vapParam = N'" + Extension.RemoveSqlInjectionChars(vapParam) + "' ";
        }
        public static string GetByDateCreated(string dapDateCreated)
        {
            return " dapDateCreated = N'" + Extension.RemoveSqlInjectionChars(dapDateCreated) + "' ";
        }
        public static string GetByDateModifield(string dapDateModifield)
        {
            return " dapDateModifield = N'" + Extension.RemoveSqlInjectionChars(dapDateModifield) + "' ";
        }
    }
}
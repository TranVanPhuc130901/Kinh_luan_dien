namespace RevosJsc.TSql
{
    public class SubItemsTSql
    {
        public static string GetById(string isId)
        {
            return " isiId = N'" + Extension.RemoveSqlInjectionChars(isId) + "' ";
        }
        public static string GetByIiid(string iiId)
        {
            return " iiId = N'" + Extension.RemoveSqlInjectionChars(iiId) + "' ";
        }
        public static string GetByLang(string vsiLang)
        {
            return " vsiLang = N'" + Extension.RemoveSqlInjectionChars(vsiLang) + "' ";
        }
        public static string GetByApp(string vsiApp)
        {
            return " vsiApp = N'" + Extension.RemoveSqlInjectionChars(vsiApp) + "' ";
        }
        public static string GetByTitle(string vsiTitle)
        {
            return " vsiTitle = N'" + Extension.RemoveSqlInjectionChars(vsiTitle) + "' ";
        }
        public static string GetByDescription(string vsiDescription)
        {
            return " vsiDescription = N'" + Extension.RemoveSqlInjectionChars(vsiDescription) + "' ";
        }
        public static string GetByContent(string vsiContent)
        {
            return " vsiContent = N'" + Extension.RemoveSqlInjectionChars(vsiContent) + "' ";
        }
        public static string GetByImage(string vsiImage)
        {
            return " vsiImage = N'" + Extension.RemoveSqlInjectionChars(vsiImage) + "' ";
        }
        public static string GetByParam(string vsiParam)
        {
            return " vsiParam = N'" + Extension.RemoveSqlInjectionChars(vsiParam) + "' ";
        }

        public static string GetByPriceOld(string fsiPriceOld)
        {
            return " fsiPriceOld = N'" + Extension.RemoveSqlInjectionChars(fsiPriceOld) + "' ";
        }
        public static string GetByPriceNew(string fsiPriceNew)
        {
            return " fsiPriceNew = N'" + Extension.RemoveSqlInjectionChars(fsiPriceNew) + "' ";
        }
        public static string GetBySortOrder(string isiSortOrder)
        {
            return " isiSortOrder = N'" + Extension.RemoveSqlInjectionChars(isiSortOrder) + "' ";
        }
        public static string GetByStatus(string isiStatus)
        {
            return " isiStatus = N'" + Extension.RemoveSqlInjectionChars(isiStatus) + "' ";
        }
        public static string GetByDateCreated(string dsiDateCreated)
        {
            return " dsiDateCreated = N'" + Extension.RemoveSqlInjectionChars(dsiDateCreated) + "' ";
        }
        public static string GetByDateModified(string dsiDateModified)
        {
            return " dsiDateModified = N'" + Extension.RemoveSqlInjectionChars(dsiDateModified) + "' ";
        }
    }
}

namespace RevosJsc.TSql
{
    /// <summary>
    /// Summary description for ContactTSql
    /// </summary>
    public class ContactsTSql
    {
        public static string GetById(string icId)
        {
            return " icId = N'" + Extension.RemoveSqlInjectionChars(icId) + "' ";
        }
        public static string GetByParentId(string icParentId)
        {
            return " icParentId = N'" + Extension.RemoveSqlInjectionChars(icParentId) + "' ";
        }
        public static string GetByLevel(string icLevel)
        {
            return " icLevel = N'" + Extension.RemoveSqlInjectionChars(icLevel) + "' ";
        }
        public static string GetBySortOrder(string icSortOrder)
        {
            return " icSortOrder = N'" + Extension.RemoveSqlInjectionChars(icSortOrder) + "' ";
        }
        public static string GetByStatus(string icStatus)
        {
            return " icStatus = N'" + Extension.RemoveSqlInjectionChars(icStatus) + "' ";
        }
        public static string GetByGenealogy(string vcGenealogy)
        {
            return " vcGenealogy = N'" + Extension.RemoveSqlInjectionChars(vcGenealogy) + "' ";
        }
        public static string GetByLang(string vcLang)
        {
            return " vcLang = N'" + Extension.RemoveSqlInjectionChars(vcLang) + "' ";
        }
        public static string GetByName(string vcName)
        {
            return " vcName = N'" + Extension.RemoveSqlInjectionChars(vcName) + "' ";
        }
        public static string GetByAddress(string vcAddress)
        {
            return " vcAddress = N'" + Extension.RemoveSqlInjectionChars(vcAddress) + "' ";
        }
        public static string GetByPhone(string vcPhone)
        {
            return " vcPhone = N'" + Extension.RemoveSqlInjectionChars(vcPhone) + "' ";
        }
        public static string GetByHotline(string vcHotline)
        {
            return " vcHotline = N'" + Extension.RemoveSqlInjectionChars(vcHotline) + "' ";
        }
        public static string GetByEmail(string vcEmail)
        {
            return " vcEmail = N'" + Extension.RemoveSqlInjectionChars(vcEmail) + "' ";
        }
        public static string GetByMap(string vcMap)
        {
            return " vcMap = N'" + Extension.RemoveSqlInjectionChars(vcMap) + "' ";
        }
        public static string GetByImage(string vcImage)
        {
            return " vcImage = N'" + Extension.RemoveSqlInjectionChars(vcImage) + "' ";
        }
        public static string GetByParam(string vcParam)
        {
            return " vcParam = N'" + Extension.RemoveSqlInjectionChars(vcParam) + "' ";
        }
        public static string GetByDateCreated(string dcDateCreated)
        {
            return " dcDateCreated = N'" + Extension.RemoveSqlInjectionChars(dcDateCreated) + "' ";
        }
        public static string GetByDateModified(string diDateModified)
        {
            return " diDateModified = N'" + Extension.RemoveSqlInjectionChars(diDateModified) + "' ";
        }
    }
}
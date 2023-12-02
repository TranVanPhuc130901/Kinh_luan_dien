namespace RevosJsc.TSql
{
    /// <summary>
    /// Summary description for ContactTSql
    /// </summary>
    public class ContactDetailsTSql
    {
        public static string GetById(string icdId)
        {
            return " icdId = N'" + Extension.RemoveSqlInjectionChars(icdId) + "' ";
        }
        public static string GetByName(string vcdName)
        {
            return " vcdName = N'" + Extension.RemoveSqlInjectionChars(vcdName) + "' ";
        }
        public static string GetByEmail(string vcdEmail)
        {
            return " vcdEmail = N'" + Extension.RemoveSqlInjectionChars(vcdEmail) + "' ";
        }
        public static string GetByPhone(string vcdPhone)
        {
            return " vcdPhone = N'" + Extension.RemoveSqlInjectionChars(vcdPhone) + "' ";
        }
        public static string GetByAddress(string vcdAddress)
        {
            return " vcdAddress = N'" + Extension.RemoveSqlInjectionChars(vcdAddress) + "' ";
        }
        public static string GetBySubject(string vcdSubject)
        {
            return " vcdSubject = N'" + Extension.RemoveSqlInjectionChars(vcdSubject) + "' ";
        }
        public static string GetByContent(string vcdContent)
        {
            return " vcdContent = N'" + Extension.RemoveSqlInjectionChars(vcdContent) + "' ";
        }
        public static string GetByParam(string vcdParam)
        {
            return " vcdParam = N'" + Extension.RemoveSqlInjectionChars(vcdParam) + "' ";
        }
        public static string GetByDateCreated(string dcdDateCreated)
        {
            return " dcdDateCreated = N'" + Extension.RemoveSqlInjectionChars(dcdDateCreated) + "' ";
        }
        public static string GetByStatus(string icdStatus)
        {
            return " icdStatus = N'" + Extension.RemoveSqlInjectionChars(icdStatus) + "' ";
        }

    }
}
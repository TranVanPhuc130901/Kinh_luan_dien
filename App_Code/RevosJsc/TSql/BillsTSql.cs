
namespace RevosJsc.TSql
{
    public class BillsTSql
    {
        public static string GetById(string ibId)
        {
            return " ibId = N'" + Extension.RemoveSqlInjectionChars(ibId) + "' ";
        }
        public static string GetByCode(string vbCode)
        {
            return " vbCode = N'" + Extension.RemoveSqlInjectionChars(vbCode) + "' ";
        }

        public static string GetByLang(string vbLang)
        {
            return " vbLang = N'" + Extension.RemoveSqlInjectionChars(vbLang) + "' ";
        }

        public static string GetByName(string vbName)
        {
            return " vbName = N'" + Extension.RemoveSqlInjectionChars(vbName) + "' ";
        }

        public static string GetByPhone(string vbPhone)
        {
            return " vbPhone = N'" + Extension.RemoveSqlInjectionChars(vbPhone) + "' ";
        }

        public static string GetByEmail(string vbEmail)
        {
            return " vbEmail = N'" + Extension.RemoveSqlInjectionChars(vbEmail) + "' ";
        }

        public static string GetByAddress(string vbAddress)
        {
            return " vbAddress = N'" + Extension.RemoveSqlInjectionChars(vbAddress) + "' ";
        }

        public static string GetByComment(string vbComment)
        {
            return " vbComment = N'" + Extension.RemoveSqlInjectionChars(vbComment) + "' ";
        }

        public static string GetByParam(string vbParam)
        {
            return " vbParam = N'" + Extension.RemoveSqlInjectionChars(vbParam) + "' ";
        }
        public static string GetByStatus(string ibStatus)
        {
            return " ibStatus = N'" + Extension.RemoveSqlInjectionChars(ibStatus) + "' ";
        }
        public static string GetByDateCreated(string dbDateCreated)
        {
            return " dbDateCreated = N'" + Extension.RemoveSqlInjectionChars(dbDateCreated) + "' ";
        }
        public static string GetByateModified(string dbDateModified)
        {
            return " dbDateModified = N'" + Extension.RemoveSqlInjectionChars(dbDateModified) + "' ";
        }
    }
}

namespace RevosJsc.TSql
{
    public class UsersTSql
    {
        public static string GetById(string iuId)
        {
            return " iuId = N'" + Extension.RemoveSqlInjectionChars(iuId) + "' ";
        }
        public static string GetByRole(string vuRole)
        {
            return " vuRole = N'" + Extension.RemoveSqlInjectionChars(vuRole) + "' ";
        }
        public static string GetByAccount(string vuAccount)
        {
            return " vuAccount = N'" + Extension.RemoveSqlInjectionChars(vuAccount) + "' ";
        }
        public static string GetByPassword(string vuPassword)
        {
            return " vuPassword = N'" + Extension.RemoveSqlInjectionChars(vuPassword) + "' ";
        }
        public static string GetByVerificationCode(string vuVerificationCode)
        {
            return " vuVerificationCode = N'" + Extension.RemoveSqlInjectionChars(vuVerificationCode) + "' ";
        }
        public static string GetByFirstName(string vuFirstName)
        {
            return " vuFirstName = N'" + Extension.RemoveSqlInjectionChars(vuFirstName) + "' ";
        }
        public static string GetByLastName(string vuLastName)
        {
            return " vuLastName = N'" + Extension.RemoveSqlInjectionChars(vuLastName) + "' ";
        }
        public static string GetByAddress(string vuAddress)
        {
            return " vuAddress = N'" + Extension.RemoveSqlInjectionChars(vuAddress) + "' ";
        }
        public static string GetByPhoneNumber(string vuPhoneNumber)
        {
            return " vuPhoneNumber = N'" + Extension.RemoveSqlInjectionChars(vuPhoneNumber) + "' ";
        }
        public static string GetByEmail(string vuEmail)
        {
            return " vuEmail = N'" + Extension.RemoveSqlInjectionChars(vuEmail) + "' ";
        }
        public static string GetByIdentityCard(string vuIdentityCard)
        {
            return " vuIdentityCard = N'" + Extension.RemoveSqlInjectionChars(vuIdentityCard) + "' ";
        }
        public static string GetBySecurityQuestion(string vuSecurityQuestion)
        {
            return " vuSecurityQuestion = N'" + Extension.RemoveSqlInjectionChars(vuSecurityQuestion) + "' ";
        }
        public static string GetBySecurityAnswer(string vuSecurityAnswer)
        {
            return " vuSecurityAnswer = N'" + Extension.RemoveSqlInjectionChars(vuSecurityAnswer) + "' ";
        }
        public static string GetByStatus(string iuStatus)
        {
            return " iuStatus = N'" + Extension.RemoveSqlInjectionChars(iuStatus) + "' ";
        }
        public static string GetByDateCreated(string duDateCreated)
        {
            return " duDateCreated = N'" + Extension.RemoveSqlInjectionChars(duDateCreated) + "' ";
        }
        public static string GetByDateModified(string duDateModified)
        {
            return " duDateModified = N'" + Extension.RemoveSqlInjectionChars(duDateModified) + "' ";
        }
        public static string GetByExpirationDate(string duExpirationDate)
        {
            return " duExpirationDate = N'" + Extension.RemoveSqlInjectionChars(duExpirationDate) + "' ";
        }
        public static string GetByParam(string vuParam)
        {
            return " vuParam = N'" + Extension.RemoveSqlInjectionChars(vuParam) + "' ";
        }
    }
}

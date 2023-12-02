
namespace RevosJsc.TSql
{
    public class MembersTSql
    {
        public static string GetById(string imId)
        {
            return " imId = N'" + Extension.RemoveSqlInjectionChars(imId) + "' ";
        }

        public static string GetByApp(string vmApp)
        {
            return " vmApp = N'" + Extension.RemoveSqlInjectionChars(vmApp) + "' ";
        }

        public static string GetByAccount(string vmAccount)
        {
            return " vmAccount = N'" + Extension.RemoveSqlInjectionChars(vmAccount) + "' ";
        }

        public static string GetByPassword(string vmPassword)
        {
            return " vmPassword = N'" + Extension.RemoveSqlInjectionChars(vmPassword) + "' ";
        }

        public static string GetByFirstName(string vmFirstName)
        {
            return " vmFirstName = N'" + Extension.RemoveSqlInjectionChars(vmFirstName) + "' ";
        }

        public static string GetByLastName(string vmLastName)
        {
            return " vmLastName = N'" + Extension.RemoveSqlInjectionChars(vmLastName) + "' ";
        }

        public static string GetByAddress(string vmAddress)
        {
            return " vmAddress = N'" + Extension.RemoveSqlInjectionChars(vmAddress) + "' ";
        }

        public static string GetByPhone(string vmPhone)
        {
            return " vmPhone = N'" + Extension.RemoveSqlInjectionChars(vmPhone) + "' ";
        }

        public static string GetByEmail(string vmEmail)
        {
            return " vmEmail = N'" + Extension.RemoveSqlInjectionChars(vmEmail) + "' ";
        }

        public static string GetByBirthday(string dmBirthday)
        {
            return " dmBirthday = N'" + Extension.RemoveSqlInjectionChars(dmBirthday) + "' ";
        }

        public static string GetByIdentityCard(string vmIdentityCard)
        {
            return " vmIdentityCard = N'" + Extension.RemoveSqlInjectionChars(vmIdentityCard) + "' ";
        }

        public static string GetByRelationship(string vmRelationship)
        {
            return " vmRelationship = N'" + Extension.RemoveSqlInjectionChars(vmRelationship) + "' ";
        }

        public static string GetByEdu(string vmEdu)
        {
            return " vmEdu = N'" + Extension.RemoveSqlInjectionChars(vmEdu) + "' ";
        }

        public static string GetByJob(string vmJob)
        {
            return " vmJob = N'" + Extension.RemoveSqlInjectionChars(vmJob) + "' ";
        }

        public static string GetBySocialNetwork(string vmSocialNetwork)
        {
            return " vmSocialNetwork = N'" + Extension.RemoveSqlInjectionChars(vmSocialNetwork) + "' ";
        }

        public static string GetByImage(string vmImage)
        {
            return " vmImage = N'" + Extension.RemoveSqlInjectionChars(vmImage) + "' ";
        }

        public static string GetBySecurityQuestion(string vmSecurityQuestion)
        {
            return " vmSecurityQuestion = N'" + Extension.RemoveSqlInjectionChars(vmSecurityQuestion) + "' ";
        }

        public static string GetBySecurityAnswer(string vmSecurityAnswer)
        {
            return " vmSecurityAnswer = N'" + Extension.RemoveSqlInjectionChars(vmSecurityAnswer) + "' ";
        }

        public static string GetByStatus(string imStatus)
        {
            return " imStatus = N'" + Extension.RemoveSqlInjectionChars(imStatus) + "' ";
        }

        public static string GetByDateCreated(string dmDateCreated)
        {
            return " dmDateCreated = N'" + dmDateCreated + "' ";
        }

        public static string GetByDateModified(string dmDateModified)
        {
            return " dmDateModified = N'" + dmDateModified + "' ";
        }

        public static string GetByExpirationDate(string dmExpirationDate)
        {
            return " dmExpirationDate = N'" + dmExpirationDate + "' ";
        }

        public static string GetByComment(string vmComment)
        {
            return " vmComment = N'" + vmComment + "' ";
        }

        public static string GetByWeight(string vmWeight)
        {
            return " vmWeight = N'" + vmWeight + "' ";
        }

        public static string GetByHeight(string vmHeight)
        {
            return " vmHeight = N'" + vmHeight + "' ";
        }
    }
}

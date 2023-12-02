namespace RevosJsc.TSql
{
    public class RedirectsTSql
    {
        public static string GetById(string irId)
        {
            return " irId = N'" + Extension.RemoveSqlInjectionChars(irId) + "' ";
        }
        public static string GetByLink(string vrLink)
        {
            return " vrLink = N'" + Extension.RemoveSqlInjectionChars(vrLink) + "' ";
        }
        public static string GetByLinkDestination(string vrLinkDestination)
        {
            return " vrLinkDestination = N'" + Extension.RemoveSqlInjectionChars(vrLinkDestination) + "' ";
        }
        public static string GetByDateCreated(string drDateCreated)
        {
            return " drDateCreated = N'" + Extension.RemoveSqlInjectionChars(drDateCreated) + "' ";
        }
        public static string GetByDateModified(string drDateModified)
        {
            return " drDateModified = N'" + Extension.RemoveSqlInjectionChars(drDateModified) + "' ";
        }
        public static string GetByStatus(string irStatus)
        {
            return " irStatus = N'" + Extension.RemoveSqlInjectionChars(irStatus) + "' ";
        }
    }
}

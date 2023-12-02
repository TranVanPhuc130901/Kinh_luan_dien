namespace RevosJsc.TSql
{
    public class LogsTSql
    {
        public static string GetById(string ilId)
        {
            return " ilId = N'" + Extension.RemoveSqlInjectionChars(ilId) + "' ";
        }

        public static string GetByUrl(string vlUrl)
        {
            return " vlUrl = N'" + Extension.RemoveSqlInjectionChars(vlUrl) + "' ";
        }

        public static string GetByDescription(string vlDescription)
        {
            return " vlDescription = N'" + Extension.RemoveSqlInjectionChars(vlDescription) + "' ";
        }

        public static string GetByAuthor(string vlAuthor)
        {
            return " vlAuthor = N'" + Extension.RemoveSqlInjectionChars(vlAuthor) + "' ";
        }

        public static string GetByDateCreated(string dlDateCreated)
        {
            return " dlDateCreated = N'" + Extension.RemoveSqlInjectionChars(dlDateCreated) + "' ";
        }
    }
}

namespace RevosJsc.TSql
{
    /// <summary>
    /// Summary description for MenuTSql
    /// </summary>
    public class MenusTSql
    {
        public static string GetSubMenuByGenealogy(string genealogy, string condition)
        {
            var s = "";
            s = " LEFT([Groups].vmnGenealogy, LEN('" + genealogy + "')) = '" + genealogy + "' AND vmnGenealogy <> '" + genealogy + "' ";
            if (!condition.Equals(""))
            {
                s += " AND " + condition;
            }
            return s;
        }

        public static string GetById(string imnId)
        {
            return " imnId = N'" + Extension.RemoveSqlInjectionChars(imnId) + "' ";
        }
        public static string GetByParentId(string parentId)
        {
            return " imnParentId = N'" + Extension.RemoveSqlInjectionChars(parentId) + "' ";
        }
        public static string GetByGenealogy(string genealogy)
        {
            return " vmnGenealogy = N'" + Extension.RemoveSqlInjectionChars(genealogy) + "' ";
        }
        public static string GetByLang(string lang)
        {
            return " vmnLang = N'" + Extension.RemoveSqlInjectionChars(lang) + "' ";
        }
        public static string GetByApp(string app)
        {
            return " vmnApp = N'" + Extension.RemoveSqlInjectionChars(app) + "' ";
        }
        public static string GetByLevel(string level)
        {
            return " imnLevel = N'" + Extension.RemoveSqlInjectionChars(level) + "' ";
        }
        public static string GetBySortOrder(string sortOrder)
        {
            return " imnSortOrder = N'" + Extension.RemoveSqlInjectionChars(sortOrder) + "' ";
        }
        public static string GetByStatus(string status)
        {
            return " imnStatus = N'" + Extension.RemoveSqlInjectionChars(status) + "' ";
        }
        public static string GetByTarget(string target)
        {
            return " imnTarget = N'" + Extension.RemoveSqlInjectionChars(target) + "' ";
        }
        public static string GetByName(string name)
        {
            return " vmnName = N'" + Extension.RemoveSqlInjectionChars(name) + "' ";
        }
        public static string GetByLink(string link)
        {
            return " vmnLink = N'" + Extension.RemoveSqlInjectionChars(link) + "' ";
        }
        public static string GetByImage(string image)
        {
            return " vmnImage = N'" + Extension.RemoveSqlInjectionChars(image) + "' ";
        }
        public static string GetByParam(string param)
        {
            return " vmnParam = N'" + Extension.RemoveSqlInjectionChars(param) + "' ";
        }
        public static string GetByDateCreated(string dateCreated)
        {
            return " dmnDateCreated = N'" + Extension.RemoveSqlInjectionChars(dateCreated) + "' ";
        }
        public static string GetByDateModified(string dateModified)
        {
            return " dmnDateModified = N'" + Extension.RemoveSqlInjectionChars(dateModified) + "' ";
        }
    }
}
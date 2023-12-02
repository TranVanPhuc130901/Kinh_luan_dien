namespace RevosJsc.TSql
{
    public class GroupsTSql
    {
        public static string GetSubGroupsByGenealogy(string genealogy, string condition)
        {
            var s = "";
            s = " LEFT([Groups].vgGenealogy, LEN('" + genealogy + "')) = '" + genealogy + "' AND vgGenealogy <> '" + genealogy + "' ";
            if (!condition.Equals(""))
            {
                s += " AND " + condition;
            }
            return s;
        }

        public static string GetById(string igId)
        {
            return " igId = N'" + Extension.RemoveSqlInjectionChars(igId) + "' ";
        }

        public static string GetByLang(string vgLang)
        {
            return " vgLang = N'" + Extension.RemoveSqlInjectionChars(vgLang) + "' ";
        }

        public static string GetByApp(string vgApp)
        {
            return " vgApp = N'" + Extension.RemoveSqlInjectionChars(vgApp) + "' ";
        }

        public static string GetByLevel(string igLevel)
        {
            return " igLevel = N'" + Extension.RemoveSqlInjectionChars(igLevel) + "' ";
        }
        public static string GetBySortOrder(string sortOrder)
        {
            return " igSortOrder = N'" + Extension.RemoveSqlInjectionChars(sortOrder) + "' ";
        }
        public static string GetByPosition(string igPosition)
        {
            return " igPosition = N'" + Extension.RemoveSqlInjectionChars(igPosition) + "' ";
        }

        public static string GetByParentId(string igParentId)
        {
            return " igParentId = N'" + Extension.RemoveSqlInjectionChars(igParentId) + "' ";
        }

        public static string GetByGenealogy(string vgGenealogy)
        {
            return " vgGenealogy = N'" + Extension.RemoveSqlInjectionChars(vgGenealogy) + "' ";
        }

        public static string GetByName(string vgName)
        {
            return " vgName = N'" + Extension.RemoveSqlInjectionChars(vgName) + "' ";
        }

        public static string GetByDescription(string vgDescription)
        {
            return " vgDescription = N'" + Extension.RemoveSqlInjectionChars(vgDescription) + "' ";
        }

        public static string GetByContent(string vgContent)
        {
            return " vgContent = N'" + Extension.RemoveSqlInjectionChars(vgContent) + "' ";
        }

        public static string GetByImage(string vgImage)
        {
            return " vgImage = N'" + Extension.RemoveSqlInjectionChars(vgImage) + "' ";
        }

        public static string GetByParam(string vgParam)
        {
            return " vgParam = N'" + Extension.RemoveSqlInjectionChars(vgParam) + "' ";
        }

        public static string GetByMetaTitle(string vgMetaTitle)
        {
            return " vgMetaTitle = N'" + Extension.RemoveSqlInjectionChars(vgMetaTitle) + "' ";
        }

        public static string GetByMetaKeyword(string vgMetaKeyword)
        {
            return " vgMetaKeyword = N'" + Extension.RemoveSqlInjectionChars(vgMetaKeyword) + "' ";
        }

        public static string GetByMetaDescription(string vgMetaDescription)
        {
            return " vgMetaDescription = N'" + Extension.RemoveSqlInjectionChars(vgMetaDescription) + "' ";
        }
        public static string GetByTag(string vgTag)
        {
            return " vgTag = N'" + Extension.RemoveSqlInjectionChars(vgTag) + "' ";
        }
        public static string GetByLink(string vgLink)
        {
            return " vgLink = N'" + Extension.RemoveSqlInjectionChars(vgLink) + "' ";
        }

        public static string GetByDateCreated(string dgDateCreated)
        {
            return " dgDateCreated = N'" + Extension.RemoveSqlInjectionChars(dgDateCreated) + "' ";
        }

        public static string GetByDateModified(string dgDateModified)
        {
            return " dgDateModified = N'" + Extension.RemoveSqlInjectionChars(dgDateModified) + "' ";
        }

        public static string GetByStatus(string igStatus)
        {
            return " igStatus = N'" + Extension.RemoveSqlInjectionChars(igStatus) + "' ";
        }
    }
}

using RevosJsc.Database;

namespace RevosJsc.TSql
{
    public class GroupItemsTSql
    {
        public static string GetItemsInGroupByGenealogy(string genealogy, string condition)
        {
            var s = "";
            if (genealogy.Equals("")) return s;
            s = " LEFT([GroupItems].vgiGenealogy, LEN('" + genealogy + "')) = '" + genealogy + "' ";
            if (!condition.Equals(""))
            {
                s += " AND " + condition;
            }
            return s;
        }

        public static string GetItemsInGroupCondition(string igid, string condition)
        {
            var s = "";
            var dt = Groups.GetData("", "*", "IGID = " + igid, "");
            if (dt.Rows.Count <= 0) return s;
            s = " LEFT([GroupItems].vgiGenealogy, LEN('" + dt.Rows[0]["vgGenealogy"] + "')) = '" + dt.Rows[0]["vgGenealogy"] + "' ";
            if (!condition.Equals(""))
            {
                s += " AND " + condition;
            }
            return s;
        }

        public static string GetByIgiid(string igiId)
        {
            return " GroupItems.igiId = N'" + Extension.RemoveSqlInjectionChars(igiId) + "' ";
        }

        public static string GetByIgid(string igId)
        {
            return " GroupItems.igId = N'" + Extension.RemoveSqlInjectionChars(igId) + "' ";
        }

        public static string GetByiiId(string iiId)
        {
            return " GroupItems.iiId = N'" + Extension.RemoveSqlInjectionChars(iiId) + "' ";
        }

        public static string GetByGenealogy(string vgiGenealogy)
        {
            return " vgiGenealogy = N'" + Extension.RemoveSqlInjectionChars(vgiGenealogy) + "' ";
        }

        public static string GetByDateCreated(string dgiDateCreated)
        {
            return " dgiDateCreated = N'" + Extension.RemoveSqlInjectionChars(dgiDateCreated) + "' ";
        }

        public static string GetByDateModified(string dgiDateModified)
        {
            return " dgiDateModified = N'" + Extension.RemoveSqlInjectionChars(dgiDateModified) + "' ";
        }
    }
}

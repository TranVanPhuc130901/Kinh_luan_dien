using RevosJsc.Database;
using RevosJsc.TSql;

namespace RevosJsc.Extension
{
    public class FilterExtension
    {
        public static string CountAllChild(string ifId)
        {
            var dt = Filter.GetData("", " ifId ", " CHARINDEX('," + ifId + ",',vfGenealogy) > 0 ", "");
            return (dt.Rows.Count - 1).ToString();
        }
        public static string CountAllChildCondition(string condition)
        {
            var dt = Filter.GetData("", " ifId ", condition, "");
            return dt.Rows.Count.ToString();
        }
        public static string CountChildFilter(string ifId)
        {
            var condition = DataExtension.AndConditon(FilterTSql.GetByParentId(ifId), "ifStatus <> 2");
            var dt = Filter.GetData("", " ifId ", condition, "");
            return dt.Rows.Count.ToString();
        }
        public static string CountChildCategory(string ifId, string condition)
        {
            condition = DataExtension.AndConditon(FilterTSql.GetByParentId(ifId), condition);
            var dt = Filter.GetData("", " ifId ", condition, "");
            return dt.Rows.Count.ToString();
        }
        public static string GetNameById(string ifId)
        {
            var dt = Filter.GetData("", "vfName", "ifId = " + ifId, "");
            if (ifId == "0") return "Thuôc tính lọc";
            return dt.Rows.Count > 0 ? dt.Rows[0]["vfName"].ToString() : "";
        }
    }
}

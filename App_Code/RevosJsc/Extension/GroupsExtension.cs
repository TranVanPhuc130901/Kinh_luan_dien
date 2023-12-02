using System;
using System.Globalization;
using RevosJsc.Database;
using RevosJsc.TSql;

namespace RevosJsc.Extension
{
    public class GroupsExtension
    {
        public static string CountItemInGroup(string igId)
        {
            var condition = GroupItemsTSql.GetItemsInGroupCondition(igId, DataExtension.AndConditon(GroupsTSql.GetByStatus("1"), "iiStatus <> 2"));
            var dt = GroupItems.GetAllData("", "Items.iiId", condition, "");
            return dt.Rows.Count.ToString();
        }
        public static string CountAllItemInGroup(string igId)
        {
            var condition = GroupItemsTSql.GetItemsInGroupCondition(igId, "");
            var dt = GroupItems.GetAllData("", "Items.iiId", condition, "");
            return dt.Rows.Count.ToString();
        }


        public static string CountItemInGroupCondition(string igId, string condition)
        {
            condition = GroupItemsTSql.GetItemsInGroupCondition(igId, condition);
            var dt = GroupItems.GetAllData("", "Items.iiId", condition, "");
            return dt.Rows.Count.ToString();
        }

        public static string CountTotalViewItemInGroup(string igId)
        {
            double view = 0;
            var dt = GroupItems.GetAllData("", "*", GroupItemsTSql.GetItemsInGroupCondition(igId, ""), "");
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                view = view + Convert.ToDouble(dt.Rows[i]["iiTotalView"]);
            }
            return view.ToString(CultureInfo.InvariantCulture);
        }


        public static string CountAllChildCategory(string igId)
        {
            var dt = Groups.GetData("", " igId ", " CHARINDEX('," + igId + ",',vgGenealogy) > 0 ", "");
            return (dt.Rows.Count - 1).ToString();
        }
        public static string CountAllChild(string condition)
        {
            var dt = Groups.GetData("", " igId ", condition, "");
            return dt.Rows.Count.ToString();
        }
        public static string CountChildCategory(string igId)
        {
            var condition = DataExtension.AndConditon(GroupsTSql.GetByParentId(igId), "igStatus <> 2");
            var dt = Groups.GetData("", " igId ", condition, "");
            return dt.Rows.Count.ToString();
        }
        public static string CountChildCategory(string igId, string condition)
        {
            condition = DataExtension.AndConditon(GroupsTSql.GetByParentId(igId), condition);
            var dt = Groups.GetData("", " igId ", condition, "");
            return dt.Rows.Count.ToString();
        }
        public static string GetNameByIgid(string igId)
        {
            var dt = Groups.GetData("", "vgName", "igId = " + igId, "");
            if (igId == "0") return "Danh mục sản phẩm";
            return dt.Rows.Count > 0 ? dt.Rows[0]["vgName"].ToString() : "";
        }
        public static string GetImageByIgid(string igId)
        {
            var dt = Groups.GetData("", "vgImage", "igId = " + igId, "");
            if (igId == "0") return "";
            return dt.Rows.Count > 0 ? dt.Rows[0]["vgImage"].ToString() : "";
        }

        public static string GetByIgparentid(string igId)
        {
            var dt = Groups.GetData("", "*", "igId = " + igId, "");
            var s = "";
            if (dt.Rows.Count > 0)
            {
                s = dt.Rows[0]["vgGenealogy"].ToString();
            }
            return s;
        }
    }
}

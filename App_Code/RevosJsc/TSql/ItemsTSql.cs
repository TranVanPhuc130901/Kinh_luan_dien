using System;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;

namespace RevosJsc.TSql
{
    public class ItemsTSql
    {
        public static string GetById(string iiId)
        {
            return " dbo.[Items].iiId = N'" + Extension.RemoveSqlInjectionChars(iiId) + "' ";
        }

        public static string GetByLang(string viLang)
        {
            return " viLang = N'" + Extension.RemoveSqlInjectionChars(viLang) + "' ";
        }

        public static string GetByApp(string viApp)
        {
            return " viApp = N'" + Extension.RemoveSqlInjectionChars(viApp) + "' ";
        }

        public static string GetByCode(string viCode)
        {
            return " viCode = N'" + Extension.RemoveSqlInjectionChars(viCode) + "' ";
        }

        public static string GetByTitle(string viTitle)
        {
            return " viTitle = N'" + Extension.RemoveSqlInjectionChars(viTitle) + "' ";
        }

        public static string GetByDescription(string viDescription)
        {
            return " viDescription = N'" + Extension.RemoveSqlInjectionChars(viDescription) + "' ";
        }

        public static string GetByContent(string viContent)
        {
            return " viContent = N'" + Extension.RemoveSqlInjectionChars(viContent) + "' ";
        }

        public static string GetByImage(string viImage)
        {
            return " viImage = N'" + Extension.RemoveSqlInjectionChars(viImage) + "' ";
        }

        public static string GetByAuthor(string viAuthor)
        {
            return " viAuthor = N'" + Extension.RemoveSqlInjectionChars(viAuthor) + "' ";
        }

        public static string GetByMetaTitle(string viMetaTitle)
        {
            return " viMetaTitle = N'" + Extension.RemoveSqlInjectionChars(viMetaTitle) + "' ";
        }

        public static string GetByMetaKeyword(string viMetaKeyword)
        {
            return " viMetaKeyword = N'" + Extension.RemoveSqlInjectionChars(viMetaKeyword) + "' ";
        }

        public static string GetByMetaDescription(string viMetaDescription)
        {
            return " viMetaDescription = N'" + Extension.RemoveSqlInjectionChars(viMetaDescription) + "' ";
        }
        public static string GetByTag(string viTag)
        {
            return " viTag = N'" + Extension.RemoveSqlInjectionChars(viTag) + "' ";
        }

        public static string GetByLink(string viLink)
        {
            return " viLink = N'" + Extension.RemoveSqlInjectionChars(viLink) + "' ";
        }

        public static string GetByPriceOld(string fiPriceOld)
        {
            return " fiPriceOld = N'" + Extension.RemoveSqlInjectionChars(fiPriceOld) + "' ";
        }

        public static string GetByPriceNew(string fiPriceNew)
        {
            return " fiPriceNew = N'" + Extension.RemoveSqlInjectionChars(fiPriceNew) + "' ";
        }

        public static string GetByParam(string viParam)
        {
            return " viParam = N'" + Extension.RemoveSqlInjectionChars(viParam) + "' ";
        }

        public static string GetByTotalView(string iiTotalView)
        {
            return " iiTotalView = N'" + Extension.RemoveSqlInjectionChars(iiTotalView) + "' ";
        }

        public static string GetBySortOrder(string iiSortOrder)
        {
            return " iiSortOrder = N'" + Extension.RemoveSqlInjectionChars(iiSortOrder) + "' ";
        }

        public static string GetByDateCreated(string diDateCreated)
        {
            return " diDateCreated = N'" + Extension.RemoveSqlInjectionChars(diDateCreated) + "' ";
        }

        public static string GetByDateModified(string diDateModified)
        {
            return " diDateModified = N'" + Extension.RemoveSqlInjectionChars(diDateModified) + "' ";
        }

        public static string GetByStatus(string iiStatus)
        {
            return " iiStatus = N'" + Extension.RemoveSqlInjectionChars(iiStatus) + "' ";
        }
        public static string GetByMasterId(string id)
        {
            return " MasterId = " + Extension.RemoveSqlInjectionChars(id) + " ";
        }

        public static string GetByListId(string listId)
        {
            var s = "";
            s = "CHARINDEX( ','+CAST(iiId AS NVARCHAR(10)) +',', '" + Extension.RemoveSqlInjectionChars(listId) + "') > 0";
            return s;
        }
        /// <summary>
        /// Lấy iiId theo thuộc tính lọc truyền vào từ querysring
        /// </summary>
        /// <param name="queryStringFilterValue">Danh sách thuộc tính lọc (vd: ,12,24,)</param>
        /// <returns></returns>
        public static string GetFilterAndCondition(string queryStringFilterValue)
        {
            queryStringFilterValue = StringExtension.RemoveSqlInjectionChars(queryStringFilterValue);
            var condition = "";
            foreach (var i in queryStringFilterValue.Split(new string[] {","}, StringSplitOptions.None))
            {
                //Kiem tra, chỉ tạo điều kiện lọc khi filter là số
                try
                {
                    var int32 = Convert.ToInt32(i);
                    if (i.Length > 0) condition = DataExtension.AndConditon(condition, "charindex('" + "," + i + ",'," + FilterItemsColumns.VfiParam + ")>0");
                }
                catch
                {
                    // Do nothing
                }
            }

            if (condition.Length <= 0) return "";
            var dt = FilterItems.GetData("", "*", condition, "");
            var s = "0,";
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                s += dt.Rows[i]["iiId"] + ",";
            }
            s = s.Trim(',');
            return " Items.iiId IN (" + s + ")";
        }
        /// <summary>
        /// Lấy iiId theo thuộc tính lọc truyền vào từ querysring
        /// </summary>
        /// <param name="queryStringFilterValue">Danh sách thuộc tính lọc (vd: ,12,24,)</param>
        /// <returns></returns>
        public static string GetFilterOrCondition(string queryStringFilterValue)
        {
            queryStringFilterValue = StringExtension.RemoveSqlInjectionChars(queryStringFilterValue);
            var condition = "";
            foreach (var i in queryStringFilterValue.Split(new string[] { "," }, StringSplitOptions.None))
            {
                //Kiem tra, chỉ tạo điều kiện lọc khi filter là số
                try
                {
                    var int32 = Convert.ToInt32(i);
                    if (i.Length > 0) condition = DataExtension.OrConditon(condition, "charindex('" + "," + i + ",'," + FilterItemsColumns.VfiParam + ")>0");
                }
                catch
                {
                    // Do nothing
                }
            }
            if (condition.Length <= 0) return "";
            var dt = FilterItems.GetData("", "*", condition, "");
            var s = "0,";
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                s += dt.Rows[i]["iiId"] + ",";
            }
            s = s.Trim(',');
            return " Items.iiId IN (" + s + ")";
        }
    }
}

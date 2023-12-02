using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.TSql;
using System;
using System.Collections.Generic;
using System.Text;
using RevosJsc.Extension;

namespace Developer.Extension
{
    /// <summary>
    /// Summary description for ReviewExtension
    /// </summary>
    public class ProductExtension
    {
        public static string FullTextSearch(string key)
        {
            var s = new StringBuilder();
            s.Append("CONTAINS((viTitle,viTag), N'");
            foreach (var item in key.Split(' '))
            {
                if (item.Length > 0) s.Append("\"" + item + "*\" AND ");
            }
            return s.ToString().Remove(s.ToString().LastIndexOf(" AND ", StringComparison.Ordinal)) + "')";
        }
        public static string GetProductNameById(string id)
        {
            var dt = Items.GetData("1", "viTitle", ItemsTSql.GetById(id), "");
            return dt.Rows.Count > 0 ? dt.Rows[0][ItemsColumns.ViTitle].ToString() : "";
        }

        /// <summary>
        /// Hàm lấy tổng số màu trong 1 sản phẩm có biến thể
        /// </summary>
        /// <param name="masterId">masterId của sàn phẩm có biến thể</param>
        /// <returns>Số lượng màu sắc trong 1 biến thể</returns>
        public static int GetCountColor(string masterId)
        {
            var conditionSize = DataExtension.AndConditon(
                ItemsTSql.GetByMasterId(masterId),
                ItemsTSql.GetByStatus("1")
            );
            var dtColor = Items.GetData("", "*", conditionSize, "");
            HashSet<string> uniqueSỉze = new HashSet<string>();

            for (int z = 0; z < dtColor.Rows.Count; z++)
            {
                string value = dtColor.Rows[z]["viMetaTitle"].ToString();

                // Kiểm tra giá trị đã tồn tại trong tập hợp chưa
                if (!uniqueSỉze.Contains(value))
                {
                    uniqueSỉze.Add(value);
                }
            }

            var countColor = uniqueSỉze.Count;
            return countColor;
        }
    }
}

using System.Linq;

namespace RevosJsc.TSql
{
    /// <summary>
    /// Thực hiện việc lấy các điều kiện tìm kiếm như tìm kiếm theo tên các trường của các bảng
    /// </summary>
    public class SearchTSql
    {
        /// <summary>
        /// Lấy điều kiện tìm kiếm gần đúng để ghép vào câu truy vấn SQL. Để thực hiện tìm kiếm gần đúng, cần tạo thêm function trong sql
        /// </summary>
        /// <param name="keySearch">Từ khoá tìm kiếm</param>
        /// <param name="dataTableColumnName">Tên các trường chứa dữ liệu cần tìm kiếm</param>
        /// <returns></returns>
        public static string GetSearchMathedCondition(string keySearch, params string[] dataTableColumnName)
        {
            keySearch = Extension.RemoveSqlInjectionChars(keySearch);
            var s = dataTableColumnName.Where(s1 => s1.Length > 0).Aggregate("", (current, s1) => current + (s1 + "+"));
            if (s.Length > "+".Length) s = s.Substring(0, s.Length - "+".Length);
            return " dbo.SearchMatched(N'" + keySearch + "'," + s + ") = 1 ";
        }
    }
}
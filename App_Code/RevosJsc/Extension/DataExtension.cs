using System.Linq;

namespace RevosJsc.Extension
{
    public class DataExtension
    {
        /// <summary>
        /// Thực hiện xử lý dữ liệu truyền vào trước khi cập nhật
        /// </summary>
        /// <param name="fields">Các trường cần cập nhật</param>
        /// <param name="values">Giá trị được cập nhật - Nếu là chuỗi thì truyền vào như sau: 'values' </param>
        /// <returns></returns>
        public static string UpdateValues(string[] fields, string[] values)
        {
            var s = "";
            if (fields.Length <= 0 || values.Length <= 0) return s;
            if (fields.Length != values.Length) return s;
            for (var i = 0; i < fields.Length; i++) s += fields[i] + " = N'" + values[i].Replace("'", "''") + "', ";
            s = s.Substring(0, s.Length - ", ".Length);
            return s;
        }


        /// <summary>
        /// Trả về danh sách tên các cột, phục vụ cho truy vấn sql (vd: cot1,cot2,cot2)
        /// </summary>
        /// <param name="columns">Danh sách các cột, truyền vào theo kiểu mảng string hoặc các chuỗi cách nhau bởi dấu ,. Chú ý: nếu chuỗi truyền vào là chuỗi rỗng thì nó sẽ ko được ghép</param>
        /// <returns></returns>
        public static string GetListColumns(params string[] columns)
        {
            var s = columns.Where(s1 => s1.Length > 0).Aggregate("", (current, s1) => current + s1 + ",");
            if (s.Length > ",".Length) s = s.Substring(0, s.Length - ",".Length);
            return s;
        }

        /// <summary>
        /// Ghép các điều kiện với nhau để truyền vào câu truy vấn SQL bởi toán tử and
        /// </summary>
        /// <param name="conditons">Các điều kiện (Sử dụng namespace TSql để tạo các điều kiện)</param>
        /// <returns></returns>
        public static string AndConditon(params string[] conditons)
        {
            var str = conditons.Where(str2 => str2.Length > 0).Aggregate("", (current, str2) => current + str2 + " AND ");
            return "(" + str.Substring(0, str.Length - " AND ".Length) + ")";
        }

        /// <summary>
        /// Ghép các điều kiện với nhau để truyền vào câu truy vấn SQL bởi toán tử or
        /// </summary>
        /// <param name="conditons">Các điều kiện (Sử dụng namespace TSql để tạo các điều kiện)</param>
        /// <returns></returns>
        public static string OrConditon(params string[] conditons)
        {
            var str = conditons.Where(str2 => str2.Length > 0).Aggregate("", (current, str2) => current + str2 + " OR ");
            return "(" + str.Substring(0, str.Length - " OR ".Length) + ")";
        }
    }
}

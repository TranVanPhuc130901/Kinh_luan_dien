using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Logs
    {
        #region Insert

        public static void Insert(string vlUrl, string vlDescription, string vlAuthor, string dlDateCreated)
        {
            var cmd = new OleDbCommand("Logs_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@vlUrl", vlUrl);
            cmd.Parameters.AddWithValue("@vlDescription", vlDescription);
            cmd.Parameters.AddWithValue("@vlAuthor", vlAuthor);
            cmd.Parameters.AddWithValue("@dlDateCreated", dlDateCreated);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get data

        /// <summary>
        /// Lấy dữ liệu từ bảng LanguageKey
        /// </summary>
        /// <param name="top">Số lượng bản ghi lấy ra</param>
        /// <param name="fields">Trường dữ liệu cần lấy (Điền * để lấy tất cả các trường dữ liệu)</param>
        /// <param name="condition">Điều kiện lấy dữ liệu</param>
        /// <param name="orderBy">Điều kiện sắp xếp dữ liệu</param>
        /// <returns></returns>
        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Logs_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@order", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        public static DataSet GetDataPaging(string pageIndex, string pageSize, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("Logs_GetDataPaging") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }

        #endregion
    }
}
using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class FilterItems
    {
        #region Insert

        public static void Insert(string iiId, string vfiParam)
        {
            var cmd = new OleDbCommand("FilterItems_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@iiId", iiId);
            cmd.Parameters.AddWithValue("@vfiParam", vfiParam);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion
        #region Update

        public static void Update(string iiId, string vfiParam, string ifiId)
        {
            var cmd = new OleDbCommand("FilterItems_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@iiId", iiId);
            cmd.Parameters.AddWithValue("@vfiParam", vfiParam);
            cmd.Parameters.AddWithValue("@ifiId", ifiId);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion
        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("FilterItems_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get data

        /// <summary>
        /// Lấy dữ liệu từ bảng FilterItems
        /// </summary>
        /// <param name="top">Số lượng bản ghi lấy ra</param>
        /// <param name="fields">Trường dữ liệu cần lấy (Điền * để lấy tất cả các trường dữ liệu)</param>
        /// <param name="condition">Điều kiện lấy dữ liệu</param>
        /// <param name="orderBy">Điều kiện sắp xếp dữ liệu</param>
        /// <returns></returns>
        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("FilterItems_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        #endregion
    }
}
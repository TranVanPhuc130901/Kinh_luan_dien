using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Keywords
    {
        #region Insert

        public static void Insert(string vkTitle, string vkDescription)
        {
            var cmd = new OleDbCommand("Keywords_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@vkTitle", vkTitle);
            cmd.Parameters.AddWithValue("@vkDescription", vkDescription);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Update

        public static void Update(string ikId, string vkTitle, string vkDescription)
        {
            var cmd = new OleDbCommand("Keywords_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@ikId", ikId);
            cmd.Parameters.AddWithValue("@vkTitle", vkTitle);
            cmd.Parameters.AddWithValue("@vkDescription", vkDescription);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Keywords_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
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
            var cmd = new OleDbCommand("Keywords_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@order", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        /// <summary>
        /// Lấy thông tin của Keywords theo title.
        /// </summary>
        /// <param name="title">Từ khóa - tối đa 128 ký tự</param>
        /// <param name="condition">Điều kiện lấy dữ liệu</param>
        /// <returns></returns>
        public static DataTable GetDataByTitle(string title, string condition)
        {
            var cmd = new OleDbCommand("Keywords_GetDataByTitle") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@condition", condition);
            return SqlDatabase.GetData(cmd);
        }

        #endregion
    }
}

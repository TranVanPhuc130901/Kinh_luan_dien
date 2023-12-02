using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class LanguageNational
    {
        #region Insert

        public static void Insert(string vlnName, string vlnFlag, string ilnSortOrder, string ilnStatus)
        {
            var cmd = new OleDbCommand("LanguageNational_Insert") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@vlnName", vlnName);
            cmd.Parameters.AddWithValue("@vlnFlag", vlnFlag);
            cmd.Parameters.AddWithValue("@ilnSortOrder", ilnSortOrder);
            cmd.Parameters.AddWithValue("@ilnStatus", ilnStatus);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Update

        public static void Update(string ilnId, string vlnName, string vlnFlag, string ilnSortOrder, string ilnStatus)
        {
            var cmd = new OleDbCommand("LanguageNational_Update") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@ilnId", ilnId);
            cmd.Parameters.AddWithValue("@vlnName", vlnName);
            cmd.Parameters.AddWithValue("@vlnFlag", vlnFlag);
            cmd.Parameters.AddWithValue("@ilnSortOrder", ilnSortOrder);
            cmd.Parameters.AddWithValue("@ilnStatus", ilnStatus);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get data

        /// <summary>
        /// Lấy dữ liệu từ bảng LanguageNational
        /// </summary>
        /// <param name="top">Số lượng bản ghi lấy ra</param>
        /// <param name="fields">Trường dữ liệu cần lấy (Điền * để lấy tất cả các trường dữ liệu)</param>
        /// <param name="condition">Điều kiện lấy dữ liệu</param>
        /// <param name="orderBy">Điều kiện sắp xếp dữ liệu</param>
        /// <returns></returns>

        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("LanguageNational_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@order", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        #endregion

    }
}

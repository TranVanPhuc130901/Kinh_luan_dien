using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    /// <summary>
    /// Summary description for Menu
    /// </summary>
    public class Menus
    {
        #region Insert

        public static void Insert(string imnParentId, string vmnLang, string vmnApp, string imnSortOrder, string imnStatus, string imnTarget, string vmnName, string vmnLink, string vmnImage, string vmnParam, string dmnDateCreated, string dmnDateModified)
        {
            var cmd = new OleDbCommand("Menus_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@imnParentId", imnParentId);
            cmd.Parameters.AddWithValue("@vmnLang", vmnLang);
            cmd.Parameters.AddWithValue("@vmnApp", vmnApp);
            cmd.Parameters.AddWithValue("@imnSortOrder", imnSortOrder);
            cmd.Parameters.AddWithValue("@imnStatus", imnStatus);
            cmd.Parameters.AddWithValue("@imnTarget", imnTarget);
            cmd.Parameters.AddWithValue("@vmnName", vmnName);
            cmd.Parameters.AddWithValue("@vmnLink", vmnLink);
            cmd.Parameters.AddWithValue("@vmnImage", vmnImage);
            cmd.Parameters.AddWithValue("@vmnParam", vmnParam);
            cmd.Parameters.AddWithValue("@dmnDateCreated", dmnDateCreated);
            cmd.Parameters.AddWithValue("@dmnDateModified", dmnDateModified);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Update

        public static void Update(string imnParentId, string vmnLang, string vmnApp, string imnSortOrder, string imnStatus, string imnTarget, string vmnName, string vmnLink, string vmnImage, string vmnParam, string dmnDateCreated, string dmnDateModified, string imnId)
        {
            var cmd = new OleDbCommand("Menus_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@imnParentId", imnParentId);
            cmd.Parameters.AddWithValue("@vmnLang", vmnLang);
            cmd.Parameters.AddWithValue("@vmnApp", vmnApp);
            cmd.Parameters.AddWithValue("@imnSortOrder", imnSortOrder);
            cmd.Parameters.AddWithValue("@imnStatus", imnStatus);
            cmd.Parameters.AddWithValue("@imnTarget", imnTarget);
            cmd.Parameters.AddWithValue("@vmnName", vmnName);
            cmd.Parameters.AddWithValue("@vmnLink", vmnLink);
            cmd.Parameters.AddWithValue("@vmnImage", vmnImage);
            cmd.Parameters.AddWithValue("@vmnParam", vmnParam);
            cmd.Parameters.AddWithValue("@dmnDateCreated", dmnDateCreated);
            cmd.Parameters.AddWithValue("@dmnDateModified", dmnDateModified);
            cmd.Parameters.AddWithValue("@imnId", imnId);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("Menus_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Menus_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get data

        /// <summary>
        /// Lấy dữ liệu từ bảng Menu
        /// </summary>
        /// <param name="top">Số lượng bản ghi lấy ra</param>
        /// <param name="fields">Trường dữ liệu cần lấy (Điền * để lấy tất cả các trường dữ liệu)</param>
        /// <param name="condition">Điều kiện lấy dữ liệu</param>
        /// <param name="orderBy">Điều kiện sắp xếp dữ liệu</param>
        /// <returns></returns>
        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Menus_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        public static DataTable GetAllMenus(string fields, string condition, string orderby)
        {
            var cmd = new OleDbCommand("Menus_GetAllMenus") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderby);
            return SqlDatabase.GetData(cmd);
        }

        #endregion
    }
}
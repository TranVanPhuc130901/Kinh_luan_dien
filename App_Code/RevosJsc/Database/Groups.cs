using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Groups
    {
        #region Insert

        public static void Insert(string igParentId, string vgLang, string vgApp, string igSortOrder, string igPosition, string igTotalView, string vgName, string vgDescription, string vgContent, string vgImage, string vgParam, string vgMetaTitle, string vgMetaKeyword, string vgMetaDescription, string vgTag, string vgLink, string dgDateCreated, string dgDateModified, string igStatus)
        {
            var cmd = new OleDbCommand("Groups_Insert") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@igParentId", igParentId);
            cmd.Parameters.AddWithValue("@vgLang", vgLang);
            cmd.Parameters.AddWithValue("@vgApp", vgApp);
            cmd.Parameters.AddWithValue("@igSortOrder", igSortOrder);
            cmd.Parameters.AddWithValue("@igPosition", igPosition);
            cmd.Parameters.AddWithValue("@igTotalView", igTotalView);
            cmd.Parameters.AddWithValue("@vgName", vgName);
            cmd.Parameters.AddWithValue("@vgDescription", vgDescription);
            cmd.Parameters.AddWithValue("@vgContent", vgContent);
            cmd.Parameters.AddWithValue("@vgImage", vgImage);
            cmd.Parameters.AddWithValue("@vgParam", vgParam);
            cmd.Parameters.AddWithValue("@vgMetaTitle", vgMetaTitle);
            cmd.Parameters.AddWithValue("@vgMetaKeyword", vgMetaKeyword);
            cmd.Parameters.AddWithValue("@vgMetaDescription", vgMetaDescription);
            cmd.Parameters.AddWithValue("@vgTag", vgTag);
            cmd.Parameters.AddWithValue("@vgLink", vgLink);
            cmd.Parameters.AddWithValue("@dgDateCreated", dgDateCreated);
            cmd.Parameters.AddWithValue("@dgDateModified", dgDateModified);
            cmd.Parameters.AddWithValue("@igStatus", igStatus);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Update

        public static void Update(string igParentId, string vgApp, string igSortOrder, string igPosition, string igTotalView, string vgName, string vgDescription, string vgContent, string vgImage, string vgParam, string vgMetaTitle, string vgMetaKeyword, string vgMetaDescription, string vgTag, string vgLink, string dgDateCreated, string dgDateModified, string igStatus, string igId)
        {
            var cmd = new OleDbCommand("Groups_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@igParentId", igParentId);
            cmd.Parameters.AddWithValue("@vgApp", vgApp);
            cmd.Parameters.AddWithValue("@igSortOrder", igSortOrder);
            cmd.Parameters.AddWithValue("@igPosition", igPosition);
            cmd.Parameters.AddWithValue("@igTotalView", igTotalView);
            cmd.Parameters.AddWithValue("@vgName", vgName);
            cmd.Parameters.AddWithValue("@vgDescription", vgDescription);
            cmd.Parameters.AddWithValue("@vgContent", vgContent);
            cmd.Parameters.AddWithValue("@vgImage", vgImage);
            cmd.Parameters.AddWithValue("@vgParam", vgParam);
            cmd.Parameters.AddWithValue("@vgMetaTitle", vgMetaTitle);
            cmd.Parameters.AddWithValue("@vgMetaKeyword", vgMetaKeyword);
            cmd.Parameters.AddWithValue("@vgMetaDescription", vgMetaDescription);
            cmd.Parameters.AddWithValue("@vgTag", vgTag);
            cmd.Parameters.AddWithValue("@vgLink", vgLink);
            cmd.Parameters.AddWithValue("@dgDateCreated", dgDateCreated);
            cmd.Parameters.AddWithValue("@dgDateModified", dgDateModified);
            cmd.Parameters.AddWithValue("@igStatus", igStatus);
            cmd.Parameters.AddWithValue("@igId", igId);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("Groups_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Groups_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get data

        /// <summary>
        /// Lấy thông tin về Group theo title
        /// </summary>
        /// <param name="title"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable GetDataByTitle(string title, string condition)
        {
            var cmd = new OleDbCommand("Groups_GetDataByTitle") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@condition", condition);
            return SqlDatabase.GetData(cmd);
        }

        /// <summary>
        /// Lấy dữ liệu từ bảng Group
        /// </summary>
        /// <param name="top">Số lượng bản ghi lấy ra</param>
        /// <param name="fields">Trường dữ liệu cần lấy (Điền * để lấy tất cả các trường dữ liệu)</param>
        /// <param name="condition">Điều kiện lấy dữ liệu</param>
        /// <param name="orderBy">Điều kiện sắp xếp dữ liệu</param>
        /// <returns></returns>
        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Groups_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        public static DataTable GetAllGroups(string fields, string condition, string orderby)
        {
            var cmd = new OleDbCommand("Groups_GetAllGroups") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderby);
            return SqlDatabase.GetData(cmd);
        }

        public static DataSet GetDataPaging(string pageNumbers, string returnRows, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("Groups_GetDataPaging") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@pageNumbers", pageNumbers);
            cmd.Parameters.AddWithValue("@returnRows", returnRows);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }
        #endregion
    }
}
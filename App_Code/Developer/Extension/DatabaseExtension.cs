using System.Data;
using System.Data.OleDb;
using RevosJsc.Database;

namespace Developer.Extension
{
    /// <summary>
    /// Summary description for DataTableExtension
    /// </summary>
    public class DatabaseExtension
    {
        public static DataTable GetDataTable(string top, string fields, string condition, string orderBy, string table)
        {
            var cmd = new OleDbCommand("DataTable_Get") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            cmd.Parameters.AddWithValue("@table", table);
            return SqlDatabase.GetData(cmd);
        }
        public static void Delete(string condition, string table)
        {
            var cmd = new OleDbCommand("DataTable_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@table", table);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }
        public static DataSet GetAllDataPaging(string pageNumbers, string returnRows, string whereClause, string orderBy, string tableName)
        {
            var oleDbCommand = new OleDbCommand("DataTable_GetDataPaging") {CommandType = CommandType.StoredProcedure};
            var cmd = oleDbCommand;
            cmd.Parameters.AddWithValue("@pageNumbers", pageNumbers);
            cmd.Parameters.AddWithValue("@returnRows", returnRows);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            cmd.Parameters.AddWithValue("@tableName", tableName);
            return SqlDatabase.GetData_OverDataset(cmd);
        }
        public static void Update(string values, string condition, string tableName)
        {
            var oleDbCommand = new OleDbCommand("DataTable_Update") { CommandType = CommandType.StoredProcedure };
            var cmd = oleDbCommand;
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@table", tableName);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }
        public static string InsertGroup(string igParentId, string vgLang, string vgApp, string igSortOrder, string igPosition, string igTotalView, string vgName, string vgDescription, string vgContent, string vgImage, string vgParam, string vgMetaTitle, string vgMetaKeyword, string vgMetaDescription, string vgTag, string vgLink, string dgDateCreated, string dgDateModified, string igStatus)
        {
            var cmd = new OleDbCommand("Groups_Insert") { CommandType = CommandType.StoredProcedure };
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
            return SqlDatabase.ExecuteScalar(cmd);
        }
    }
}
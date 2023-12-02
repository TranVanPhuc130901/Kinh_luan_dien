using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Bills
    {
        #region Insert

        public static string Insert_ReturnId(string vbLang, string vbGender, string vbName, string vbPhone, string vbEmail, string vbAddress, string vbComment, string vbParam, string ibStatus, string dbDateCreated, string dbDateModified)
        {
            var cmd = new OleDbCommand("Bills_Insert_ReturnId") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@vbLang", vbLang);
            cmd.Parameters.AddWithValue("@vbGender", vbGender);
            cmd.Parameters.AddWithValue("@vbName", vbName);
            cmd.Parameters.AddWithValue("@vbPhone", vbPhone);
            cmd.Parameters.AddWithValue("@vbEmail", vbEmail);
            cmd.Parameters.AddWithValue("@vbAddress", vbAddress);
            cmd.Parameters.AddWithValue("@vbComment", vbComment);
            cmd.Parameters.AddWithValue("@vbParam", vbParam);
            cmd.Parameters.AddWithValue("@ibStatus", ibStatus);
            cmd.Parameters.AddWithValue("@dbDateCreated", dbDateCreated);
            cmd.Parameters.AddWithValue("@dbDateModified", dbDateModified);
            return SqlDatabase.ExecuteScalar(cmd);
        }
        public static void Insert(string vbLang, string vbGender, string vbName, string vbPhone, string vbEmail, string vbAddress, string vbComment, string vbParam, string ibStatus, string dbDateCreated, string dbDateModified)
        {
            var cmd = new OleDbCommand("Bills_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@vbLang", vbLang);
            cmd.Parameters.AddWithValue("@vbGender", vbGender);
            cmd.Parameters.AddWithValue("@vbName", vbName);
            cmd.Parameters.AddWithValue("@vbPhone", vbPhone);
            cmd.Parameters.AddWithValue("@vbEmail", vbEmail);
            cmd.Parameters.AddWithValue("@vbAddress", vbAddress);
            cmd.Parameters.AddWithValue("@vbComment", vbComment);
            cmd.Parameters.AddWithValue("@vbParam", vbParam);
            cmd.Parameters.AddWithValue("@ibStatus", ibStatus);
            cmd.Parameters.AddWithValue("@dbDateCreated", dbDateCreated);
            cmd.Parameters.AddWithValue("@dbDateModified", dbDateModified);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Insert

        #region Get data

        public static DataTable GetData(string top, string fields, string condition, string order)
        {
            var cmd = new OleDbCommand("Bills_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@order", order);
            return SqlDatabase.GetData(cmd);
        }

        public static DataSet GetBillPagging(string pageIndex, string pageSize, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("Bills_GetDataPaging") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }

        #endregion

        #region Update

        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("Bills_UpdateValues") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Bills_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion
    }
}
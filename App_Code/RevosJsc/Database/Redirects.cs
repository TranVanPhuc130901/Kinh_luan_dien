using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Redirects
    {
        #region Inserts

        public static void Insert(string vrLink, string vrLinkDestination, string drDateCreated, string drDateModified, string irStatus)
        {
            var cmd = new OleDbCommand("Redirects_Insert") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@VrLink", vrLink);
            cmd.Parameters.AddWithValue("@VrLinkDestination", vrLinkDestination);
            cmd.Parameters.AddWithValue("@drDateCreated", drDateCreated);
            cmd.Parameters.AddWithValue("@drDateModified", drDateModified);
            cmd.Parameters.AddWithValue("@irStatus", irStatus);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Inserts

        #region Update

        public static void Update(string vrLink, string vrLinkDestination, string drDateCreated, string drDateModified, string irStatus, string irId)
        {
            var cmd = new OleDbCommand("Redirects_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@VrLink", vrLink);
            cmd.Parameters.AddWithValue("@VrLinkDestination", vrLinkDestination);
            cmd.Parameters.AddWithValue("@drDateCreated", drDateCreated);
            cmd.Parameters.AddWithValue("@drDateModified", drDateModified);
            cmd.Parameters.AddWithValue("@IrStatus", irStatus);
            cmd.Parameters.AddWithValue("@IrId", irId);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }
        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("Redirects_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Update

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Redirects_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Delete

        #region Get data

        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Redirects_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        public static DataSet GetDataPaging(string pageIndex, string pageSize, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("Redirects_GetDataPaging") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@pageNumbers", pageIndex);
            cmd.Parameters.AddWithValue("@returnRows", pageSize);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }

        #endregion Get data
    }
}
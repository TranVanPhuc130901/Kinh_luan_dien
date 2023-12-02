using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class DoiSoat
    {
        #region Insert

        public static string Insert_ReturnId(string status, string dateCreated, string dateModified, string fromDate, string toDate, string note)
        {
            var cmd = new OleDbCommand("DoiSoat_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@DateCreated", dateCreated);
            cmd.Parameters.AddWithValue("@DateModified", dateModified);
            cmd.Parameters.AddWithValue("@FromDate", fromDate);
            cmd.Parameters.AddWithValue("@ToDate", toDate);
            cmd.Parameters.AddWithValue("@Note", note);
            return SqlDatabase.ExecuteScalar(cmd);
        }
        public static void Insert(string status, string dateCreated, string dateModified, string fromDate, string toDate)
        {
            var cmd = new OleDbCommand("DoiSoat_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@DateCreated", dateCreated);
            cmd.Parameters.AddWithValue("@DateModified", dateModified);
            cmd.Parameters.AddWithValue("@FromDate", fromDate);
            cmd.Parameters.AddWithValue("@ToDate", toDate);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Insert


        public static DataSet GetAllDataPaging(string pageNumbers, string returnRows, string whereClause, string orderBy)
        {
            var oleDbCommand = new OleDbCommand("DoiSoat_GetAllDataPaging") { CommandType = CommandType.StoredProcedure };
            var cmd = oleDbCommand;
            cmd.Parameters.AddWithValue("@pageNumbers", pageNumbers);
            cmd.Parameters.AddWithValue("@returnRows", returnRows);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }
    }
}
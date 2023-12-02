using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class ContactDetails
    {
        #region Insert

        public static void Insert(string vcdName, string vcdEmail, string vcdPhone, string vcdAddress, string vcdSubject, string vcdContent, string vcdParam, string dcdDateCreated, string icdStatus)
        {
            var cmd = new OleDbCommand("ContactDetails_Insert") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@vcdName", vcdName);
            cmd.Parameters.AddWithValue("@vcdEmail", vcdEmail);
            cmd.Parameters.AddWithValue("@vcdPhone", vcdPhone);
            cmd.Parameters.AddWithValue("@vcdAddress", vcdAddress);
            cmd.Parameters.AddWithValue("@vcdSubject", vcdSubject);
            cmd.Parameters.AddWithValue("@vcdContent", vcdContent);
            cmd.Parameters.AddWithValue("@vcdParam", vcdParam);
            cmd.Parameters.AddWithValue("@dcdDateCreated", dcdDateCreated);
            cmd.Parameters.AddWithValue("@icdStatus", icdStatus);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("ContactDetails_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get data
        
        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("ContactDetails_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        public static DataSet GetDataPaging(string pageNumbers, string returnRows, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("ContactDetails_GetDataPaging") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@pageNumbers", pageNumbers);
            cmd.Parameters.AddWithValue("@returnRows", returnRows);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }

        #endregion

        #region Update

        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("ContactDetails_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion
    }
}
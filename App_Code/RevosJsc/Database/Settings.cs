using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Settings
    {
        #region Get Data

        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Settings_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@order", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        #endregion

        #region Insert

        public static void Insert(string vsKey, string vsValue, string vsLang)
        {
            var cmd = new OleDbCommand("Settings_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@vsKey", vsKey);
            cmd.Parameters.AddWithValue("@vsValue", vsValue);
            cmd.Parameters.AddWithValue("@vsLang", vsLang);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Settings_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Update

        public static void Update(string vsKey, string vsValue, string vsLang)
        {
            if (vsKey.Equals("")) return;
            var cmd = new OleDbCommand("Settings_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@vsKey", vsKey);
            cmd.Parameters.AddWithValue("@vsValue", vsValue);
            cmd.Parameters.AddWithValue("@vsLang", vsLang);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }
        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("Settings_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion
    }
}
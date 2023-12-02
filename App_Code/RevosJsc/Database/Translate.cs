using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Translate
    {
        #region Insert

        public static void Insert(string ikId, string vtLang, string vtValue)
        {
            var cmd = new OleDbCommand("Translate_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@ikId", ikId);
            cmd.Parameters.AddWithValue("@vtLang", vtLang);
            cmd.Parameters.AddWithValue("@vtValue", vtValue);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Insert

        #region Update

        public static void Update(string ikId, string vtLang, string vtValue)
        {
            var cmd = new OleDbCommand("Translate_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@ikId", ikId);
            cmd.Parameters.AddWithValue("@vtLang", vtLang);
            cmd.Parameters.AddWithValue("@vtValue", vtValue);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Update

        #region Get data

        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Translate_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@order", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        #endregion Get data
    }
}
using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    /// <summary>
    /// Summary description for AdvertistmentPositions
    /// </summary>
    public class AdvertistmentPositions
    {
        #region Insert

        public static void Insert(string iapLang, string iaPosition, string vapName, string vapDescription, string vapImage, string iapStatus, string iapSortOrder, string vapParam, string dapDateCreated, string dapDateModifield)
        {
            var cmd = new OleDbCommand("AdvertistmentPositions_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@iapLang", iapLang);
            cmd.Parameters.AddWithValue("@iaPosition", iaPosition);
            cmd.Parameters.AddWithValue("@vapName", vapName);
            cmd.Parameters.AddWithValue("@vapDescription", vapDescription);
            cmd.Parameters.AddWithValue("@vapImage", vapImage);
            cmd.Parameters.AddWithValue("@iapStatus", iapStatus);
            cmd.Parameters.AddWithValue("@iapSortOrder", iapSortOrder);
            cmd.Parameters.AddWithValue("@vapParam", vapParam);
            cmd.Parameters.AddWithValue("@dapDateCreated", dapDateCreated);
            cmd.Parameters.AddWithValue("@dapDateModifield", dapDateModifield);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Update

        public static void Update(string iapLang, string iaPosition, string vapName, string vapDescription, string vapImage, string iapStatus, string iapSortOrder, string vapParam, string dapDateCreated, string dapDateModifield, string iapId)
        {
            var cmd = new OleDbCommand("AdvertistmentPositions_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@iapLang", iapLang);
            cmd.Parameters.AddWithValue("@iaPosition", iaPosition);
            cmd.Parameters.AddWithValue("@vapName", vapName);
            cmd.Parameters.AddWithValue("@vapDescription", vapDescription);
            cmd.Parameters.AddWithValue("@vapImage", vapImage);
            cmd.Parameters.AddWithValue("@iapStatus", iapStatus);
            cmd.Parameters.AddWithValue("@iapSortOrder", iapSortOrder);
            cmd.Parameters.AddWithValue("@vapParam", vapParam);
            cmd.Parameters.AddWithValue("@dapDateCreated", dapDateCreated);
            cmd.Parameters.AddWithValue("@dapDateModifield", dapDateModifield);
            cmd.Parameters.AddWithValue("@iapId", iapId);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("AdvertistmentPositions_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("AdvertistmentPositions_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get data

        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("AdvertistmentPositions_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        #endregion
    }
}
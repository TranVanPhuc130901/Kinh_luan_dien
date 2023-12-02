using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Advertistments
    {
        #region Insert

        public static void Insert(string iapId, string iaLang, string vaTitle, string vaLink, string iaTarget, string vaDescription, string vaImage, string daDateCreated, string daDateModifield, string vaParam, string iaSortOrder, string iaStatus)
        {
            var cmd = new OleDbCommand("Advertistments_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@iapId", iapId);
            cmd.Parameters.AddWithValue("@iaLang", iaLang);
            cmd.Parameters.AddWithValue("@vaTitle", vaTitle);
            cmd.Parameters.AddWithValue("@vaLink", vaLink);
            cmd.Parameters.AddWithValue("@iaTarget", iaTarget);
            cmd.Parameters.AddWithValue("@vaDescription", vaDescription);
            cmd.Parameters.AddWithValue("@vaImage", vaImage);
            cmd.Parameters.AddWithValue("@daDateCreated", daDateCreated);
            cmd.Parameters.AddWithValue("@daDateModifield", daDateModifield);
            cmd.Parameters.AddWithValue("@vaParam", vaParam);
            cmd.Parameters.AddWithValue("@iaSortOrder", iaSortOrder);
            cmd.Parameters.AddWithValue("@iaStatus", iaStatus);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Update

        public static void Update(string iapId, string iaLang, string vaTitle, string vaLink, string iaTarget, string vaDescription, string vaImage, string daDateCreated, string daDateModifield, string vaParam, string iaSortOrder, string iaStatus, string iaId)
        {
            var cmd = new OleDbCommand("Advertistments_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@iapId", iapId);
            cmd.Parameters.AddWithValue("@iaLang", iaLang);
            cmd.Parameters.AddWithValue("@vaTitle", vaTitle);
            cmd.Parameters.AddWithValue("@vaLink", vaLink);
            cmd.Parameters.AddWithValue("@iaTarget", iaTarget);
            cmd.Parameters.AddWithValue("@vaDescription", vaDescription);
            cmd.Parameters.AddWithValue("@vaImage", vaImage);
            cmd.Parameters.AddWithValue("@daDateCreated", daDateCreated);
            cmd.Parameters.AddWithValue("@daDateModifield", daDateModifield);
            cmd.Parameters.AddWithValue("@vaParam", vaParam);
            cmd.Parameters.AddWithValue("@iaSortOrder", iaSortOrder);
            cmd.Parameters.AddWithValue("@iaStatus", iaStatus);
            cmd.Parameters.AddWithValue("@iaId", iaId);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("Advertistments_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Advertistments_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get data

        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Advertistments_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        public static DataSet GetAllDataPaging(string pageNumbers, string returnRows, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("Advertistments_GetAllDataPaging") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@pageNumbers", pageNumbers);
            cmd.Parameters.AddWithValue("@returnRows", returnRows);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }

        public static DataTable GetAllData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Advertistments_GetAllData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        #endregion
    }
}
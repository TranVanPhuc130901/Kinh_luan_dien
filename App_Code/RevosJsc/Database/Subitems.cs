using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Subitems
    {
        #region Insert

        public static void Insert(string iiId, string vsiLang, string vsiApp, string vsiTitle, string vsiDescription, string vsiContent, string vsiImage, string vsiParam, string fsiPriceOld, string fsiPriceNew, string isiSortOrder, string isiStatus, string dsiDateCreated, string dsiDateModified)
        {
            var cmd = new OleDbCommand("Subitems_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@iiId", iiId);
            cmd.Parameters.AddWithValue("@vsiLang", vsiLang);
            cmd.Parameters.AddWithValue("@vsiApp", vsiApp);
            cmd.Parameters.AddWithValue("@vsiTitle", vsiTitle);
            cmd.Parameters.AddWithValue("@vsiDescription", vsiDescription);
            cmd.Parameters.AddWithValue("@vsiContent", vsiContent);
            cmd.Parameters.AddWithValue("@vsiImage", vsiImage);
            cmd.Parameters.AddWithValue("@vsiParam", vsiParam);
            cmd.Parameters.AddWithValue("@fsiPriceOld", fsiPriceOld);
            cmd.Parameters.AddWithValue("@fsiPriceNew", fsiPriceNew);
            cmd.Parameters.AddWithValue("@isiSortOrder", isiSortOrder);
            cmd.Parameters.AddWithValue("@isiStatus", isiStatus);
            cmd.Parameters.AddWithValue("@dsiDateCreated", dsiDateCreated);
            cmd.Parameters.AddWithValue("@dsiDateModified", dsiDateModified);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Insert

        #region Update

        public static void Update(string iiId, string vsiLang, string vsiApp, string vsiTitle, string vsiDescription, string vsiContent, string vsiImage, string vsiParam, string fsiPriceOld, string fsiPriceNew, string isiSortOrder, string isiStatus, string dsiDateCreated, string dsiDateModified, string isiId)
        {
            var cmd = new OleDbCommand("Subitems_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@isiId", isiId);
            cmd.Parameters.AddWithValue("@iiId", iiId);
            cmd.Parameters.AddWithValue("@vsiLang", vsiLang);
            cmd.Parameters.AddWithValue("@vsiApp", vsiApp);
            cmd.Parameters.AddWithValue("@vsiTitle", vsiTitle);
            cmd.Parameters.AddWithValue("@vsiDescription", vsiDescription);
            cmd.Parameters.AddWithValue("@vsiContent", vsiContent);
            cmd.Parameters.AddWithValue("@vsiImage", vsiImage);
            cmd.Parameters.AddWithValue("@vsiParam", vsiParam);
            cmd.Parameters.AddWithValue("@fsiPriceOld", fsiPriceOld);
            cmd.Parameters.AddWithValue("@fsiPriceNew", fsiPriceNew);
            cmd.Parameters.AddWithValue("@isiSortOrder", isiSortOrder);
            cmd.Parameters.AddWithValue("@isiStatus", isiStatus);
            cmd.Parameters.AddWithValue("@dsiDateCreated", dsiDateCreated);
            cmd.Parameters.AddWithValue("@dsiDateModified", dsiDateModified);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }
        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("Subitems_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Update

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Subitems_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Delete

        #region Get Data

        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Subitems_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@order", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        public static DataSet GetDataPaging(string pageIndex, string pageSize, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("Subitems_GetDataPaging") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }

        #endregion Get Data
    }
}
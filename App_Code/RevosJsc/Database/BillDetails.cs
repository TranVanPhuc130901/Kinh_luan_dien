using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    /// <summary>
    /// Summary description for BillDetail
    /// </summary>
    public class BillDetails
    {
        #region Insert

        public static void Insert(string ibId, string vbdTitle, string ibdQuantity, string fbdPriceOld, string fbdPriceNew, string vbdParam, string dbdDateCreated, string dbdDateModified)
        {
            var cmd = new OleDbCommand("BillDetails_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@ibId", ibId);
            cmd.Parameters.AddWithValue("@vbdTitle", vbdTitle);
            cmd.Parameters.AddWithValue("@ibdQuantity", ibdQuantity);
            cmd.Parameters.AddWithValue("@fbdPriceOld", fbdPriceOld);
            cmd.Parameters.AddWithValue("@fbdPriceNew", fbdPriceNew);
            cmd.Parameters.AddWithValue("@vbdParam", vbdParam);
            cmd.Parameters.AddWithValue("@dbdDateCreated", dbdDateCreated);
            cmd.Parameters.AddWithValue("@dbdDateModified", dbdDateModified);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get

        public static DataTable GetData(string top, string fields, string condition, string order)
        {
            var cmd = new OleDbCommand("BillDetails_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@order", order);
            return SqlDatabase.GetData(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("BillDetails_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion
    }
}

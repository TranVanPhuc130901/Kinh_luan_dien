using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class DoiSoatChiTiet
    {
        #region Insert

        public static string Insert(string hoaDonID, string price, string code, string fromDate, string toDate, string dateCreated, string bankName, string bankAccount, string bankHolder)
        {
            var cmd = new OleDbCommand("DoiSoatChiTiet_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@HoaDonID", hoaDonID);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Code", code);
            cmd.Parameters.AddWithValue("@FromDate", fromDate);
            cmd.Parameters.AddWithValue("@ToDate", toDate);
            cmd.Parameters.AddWithValue("@DateCreated", dateCreated);
            cmd.Parameters.AddWithValue("@BankName", bankName);
            cmd.Parameters.AddWithValue("@BankAccount", bankAccount);
            cmd.Parameters.AddWithValue("@BankHolder", bankHolder);
            return SqlDatabase.ExecuteScalar(cmd);
        }

        #endregion Insert
    }
}
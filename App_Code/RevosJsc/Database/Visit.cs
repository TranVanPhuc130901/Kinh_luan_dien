using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Visit
    {
        #region Insert
        
        public static void Insert(string itemId, string groupId, string referrer, string ip, string device, string browser, string account, string name, string gender, string email, string phone, string dateCreated, string code)
        {
            var cmd = new OleDbCommand("INSERT INTO Visit VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)") {CommandType = CommandType.Text};
            cmd.Parameters.AddWithValue("@ItemId", itemId);
            cmd.Parameters.AddWithValue("@GroupId", groupId);
            cmd.Parameters.AddWithValue("@Code", code);
            cmd.Parameters.AddWithValue("@Referrer", referrer);
            cmd.Parameters.AddWithValue("@IP", ip);
            cmd.Parameters.AddWithValue("@Device", device);
            cmd.Parameters.AddWithValue("@Browser", browser);
            cmd.Parameters.AddWithValue("@Account", account);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Gender", gender);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@DateCreated", dateCreated);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Insert
    }
}
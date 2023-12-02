using System.Data;
using System.Data.OleDb;
using RevosJsc.Extension;

namespace RevosJsc.Database
{
    public class Users
    {
        #region Get data

        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Users_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@order", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        public static DataSet GetDataPaging(string pageIndex, string pageSize, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("Users_GetDataPaging") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }

        #endregion

        #region Insert

        public static void Insert(string vuRole, string vuAccount, string vuPassword, string vuVerificationCode, string vuFirstName, string vuLastName, string vuAddress, string vuPhoneNumber, string vuEmail, string vuImage, string vuIdentityCard, string vuSecurityQuestion, string vuSecurityAnswer, string iuStatus, string duDateCreated, string duDateModified, string duExpirationDate, string vuParam)
        {
            vuPassword = SecurityExtension.BuildPassword(vuPassword);
            var cmd = new OleDbCommand("Users_Insert") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@vuRole", vuRole);
            cmd.Parameters.AddWithValue("@vuAccount", vuAccount);
            cmd.Parameters.AddWithValue("@vuPassword", vuPassword);
            cmd.Parameters.AddWithValue("@vuVerificationCode", vuVerificationCode);
            cmd.Parameters.AddWithValue("@vuFirstName", vuFirstName);
            cmd.Parameters.AddWithValue("@vuLastName", vuLastName);
            cmd.Parameters.AddWithValue("@vuAddress", vuAddress);
            cmd.Parameters.AddWithValue("@vuPhoneNumber", vuPhoneNumber);
            cmd.Parameters.AddWithValue("@vuEmail", vuEmail);
            cmd.Parameters.AddWithValue("@vuImage", vuImage);
            cmd.Parameters.AddWithValue("@vuIdentityCard", vuIdentityCard);
            cmd.Parameters.AddWithValue("@vuSecurityQuestion", vuSecurityQuestion);
            cmd.Parameters.AddWithValue("@vuSecurityAnswer", vuSecurityAnswer);
            cmd.Parameters.AddWithValue("@iuStatus", iuStatus);
            cmd.Parameters.AddWithValue("@duDateCreated", duDateCreated);
            cmd.Parameters.AddWithValue("@duDateModified", duDateModified);
            cmd.Parameters.AddWithValue("@duExpirationDate", duExpirationDate);
            cmd.Parameters.AddWithValue("@vuParam", vuParam);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Update

        public static void Update(string iuId, string vuRole, string vuVerificationCode, string vuFirstName, string vuLastName, string vuAddress, string vuPhoneNumber, string vuEmail, string vuImage, string vuIdentityCard, string vuSecurityQuestion, string vuSecurityAnswer, string iuStatus, string duDateCreated, string duDateModified, string duExpirationDate, string vuParam)
        {
            var cmd = new OleDbCommand("Users_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@iuId", iuId);
            cmd.Parameters.AddWithValue("@vuRole", vuRole);
            cmd.Parameters.AddWithValue("@vuVerificationCode", vuVerificationCode);
            cmd.Parameters.AddWithValue("@vuFirstName", vuFirstName);
            cmd.Parameters.AddWithValue("@vuLastName", vuLastName);
            cmd.Parameters.AddWithValue("@vuAddress", vuAddress);
            cmd.Parameters.AddWithValue("@vuPhoneNumber", vuPhoneNumber);
            cmd.Parameters.AddWithValue("@vuEmail", vuEmail);
            cmd.Parameters.AddWithValue("@vuImage", vuImage);
            cmd.Parameters.AddWithValue("@vuIdentityCard", vuIdentityCard);
            cmd.Parameters.AddWithValue("@vuSecurityQuestion", vuSecurityQuestion);
            cmd.Parameters.AddWithValue("@vuSecurityAnswer", vuSecurityAnswer);
            cmd.Parameters.AddWithValue("@iuStatus", iuStatus);
            cmd.Parameters.AddWithValue("@duDateCreated", duDateCreated);
            cmd.Parameters.AddWithValue("@duDateModified", duDateModified);
            cmd.Parameters.AddWithValue("@duExpirationDate", duExpirationDate);
            cmd.Parameters.AddWithValue("@vuParam", vuParam);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }
        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("Users_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Users_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion


    }
}

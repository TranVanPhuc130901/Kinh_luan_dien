using System.Data;
using System.Data.OleDb;
using RevosJsc.Extension;

namespace RevosJsc.Database
{
    public class Members
    {
        #region Insert

        public static void Insert(string vmApp, string vmAccount, string vmPassword, string vmFirstName, string vmLastName, string vmAddress, string vmPhone, string vmEmail, string dmBirthday, string vmIdentityCard, string vmRelationship, string vmEdu, string vmJob, string vmSocialNetwork, string vmImage, string vmSecurityQuestion, string vmSecurityAnswer, string imStatus, string dmDateCreated, string dmDateModified, string dmExpirationDate, string vmComment, string vmWeight, string vmHeight)
        {
            vmPassword = SecurityExtension.BuildPassword(vmPassword);
            var cmd = new OleDbCommand("Members_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@vmApp", vmApp);
            cmd.Parameters.AddWithValue("@vmAccount", vmAccount);
            cmd.Parameters.AddWithValue("@vmPassword", vmPassword);
            cmd.Parameters.AddWithValue("@vmFirstName", vmFirstName);
            cmd.Parameters.AddWithValue("@vmLastName", vmLastName);
            cmd.Parameters.AddWithValue("@vmAddress", vmAddress);
            cmd.Parameters.AddWithValue("@vmPhone", vmPhone);
            cmd.Parameters.AddWithValue("@vmEmail", vmEmail);
            cmd.Parameters.AddWithValue("@dmBirthday", dmBirthday);
            cmd.Parameters.AddWithValue("@vmIdentityCard", vmIdentityCard);
            cmd.Parameters.AddWithValue("@vmRelationship", vmRelationship);
            cmd.Parameters.AddWithValue("@vmEdu", vmEdu);
            cmd.Parameters.AddWithValue("@vmJob", vmJob);
            cmd.Parameters.AddWithValue("@vmSocialNetwork", vmSocialNetwork);
            cmd.Parameters.AddWithValue("@vmImage", vmImage);
            cmd.Parameters.AddWithValue("@vmSecurityQuestion", vmSecurityQuestion);
            cmd.Parameters.AddWithValue("@vmSecurityAnswer", vmSecurityAnswer);
            cmd.Parameters.AddWithValue("@imStatus", imStatus);
            cmd.Parameters.AddWithValue("@dmDateCreated", dmDateCreated);
            cmd.Parameters.AddWithValue("@dmDateModified", dmDateModified);
            cmd.Parameters.AddWithValue("@dmExpirationDate", dmExpirationDate);
            cmd.Parameters.AddWithValue("@vmComment", vmComment);
            cmd.Parameters.AddWithValue("@vmWeight", vmWeight);
            cmd.Parameters.AddWithValue("@vmHeight", vmHeight);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Update

        public static void Update(string vmApp, string vmAccount, string vmPassword, string vmFirstName, string vmLastName, string vmAddress, string vmPhone, string vmEmail, string dmBirthday, string vmIdentityCard, string vmRelationship, string vmEdu, string vmJob, string vmSocialNetwork, string vmImage, string vmSecurityQuestion, string vmSecurityAnswer, string imStatus, string dmDateCreated, string dmDateModified, string dmExpirationDate, string vmComment, string vmWeight, string vmHeight, string imId)
        {
            var cmd = new OleDbCommand("Members_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@vmApp", vmApp);
            cmd.Parameters.AddWithValue("@vmAccount", vmAccount);
            cmd.Parameters.AddWithValue("@vmPassword", vmPassword);
            cmd.Parameters.AddWithValue("@vmFirstName", vmFirstName);
            cmd.Parameters.AddWithValue("@vmLastName", vmLastName);
            cmd.Parameters.AddWithValue("@vmAddress", vmAddress);
            cmd.Parameters.AddWithValue("@vmPhone", vmPhone);
            cmd.Parameters.AddWithValue("@vmEmail", vmEmail);
            cmd.Parameters.AddWithValue("@dmBirthday", dmBirthday);
            cmd.Parameters.AddWithValue("@vmIdentityCard", vmIdentityCard);
            cmd.Parameters.AddWithValue("@vmRelationship", vmRelationship);
            cmd.Parameters.AddWithValue("@vmEdu", vmEdu);
            cmd.Parameters.AddWithValue("@vmJob", vmJob);
            cmd.Parameters.AddWithValue("@vmSocialNetwork", vmSocialNetwork);
            cmd.Parameters.AddWithValue("@vmImage", vmImage);
            cmd.Parameters.AddWithValue("@vmSecurityQuestion", vmSecurityQuestion);
            cmd.Parameters.AddWithValue("@vmSecurityAnswer", vmSecurityAnswer);
            cmd.Parameters.AddWithValue("@imStatus", imStatus);
            cmd.Parameters.AddWithValue("@dmDateCreated", dmDateCreated);
            cmd.Parameters.AddWithValue("@dmDateModified", dmDateModified);
            cmd.Parameters.AddWithValue("@dmExpirationDate", dmExpirationDate);
            cmd.Parameters.AddWithValue("@vmComment", vmComment);
            cmd.Parameters.AddWithValue("@vmWeight", vmWeight);
            cmd.Parameters.AddWithValue("@vmHeight", vmHeight);
            cmd.Parameters.AddWithValue("@imId", imId);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("Members_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        public static void UpdatePassword(string imId, string vmPassword)
        {
            vmPassword = SecurityExtension.BuildPassword(vmPassword);
            var cmd = new OleDbCommand("Members_UpdatePassword") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@imId", imId);
            cmd.Parameters.AddWithValue("@vmPassword", vmPassword);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        public static void UpdatePasswordByAccount(string vmAccount, string vmPassword)
        {
            vmPassword = SecurityExtension.BuildPassword(vmPassword);
            var cmd = new OleDbCommand("Members_UpdatePasswordByAccount") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@vmAccount", vmAccount);
            cmd.Parameters.AddWithValue("@vmPassword", vmPassword);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get data

        /// <summary>
        /// Lấy dữ liệu từ bảng LanguageKey
        /// </summary>
        /// <param name="top">Số lượng bản ghi lấy ra</param>
        /// <param name="fields">Trường dữ liệu cần lấy (Điền * để lấy tất cả các trường dữ liệu)</param>
        /// <param name="condition">Điều kiện lấy dữ liệu</param>
        /// <param name="orderBy">Điều kiện sắp xếp dữ liệu</param>
        /// <returns></returns>
        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Members_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        public static DataSet GetDataPaging(string pageIndex, string pageSize, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("Members_GetDataPaging") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Members_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

    }
}

using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Contacts
    {
        #region Insert

        public static void Insert(string icParentId, string icSortOrder, string icStatus, string vcLang, string vcName, string vcAddress, string vcPhone, string vcHotline, string vcEmail, string vcMap, string vcImage, string vcParam, string dcDateCreated, string diDateModified)
        {
            var cmd = new OleDbCommand("Contacts_Insert") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@icParentId", icParentId);
            cmd.Parameters.AddWithValue("@icSortOrder", icSortOrder);
            cmd.Parameters.AddWithValue("@icStatus", icStatus);
            cmd.Parameters.AddWithValue("@vcLang", vcLang);
            cmd.Parameters.AddWithValue("@vcName", vcName);
            cmd.Parameters.AddWithValue("@vcAddress", vcAddress);
            cmd.Parameters.AddWithValue("@vcPhone", vcPhone);
            cmd.Parameters.AddWithValue("@vcHotline", vcHotline);
            cmd.Parameters.AddWithValue("@vcEmail", vcEmail);
            cmd.Parameters.AddWithValue("@vcMap", vcMap);
            cmd.Parameters.AddWithValue("@vcImage", vcImage);
            cmd.Parameters.AddWithValue("@vcParam", vcParam);
            cmd.Parameters.AddWithValue("@dcDateCreated", dcDateCreated);
            cmd.Parameters.AddWithValue("@diDateModified", diDateModified);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Update

        public static void Update(string icParentId, string icSortOrder, string icStatus, string vcLang, string vcName, string vcAddress, string vcPhone, string vcHotline, string vcEmail, string vcMap, string vcImage, string vcParam, string dcDateCreated, string diDateModified, string icId)
        {
            var cmd = new OleDbCommand("Contacts_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@icParentId", icParentId);
            cmd.Parameters.AddWithValue("@icSortOrder", icSortOrder);
            cmd.Parameters.AddWithValue("@icStatus", icStatus);
            cmd.Parameters.AddWithValue("@vcLang", vcLang);
            cmd.Parameters.AddWithValue("@vcName", vcName);
            cmd.Parameters.AddWithValue("@vcAddress", vcAddress);
            cmd.Parameters.AddWithValue("@vcPhone", vcPhone);
            cmd.Parameters.AddWithValue("@vcHotline", vcHotline);
            cmd.Parameters.AddWithValue("@vcEmail", vcEmail);
            cmd.Parameters.AddWithValue("@vcMap", vcMap);
            cmd.Parameters.AddWithValue("@vcImage", vcImage);
            cmd.Parameters.AddWithValue("@vcParam", vcParam);
            cmd.Parameters.AddWithValue("@dcDateCreated", dcDateCreated);
            cmd.Parameters.AddWithValue("@diDateModified", diDateModified);
            cmd.Parameters.AddWithValue("@icId", icId);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("Contacts_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Contacts_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get data
        
        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Contacts_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        public static DataTable GetAllContact(string fields, string condition, string orderby)
        {
            var cmd = new OleDbCommand("Contacts_GetAllContacts") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderby);
            return SqlDatabase.GetData(cmd);
        }
        
        #endregion
    }
}
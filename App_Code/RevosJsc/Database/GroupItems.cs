using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class GroupItems
    {
        #region Insert

        public static void Insert(string igId, string iiId, string vgiGenealogy, string dgiDateCreated, string dgiDateModified)
        {
            var cmd = new OleDbCommand("GroupItems_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@igId", igId);
            cmd.Parameters.AddWithValue("@iiId", iiId);
            cmd.Parameters.AddWithValue("@vgiGenealogy", vgiGenealogy);
            cmd.Parameters.AddWithValue("@dgiDateCreated", dgiDateCreated);
            cmd.Parameters.AddWithValue("@dgiDateModified", dgiDateModified);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        public static void InsertItemGroupItem(string viLang, string viApp, string viCode, string viTitle, string viDescription, string viContent, string viImage, string viAuthor, string viMetaTitle, string viMetaKeyword, string viMetaDescription, string viTag, string viLink, string fiPriceOld, string fiPriceNew, string viParam, string iiTotalView, string iiSortOrder, string diDateCreated, string diDateModified, string iiStatus, string igId, string dgiDateCreated, string dgiDateModified)
        {
            var cmd = new OleDbCommand("ItemsGroupItems_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@viLang", viLang);
            cmd.Parameters.AddWithValue("@viApp", viApp);
            cmd.Parameters.AddWithValue("@viCode", viCode);
            cmd.Parameters.AddWithValue("@viTitle", viTitle);
            cmd.Parameters.AddWithValue("@viDescription", viDescription);
            cmd.Parameters.AddWithValue("@viContent", viContent);
            cmd.Parameters.AddWithValue("@viImage", viImage);
            cmd.Parameters.AddWithValue("@viAuthor", viAuthor);
            cmd.Parameters.AddWithValue("@viMetaTitle", viMetaTitle);
            cmd.Parameters.AddWithValue("@viMetaKeyword", viMetaKeyword);
            cmd.Parameters.AddWithValue("@viMetaDescription", viMetaDescription);
            cmd.Parameters.AddWithValue("@viTag", viTag);
            cmd.Parameters.AddWithValue("@viLink", viLink);
            cmd.Parameters.AddWithValue("@fiPriceOld", fiPriceOld);
            cmd.Parameters.AddWithValue("@fiPriceNew", fiPriceNew);
            cmd.Parameters.AddWithValue("@viParam", viParam);
            cmd.Parameters.AddWithValue("@iiTotalView", iiTotalView);
            cmd.Parameters.AddWithValue("@iiSortOrder", iiSortOrder);
            cmd.Parameters.AddWithValue("@diDateCreated", diDateCreated);
            cmd.Parameters.AddWithValue("@diDateModified", diDateModified);
            cmd.Parameters.AddWithValue("@iiStatus", iiStatus);
            cmd.Parameters.AddWithValue("@igId", igId);
            cmd.Parameters.AddWithValue("@dgiDateCreated", dgiDateCreated);
            cmd.Parameters.AddWithValue("@dgiDateModified", dgiDateModified);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }
        public static string InsertItemGroupItem_returnItemsId(string viLang, string viApp, string viCode, string viTitle, string viDescription, string viContent, string viImage, string viAuthor, string viMetaTitle, string viMetaKeyword, string viMetaDescription, string viTag, string viLink, string fiPriceOld, string fiPriceNew, string viParam, string iiTotalView, string iiSortOrder, string diDateCreated, string diDateModified, string iiStatus, string igId, string dgiDateCreated, string dgiDateModified)
        {
            var cmd = new OleDbCommand("ItemsGroupItems_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@viLang", viLang);
            cmd.Parameters.AddWithValue("@viApp", viApp);
            cmd.Parameters.AddWithValue("@viCode", viCode);
            cmd.Parameters.AddWithValue("@viTitle", viTitle);
            cmd.Parameters.AddWithValue("@viDescription", viDescription);
            cmd.Parameters.AddWithValue("@viContent", viContent);
            cmd.Parameters.AddWithValue("@viImage", viImage);
            cmd.Parameters.AddWithValue("@viAuthor", viAuthor);
            cmd.Parameters.AddWithValue("@viMetaTitle", viMetaTitle);
            cmd.Parameters.AddWithValue("@viMetaKeyword", viMetaKeyword);
            cmd.Parameters.AddWithValue("@viMetaDescription", viMetaDescription);
            cmd.Parameters.AddWithValue("@viTag", viTag);
            cmd.Parameters.AddWithValue("@viLink", viLink);
            cmd.Parameters.AddWithValue("@fiPriceOld", fiPriceOld);
            cmd.Parameters.AddWithValue("@fiPriceNew", fiPriceNew);
            cmd.Parameters.AddWithValue("@viParam", viParam);
            cmd.Parameters.AddWithValue("@iiTotalView", iiTotalView);
            cmd.Parameters.AddWithValue("@iiSortOrder", iiSortOrder);
            cmd.Parameters.AddWithValue("@diDateCreated", diDateCreated);
            cmd.Parameters.AddWithValue("@diDateModified", diDateModified);
            cmd.Parameters.AddWithValue("@iiStatus", iiStatus);
            cmd.Parameters.AddWithValue("@igId", igId);
            cmd.Parameters.AddWithValue("@dgiDateCreated", dgiDateCreated);
            cmd.Parameters.AddWithValue("@dgiDateModified", dgiDateModified);
            return SqlDatabase.ExecuteScalar(cmd);
        }

        #endregion

        #region Update

        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("GroupItems_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        public static void UpdateItemGroupItem(string viLang, string viApp, string viCode, string viTitle, string viDescription, string viContent, string viImage, string viAuthor, string viMetaTitle, string viMetaKeyword, string viMetaDescription, string viTag, string viLink, string fiPriceOld, string fiPriceNew, string viParam, string iiTotalView, string iiSortOrder, string diDateCreated, string diDateModified, string iiStatus, string igId, string dgiDateCreated, string dgiDateModified, string iiId, string oldIgId)
        {
            var cmd = new OleDbCommand("ItemsGroupItems_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@viLang", viLang);
            cmd.Parameters.AddWithValue("@viApp", viApp);
            cmd.Parameters.AddWithValue("@viCode", viCode);
            cmd.Parameters.AddWithValue("@viTitle", viTitle);
            cmd.Parameters.AddWithValue("@viDescription", viDescription);
            cmd.Parameters.AddWithValue("@viContent", viContent);
            cmd.Parameters.AddWithValue("@viImage", viImage);
            cmd.Parameters.AddWithValue("@viAuthor", viAuthor);
            cmd.Parameters.AddWithValue("@viMetaTitle", viMetaTitle);
            cmd.Parameters.AddWithValue("@viMetaKeyword", viMetaKeyword);
            cmd.Parameters.AddWithValue("@viMetaDescription", viMetaDescription);
            cmd.Parameters.AddWithValue("@viTag", viTag);
            cmd.Parameters.AddWithValue("@viLink", viLink);
            cmd.Parameters.AddWithValue("@fiPriceOld", fiPriceOld);
            cmd.Parameters.AddWithValue("@fiPriceNew", fiPriceNew);
            cmd.Parameters.AddWithValue("@viParam", viParam);
            cmd.Parameters.AddWithValue("@iiTotalView", iiTotalView);
            cmd.Parameters.AddWithValue("@iiSortOrder", iiSortOrder);
            cmd.Parameters.AddWithValue("@diDateCreated", diDateCreated);
            cmd.Parameters.AddWithValue("@diDateModified", diDateModified);
            cmd.Parameters.AddWithValue("@iiStatus", iiStatus);
            cmd.Parameters.AddWithValue("@igId", igId);
            cmd.Parameters.AddWithValue("@dgiDateCreated", dgiDateCreated);
            cmd.Parameters.AddWithValue("@dgiDateModified", dgiDateModified);
            cmd.Parameters.AddWithValue("@iiId", iiId);
            cmd.Parameters.AddWithValue("@oldIgId", oldIgId);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            if (string.IsNullOrEmpty(condition)) return;
            var cmd = new OleDbCommand("GroupItems_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get data

        public static DataTable GetGroupItem(string top, string fields, string condition, string order)
        {
            var cmd = new OleDbCommand("GroupItems_GetData") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@order", order);
            return SqlDatabase.GetData(cmd);
        }

        public static DataTable GetAllData(string top, string fields, string condition, string orderby)
        {
            var cmd = new OleDbCommand("GroupsGroupItemsItems_GetAllData") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderby", orderby);
            return SqlDatabase.GetData(cmd);
        }

        public static DataSet GetAllDataPaging(string pageNumbers, string returnRows, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("GroupsGroupItemsItems_GetAllDataPaging") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@pageNumbers", pageNumbers);
            cmd.Parameters.AddWithValue("@returnRows", returnRows);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }

        public static DataSet GetDataPaging(string pageNumbers, string returnRows, string fields, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("GroupsGroupItemsItems_GetAllDataPaging_Field") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@pageNumbers", pageNumbers);
            cmd.Parameters.AddWithValue("@returnRows", returnRows);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }

        #endregion
    }
}
using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Items
    {
        #region Insert

        public static void Insert(string viLang, string viApp, string viCode, string viTitle, string viDescription, string viContent, string viImage, string viAuthor, string viMetaTitle, string viMetaKeyword, string viMetaDescription, string viTag, string viLink, string fiPriceOld, string fiPriceNew, string viParam, string iiTotalView, string iiSortOrder, string diDateCreated, string diDateModified, string iiStatus)
        {
            var cmd = new OleDbCommand("Items_Insert") {CommandType = CommandType.StoredProcedure};
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
            SqlDatabase.ExecuteNoneQuery(cmd);
        }
        public static void InsertProduct(string viLang, string viApp, string viCode, string viTitle, string viDescription, string viContent, string viImage, string viAuthor, string viMetaTitle, string viMetaKeyword, string viMetaDescription, string viTag, string viLink, string fiPriceOld, string fiPriceNew, string viParam, string iiTotalView, string iiSortOrder, string diDateCreated, string diDateModified, string iiStatus, string promotionStartDate, string promotionEndDate, string variant, string masterId, string inventory)
        {
            var cmd = new OleDbCommand("Items_Insert") { CommandType = CommandType.StoredProcedure };
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
            cmd.Parameters.AddWithValue("@PromotionStartDate", promotionStartDate);
            cmd.Parameters.AddWithValue("@PromotionEndDate", promotionEndDate);
            cmd.Parameters.AddWithValue("@Variant", variant);
            cmd.Parameters.AddWithValue("@MasterId", masterId);
            cmd.Parameters.AddWithValue("@Inventory", inventory);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Insert

        #region Update

        public static void Update(string viLang, string viApp, string viCode, string viTitle, string viDescription, string viContent, string viImage, string viAuthor, string viMetaTitle, string viMetaKeyword, string viMetaDescription, string viTag, string viLink, string fiPriceOld, string fiPriceNew, string viParam, string iiTotalView, string iiSortOrder, string diDateCreated, string diDateModified, string iiStatus, string iiId)
        {
            var cmd = new OleDbCommand("Items_Update") {CommandType = CommandType.StoredProcedure};
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
            cmd.Parameters.AddWithValue("@iiId", iiId);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }
        public static void UpdateProduct(string viLang, string viApp, string viCode, string viTitle, string viDescription, string viContent, string viImage, string viAuthor, string viMetaTitle, string viMetaKeyword, string viMetaDescription, string viTag, string viLink, string fiPriceOld, string fiPriceNew, string viParam, string iiTotalView, string iiSortOrder, string diDateCreated, string diDateModified, string iiStatus, string promotionStartDate, string promotionEndDate, string variant, string masterId, string inventory, string iiId)
        {
            var cmd = new OleDbCommand("Items_Update") { CommandType = CommandType.StoredProcedure };
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
            cmd.Parameters.AddWithValue("@PromotionStartDate", promotionStartDate);
            cmd.Parameters.AddWithValue("@PromotionEndDate", promotionEndDate);
            cmd.Parameters.AddWithValue("@Variant", variant);
            cmd.Parameters.AddWithValue("@MasterId", masterId);
            cmd.Parameters.AddWithValue("@Inventory", inventory);
            cmd.Parameters.AddWithValue("@iiId", iiId);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("Items_UpdateValues") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Update

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Items_Delete") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion Delete

        #region Get data

        /// <summary>
        /// Lấy thông tin của Items theo title
        /// </summary>
        /// <param name="title"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable GetByTitle(string title, string condition)
        {
            var cmd = new OleDbCommand("Items_GetDataByTitle") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@condition", condition);
            return SqlDatabase.GetData(cmd);
        }

        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Items_GetData") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@order", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        public static DataSet GetDataPaging(string pageIndex, string pageSize, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("Items_GetDataPaging") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@pageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@pageSize", pageSize);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }

        #endregion Get data
    }
}
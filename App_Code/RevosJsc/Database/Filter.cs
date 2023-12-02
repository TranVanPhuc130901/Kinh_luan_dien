using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Filter
    {
        #region Insert

        public static void Insert(string ifParentId, string vfLang, string ifSortOrder, string vfName, string vfDescription, string vfImage, string vfParam, string dfDateCreated, string dfDateModified, string ifType, string ifStatus)
        {
            var cmd = new OleDbCommand("Filter_Insert") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@ifParentId", ifParentId);
            cmd.Parameters.AddWithValue("@vfLang", vfLang);
            cmd.Parameters.AddWithValue("@ifSortOrder", ifSortOrder);
            cmd.Parameters.AddWithValue("@vfName", vfName);
            cmd.Parameters.AddWithValue("@vfDesc", vfDescription);
            cmd.Parameters.AddWithValue("@vfImage", vfImage);
            cmd.Parameters.AddWithValue("@vfParam", vfParam);
            cmd.Parameters.AddWithValue("@dfDateCreated", dfDateCreated);
            cmd.Parameters.AddWithValue("@dfDateModified", dfDateModified);
            cmd.Parameters.AddWithValue("@ifType", ifType);
            cmd.Parameters.AddWithValue("@ifStatus", ifStatus);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Update

        public static void Update(string ifParentId, string vfLang, string ifSortOrder, string vfName, string vfDescription, string vfImage, string vfParam, string dfDateCreated, string dfDateModified, string ifType, string ifStatus, string ifId)
        {
            var cmd = new OleDbCommand("Filter_Update") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@ifParentId", ifParentId);
            cmd.Parameters.AddWithValue("@vfLang", vfLang);
            cmd.Parameters.AddWithValue("@ifSortOrder", ifSortOrder);
            cmd.Parameters.AddWithValue("@vfName", vfName);
            cmd.Parameters.AddWithValue("@vfDesc", vfDescription);
            cmd.Parameters.AddWithValue("@vfImage", vfImage);
            cmd.Parameters.AddWithValue("@vfParam", vfParam);
            cmd.Parameters.AddWithValue("@dfDateCreated", dfDateCreated);
            cmd.Parameters.AddWithValue("@dfDateModified", dfDateModified);
            cmd.Parameters.AddWithValue("@vfType", ifType);
            cmd.Parameters.AddWithValue("@ifStatus", ifStatus);
            cmd.Parameters.AddWithValue("@ifId", ifId);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        public static void UpdateValues(string values, string condition)
        {
            var cmd = new OleDbCommand("Filter_UpdateValues") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@values", values);
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Delete

        public static void Delete(string condition)
        {
            var cmd = new OleDbCommand("Filter_Delete") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@condition", condition);
            SqlDatabase.ExecuteNoneQuery(cmd);
        }

        #endregion

        #region Get data

        /// <summary>
        /// Lấy dữ liệu từ bảng Filter
        /// </summary>
        /// <param name="top">Số lượng bản ghi lấy ra</param>
        /// <param name="fields">Trường dữ liệu cần lấy (Điền * để lấy tất cả các trường dữ liệu)</param>
        /// <param name="condition">Điều kiện lấy dữ liệu</param>
        /// <param name="orderBy">Điều kiện sắp xếp dữ liệu</param>
        /// <returns></returns>
        public static DataTable GetData(string top, string fields, string condition, string orderBy)
        {
            var cmd = new OleDbCommand("Filter_GetData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData(cmd);
        }

        public static DataTable GetAllData(string fields, string condition, string orderby)
        {
            var cmd = new OleDbCommand("Filter_GetAllData") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@fields", fields);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@orderBy", orderby);
            return SqlDatabase.GetData(cmd);
        }

        public static DataSet GetDataPaging(string pageNumbers, string returnRows, string whereClause, string orderBy)
        {
            var cmd = new OleDbCommand("Filter_GetDataPaging") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@pageNumbers", pageNumbers);
            cmd.Parameters.AddWithValue("@returnRows", returnRows);
            cmd.Parameters.AddWithValue("@whereClause", whereClause);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }
        #endregion
    }
}
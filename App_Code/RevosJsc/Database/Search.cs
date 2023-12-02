using System.Data;
using System.Data.OleDb;

namespace RevosJsc.Database
{
    public class Search
    {
        #region Groups + Items

        public static DataTable FromGroupsItems(string top, string conditionGroups, string conditionItems, string orderBy)
        {
            var cmd = new OleDbCommand("Search_GroupsItems") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@top", top);
            cmd.Parameters.AddWithValue("@conditionGroups", conditionGroups);
            cmd.Parameters.AddWithValue("@conditionItems", conditionItems);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData(cmd);
        }
        public static DataSet FromGroupsItemsPaging(string pageIndex, string pageSize, string conditionGroups, string conditionItems, string orderBy)
        {
            var cmd = new OleDbCommand("Search_GroupsItems_Paging") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.AddWithValue("@conditionGroups", conditionGroups);
            cmd.Parameters.AddWithValue("@conditionItems", conditionItems);
            cmd.Parameters.AddWithValue("@orderBy", orderBy);
            return SqlDatabase.GetData_OverDataset(cmd);
        }

        #endregion
    }
}
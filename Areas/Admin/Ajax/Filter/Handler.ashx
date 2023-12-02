<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public class Handler : IHttpHandler {

    private readonly JavaScriptSerializer _js = new JavaScriptSerializer();
    public void ProcessRequest (HttpContext context) {
        var action = context.Request["action"];
        //context.Response.ContentType = "json";
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) return;
        switch (action)
        {
            case "OnOffFilter":
                OnOffFilter(context);
                break;
            case "UpdateOrderFilter":
                UpdateOrderFilter(context);
                break;
            case "DeleteFilter":
                DeleteFilter(context);
                break;
            case "DeleteListFilter":
                DeleteListFilter(context);
                break;
            case "RestoreFilter":
                RestoreFilter(context);
                break;
            case "DeleteRecFilter":
                DeleteRecFilter(context);
                break;
            case "DeleteRecListFilter":
                DeleteRecListFilter(context);
                break;
        }
    }
    private void DeleteRecListFilter(HttpContext context)
    {
        var listId = context.Request["listId"];
        var control = context.Request["control"];
        var pic = context.Request["pic"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        var fields = DataExtension.GetListColumns(FilterColumns.VfName, FilterColumns.IfId);
        var dt = Filter.GetData("", fields, "ifId in (" + listId + ")", "");
        var listIdHasDel = "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            DeleteRecFilter(control, dt.Rows[i][FilterColumns.IfId].ToString(), dt.Rows[i][FilterColumns.VfName].ToString(), pic, ref listIdHasDel);
        }
        context.Response.Write(_js.Serialize(new[] {listIdHasDel}));
    }

    private void DeleteRecFilter(HttpContext context)
    {
        var control = context.Request["control"];
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        var pic = context.Request["pic"];
        var listIdHasDel = "";

        DeleteRecFilter(control, id, titleItem, pic, ref listIdHasDel);

        context.Response.Write(_js.Serialize(new[] {listIdHasDel}));
    }

    private static void DeleteRecFilter(string control, string id, string titleItem, string pic, ref string listIdHasDel)
    {
        var condition = " CHARINDEX('," + id + ",',vfGenealogy) > 0 ";
        var dt = Filter.GetData("", FilterColumns.IfId + ", " + FilterColumns.VfName + ", " + FilterColumns.VfImage, condition, "");

        #region Xóa danh mục + ảnh đại diện
        const string split = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            listIdHasDel = listIdHasDel + "," + dt.Rows[i][FilterColumns.IfId];
            if (dt.Rows[i][FilterColumns.VfImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(pic, dt.Rows[i][FilterColumns.VfImage].ToString());
            else
            {
                foreach (var s in dt.Rows[i][FilterColumns.VfImage].ToString().Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
                {
                    ImagesExtension.DeleteImageWhenDeleteItem(pic, s);
                }
            }
            // Xóa các bản ghi ở Filter theo ifid
            Filter.Delete("ifId = '" + dt.Rows[i][FilterColumns.IfId] + "'");
        }
        #endregion

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(control, "RecycleCategory"), logCreateDate + ": " + logAuthor + " xóa khỏi thùng rác " + titleItem + (dt.Rows.Count > 1 ? " và các mục con":""), logAuthor, logCreateDate);
        #endregion
    }

    private void RestoreFilter(HttpContext context)
    {
        var control = context.Request["control"];
        var typePage = context.Request["typePage"];
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        var dt = Filter.GetData("", FilterColumns.IfParentId, FilterTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var groupName = "";
            if (CheckParentStatus(dt.Rows[0][FilterColumns.IfParentId].ToString(), ref groupName))
            {
                context.Response.Write(_js.Serialize(new [] {groupName}));
                return;
            }
        }
        string[] fields = { FilterColumns.IfStatus };
        string[] values = { "1" };
        Filter.UpdateValues(DataExtension.UpdateValues(fields, values), FilterTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(control, typePage), logCreateDate + ": " + logAuthor + " khôi phục " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.Write(_js.Serialize(new [] {"Success"}));
    }

    private static bool CheckParentStatus(string id, ref string groupName)
    {
        var dt = Filter.GetData("", FilterColumns.IfStatus + "," + FilterColumns.VfName, FilterTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            groupName = dt.Rows[0][FilterColumns.VfName].ToString();
        }
        return dt.Rows.Count > 0 && dt.Rows[0][FilterColumns.IfStatus].Equals(2);
    }

    private static void DeleteListFilter(HttpContext context)
    {
        var listId = context.Request["listId"];
        var control = context.Request["control"];
        var typePage = context.Request["typePage"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        foreach (var id in listId.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
        {
            var condition = DataExtension.AndConditon(
                " CHARINDEX('," + id + ",',vfGenealogy) > 0 ",
                "ifStatus <> 2"
            );
            var dt = Filter.GetData("", FilterColumns.IfId + "," + FilterColumns.VfName, condition, "");
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                string[] fields = { FilterColumns.IfStatus };
                string[] values = { "2" };
                Filter.UpdateValues(DataExtension.UpdateValues(fields, values), FilterTSql.GetById(dt.Rows[i][FilterColumns.IfId].ToString()));
                #region Logs
                var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
                var logCreateDate = DateTime.Now.ToString();
                Logs.Insert(LinkAdmin.GoAdminSubControl(control, typePage), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác: " + dt.Rows[i][FilterColumns.VfName], logAuthor, logCreateDate);
                #endregion
            }
        }
        context.Response.End();
    }

    private static void DeleteFilter(HttpContext context)
    {
        var id = context.Request["id"];
        var control = context.Request["control"];
        var typePage = context.Request["typePage"];
        var titleItem = context.Request["titleItem"];
        if (id.Length < 1) context.Response.End();
        string[] fields = { FilterColumns.IfStatus };
        string[] values = { "2" };
        var condition = " CHARINDEX('," + id + ",',vfGenealogy) > 0 ";
        Filter.UpdateValues(DataExtension.UpdateValues(fields, values), condition);
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(control, typePage), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác " + titleItem + (FilterExtension.CountChildCategory(id, FilterTSql.GetByStatus("1")).Equals("0") ? "":" và các mục con"), logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private void UpdateOrderFilter(HttpContext context)
    {
        var control = context.Request["control"];
        var typePage = context.Request["typePage"];
        var id = context.Request["id"];
        var value = context.Request["value"];
        var dt = Filter.GetData("1", FilterColumns.VfName, FilterTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            string[] fields = { FilterColumns.IfSortOrder };
            string[] values = { value };
            Filter.UpdateValues(DataExtension.UpdateValues(fields, values), FilterTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(control, typePage), logCreateDate + ": " + logAuthor + " cập nhật số thứ tự: " + dt.Rows[0][FilterColumns.VfName], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }

    private void OnOffFilter(HttpContext context)
    {
        var id = context.Request["id"];
        var control = context.Request["control"];
        var typePage = context.Request["typePage"];
        var dt = Filter.GetData("1", FilterColumns.IfStatus + "," + FilterColumns.VfName, FilterTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][FilterColumns.IfStatus].Equals(0) ? "1" : "0";
            string[] fields = { FilterColumns.IfStatus };
            string[] values = { valueUpdate };
            Filter.UpdateValues(DataExtension.UpdateValues(fields, values), FilterTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(control, typePage), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " " + dt.Rows[0][FilterColumns.VfName], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}
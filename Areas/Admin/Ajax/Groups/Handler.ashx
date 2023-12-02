<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Data;
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
            case "OnOffGroup":
                OnOffGroup(context);
                break;
            case "UpdateOrderGroup":
                UpdateOrderGroup(context);
                break;
            case "DeleteGroup":
                DeleteGroup(context);
                break;
            case "DeleteListGroups":
                DeleteListGroups(context);
                break;
            case "RestoreGroup":
                RestoreGroup(context);
                break;
            case "DeleteRecGroup":
                DeleteRecGroup(context);
                break;
            case "DeleteRecListGroups":
                DeleteRecListGroups(context);
                break;
        }
    }
    private void DeleteRecListGroups(HttpContext context)
    {
        var listId = context.Request["listId"];
        var control = context.Request["control"];
        var pic = context.Request["pic"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        var fields = DataExtension.GetListColumns(GroupsColumns.VgName, GroupsColumns.IgId, GroupsColumns.VgApp);
        var dt = Groups.GetData("", fields, "igId in (" + listId + ")", "");
        var listIdHasDel = "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            DeleteRecGroup(control, dt.Rows[i][GroupsColumns.VgApp].ToString(), dt.Rows[i][GroupsColumns.IgId].ToString(), dt.Rows[i][GroupsColumns.VgName].ToString(), pic, ref listIdHasDel);
        }
        context.Response.Write(_js.Serialize(new[] {listIdHasDel}));
    }

    private void DeleteRecGroup(HttpContext context)
    {
        var control = context.Request["control"];
        var app = context.Request["app"];
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        var pic = context.Request["pic"];
        var listIdHasDel = "";

        DeleteRecGroup(control, app, id, titleItem, pic, ref listIdHasDel);

        context.Response.Write(_js.Serialize(new[] {listIdHasDel}));
    }

    private static void DeleteRecGroup(string control, string app, string id, string titleItem, string pic, ref string listIdHasDel)
    {
        var condition = " CHARINDEX('," + id + ",',vgGenealogy) > 0 ";
        var dt = Groups.GetData("", GroupsColumns.IgId + ", " + GroupsColumns.VgName + ", " + GroupsColumns.VgImage, condition, "");
        var conditionItem = GroupItemsTSql.GetItemsInGroupCondition(id, ItemsTSql.GetByApp(app));
        var dtItem = conditionItem.Length > 0 ? GroupItems.GetAllData("", "Items.iiId, Items.viImage", conditionItem, "") : new DataTable();

        #region Xóa danh mục + ảnh đại diện
        const string split = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            listIdHasDel = listIdHasDel + "," + dt.Rows[i][GroupsColumns.IgId];
            if (dt.Rows[i][GroupsColumns.VgImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(pic, dt.Rows[i][GroupsColumns.VgImage].ToString());
            else
            {
                foreach (var s in dt.Rows[i][GroupsColumns.VgImage].ToString().Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
                {
                    ImagesExtension.DeleteImageWhenDeleteItem(pic, s);
                }
            }
            // Xóa các bản ghi ở GroupItems theo igid
            GroupItems.Delete("igId = '" + dt.Rows[i][GroupsColumns.IgId] + "'");
            // Xóa các bản ghi ở Groups theo igid
            Groups.Delete("igId = '" + dt.Rows[i][GroupsColumns.IgId] + "'");
        }
        #endregion

        // Nếu là danh mục thì xóa cả subitems và items
        if (control.Equals(app))
        {
            #region Xóa SubItems và Items

            for (var i = 0; i < dtItem.Rows.Count; i++)
            {
                var dtSubItems = Subitems.GetData("", SubitemsColumns.IsiId + "," + SubitemsColumns.VsiImage, SubItemsTSql.GetByIiid(dtItem.Rows[i][ItemsColumns.IiId].ToString()), "");
                for (var x = 0; x < dtSubItems.Rows.Count; x++)
                {
                    if (dtSubItems.Rows[x][SubitemsColumns.VsiImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0)
                        ImagesExtension.DeleteImageWhenDeleteItem(pic, dtSubItems.Rows[x][SubitemsColumns.VsiImage].ToString());
                    else
                        foreach (var s in dtSubItems.Rows[x][SubitemsColumns.VsiImage].ToString().Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries)) ImagesExtension.DeleteImageWhenDeleteItem(pic, s);
                }
                Subitems.Delete(SubItemsTSql.GetByIiid(dtItem.Rows[i][ItemsColumns.IiId].ToString()));

                if (dtItem.Rows[i][ItemsColumns.ViImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0)
                    ImagesExtension.DeleteImageWhenDeleteItem(pic, dtItem.Rows[i][ItemsColumns.ViImage].ToString());
                else
                    foreach (var s in dtItem.Rows[i][ItemsColumns.ViImage].ToString().Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries)) ImagesExtension.DeleteImageWhenDeleteItem(pic, s);
                Items.Delete(ItemsTSql.GetById(dtItem.Rows[i][ItemsColumns.IiId].ToString()));
            }

            #endregion
        }

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(control, "RecycleCategory"), logCreateDate + ": " + logAuthor + " xóa khỏi thùng rác " + titleItem + (dt.Rows.Count > 1 ? " và các mục con":""), logAuthor, logCreateDate);
        #endregion
    }

    private void RestoreGroup(HttpContext context)
    {
        var control = context.Request["control"];
        var typePage = context.Request["typePage"];
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        var dt = Groups.GetData("", GroupsColumns.IgParentId, GroupsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var catename = "";
            if (CheckParentStatus(dt.Rows[0][GroupsColumns.IgParentId].ToString(), ref catename))
            {
                context.Response.Write(_js.Serialize(new [] {catename}));
                return;
            }
        }
        string[] fields = { GroupsColumns.IgStatus };
        string[] values = { "1" };
        Groups.UpdateValues(DataExtension.UpdateValues(fields, values), GroupsTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(control, typePage), logCreateDate + ": " + logAuthor + " khôi phục " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.Write(_js.Serialize(new [] {"Success"}));
    }

    private static bool CheckParentStatus(string id, ref string catename)
    {
        var dt = Groups.GetData("", GroupsColumns.IgStatus + "," + GroupsColumns.VgName, GroupsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            catename = dt.Rows[0][GroupsColumns.VgName].ToString();
        }
        return dt.Rows.Count > 0 && dt.Rows[0][GroupsColumns.IgStatus].Equals(2);
    }

    private static void DeleteListGroups(HttpContext context)
    {
        var listId = context.Request["listId"];
        var control = context.Request["control"];
        var typePage = context.Request["typePage"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        foreach (var id in listId.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
        {
            var condition = DataExtension.AndConditon(
                " CHARINDEX('," + id + ",',vgGenealogy) > 0 ",
                "igStatus <> 2"
            );
            var dt = Groups.GetData("", GroupsColumns.IgId + "," + GroupsColumns.VgName, condition, "");
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                string[] fields = { GroupsColumns.IgStatus };
                string[] values = { "2" };
                Groups.UpdateValues(DataExtension.UpdateValues(fields, values), GroupsTSql.GetById(dt.Rows[i][GroupsColumns.IgId].ToString()));
                #region Logs
                var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
                var logCreateDate = DateTime.Now.ToString();
                Logs.Insert(LinkAdmin.GoAdminSubControl(control, typePage), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác: " + dt.Rows[i][GroupsColumns.VgName], logAuthor, logCreateDate);
                #endregion
            }
        }
        context.Response.End();
    }

    private static void DeleteGroup(HttpContext context)
    {
        var id = context.Request["id"];
        var control = context.Request["control"];
        var typePage = context.Request["typePage"];
        var titleItem = context.Request["titleItem"];
        if (id.Length < 1) context.Response.End();
        string[] fields = { GroupsColumns.IgStatus };
        string[] values = { "2" };
        var condition = " CHARINDEX('," + id + ",',vgGenealogy) > 0 ";
        Groups.UpdateValues(DataExtension.UpdateValues(fields, values), condition);
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(control, typePage), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác " + titleItem + (GroupsExtension.CountChildCategory(id, GroupsTSql.GetByStatus("1")).Equals("0") ? "":" và các mục con"), logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private void UpdateOrderGroup(HttpContext context)
    {
        var control = context.Request["control"];
        var typePage = context.Request["typePage"];
        var id = context.Request["id"];
        var value = context.Request["value"];
        var dt = Groups.GetData("1", GroupsColumns.VgApp + "," + GroupsColumns.IgStatus + "," + GroupsColumns.VgName, GroupsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            string[] fields = { GroupsColumns.IgSortOrder };
            string[] values = { value };
            Groups.UpdateValues(DataExtension.UpdateValues(fields, values), GroupsTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(control, typePage), logCreateDate + ": " + logAuthor + " cập nhật số thứ tự: " + dt.Rows[0][GroupsColumns.VgName], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }

    private void OnOffGroup(HttpContext context)
    {
        var id = context.Request["id"];
        var control = context.Request["control"];
        var typePage = context.Request["typePage"];
        var dt = Groups.GetData("1", GroupsColumns.IgStatus + "," + GroupsColumns.VgName, GroupsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][GroupsColumns.IgStatus].Equals(0) ? "1" : "0";
            string[] fields = { GroupsColumns.IgStatus };
            string[] values = { valueUpdate };
            Groups.UpdateValues(DataExtension.UpdateValues(fields, values), GroupsTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(control, typePage), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " " + dt.Rows[0][GroupsColumns.VgName], logAuthor, logCreateDate);
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
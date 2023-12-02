<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.MenusControl;
using RevosJsc.TSql;

public class Handler : IHttpHandler {

    private readonly JavaScriptSerializer _js = new JavaScriptSerializer();
    private readonly string _control = "Menus";
    public void ProcessRequest (HttpContext context) {
        var action = context.Request["action"];
        //context.Response.ContentType = "json";
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) return;
        switch (action)
        {
            case "OnOffMenu":
                OnOffMenu(context);
                break;
            case "UpdateOrderMenu":
                UpdateOrderMenu(context);
                break;
            case "DeleteMenu":
                DeleteMenu(context);
                break;
            case "DeleteListMenus":
                DeleteListMenus(context);
                break;
            case "RestoreMenu":
                RestoreMenu(context);
                break;
            case "DeleteRecMenu":
                DeleteRecMenu(context);
                break;
            case "DeleteRecListMenus":
                DeleteRecListMenus(context);
                break;
        }
    }
    private void DeleteRecListMenus(HttpContext context)
    {
        var listId = context.Request["listId"];
        var pic = context.Request["pic"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        var fields = DataExtension.GetListColumns(MenusColumns.VmnName, MenusColumns.ImnId, MenusColumns.VmnApp);
        var dt = Menus.GetData("", fields, "imnId IN (" + listId + ")", "");
        var listIdHasDel = "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            DeleteRecMenu(dt.Rows[i][MenusColumns.VmnApp].ToString(), dt.Rows[i][MenusColumns.ImnId].ToString(), dt.Rows[i][MenusColumns.VmnName].ToString(), pic, ref listIdHasDel);
        }
        context.Response.Write(_js.Serialize(new[] {listIdHasDel}));
    }

    private void DeleteRecMenu(HttpContext context)
    {
        var app = context.Request["app"];
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        var pic = context.Request["pic"];
        var listIdHasDel = "";

        DeleteRecMenu(app, id, titleItem, pic, ref listIdHasDel);

        context.Response.Write(_js.Serialize(new[] {listIdHasDel}));
    }

    private void DeleteRecMenu(string app, string id, string titleItem, string pic, ref string listIdHasDel)
    {
        var condition = " CHARINDEX('," + id + ",',vmnGenealogy) > 0 ";
        var dt = Menus.GetData("", MenusColumns.ImnId + ", " + MenusColumns.VmnName + ", " + MenusColumns.VmnImage, condition, "");

        #region Xóa danh mục + ảnh đại diện
        const string split = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            listIdHasDel = listIdHasDel + "," + dt.Rows[i][MenusColumns.ImnId];
            if (dt.Rows[i][MenusColumns.VmnImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(pic, dt.Rows[i][MenusColumns.VmnImage].ToString());
            else
            {
                foreach (var s in dt.Rows[i][MenusColumns.VmnImage].ToString().Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
                {
                    ImagesExtension.DeleteImageWhenDeleteItem(pic, s);
                }
            }
            // Xóa các bản ghi ở Menus theo imnId
            Menus.Delete(MenusTSql.GetById(dt.Rows[i][MenusColumns.ImnId].ToString()));
        }
        #endregion

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminOption(_control, TypePage.Recycle, "app", app), logCreateDate + ": " + logAuthor + " xóa khỏi thùng rác menu " + titleItem + (dt.Rows.Count > 1 ? " và các mục con":""), logAuthor, logCreateDate);
        #endregion
    }

    private void RestoreMenu(HttpContext context)
    {
        var app = context.Request["app"];
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        var dt = Menus.GetData("", MenusColumns.ImnParentId, MenusTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var catename = "";
            if (CheckParentStatus(dt.Rows[0][MenusColumns.ImnParentId].ToString(), ref catename))
            {
                context.Response.Write(_js.Serialize(new [] {catename}));
                return;
            }
        }
        string[] fields = { MenusColumns.ImnStatus };
        string[] values = { "1" };
        Menus.UpdateValues(DataExtension.UpdateValues(fields, values), MenusTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminOption(_control, TypePage.Recycle, "app", app), logCreateDate + ": " + logAuthor + " khôi phục menu " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.Write(_js.Serialize(new [] {"Success"}));
    }

    private static bool CheckParentStatus(string id, ref string catename)
    {
        var dt = Menus.GetData("", MenusColumns.ImnStatus + "," + MenusColumns.VmnName, MenusTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            catename = dt.Rows[0][MenusColumns.VmnName].ToString();
        }
        return dt.Rows.Count > 0 && dt.Rows[0][MenusColumns.ImnStatus].Equals(2);
    }

    private void DeleteListMenus(HttpContext context)
    {
        var listId = context.Request["listId"];
        var app = context.Request["app"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        foreach (var id in listId.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
        {
            var condition = DataExtension.AndConditon(
                " CHARINDEX('," + id + ",',vmnGenealogy) > 0 ",
                "imnStatus <> 2"
            );
            var dt = Menus.GetData("", MenusColumns.ImnId + "," + MenusColumns.VmnName, condition, "");
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                string[] fields = { MenusColumns.ImnStatus };
                string[] values = { "2" };
                Menus.UpdateValues(DataExtension.UpdateValues(fields, values), MenusTSql.GetById(dt.Rows[i][MenusColumns.ImnId].ToString()));
                #region Logs
                var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
                var logCreateDate = DateTime.Now.ToString();
                Logs.Insert(LinkAdmin.GoAdminOption(_control, TypePage.Index, "app", app), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác menu: " + dt.Rows[i][MenusColumns.VmnName], logAuthor, logCreateDate);
                #endregion
            }
        }
        context.Response.End();
    }

    private void DeleteMenu(HttpContext context)
    {
        var id = context.Request["id"];
        var app = context.Request["app"];
        var titleItem = context.Request["titleItem"];
        if (id.Length < 1) context.Response.End();
        string[] fields = { MenusColumns.ImnStatus };
        string[] values = { "2" };
        var condition = " CHARINDEX('," + id + ",',vmnGenealogy) > 0 ";
        Menus.UpdateValues(DataExtension.UpdateValues(fields, values), condition);
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminOption(_control, TypePage.Index, "app", app), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác menu " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private void UpdateOrderMenu(HttpContext context)
    {
        var id = context.Request["id"];
        var value = context.Request["value"];
        var dt = Menus.GetData("1", MenusColumns.VmnApp + "," + MenusColumns.ImnStatus + "," + MenusColumns.VmnName, MenusTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            string[] fields = { MenusColumns.ImnSortOrder };
            string[] values = { value };
            Menus.UpdateValues(DataExtension.UpdateValues(fields, values), MenusTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminOption(_control, TypePage.Index, "app", dt.Rows[0][MenusColumns.VmnApp].ToString()), logCreateDate + ": " + logAuthor + " cập nhật số thứ tự menu: " + dt.Rows[0][MenusColumns.VmnName], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }

    private void OnOffMenu(HttpContext context)
    {
        var id = context.Request["id"];
        var dt = Menus.GetData("1", MenusColumns.ImnStatus + "," + MenusColumns.VmnName + "," + MenusColumns.VmnApp, MenusTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][MenusColumns.ImnStatus].Equals(0) ? "1" : "0";
            string[] fields = { MenusColumns.ImnStatus };
            string[] values = { valueUpdate };
            Menus.UpdateValues(DataExtension.UpdateValues(fields, values), MenusTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminOption(_control, TypePage.Index, "app", dt.Rows[0][MenusColumns.VmnApp].ToString()), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " menu " + dt.Rows[0][MenusColumns.VmnName], logAuthor, logCreateDate);
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
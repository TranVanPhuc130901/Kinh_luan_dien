<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public class Handler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        var action = context.Request["action"];
        context.Response.ContentType = "text/plain";
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) return;
        switch (action)
        {
            case "OnOffSubItems":
                OnOffSubItems(context);
                break;
            case "DeleteSubItem":
                DeleteSubItem(context);
                break;
            case "OnOffItems":
                OnOffItems(context);
                break;
            case "UpdateOrderItems":
                UpdateOrderItems(context);
                break;
            case "DeleteListItems":
                DeleteListItems(context);
                break;
            case "DeleteItem":
                DeleteItem(context);
                break;
            case "RestoreItem":
                RestoreItem(context);
                break;
            case "DeleteRecItem":
                DeleteRecItem(context);
                break;
            case "DeleteRecListItems":
                DeleteRecListItems(context);
                break;
            case "DeleteRecListSubItems":
                DeleteRecListSubItems(context);
                break;
        }
    }

    private static void DeleteRecListSubItems(HttpContext context)
    {
        var listId = context.Request["listId"];
        var control = context.Request["control"];
        var pic = context.Request["pic"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        foreach (var id in listId.Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries))  DeleteSubItem(control, pic, id);
        context.Response.End();
    }

    private static void DeleteSubItem(string control, string pic, string id)
    {
        const string split = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        var dt = Subitems.GetData("", SubitemsColumns.IsiId + "," + SubitemsColumns.VsiImage + "," + SubitemsColumns.VsiTitle, SubItemsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][SubitemsColumns.VsiImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0)
                ImagesExtension.DeleteImageWhenDeleteItem(pic, dt.Rows[0][SubitemsColumns.VsiImage].ToString());
            else
                foreach (var s in dt.Rows[0][SubitemsColumns.VsiImage].ToString().Split(new [] { split }, StringSplitOptions.RemoveEmptyEntries)) ImagesExtension.DeleteImageWhenDeleteItem(pic, s);

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(control, "Comment"), logCreateDate + ": " + logAuthor + " xóa " + dt.Rows[0][SubitemsColumns.VsiTitle], logAuthor, logCreateDate);
            #endregion
        }
        // Xóa bản ghi ở SubItems
        Subitems.Delete(SubItemsTSql.GetById(id));
    }

    private static void DeleteRecListItems(HttpContext context)
    {
        var listId = context.Request["listId"];
        var control = context.Request["control"];
        var pic = context.Request["pic"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        foreach (var id in listId.Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries))  DeleteRecItem(control, pic, id);
        context.Response.End();
    }

    private static void DeleteRecItem(HttpContext context)
    {
        var control = context.Request["control"];
        var pic = context.Request["pic"];
        var id = context.Request["id"];
        DeleteRecItem(control, pic, id);
        context.Response.End();
    }

    private static void DeleteRecItem(string control, string pic, string id)
    {
        const string split = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        var fields = DataExtension.GetListColumns(ItemsColumns.IiId, ItemsColumns.ViTitle, ItemsColumns.ViImage);
        var dtItem = Items.GetData("", fields, ItemsTSql.GetById(id), "");
        if (dtItem.Rows.Count <= 0) return;

        #region Xóa các biến thể

        Items.Delete(DataExtension.AndConditon(ItemsTSql.GetByTag(id), ItemsTSql.GetByApp("Variant")));

        #endregion

        #region Xóa SubItems và Items
        var dtSubItems = Subitems.GetData("", SubitemsColumns.IsiId + "," + SubitemsColumns.VsiImage, SubItemsTSql.GetByIiid(dtItem.Rows[0][ItemsColumns.IiId].ToString()), "");
        for (var x = 0; x < dtSubItems.Rows.Count; x++)
        {
            if (dtSubItems.Rows[x][SubitemsColumns.VsiImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0)
                ImagesExtension.DeleteImageWhenDeleteItem(pic, dtSubItems.Rows[x][SubitemsColumns.VsiImage].ToString());
            else
                foreach (var s in dtSubItems.Rows[x][SubitemsColumns.VsiImage].ToString().Split(new [] { split }, StringSplitOptions.RemoveEmptyEntries)) ImagesExtension.DeleteImageWhenDeleteItem(pic, s);
        }
        // Xóa các bản ghi ở SubItems theo IiId
        Subitems.Delete(SubItemsTSql.GetByIiid(dtItem.Rows[0][ItemsColumns.IiId].ToString()));

        if (dtItem.Rows[0][ItemsColumns.ViImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0)
            ImagesExtension.DeleteImageWhenDeleteItem(pic, dtItem.Rows[0][ItemsColumns.ViImage].ToString());
        else
            foreach (var s in dtItem.Rows[0][ItemsColumns.ViImage].ToString().Split(new [] { split }, StringSplitOptions.RemoveEmptyEntries)) ImagesExtension.DeleteImageWhenDeleteItem(pic, s);

        // Xóa các bản ghi ở GroupItems theo igid
        GroupItems.Delete("IiId = '" + dtItem.Rows[0][ItemsColumns.IiId] + "'");
        // Xóa các bản ghi ở Items theo IiId
        Items.Delete(ItemsTSql.GetById(dtItem.Rows[0][ItemsColumns.IiId].ToString()));
        #endregion

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(control, "RecycleItem"), logCreateDate + ": " + logAuthor + " xóa khỏi thùng rác " + dtItem.Rows[0][ItemsColumns.ViTitle], logAuthor, logCreateDate);
        #endregion
    }

    private static void RestoreItem(HttpContext context)
    {
        var control = context.Request["control"];
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        string[] fields = { ItemsColumns.IiStatus };
        string[] values = { "1" };
        Items.UpdateValues(DataExtension.UpdateValues(fields, values), ItemsTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(control, "Item"), logCreateDate + ": " + logAuthor + " khôi phục " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void DeleteItem(HttpContext context)
    {
        var control = context.Request["control"];
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        string[] fields = { ItemsColumns.IiStatus };
        string[] values = { "2" };
        Items.UpdateValues(DataExtension.UpdateValues(fields, values), ItemsTSql.GetById(id));

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(control, "Item"), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác: " + titleItem, logAuthor, logCreateDate);
        #endregion

        context.Response.End();
    }

    private static void DeleteListItems(HttpContext context)
    {
        var listId = context.Request["listId"];
        var control = context.Request["control"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        string[] fields = { ItemsColumns.IiStatus };
        string[] values = { "2" };
        Items.UpdateValues(DataExtension.UpdateValues(fields, values), "iiId IN ("+ listId +")");
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(control, "Item"), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác danh sách bài viết (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void UpdateOrderItems(HttpContext context)
    {
        var id = context.Request["id"];
        var value = context.Request["value"];
        var dt = Items.GetData("1", ItemsColumns.ViApp + "," + ItemsColumns.IiStatus + "," + ItemsColumns.ViTitle, ItemsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            string[] fields = { ItemsColumns.IiSortOrder };
            string[] values = { value };
            Items.UpdateValues(DataExtension.UpdateValues(fields, values), ItemsTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(dt.Rows[0][ItemsColumns.ViApp].ToString(), "Item"), logCreateDate + ": " + logAuthor + " cập nhật số thứ tự: " + dt.Rows[0][ItemsColumns.ViTitle], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }

    private static void OnOffItems(HttpContext context)
    {
        var id = context.Request["id"];
        var dt = Items.GetData("1", ItemsColumns.ViApp + "," + ItemsColumns.IiStatus + "," + ItemsColumns.ViTitle, ItemsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][ItemsColumns.IiStatus].Equals(0) ? "1" : "0";
            string[] fields = { ItemsColumns.IiStatus };
            string[] values = { valueUpdate };
            Items.UpdateValues(DataExtension.UpdateValues(fields, values), ItemsTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(dt.Rows[0][ItemsColumns.ViApp].ToString(), "Item"), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " " + dt.Rows[0][ItemsColumns.ViTitle], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }

    private static void DeleteSubItem(HttpContext context)
    {
        var link = context.Request["link"];
        var id = context.Request["id"];
        var pic = context.Request["pic"];
        const string split = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        var dt = Subitems.GetData("", SubitemsColumns.IsiId + "," + SubitemsColumns.VsiImage + "," + SubitemsColumns.VsiTitle, SubItemsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][SubitemsColumns.VsiImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0)
                ImagesExtension.DeleteImageWhenDeleteItem(pic, dt.Rows[0][SubitemsColumns.VsiImage].ToString());
            else
                foreach (var s in dt.Rows[0][SubitemsColumns.VsiImage].ToString().Split(new [] { split }, StringSplitOptions.RemoveEmptyEntries)) ImagesExtension.DeleteImageWhenDeleteItem(pic, s);

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(link, logCreateDate + ": " + logAuthor + " xóa " + dt.Rows[0][SubitemsColumns.VsiTitle], logAuthor, logCreateDate);
            #endregion
        }
        // Xóa bản ghi ở SubItems
        Subitems.Delete(SubItemsTSql.GetById(id));
    }

    private static void OnOffSubItems(HttpContext context)
    {
        var id = context.Request["id"];
        var dt = Subitems.GetData("1", SubitemsColumns.VsiApp + "," + SubitemsColumns.IsiStatus, SubItemsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][SubitemsColumns.IsiStatus].Equals(0) ? "1" : "0";
            string[] fields = { SubitemsColumns.IsiStatus };
            string[] values = { valueUpdate };
            Subitems.UpdateValues(DataExtension.UpdateValues(fields, values), SubItemsTSql.GetById(id));
        }
        context.Response.End();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }
}
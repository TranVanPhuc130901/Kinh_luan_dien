<%@ WebHandler Language="C#" Class="Redirect_Handler" %>

using System;
using System.Web;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.RedirectsControl;
using RevosJsc.TSql;

public class Redirect_Handler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        var action = context.Request["action"];
        context.Response.ContentType = "text/plain";
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) return;
        switch (action)
        {
            case "OnOffRedirect":
                OnOffRedirect(context);
                break;
            case "DeleteRedirect":
                DeleteRedirect(context);
                break;
            case "RestoreRedirect":
                RestoreRedirect(context);
                break;
            case "DeleteListRedirect":
                DeleteListRedirect(context);
                break;
            case "RestoreListRedirect":
                RestoreListRedirect(context);
                break;
            case "DeleteRecRedirect":
                DeleteRecRedirect(context);
                break;
            case "DeleteRecListRedirect":
                DeleteRecListRedirect(context);
                break;
        }
    }

    private static void DeleteRecListRedirect(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        Redirects.Delete("irId IN ("+ listId +")");
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Redirects, TypePage.Recycle), logCreateDate + ": " + logAuthor + " xóa vĩnh viễn danh sách link chuyển hướng (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void DeleteRecRedirect(HttpContext context)
    {
        var id = context.Request["id"];
        var link = context.Request["link"];
        Redirects.Delete(RedirectsTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Redirects, TypePage.Recycle), logCreateDate + ": " + logAuthor + " xóa vĩnh viễn link chuyển hướng: " + link, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void RestoreListRedirect(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        string[] fields = { RedirectsColumns.IrStatus };
        string[] values = { "1" };
        Redirects.UpdateValues(DataExtension.UpdateValues(fields, values), "irId IN ("+ listId +")");
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Redirects, TypePage.Recycle), logCreateDate + ": " + logAuthor + " khôi phục danh sách link chuyển hướng (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion
        context.Response.End(); 
    }

    private static void DeleteListRedirect(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        string[] fields = { RedirectsColumns.IrStatus };
        string[] values = { "2" };
        Redirects.UpdateValues(DataExtension.UpdateValues(fields, values), "irId IN ("+ listId +")");
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminControl(CodeApplications.Redirects), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác danh sách link chuyển hướng (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void RestoreRedirect(HttpContext context)
    {
        var id = context.Request["id"];
        var link = context.Request["link"];
        string[] fields = { RedirectsColumns.IrStatus };
        string[] values = { "1" };
        Redirects.UpdateValues(DataExtension.UpdateValues(fields, values), RedirectsTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Redirects, TypePage.Recycle), logCreateDate + ": " + logAuthor + " Khôi phục link chuyển hướng: " + link, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void DeleteRedirect(HttpContext context)
    {
        var id = context.Request["id"];
        var link = context.Request["link"];
        string[] fields = { RedirectsColumns.IrStatus };
        string[] values = { "2" };
        Redirects.UpdateValues(DataExtension.UpdateValues(fields, values), RedirectsTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminControl(CodeApplications.Redirects), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác link chuyển hướng: " + link, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void OnOffRedirect(HttpContext context)
    {
        var id = context.Request["id"];
        var dt = Redirects.GetData("1", RedirectsColumns.IrStatus + "," + RedirectsColumns.VrLink, RedirectsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][RedirectsColumns.IrStatus].Equals(0) ? "1" : "0";
            string[] fields = { RedirectsColumns.IrStatus };
            string[] values = { valueUpdate };
            Redirects.UpdateValues(DataExtension.UpdateValues(fields, values), RedirectsTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminControl(CodeApplications.Redirects), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " link chuyển hướng: " + dt.Rows[0][RedirectsColumns.VrLink], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
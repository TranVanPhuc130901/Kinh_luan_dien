<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Globalization;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Developer.Extension;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;
using RevosJsc.UsersControl;

public class Handler : IHttpHandler, IRequiresSessionState {
    private readonly JavaScriptSerializer _js = new JavaScriptSerializer();
    public void ProcessRequest(HttpContext context)
    {
        var action = context.Request["action"];
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) return;
        switch (action)
        {
            case "OnOffUsers":
                OnOffUsers(context);
                break;
            case "DeleteUsers":
                DeleteUsers(context);
                break;
            case "RestoreUsers":
                RestoreUsers(context);
                break;
            case "DeleteListUsers":
                DeleteListUsers(context);
                break;
            case "RestoreListUsers":
                RestoreListUsers(context);
                break;
            case "DeleteRecUsers":
                DeleteRecUsers(context);
                break;
            case "DeleteRecListUsers":
                DeleteRecListUsers(context);
                break;
            case "ResetPassword":
                ResetPassword(context);
                break;
        }
    }

    private void ResetPassword(HttpContext context)
    {
        #region Lấy thông tin

        var username = context.Request["username"];
        var captcha = context.Request["captcha"];

        #endregion

        var s = "";
        var s1 = "";
        if (context.Session != null && context.Session["captchaAdminLogin"] != null)
        {
            var dt = Users.GetData("1", "*", UsersTSql.GetByAccount(username), "");
            var userPasswordSalt = SecurityExtension.BuildPassword(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            if (dt.Rows.Count > 0)
            {
                Users.UpdateValues(UsersColumns.VuVerificationCode + " = '" + userPasswordSalt + "' ", UsersTSql.GetByAccount(username));
                var contentEmail = @"
<table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; width: 100%; max-width: 600px;' class='content'>
    <tr>
        <td align='center' style='padding: 20px 20px 20px 20px; color: #ffffff; font-family: Arial, sans-serif; font-size: 36px; font-weight: bold;'>
            <img src='" + UrlExtension.WebsiteUrl + @"Areas/Admin/img/icon120.png' alt='Revos Viet Nam' width='120' height='120' style='display:block;' />
        </td>
    </tr>
    <tr>
        <td bgcolor='#f9f9f9' style='padding: 20px 20px 0 20px; color: #555555; font-family: Arial, sans-serif; '>
            Hệ thống Revos CMS đã nhận được yêu cầu khôi phục mật khẩu cho tài khoản quản trị " + username + @" tại website " + UrlExtension.WebsiteUrl + @"<br/><br/>
            Nếu bạn không yêu cầu khôi phục mật khẩu, vui lòng bỏ qua email này.<br/><br/>
            Nếu bạn muốn khôi phục mật khẩu, vui lòng nhấn xác nhận để kích hoạt mật khẩu mới
        </td>
    </tr>
    <tr>
        <td align='center' bgcolor='#f9f9f9' style='padding: 30px 20px 30px 20px; font-family: Arial, sans-serif; border-bottom: 1px solid #f6f6f6;'>
            <table bgcolor='#1bbae1' border='0' cellspacing='0' cellpadding='0' class='buttonwrapper'>
                <tr>
                    <td align='center' height='50' style=' padding: 0 25px 0 25px; font-family: Arial, sans-serif; font-size: 16px; font-weight: bold;' class='button'>
                        <a href='" + UrlExtension.WebsiteUrl + "Admin/confirm?account=" + username + "&confirm=" + userPasswordSalt + @"' style='color: #ffffff; text-align: center; text-decoration: none;'>Xác nhận</a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align='center' bgcolor='#dddddd' style='padding: 15px 10px 15px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 12px; line-height: 18px;'>
            <b>Revos CMS</b><br/>Revos Viet Nam ,.JSC
        </td>
    </tr>
</table>
";
                EmailExtension.SendEmail(dt.Rows[0][UsersColumns.VuEmail].ToString(), "Khôi phục mật khẩu quản trị website: " + UrlExtension.WebsiteUrl, contentEmail);
                s = "success";
                s1 = "Hệ thống đã gửi một email tới <b>" + dt.Rows[0][UsersColumns.VuEmail] + "</b>, vui lòng xác nhận email để reset mật khẩu";
            }
            else
            {
                s = "danger";
                s1 = "Tài khoản không tồn tại";
            }
        }
        else
        {
            s = "warning";
            s1 = "Mã captcha không chính xác, vui lòng thử lại.";
        }
        context.Response.Write(_js.Serialize(new [] {s, s1}));
    }

    private static void DeleteRecListUsers(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        Users.Delete("iuId IN ("+ listId +")");
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Users, TypePage.Recycle), logCreateDate + ": " + logAuthor + " xóa vĩnh viễn danh sách tài khoản (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void DeleteRecUsers(HttpContext context)
    {
        var id = context.Request["id"];
        var account = context.Request["account"];
        Users.Delete(UsersTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Users, TypePage.Recycle), logCreateDate + ": " + logAuthor + " xóa vĩnh viễn tài khoản: " + account, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void RestoreListUsers(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        string[] fields = { UsersColumns.IuStatus };
        string[] values = { "1" };
        Users.UpdateValues(DataExtension.UpdateValues(fields, values), "iuid IN ("+ listId +")");
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Users, TypePage.Recycle), logCreateDate + ": " + logAuthor + " khôi phục danh sách tài khoản (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void DeleteListUsers(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.Substring(0, listId.Length - 1);
        string[] fields = { UsersColumns.IuStatus };
        string[] values = { "2" };
        Users.UpdateValues(DataExtension.UpdateValues(fields, values), "iuId IN ("+ listId +")");
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminControl(CodeApplications.Users), logCreateDate + ": " + logAuthor + " xóa danh sách tài khoản (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void RestoreUsers(HttpContext context)
    {
        var id = context.Request["id"];
        var account = context.Request["account"];
        string[] fields = { UsersColumns.IuStatus };
        string[] values = { "1" };
        Users.UpdateValues(DataExtension.UpdateValues(fields, values), UsersTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Users, TypePage.Recycle), logCreateDate + ": " + logAuthor + " Khôi phục tài khoản quản trị: " + account, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void DeleteUsers(HttpContext context)
    {
        var id = context.Request["id"];
        var account = context.Request["account"];
        string[] fields = { UsersColumns.IuStatus };
        string[] values = { "2" };
        Users.UpdateValues(DataExtension.UpdateValues(fields, values), UsersTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminControl(CodeApplications.Users), logCreateDate + ": " + logAuthor + " xóa tài khoản quản trị: " + account, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void OnOffUsers(HttpContext context)
    {
        var id = context.Request["id"];
        var dt = Users.GetData("1", UsersColumns.IuStatus + "," + UsersColumns.VuAccount, UsersTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][UsersColumns.IuStatus].Equals(0) ? "1" : "0";
            string[] fields = { UsersColumns.IuStatus };
            string[] values = { valueUpdate };
            Users.UpdateValues(DataExtension.UpdateValues(fields, values), UsersTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminControl(CodeApplications.Users), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " tài khoản quản trị: " + dt.Rows[0][UsersColumns.VuAccount], logAuthor, logCreateDate);
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
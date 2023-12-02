<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.MemberControl;
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
            case "OnOffMember":
                OnOffMember(context);
                break;
            case "DeleteMember":
                DeleteMember(context);
                break;
            case "RestoreMember":
                RestoreMember(context);
                break;
            case "DeleteListMembers":
                DeleteListMembers(context);
                break;
            case "DeleteRecMember":
                DeleteRecMember(context);
                break;
            case "DeleteRecListMembers":
                DeleteRecListMembers(context);
                break;
        }
    }

    private void DeleteRecListMembers(HttpContext context)
    {
        var page = context.Request["page"];
        var listId = context.Request["listId"];
        var pic = context.Request["pic"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        var listIdHasDel = "";
        var dt = Members.GetData("", MembersColumns.ImId + ", " + MembersColumns.VmAccount + ", " + MembersColumns.VmImage, "imId IN ("+ listId +")", "");
        #region Xóa liên hệ + ảnh đại diện
        const string split = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            listIdHasDel = listIdHasDel + "," + dt.Rows[i][MembersColumns.ImId];
            if (dt.Rows[i][MembersColumns.VmImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(pic, dt.Rows[i][MembersColumns.VmImage].ToString());
            else
            {
                foreach (var s in dt.Rows[i][MembersColumns.VmImage].ToString().Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
                {
                    ImagesExtension.DeleteImageWhenDeleteItem(pic, s);
                }
            }
            // Xóa các bản ghi ở Member theo id
            Members.Delete(MembersTSql.GetById(dt.Rows[i][MembersColumns.ImId].ToString()));

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Member, page), logCreateDate + ": " + logAuthor + " xóa khỏi thùng rác " + dt.Rows[i][MembersColumns.VmAccount], logAuthor, logCreateDate);
            #endregion
        }
        #endregion
        context.Response.Write(_js.Serialize(new[] {listIdHasDel}));
    }

    private static void DeleteRecMember(HttpContext context)
    {
        var page = context.Request["page"];
        var id = context.Request["id"];
        var pic = context.Request["pic"];

        var dt = Members.GetData("", MembersColumns.ImId + ", " + MembersColumns.VmAccount + ", " + MembersColumns.VmImage, MembersTSql.GetById(id), "");
        #region Xóa liên hệ + ảnh đại diện
        const string split = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][MembersColumns.VmImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(pic, dt.Rows[0][MembersColumns.VmImage].ToString());
            else
            {
                foreach (var s in dt.Rows[0][MembersColumns.VmImage].ToString().Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
                {
                    ImagesExtension.DeleteImageWhenDeleteItem(pic, s);
                }
            }
            // Xóa các bản ghi ở Member theo icid
            Members.Delete(MembersTSql.GetById(dt.Rows[0][MembersColumns.ImId].ToString()));

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Member, page), logCreateDate + ": " + logAuthor + " xóa khỏi thùng rác " + dt.Rows[0][MembersColumns.VmAccount], logAuthor, logCreateDate);
            #endregion
        }
        #endregion
        
        context.Response.End();
    }

    private static void DeleteListMembers(HttpContext context)
    {
        var page = context.Request["page"];
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        var dt = Members.GetData("", MembersColumns.ImId + "," + MembersColumns.VmAccount, "imId IN ("+ listId +")", "");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            string[] fields = { MembersColumns.ImStatus };
            string[] values = { "2" };
            Members.UpdateValues(DataExtension.UpdateValues(fields, values), MembersTSql.GetById(dt.Rows[i][MembersColumns.ImId].ToString()));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Member, page), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác : " + dt.Rows[i][MembersColumns.VmAccount], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }

    private void RestoreMember(HttpContext context)
    {
        var page = context.Request["page"];
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        string[] fields = { MembersColumns.ImStatus };
        string[] values = { "1" };
        Members.UpdateValues(DataExtension.UpdateValues(fields, values), MembersTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Member, page), logCreateDate + ": " + logAuthor + " khôi phục " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }


    private static void DeleteMember(HttpContext context)
    {
        var page = context.Request["page"];
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        if (id.Length < 1) context.Response.End();
        string[] fields = { MembersColumns.ImStatus };
        string[] values = { "2" };
        Members.UpdateValues(DataExtension.UpdateValues(fields, values), MembersTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Member, page), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void OnOffMember(HttpContext context)
    {
        var id = context.Request["id"];
        var dt = Members.GetData("1", MembersColumns.ImStatus + "," + MembersColumns.VmAccount, MembersTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][MembersColumns.ImStatus].Equals(0) ? "1" : "0";
            string[] fields = { MembersColumns.ImStatus };
            string[] values = { valueUpdate };
            Members.UpdateValues(DataExtension.UpdateValues(fields, values), MembersTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Member, TypePage.Item), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " " + dt.Rows[0][MembersColumns.VmAccount], logAuthor, logCreateDate);
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
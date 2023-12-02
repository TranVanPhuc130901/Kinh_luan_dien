<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.LanguageControl;
using RevosJsc.TSql;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        var action = context.Request["action"];
        context.Response.ContentType = "text/plain";
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) return;
        switch (action)
        {
            case "OnOffLanguage":
                OnOffLanguage(context);
                break;
            case "DeleteKeyword":
                DeleteKeyword(context);
                break;
            case "DeleteListKeyword":
                DeleteListKeyword(context);
                break;
        }
    }
        
    private void DeleteListKeyword(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        // Xóa các bản ghi ở ContactDetail theo id
        Keywords.Delete("ikId IN ("+ listId +")");

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Language, TypePage.Keyword), logCreateDate + ": " + logAuthor + " xóa danh sách từ khóa (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion

        context.Response.End();
    }

    private void DeleteKeyword(HttpContext context)
    {
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        // Xóa các bản ghi ở ContactDetail theo id
        Keywords.Delete(KeywordsTSql.GetById(id));

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Language, TypePage.Keyword), logCreateDate + ": " + logAuthor + " xóa từ khóa: " + titleItem, logAuthor, logCreateDate);
        #endregion

        context.Response.End();
    }

    private static void OnOffLanguage(HttpContext context)
    {
        var id = context.Request["id"];
        var dt = LanguageNational.GetData("1", "*", LanguageNationalTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][LanguageNationalColumns.IlnStatus].Equals(0) ? "1" : "0";
            LanguageNational.Update(id, dt.Rows[0][LanguageNationalColumns.VlnName].ToString(), dt.Rows[0][LanguageNationalColumns.VlnFlag].ToString(), dt.Rows[0][LanguageNationalColumns.IlnSortOrder].ToString(), valueUpdate);

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Language, TypePage.Flag), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " ngôn ngữ: " + dt.Rows[0][LanguageNationalColumns.VlnName], logAuthor, logCreateDate);
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
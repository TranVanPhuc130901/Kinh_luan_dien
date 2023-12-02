<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.ContactControl;
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
            case "OnOffContact":
                OnOffContact(context);
                break;
            case "OnOffContactDetail":
                OnOffContactDetail(context);
                break;
            case "UpdateOrderContact":
                UpdateOrderContact(context);
                break;
            case "DeleteContact":
                DeleteContact(context);
                break;
            case "RestoreContact":
                RestoreContact(context);
                break;
            case "DeleteListContact":
                DeleteListContact(context);
                break;
            case "DeleteRecContact":
                DeleteRecContact(context);
                break;
            case "DeleteRecListContact":
                DeleteRecListContact(context);
                break;
            case "DeleteContactDetail":
                DeleteContactDetail(context);
                break;
            case "DeleteListContactDetail":
                DeleteListContactDetail(context);
                break;
        }
    }
        
    private static void DeleteListContactDetail(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        // Xóa các bản ghi ở ContactDetail theo id
        ContactDetails.Delete("icdId IN (" + listId + ")");

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Contact, TypePage.Item), logCreateDate + ": " + logAuthor + " xóa danh sách liên hệ (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion

        context.Response.End();
    }
    private static void DeleteContactDetail(HttpContext context)
    {
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        // Xóa các bản ghi ở ContactDetail theo id
        ContactDetails.Delete("icdId = '" + id + "'");

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Contact, TypePage.Item), logCreateDate + ": " + logAuthor + " xóa liên hệ: " + titleItem, logAuthor, logCreateDate);
        #endregion

        context.Response.End();
    }

    private void DeleteRecListContact(HttpContext context)
    {
        var listId = context.Request["listId"];
        var pic = context.Request["pic"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        var listIdHasDel = "";
        foreach (var id in listId.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
        {
            var condition = " CHARINDEX('," + id + ",',vcGenealogy) > 0 ";
            var dt = Contacts.GetData("", ContactsColumns.IcId + ", " + ContactsColumns.VcName + ", " + ContactsColumns.VcImage, condition, "");
            #region Xóa liên hệ + ảnh đại diện
            const string split = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                listIdHasDel = listIdHasDel + "," + dt.Rows[i][ContactsColumns.IcId];
                if (dt.Rows[i][ContactsColumns.VcImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(pic, dt.Rows[i][ContactsColumns.VcImage].ToString());
                else
                {
                    foreach (var s in dt.Rows[i][ContactsColumns.VcImage].ToString().Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ImagesExtension.DeleteImageWhenDeleteItem(pic, s);
                    }
                }
                // Xóa các bản ghi ở Contact theo icid
                Contacts.Delete(ContactsTSql.GetById(dt.Rows[i][ContactsColumns.IcId].ToString()));

                #region Logs
                var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
                var logCreateDate = DateTime.Now.ToString();
                Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Contact, TypePage.RecycleCategory), logCreateDate + ": " + logAuthor + " xóa khỏi thùng rác " + dt.Rows[i][ContactsColumns.VcName], logAuthor, logCreateDate);
                #endregion
            }
            #endregion
        }
        context.Response.Write(_js.Serialize(new[] {listIdHasDel}));
    }

    private void DeleteRecContact(HttpContext context)
    {
        var id = context.Request["id"];
        var pic = context.Request["pic"];
        var listIdHasDel = "";

        var condition = " CHARINDEX('," + id + ",',vcGenealogy) > 0 ";
        var dt = Contacts.GetData("", ContactsColumns.IcId + ", " + ContactsColumns.VcName + ", " + ContactsColumns.VcImage, condition, "");
        #region Xóa liên hệ + ảnh đại diện
        const string split = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            listIdHasDel = listIdHasDel + "," + dt.Rows[i][ContactsColumns.IcId];
            if (dt.Rows[i][ContactsColumns.VcImage].ToString().IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(pic, dt.Rows[i][ContactsColumns.VcImage].ToString());
            else
            {
                foreach (var s in dt.Rows[i][ContactsColumns.VcImage].ToString().Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
                {
                    ImagesExtension.DeleteImageWhenDeleteItem(pic, s);
                }
            }
            // Xóa các bản ghi ở Contact theo icid
            Contacts.Delete("icId = '" + dt.Rows[i][ContactsColumns.IcId] + "'");

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Contact, TypePage.RecycleCategory), logCreateDate + ": " + logAuthor + " xóa khỏi thùng rác " + dt.Rows[i][ContactsColumns.VcName], logAuthor, logCreateDate);
            #endregion
        }
        #endregion

        context.Response.Write(_js.Serialize(new[] {listIdHasDel}));
    }

    private static void DeleteListContact(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        foreach (var id in listId.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
        {
            var condition = DataExtension.AndConditon(
                " CHARINDEX('," + id + ",',vcGenealogy) > 0 ",
                "icStatus <> 2"
                );
            var dt = Contacts.GetData("", ContactsColumns.IcId + "," + ContactsColumns.VcName, condition, "");
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                string[] fields = { ContactsColumns.IcStatus };
                string[] values = { "2" };
                Contacts.UpdateValues(DataExtension.UpdateValues(fields, values), ContactsTSql.GetById(dt.Rows[i][ContactsColumns.IcId].ToString()));
                #region Logs
                var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
                var logCreateDate = DateTime.Now.ToString();
                Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Contact, TypePage.Category), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác : " + dt.Rows[i][ContactsColumns.VcName], logAuthor, logCreateDate);
                #endregion
            }
        }
        context.Response.End();
    }

    private void RestoreContact(HttpContext context)
    {
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        var dt = Contacts.GetData("", ContactsColumns.IcParentId, ContactsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var catename = "";
            if (CheckParentStatus(dt.Rows[0][ContactsColumns.IcParentId].ToString(), ref catename))
            {
                context.Response.Write(_js.Serialize(new [] {catename}));
                return;
            }
        }
        string[] fields = { ContactsColumns.IcStatus };
        string[] values = { "1" };
        Contacts.UpdateValues(DataExtension.UpdateValues(fields, values), ContactsTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Contact, TypePage.RecycleCategory), logCreateDate + ": " + logAuthor + " khôi phục " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.Write(_js.Serialize(new [] {"Success"}));
    }

    private static bool CheckParentStatus(string id, ref string catename)
    {
        var dt = Contacts.GetData("", ContactsColumns.IcStatus + "," + ContactsColumns.VcName, ContactsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            catename = dt.Rows[0][ContactsColumns.VcName].ToString();
        }
        return dt.Rows.Count > 0 && dt.Rows[0][ContactsColumns.IcStatus].Equals(2);
    }

    private static void DeleteContact(HttpContext context)
    {
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        if (id.Length < 1) context.Response.End();
        string[] fields = { ContactsColumns.IcStatus };
        string[] values = { "2" };
        var condition = " CHARINDEX('," + id + ",',vcGenealogy) > 0 ";
        Contacts.UpdateValues(DataExtension.UpdateValues(fields, values), condition);
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Contact, TypePage.Category), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void UpdateOrderContact(HttpContext context)
    {
        var id = context.Request["id"];
        var value = context.Request["value"];
        var dt = Contacts.GetData("1", ContactsColumns.IcStatus + "," + ContactsColumns.VcName, ContactsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            string[] fields = { ContactsColumns.IcSortOrder };
            string[] values = { value };
            Contacts.UpdateValues(DataExtension.UpdateValues(fields, values), ContactsTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Contact, TypePage.Category), logCreateDate + ": " + logAuthor + " cập nhật số thứ tự: " + dt.Rows[0][ContactsColumns.VcName], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }
    private static void OnOffContactDetail(HttpContext context)
    {
        var id = context.Request["id"];
        var dt = ContactDetails.GetData("1", "icdStatus, vcdName", "icdId = "+ id, "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0]["icdStatus"].Equals(0) ? "1" : "0";
            string[] fields = { "icdStatus" };
            string[] values = { valueUpdate };
            ContactDetails.UpdateValues(DataExtension.UpdateValues(fields, values), "icdId = "+ id);
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Contact, TypePage.Item), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " " + dt.Rows[0]["vcdName"], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }
    private static void OnOffContact(HttpContext context)
    {
        var id = context.Request["id"];
        var dt = Contacts.GetData("1", ContactsColumns.IcStatus + "," + ContactsColumns.VcName, ContactsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][ContactsColumns.IcStatus].Equals(0) ? "1" : "0";
            string[] fields = { ContactsColumns.IcStatus };
            string[] values = { valueUpdate };
            Contacts.UpdateValues(DataExtension.UpdateValues(fields, values), ContactsTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Contact, TypePage.Category), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " " + dt.Rows[0][ContactsColumns.VcName], logAuthor, logCreateDate);
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
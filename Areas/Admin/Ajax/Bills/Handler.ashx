<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.ProductControl;
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
            case "OnOffBill":
                OnOffBill(context);
                break;
            case "DeleteBill":
                DeleteBill(context);
                break;
            case "DeleteListBills":
                DeleteListBills(context);
                break;
        }
    }
        
    private static void DeleteListBills(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        
        // Xóa BillDetails trước
        BillDetails.Delete("ibId IN (" + listId + ")");
        // Xóa các bản ghi ở ProductDetail theo id
        Bills.Delete("ibId IN (" + listId + ")");

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Product, TypePage.Item), logCreateDate + ": " + logAuthor + " xóa danh sách đơn hàng (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion

        context.Response.End();
    }
    private static void DeleteBill(HttpContext context)
    {
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        // Xóa BillDetails trước
        BillDetails.Delete(BillDetailsTSql.GetByIbId(id));
        // Xóa các bản ghi ở Bills theo id
        Bills.Delete(BillsTSql.GetById(id));

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Product, TypePage.Item), logCreateDate + ": " + logAuthor + " xóa đơn hàng " + titleItem, logAuthor, logCreateDate);
        #endregion

        context.Response.End();
    }


    private static void OnOffBill(HttpContext context)
    {
        var id = context.Request["id"];
        var dt = Bills.GetData("1", BillsColumns.IbStatus + "," + BillsColumns.VbCode, BillsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][BillsColumns.IbStatus].Equals(0) ? "1" : "0";
            string[] fields = { BillsColumns.IbStatus };
            string[] values = { valueUpdate };
            Bills.UpdateValues(DataExtension.UpdateValues(fields, values), BillsTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Product, TypePage.Bill), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " đơn hàng " + dt.Rows[0][BillsColumns.VbCode], logAuthor, logCreateDate);
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
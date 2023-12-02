<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.AdvertistmentsControl;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public class Handler : IHttpHandler {

    private readonly JavaScriptSerializer _js = new JavaScriptSerializer();
    private readonly string _pic = FolderPic.Advertistments;
    private readonly string split = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
    public void ProcessRequest (HttpContext context) {
        var action = context.Request["action"];
        //context.Response.ContentType = "json";
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) return;
        switch (action)
        {
            case "OnOffAdvertistmentPosition":
                OnOffAdvertistmentPosition(context);
                break;
            case "UpdateOrderAdvertistmentPosition":
                UpdateOrderAdvertistmentPosition(context);
                break;
            case "DeleteAdvertistmentPosition":
                DeleteAdvertistmentPosition(context);
                break;
            case "RestoreAdvertistmentPosition":
                RestoreAdvertistmentPosition(context);
                break;
            case "DeleteListAdvertistmentPositions":
                DeleteListAdvertistmentPositions(context);
                break;
            case "DeleteRecAdvertistmentPosition":
                DeleteRecAdvertistmentPosition(context);
                break;
            case "DeleteRecListAdvertistmentPositions":
                DeleteRecListAdvertistmentPositions(context);
                break;
            case "DeleteAdvertistment":
                DeleteAdvertistment(context);
                break;
            case "DeleteListAdvertistments":
                DeleteListAdvertistments(context);
                break;
            case "RestoreAdvertistment":
                RestoreAdvertistment(context);
                break;
            case "DeleteRecAdvertistment":
                DeleteRecAdvertistment(context);
                break;
            case "DeleteRecListAdvertistments":
                DeleteRecListAdvertistments(context);
                break;
            case "UpdateOrderAdvertistment":
                UpdateOrderAdvertistment(context);
                break;
            case "OnOffAdvertistment":
                OnOffAdvertistment(context);
                break;
        }
    }

    private static void UpdateOrderAdvertistment(HttpContext context)
    {
        var id = context.Request["id"];
        var value = context.Request["value"];
        var dt = Advertistments.GetData("1", AdvertistmentsColumns.VaTitle, AdvertistmentsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            string[] fields = { AdvertistmentsColumns.IaSortOrder };
            string[] values = { value };
            Advertistments.UpdateValues(DataExtension.UpdateValues(fields, values), AdvertistmentsTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.Item), logCreateDate + ": " + logAuthor + " cập nhật số thứ tự quảng cáo: " + dt.Rows[0][AdvertistmentsColumns.VaTitle], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }

    private static void OnOffAdvertistment(HttpContext context)
    {
        var id = context.Request["id"];
        var dt = Advertistments.GetData("1", AdvertistmentsColumns.IaStatus + "," + AdvertistmentsColumns.VaTitle, AdvertistmentsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][AdvertistmentsColumns.IaStatus].Equals(0) ? "1" : "0";
            string[] fields = { AdvertistmentsColumns.IaStatus };
            string[] values = { valueUpdate };
            Advertistments.UpdateValues(DataExtension.UpdateValues(fields, values), AdvertistmentsTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.Item), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " quảng cáo: " + dt.Rows[0][AdvertistmentsColumns.VaTitle], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }
        
    private void DeleteRecListAdvertistments(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        // Xóa các bản ghi ở ContactDetail theo id
        var dt = Advertistments.GetData("", AdvertistmentsColumns.IaId + "," + AdvertistmentsColumns.VaImage, "iaId IN (" + listId + ")", "");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var image = dt.Rows[i][AdvertistmentsColumns.VaImage].ToString();
            if (image.IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(_pic, image);
            else
            {
                foreach (var s in image.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
                {
                    ImagesExtension.DeleteImageWhenDeleteItem(_pic, s);
                }
            }
            Advertistments.Delete(AdvertistmentsTSql.GetById(dt.Rows[i][AdvertistmentsColumns.IaId].ToString()));
        }
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.Item), logCreateDate + ": " + logAuthor + " xóa danh sách quảng cáo (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion

        context.Response.End();
    }

    private void DeleteRecAdvertistment(HttpContext context)
    {
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        // Xóa các bản ghi ở Advertistments theo id
        var dt = Advertistments.GetData("", AdvertistmentsColumns.VaImage, AdvertistmentsTSql.GetById(id), "");
        if (dt.Rows.Count < 1) context.Response.End();
        var image = dt.Rows[0][AdvertistmentsColumns.VaImage].ToString();
        if (image.IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(_pic, image);
        else
        {
            foreach (var s in image.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
            {
                ImagesExtension.DeleteImageWhenDeleteItem(_pic, s);
            }
        }
        Advertistments.Delete(AdvertistmentsTSql.GetById(id));

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.Item), logCreateDate + ": " + logAuthor + " xóa quảng cáo: " + titleItem, logAuthor, logCreateDate);
        #endregion

        context.Response.End();
    }

    private static void RestoreAdvertistment(HttpContext context)
    {
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];

        string[] fields = { AdvertistmentsColumns.IaStatus };
        string[] values = { "1" };
        Advertistments.UpdateValues(DataExtension.UpdateValues(fields, values), AdvertistmentsTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.RecycleItem), logCreateDate + ": " + logAuthor + " khôi phục quảng cáo: " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }
        
    private static void DeleteListAdvertistments(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        string[] fields = { AdvertistmentsColumns.IaStatus };
        string[] values = { "2" };
        Advertistments.UpdateValues(DataExtension.UpdateValues(fields, values), "iaId IN ("+ listId +")");
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.Item), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác danh sách quảng cáo (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void DeleteAdvertistment(HttpContext context)
    {
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        if (id.Length < 1) context.Response.End();
        string[] fields = { AdvertistmentsColumns.IaStatus };
        string[] values = { "2" };
        Advertistments.UpdateValues(DataExtension.UpdateValues(fields, values), AdvertistmentsTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.Item), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác quảng cáo: " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }
    private void DeleteRecListAdvertistmentPositions(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        var listIdHasDel = "";
        var fields = DataExtension.GetListColumns(
            AdvertistmentPositionsColumns.IapId ,
            AdvertistmentPositionsColumns.VapName,
            AdvertistmentPositionsColumns.VapImage
        );
        var dt = AdvertistmentPositions.GetData("", fields, "iapId IN ("+ listId +")", "");
        #region Xóa quảng cáo + ảnh đại diện
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            listIdHasDel = listIdHasDel + "," + dt.Rows[i][AdvertistmentPositionsColumns.IapId];
            var image = dt.Rows[i][AdvertistmentPositionsColumns.VapImage].ToString();
            if (image.IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(_pic, image);
            else
            {
                foreach (var s in image.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
                {
                    ImagesExtension.DeleteImageWhenDeleteItem(_pic, s);
                }
            }
            // Xóa các bản ghi ở Advertistments
            var dtx = Advertistments.GetData("", AdvertistmentsColumns.IaId + "," + AdvertistmentsColumns.VaImage, AdvertistmentsTSql.GetByPositionId(dt.Rows[i][AdvertistmentPositionsColumns.IapId].ToString()), "");
            for (var x = 0; x < dtx.Rows.Count; x++)
            {
                image = dtx.Rows[x][AdvertistmentsColumns.VaImage].ToString();
                if (image.IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(_pic, image);
                else
                {
                    foreach (var s in image.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ImagesExtension.DeleteImageWhenDeleteItem(_pic, s);
                    }
                }
                Advertistments.Delete(AdvertistmentsTSql.GetById(dtx.Rows[x][AdvertistmentsColumns.IaId].ToString()));
            }
            // Xóa các bản ghi ở AdvertistmentPositions theo id
            AdvertistmentPositions.Delete(AdvertistmentPositionsTSql.GetById(dt.Rows[i][AdvertistmentPositionsColumns.IapId].ToString()));

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.RecycleCategory), logCreateDate + ": " + logAuthor + " xóa khỏi thùng rác danh mục quảng cáo: " + dt.Rows[i][AdvertistmentPositionsColumns.VapName], logAuthor, logCreateDate);
            #endregion
        }
        #endregion
        context.Response.Write(_js.Serialize(new[] {listIdHasDel}));
    }

    private void DeleteRecAdvertistmentPosition(HttpContext context)
    {
        var id = context.Request["id"];
        var fields = DataExtension.GetListColumns(
            AdvertistmentPositionsColumns.IapId ,
            AdvertistmentPositionsColumns.VapName,
            AdvertistmentPositionsColumns.VapImage
            );
        var dt = AdvertistmentPositions.GetData("", fields, AdvertistmentPositionsTSql.GetById(id), "");
        if (dt.Rows.Count < 1) context.Response.End();
        #region Xóa quảng cáo + ảnh đại diện
        var image = dt.Rows[0][AdvertistmentPositionsColumns.VapImage].ToString();
        if (image.IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(_pic, image);
        else
        {
            foreach (var s in image.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
            {
                ImagesExtension.DeleteImageWhenDeleteItem(_pic, s);
            }
        }
        // Xóa các bản ghi ở Advertistments
        var dtx = Advertistments.GetData("", AdvertistmentsColumns.IaId, AdvertistmentsTSql.GetByPositionId(id), "");
        for (var i = 0; i < dtx.Rows.Count; i++)
        {
            image = dt.Rows[i][AdvertistmentsColumns.VaImage].ToString();
            if (image.IndexOf(split, StringComparison.Ordinal) < 0) ImagesExtension.DeleteImageWhenDeleteItem(_pic, image);
            else
            {
                foreach (var s in image.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries))
                {
                    ImagesExtension.DeleteImageWhenDeleteItem(_pic, s);
                }
            }
            Advertistments.Delete(AdvertistmentsTSql.GetById(dt.Rows[i][AdvertistmentsColumns.IaId].ToString()));
        }
        // Xóa bản ghi ở AdvertistmentPositions
        AdvertistmentPositions.Delete(AdvertistmentPositionsTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.RecycleCategory), logCreateDate + ": " + logAuthor + " xóa khỏi thùng rác danh mục quảng cáo: " + dt.Rows[0][AdvertistmentPositionsColumns.VapName], logAuthor, logCreateDate);
        #endregion
        #endregion

        context.Response.End();
    }

    private static void DeleteListAdvertistmentPositions(HttpContext context)
    {
        var listId = context.Request["listId"];
        if (listId.Length < 1) context.Response.End();
        if (listId.EndsWith(",")) listId = listId.TrimEnd(',');
        string[] fields = { AdvertistmentPositionsColumns.IapStatus };
        string[] values = { "2" };
        AdvertistmentPositions.UpdateValues(DataExtension.UpdateValues(fields, values), "iapId IN ("+ listId +")");
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.Category), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác các danh mục quảng cáo (id: " + listId + ")", logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void RestoreAdvertistmentPosition(HttpContext context)
    {
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];

        string[] fields = { AdvertistmentPositionsColumns.IapStatus };
        string[] values = { "1" };
        AdvertistmentPositions.UpdateValues(DataExtension.UpdateValues(fields, values), AdvertistmentPositionsTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.RecycleCategory), logCreateDate + ": " + logAuthor + " khôi phục danh mục quảng cáo: " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }


    private static void DeleteAdvertistmentPosition(HttpContext context)
    {
        var id = context.Request["id"];
        var titleItem = context.Request["titleItem"];
        if (id.Length < 1) context.Response.End();
        string[] fields = { AdvertistmentPositionsColumns.IapStatus };
        string[] values = { "2" };
        AdvertistmentPositions.UpdateValues(DataExtension.UpdateValues(fields, values), AdvertistmentPositionsTSql.GetById(id));
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.Category), logCreateDate + ": " + logAuthor + " chuyển vào thùng rác danh mục quảng cáo: " + titleItem, logAuthor, logCreateDate);
        #endregion
        context.Response.End();
    }

    private static void UpdateOrderAdvertistmentPosition(HttpContext context)
    {
        var id = context.Request["id"];
        var value = context.Request["value"];
        var dt = AdvertistmentPositions.GetData("1", AdvertistmentPositionsColumns.VapName, AdvertistmentPositionsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            string[] fields = { AdvertistmentPositionsColumns.IapSortOrder };
            string[] values = { value };
            AdvertistmentPositions.UpdateValues(DataExtension.UpdateValues(fields, values), AdvertistmentPositionsTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.Category), logCreateDate + ": " + logAuthor + " cập nhật số thứ tự danh mục quảng cáo: " + dt.Rows[0][AdvertistmentPositionsColumns.VapName], logAuthor, logCreateDate);
            #endregion
        }
        context.Response.End();
    }

    private static void OnOffAdvertistmentPosition(HttpContext context)
    {
        var id = context.Request["id"];
        var dt = AdvertistmentPositions.GetData("1", AdvertistmentPositionsColumns.IapStatus + "," + AdvertistmentPositionsColumns.VapName, AdvertistmentPositionsTSql.GetById(id), "");
        if (dt.Rows.Count > 0)
        {
            var valueUpdate = dt.Rows[0][AdvertistmentPositionsColumns.IapStatus].Equals(0) ? "1" : "0";
            string[] fields = { AdvertistmentPositionsColumns.IapStatus };
            string[] values = { valueUpdate };
            AdvertistmentPositions.UpdateValues(DataExtension.UpdateValues(fields, values), AdvertistmentPositionsTSql.GetById(id));
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.Category), logCreateDate + ": " + logAuthor + " " + (valueUpdate.Equals("1") ? "enable" : "disable") + " danh mục quảng cáo: " + dt.Rows[0][AdvertistmentPositionsColumns.VapName], logAuthor, logCreateDate);
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
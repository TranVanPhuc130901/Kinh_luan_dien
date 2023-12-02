using System;
using System.Text;
using System.Web.Script.Serialization;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.NewsControl;
using RevosJsc.TSql;

public partial class Areas_Display_Ajax_News : System.Web.UI.Page
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _app = CodeApplications.News;
    private readonly string _pic = FolderPic.News;
    private string _action = "";
    private readonly JavaScriptSerializer _js = new JavaScriptSerializer();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (Request.Form["action"] != null) _action = Request.Form["action"];
        if (_action == "") return;
        switch (_action)
        {
            case "GetTopNews":
                GetTopNews();
                break;
            case "LoadMoreNews":
                LoadMoreNews();
                break;
        }
    }

    private void LoadMoreNews()
    {
        var s = new StringBuilder();

        #region Lấy thông tin

        var parent = Request.Form["parent"] ?? "";
        var keyword = Request.Form["keyword"] ?? "";
        var page = Request.Form["page"] ?? "";

        #endregion Lấy thông tin

        if (page.Equals("")) return;

        var top = parent.Length > 0 ? SettingsExtension.GetSettingKey(SettingKey.SoNewsTrenTrangDanhMuc, _lang) : SettingsExtension.GetSettingKey(SettingKey.SoNewsTrenTrangChu, _lang);
        if (top.Equals("")) top = "1";
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(_app),
            GroupsTSql.GetByStatus("1"),
            GroupsTSql.GetByLang(_lang),
            ItemsTSql.GetByApp(_app),
            ItemsTSql.GetByLang(_lang),
            ItemsTSql.GetByStatus("1"),
            keyword.Length > 0 ? ProductExtension.FullTextSearch(keyword) : ""
        );
        if (parent.Length > 0) condition = GroupItemsTSql.GetItemsInGroupByGenealogy(parent, condition);
        var orderBy = ItemsColumns.IiSortOrder + "," + ItemsColumns.DiDateCreated + " DESC";
        var ds = GroupItems.GetAllDataPaging(page, top, condition, orderBy);
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][ItemsColumns.ViTitle].ToString();
            var desc = dt.Rows[i][ItemsColumns.ViDescription].ToString();
            var image = dt.Rows[i][ItemsColumns.ViImage].ToString();
            var link = (UrlExtension.WebsiteUrl + dt.Rows[i][ItemsColumns.ViLink] + RewriteExtension.Extensions).ToLower();
            s.Append(@"
<div class='item'>
    <a class='wImage' href='" + link + "' title='" + titleItem + @"'>
        " + ImagesExtension.GetImage(_pic, image, titleItem, "lazy", false, false, "", false, false, "") + @"
    </a>
    <a class='title' href='" + link + "' title='" + titleItem + @"'>" + titleItem + @"</a>
    <div class='date_view'>
        <span class='date'>Ngày đăng: " + DateTimeExtension.GetTimeDistance((DateTime)dt.Rows[0][ItemsColumns.DiDateCreated]) + @"</span>
        <span class='view'>Lượt xem: " + NumberExtension.FormatNumber(dt.Rows[i][ItemsColumns.IiTotalView].ToString()) + @"</span>
    </div>
    <div class='desc'>" + desc + @"</div>
    <a class='detail' href='" + link + "' title='" + titleItem + @"'>Đọc tiếp</a>
</div>");
        }
        var remain = Convert.ToInt32(dtPager.Rows[0]["TotalRows"]) - (Convert.ToInt32(top) * (Convert.ToInt32(page) - 1) + dt.Rows.Count);
        string[] reply = { s.ToString(), remain.ToString() };
        Response.Output.Write(_js.Serialize(reply));
    }

    private void GetTopNews()
    {
        var s = new StringBuilder();

        #region Lấy thông tin

        var parent = Request.Form["parent"];

        #endregion Lấy thông tin

        if (parent.Equals("")) return;

        var fields = DataExtension.GetListColumns(ItemsColumns.ViTitle, ItemsColumns.ViImage, ItemsColumns.ViLink, ItemsColumns.DiDateCreated, ItemsColumns.IiTotalView);
        var condition = DataExtension.AndConditon(
            ItemsTSql.GetByApp(_app),
            ItemsTSql.GetByStatus("1"),
            "iiSortOrder < 0"
            );
        condition = GroupItemsTSql.GetItemsInGroupByGenealogy(parent, condition);
        var dt = GroupItems.GetAllData("3", fields, condition, ItemsColumns.IiSortOrder);
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][ItemsColumns.ViTitle].ToString();
            var image = dt.Rows[i][ItemsColumns.ViImage].ToString();
            var link = (UrlExtension.WebsiteUrl + dt.Rows[i][ItemsColumns.ViLink] + RewriteExtension.Extensions).ToLower();
            s.Append(@"
<div class='item'>
    <div class='wImage'>
        <a href='" + link + "' title='" + titleItem + @"' class='image cover'>
            " + ImagesExtension.GetImage(_pic, image, titleItem, "", true, false, "") + @"
        </a>
    </div>
    <a href='" + link + "' title='" + titleItem + @"' class='name'><h3 class='noStyle'>" + titleItem + @"</h3></a>
    <div class='date'>" + DateTimeExtension.GetTimeDistance((DateTime)dt.Rows[0][ItemsColumns.DiDateCreated]) + @" - " + NumberExtension.FormatNumber(dt.Rows[i][ItemsColumns.IiTotalView].ToString()) + @" lượt xem</div>
</div>");
        }
        string[] reply = { s.ToString() };
        Response.Output.Write(_js.Serialize(reply));
    }
}
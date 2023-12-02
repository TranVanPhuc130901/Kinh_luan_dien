using System;
using System.Text;
using Developer.Extension;
using RevosJsc;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.NewsControl;
using RevosJsc.TSql;

public partial class Areas_Display_Search_Control_Article : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _pic = FolderPic.News;
    private readonly string _app = CodeApplications.News;
    private readonly string _maxItem = SettingKey.SoNewsTrenTrangDanhMuc;
    private readonly string _noResultText = LanguageExtension.TranslateKeyword("Updating ...");
    private string _p = "1";
    private string _key = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = QueryStringExtension.GetQueryString("p");
        if (Request.QueryString["keyword"] != null) _key = Request.QueryString["keyword"];
        ltrList.Text = GetList();
    }

    private string GetList()
    {
        var s = new StringBuilder();
        var top = SettingsExtension.GetSettingKey(_maxItem, _lang);
        if (top.Equals("")) top = "8";
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(_app),
            GroupsTSql.GetByStatus("1"),
            GroupsTSql.GetByLang(_lang),
            ItemsTSql.GetByApp(_app),
            ItemsTSql.GetByLang(_lang),
            _key.Length > 0 ? ProductExtension.FullTextSearch(_key) : ""
            );
        var orderBy = ItemsColumns.IiSortOrder + "," + ItemsColumns.DiDateCreated + " DESC";
        var ds = GroupItems.GetAllDataPaging("1", top, condition, orderBy);
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        if (dtPager.Rows[0]["TotalRows"].ToString().Equals("0")) Response.Redirect(UrlExtension.WebsiteUrl + "?rewrite=search&page=no-results&keyword=" + _key);
        #region Lấy danh sách bài viết

        s.Append("<p>Tìm thấy <b>" + dtPager.Rows[0]["TotalRows"] + " kết quả</b> với từ khóa: \"<b>" + _key + "</b>\"</p>");
        s.Append("<div class='news_list'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][ItemsColumns.ViTitle].ToString();
            var desc = dt.Rows[i][ItemsColumns.ViDescription].ToString();
            var image = dt.Rows[i][ItemsColumns.ViImage].ToString();
            var link = (UrlExtension.WebsiteUrl + dt.Rows[i][ItemsColumns.ViLink] + RewriteExtension.Extensions).ToLower();
            s.Append(@"
<div class='item'>
    <a class='wImage' href='" + link + "' title='" + titleItem + @"'>
        " + ImagesExtension.GetImage(_pic, image, titleItem, "lazy", false, false, "", true, true, "") + @"
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
        s.Append("</div>");

        #endregion

        #region Xuất ra phân trang

        if (Convert.ToInt32(dtPager.Rows[0]["TotalRows"]) > Convert.ToInt32(top) && dt.Rows.Count > 0)
        {
            s.Append("<div class='xemthem'><a href='javascript:void(0);' onclick=newsLoadMore('','" + _key + "',2) class='viewMore mt20'>Xem thêm</a></div>");
        }

        #endregion Xuất ra phân trang

        return s.ToString();
    }
}
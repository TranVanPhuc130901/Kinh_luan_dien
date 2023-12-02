using System;
using System.Data;

using System.Text;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.NewsControl;
using RevosJsc.TSql;



public partial class Areas_Display_News_Control_Category : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _app = CodeApplications.News;
    private readonly string _pic = FolderPic.News;
    private readonly string _maxItem = SettingKey.SoNewsTrenTrangDanhMuc;
    //private readonly string _noResultText = "Bài viết đang được cập nhật ...";
    private string _p = "1";
    protected string Parent = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = QueryStringExtension.GetQueryString("p");
        ltrList.Text = GetGroup();
    }

    protected string GetGroup()
    {
        var s = new StringBuilder();
        var dt = (DataTable)Session["dataByTitle_Category"];
        if (dt.Rows.Count <= 0) return "";
        var nameG = dt.Rows[0][GroupsColumns.VgName].ToString();
        //var bannerG = StringExtension.LayChuoi(dt.Rows[0][GroupsColumns.VgImage].ToString(), "", 2);
        Parent = dt.Rows[0][GroupsColumns.VgGenealogy].ToString();
        s.Append(@"<div class='head'><h1>" + nameG + @"</h1></div>");
        s.Append(GetList(Parent));
        return s.ToString();
    }

    private string GetList(string genealogy)
    {
        var s = new StringBuilder();
        var top = SettingsExtension.GetSettingKey(_maxItem, _lang);
        if (top.Equals("")) top = "12";
        var condition = DataExtension.AndConditon(
            ItemsTSql.GetByApp(_app),
            ItemsTSql.GetByStatus("1")
            );
        condition = GroupItemsTSql.GetItemsInGroupByGenealogy(genealogy, condition);
        var orderBy = ItemsColumns.IiSortOrder + "," + ItemsColumns.DiDateModified + " DESC";
        var ds = GroupItems.GetAllDataPaging(_p, top, condition, orderBy);
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        if (dt.Rows.Count < 1) return "";

        #region Lấy danh sách bài viết

        var brandName = SettingsExtension.GetSettingKey("KeyBrandName", _lang);
        s.Append("<div class='group_items'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][ItemsColumns.ViTitle].ToString();
            var author = dt.Rows[i][ItemsColumns.ViAuthor].ToString();
            if (author.Equals("")) author = brandName;
            var content = dt.Rows[i][ItemsColumns.ViContent].ToString();
            string[] words = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Đếm số ký tự trong văn bản
            int characterCount = words.Length;
            var minMinute = characterCount / 220;
            if (minMinute < 1)
            {
                minMinute = 1;
            }
            
            var minread = dt.Rows[i][ItemsColumns.FiPriceOld].ToString();
            var image = dt.Rows[i][ItemsColumns.ViImage].ToString();
            if (image.Length > 0) image += ".ashx?w=400";
            else image = "no_image.svg";
            var desc = dt.Rows[i][ItemsColumns.ViDescription].ToString();
            var link = (UrlExtension.WebsiteUrl + dt.Rows[i][ItemsColumns.ViLink] + RewriteExtension.Extensions).ToLower();
            s.Append(
                @"<a href='" + link + "' title='" + titleItem + @"' class='card'>
    <div class='wImage'>
        " + ImagesExtension.GetImage(_pic, image, titleItem, "lazyload", true, false, "", false, true) + @"
    </div>
    <div class='card_content'>
        <h3 class='title'>" + titleItem + @"</h3>
        <div class='author'>" + author + @"</div>
        <div class='desc'>" + desc + @"</div>
        <span class='btn_light'>" + minMinute + @" min read</span>
    </div>
</a>");
        }
        s.Append("</div>");

        #endregion

        #region Xuất ra phân trang

        s.Append("<div class='pager col-xs-12 pull-right'>");
        if (Convert.ToInt32(dtPager.Rows[0]["TotalRows"]) > Convert.ToInt32(top) && dt.Rows.Count > 0)
        {
            var split = PagingCollection.SplitPages(int.Parse(dtPager.Rows[0]["TotalRows"].ToString()), int.Parse(top), int.Parse(_p), Request.QueryString["title"], "active", "other", "first", "last", "prev", "next");
            s.Append(split);
        }
        s.Append("</div>");

        #endregion Xuất ra phân trang

        return s.ToString();
    }
}
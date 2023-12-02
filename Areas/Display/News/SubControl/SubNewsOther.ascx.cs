using System;
using System.Text;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.NewsControl;
using RevosJsc.TSql;

public partial class Areas_Display_News_SubControl_SubNewsOther : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _app = CodeApplications.News;
    private readonly string _pic = FolderPic.News;
    private readonly string _maxItem = SettingKey.SoNewsKhacTrenMotTrang;
    private string _iiid = "";
    private string _igid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["iiid"] != null) _iiid = QueryStringExtension.GetQueryString("iiid");
        if (Request.QueryString["igid"] != null) _igid = QueryStringExtension.GetQueryString("igid");

        #region title
        if (Request.QueryString["title"] != null)
        {
            //Lấy igid từ session ra vì nó đã dược lưu khi xét tại Default.aspx
            if (_iiid.Length < 1 && Session["iiid"] != null) _iiid = Session["iiid"].ToString();
            if (_igid.Length < 1 && Session["igid"] != null) _igid = Session["igid"].ToString();
        }
        #endregion title
        ltrList.Text = GetList();
    }


    private string GetList()
    {
        var s = new StringBuilder();
        var top = SettingsExtension.GetSettingKey(_maxItem, _lang);
        if (top.Equals("")) top = "10";
        var fields = DataExtension.GetListColumns(ItemsColumns.ViTitle,
            ItemsColumns.ViImage, ItemsColumns.ViLink, ItemsColumns.IiTotalView, ItemsColumns.DiDateCreated);
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(_app),
            GroupsTSql.GetByStatus("1"),
            ItemsTSql.GetByApp(_app),
            ItemsTSql.GetByStatus("1"),
            " Items.iiId <> " + _iiid + " "
            );
        condition = GroupItemsTSql.GetItemsInGroupCondition(_igid, condition);
        var dt = GroupItems.GetAllData(top, "*", condition, ItemsColumns.IiSortOrder + "," + ItemsColumns.DiDateCreated + " DESC");
        if (dt.Rows.Count < 1) return "";
        s.Append("<div class='head'><h2>Bài viết liên quan</h2></div>");
        s.Append("<div class='group_items owl-carousel'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][ItemsColumns.ViTitle].ToString();
            var author = dt.Rows[i][ItemsColumns.ViAuthor].ToString();
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
        <span class='btn_light'>" + minread + @" min read</span>
    </div>
</a>");
        }
        s.Append("</div>");
        return s.ToString();
    }
}
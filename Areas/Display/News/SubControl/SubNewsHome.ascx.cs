using System;
using System.Text;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.NewsControl;
using RevosJsc.TSql;

public partial class Areas_Display_News_SubControl_SubNewsHome : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _pic = FolderPic.News;
    private readonly string _app = CodeApplications.News;
    private readonly string _appG = CodeApplications.NewsGroupItem;
    protected void Page_Load(object sender, EventArgs e)
    {
        ltrList.Text = GetList("1");
        if (ltrList.Text.Equals("")) Visible = false;
    }

    private string GetList(string pos)
    {
        var s = new StringBuilder();
        var fields = DataExtension.GetListColumns(
            GroupsColumns.VgName,
            ItemsColumns.ViTitle,
            ItemsColumns.ViAuthor,
            ItemsColumns.FiPriceOld,
            ItemsColumns.ViDescription,
            ItemsColumns.ViImage,
            ItemsColumns.ViLink,
            ItemsColumns.DiDateCreated
            );
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(_appG),
            GroupsTSql.GetByStatus("1"),
            GroupsTSql.GetByLang(_lang),
            GroupsTSql.GetByPosition(pos),
            ItemsTSql.GetByStatus("1")
        );
        var dt = GroupItems.GetAllData("", fields, condition, "");
        if (dt.Rows.Count < 1) return "";
        var cName = dt.Rows[0][GroupsColumns.VgName].ToString();
        s.Append(@"
<a class='head' href='"+ UrlExtension.WebsiteUrl + RewriteExtension.News + RewriteExtension.Extensions +@"'>
    <h2>" + cName + @"</h2>
    <span>Xem thêm</span>
</a>");
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
        <span class='btn_light'>"+ minread + @" min read</span>
    </div>
</a>");
        }
        s.Append("</div>");
        return s.ToString();
    }
}
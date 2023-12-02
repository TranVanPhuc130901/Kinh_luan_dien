using System;
using System.Text;
using Developer.Extension;
using RevosJsc.AdvertistmentsControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Display_Adv_AdvMidHomePage : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _pic = FolderPic.Advertistments;
    protected void Page_Load(object sender, EventArgs e)
    {
        ltrAdv.Text = GetList("4", "");
        if (ltrAdv.Text == "") Visible = false;
    }

    private string GetList(string pos, string cssClass)
    {
        var s = new StringBuilder();
        var fields = DataExtension.GetListColumns(
            AdvertistmentsColumns.VaTitle,
            AdvertistmentsColumns.VaDescription,
            AdvertistmentsColumns.VaImage,
            AdvertistmentsColumns.VaLink,
            AdvertistmentsColumns.IaTarget
            );
        var condition = DataExtension.AndConditon(
            AdvertistmentPositionsTSql.GetByPosition(pos),
            AdvertistmentPositionsTSql.GetByStatus("1"),
            AdvertistmentPositionsTSql.GetByLang(_lang),
            AdvertistmentsTSql.GetByStatus("1")
            );
        var dt = Advertistments.GetAllData("1", fields, condition, AdvertistmentsColumns.IaSortOrder);
        if (dt.Rows.Count <= 0) return s.ToString();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][AdvertistmentsColumns.VaTitle].ToString();
            var desc = dt.Rows[i][AdvertistmentsColumns.VaDescription].ToString();
            var image = dt.Rows[i][AdvertistmentsColumns.VaImage].ToString();
            var link = dt.Rows[i][AdvertistmentsColumns.VaLink].ToString();
            if (link.Equals("")) link = "javascript://";
            var target = dt.Rows[i][AdvertistmentsColumns.IaTarget].ToString().Equals("1") ? "target='_blank'" : "target='_self'";
            var imgSrc = image.Length > 0 ? UrlExtension.WebsiteUrl + _pic + "/" + image : "/images/snow-BCT-hero.jpg";
            s.Append("<section class='WeHero_stories'>");
            s.Append("<div class='bg_wrap'>");
            s.Append("<div class='background-image' style='background-image: url(" + imgSrc + "); background-position: center; background-repeat: no-repeat;background-size: cover; '></div>");
            s.Append("</div>");
            s.Append("<div class='wrp'>");
            s.Append("<div class='WeHero_stories_inner'>");
            s.Append("<h2>" + titleItem + @"</h2>");
            s.Append("<div class='desc'>"+ desc.Replace("\n","<br/>") + "</div>");
            s.Append("<a href='" + link + "' " + target + @" class='btn'>Xem chi tiết</a>");
            s.Append("</div>");
            s.Append("</div>");
            s.Append("</section>");
        }
        return s.ToString();
    }
}
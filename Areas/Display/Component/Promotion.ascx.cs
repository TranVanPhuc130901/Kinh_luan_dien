using System;
using System.Text;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.MenusControl;
using RevosJsc.TSql;
using FolderPic = RevosJsc.AdvertistmentsControl.FolderPic;

public partial class Areas_Display_Component_MenuOther : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _pic = FolderPic.Advertistments;
    protected string Rewrite = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["rewrite"] != null) Rewrite = QueryStringExtension.GetQueryString("rewrite");
        if (Rewrite.Length < 1 && Session["rewrite"] != null) Rewrite = Session["rewrite"].ToString();
        //ltrAdv.Text = MobileExtension.IsMobileView() ? GetVideoBackground("2") : GetVideoBackground("1");

        var s = new StringBuilder();

        ltrPromotion.Text = GetList("9", "lazyload");
    }

    private string GetList(string pos, string cssClass)
    {
        var s = new StringBuilder();
        var fields = DataExtension.GetListColumns(
            AdvertistmentsColumns.VaTitle,
            AdvertistmentsColumns.VaParam,
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
        
        //s.Append("<video loop muted autoplay playsinline preload=''><source src='/video/outdoorclothings.webm' type='video/webm'></video>");
        if (dt.Rows.Count <= 0) return s.ToString();
        s.Append("<div class='promotionHomePage'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][AdvertistmentsColumns.VaTitle].ToString();
            var desc = dt.Rows[i][AdvertistmentsColumns.VaParam].ToString();
            var image = dt.Rows[i][AdvertistmentsColumns.VaImage].ToString();
            var link = dt.Rows[i][AdvertistmentsColumns.VaLink].ToString();
            if (link.Equals("")) link = "javascript://";
            var target = dt.Rows[i][AdvertistmentsColumns.IaTarget].ToString().Equals("1") ? "target='_blank'" : "target='_self'";
            s.Append(@"
        <a href='" + link + "' " + target + @" class='image'>" + ImagesExtension.GetImage(_pic, image, titleItem, "lazyload", false, false, "", false, true, "") + @"</a>
   ");
        }
        s.Append("</div>");
        return s.ToString();
    }
}
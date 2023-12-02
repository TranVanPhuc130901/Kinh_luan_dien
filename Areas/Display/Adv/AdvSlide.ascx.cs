using System;
using System.Text;
using RevosJsc.AdvertistmentsControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Display_Adv_AdvSlide : System.Web.UI.UserControl
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
        //ltrAdv.Text = MobileExtension.IsMobileView() ? GetList("10") : GetList("2");
        ltrAdv.Text = GetList("2");
    }

    private string GetVideoBackground(string pos)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(RevosJsc.FileLibraryControl.CodeApplications.FileLibraryGroupItem),
            GroupsTSql.GetByStatus("1"),
            GroupsTSql.GetByLang(_lang),
            GroupsTSql.GetByPosition(pos),
            ItemsTSql.GetByStatus("1")
        );
        var dt = GroupItems.GetAllData("1", "*", condition, "");
        if (dt.Rows.Count < 1) return "";
        s.Append("<section class='"+ (Rewrite.Length > 0 ? "WeHero" : "WeHero home") +"'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][ItemsColumns.ViTitle].ToString();
            var image = StringExtension.LayChuoi(dt.Rows[i][ItemsColumns.ViImage].ToString(), "", 1);
            var video = StringExtension.LayChuoi(dt.Rows[i][ItemsColumns.ViImage].ToString(), "", 2);

            s.Append("<video class='lazyloaded' loop='' muted='' autoplay='' poster='/"+ RevosJsc.FileLibraryControl.FolderPic.FileLibrary + "/" + image +"'>");
            if (video.EndsWith(".mp4")) s.Append("<source type='video/mp4' src='/" + RevosJsc.FileLibraryControl.FolderPic.FileLibrary + "/" + video + "'>");
            if (video.EndsWith(".webm")) s.Append("<source type='video/webm' src='/" + RevosJsc.FileLibraryControl.FolderPic.FileLibrary + "/" + video + "'>");
            s.Append("</video>");
        }

        //s.Append(GetList("2", "lazy"));
        s.Append("</section>");
        return s.ToString();
    }

    private string GetList(string pos)
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
        var dt = Advertistments.GetAllData("", fields, condition, AdvertistmentsColumns.IaSortOrder);
        s.Append("<section class='" + (Rewrite.Length > 0 ? "WeHero" : "WeHero home") + "'>");
        //s.Append("<video loop muted autoplay playsinline preload=''><source src='/video/outdoorclothings.webm' type='video/webm'></video>");
        if (dt.Rows.Count <= 0)
        {
            if (MobileExtension.IsMobileView())
            {
                //s.Append("<video loop muted autoplay playsinline preload=''><source src='/video/outdoor-clothings-1080x1920.mp4' type='video/mp4'></video>");
                s.Append("<video loop muted autoplay playsinline preload=''><source src='/video/outdoor-clothings-1080x1920.mp4' type='video/mp4'></video>");
            }
            else
            {
                s.Append("<video loop muted autoplay playsinline preload=''><source src='/video/outdoor-clothings.mp4' type='video/mp4'></video>");
            }
        }
        s.Append("<div class='image_bannerHeader owl-carousel'>");
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
        s.Append("</section>");
        return s.ToString();
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Developer.Extension;
using RevosJsc;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.NewsControl;
using RevosJsc.TSql;
using System.Web;

public partial class Areas_Display_News_Control_Detail : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _app = CodeApplications.News;
    private readonly string _pic = FolderPic.News;
    private readonly string _noResultText = LanguageExtension.TranslateKeyword("Updating ...");
    private string _iiid = "";
    private string _igid = "";
    protected string TitleItem = "";
    protected string LinkShare = "";
    protected string Image = "";
    protected string Published = "";
    protected string Modified = "";
    protected string Author = "";
    protected string Description = "";
    protected string BrandName = "";
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

        GetDataBySession();
    }
    private void GetDataBySession()
    {
        var dt = (DataTable)Session["dataByTitle"];
        UpdateTotalView(dt.Rows[0][ItemsColumns.IiId].ToString());
        TitleItem = dt.Rows[0][ItemsColumns.ViTitle].ToString();
        var image = dt.Rows[0][ItemsColumns.ViImage].ToString();
        var minread = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();
        var content = dt.Rows[0][ItemsColumns.ViContent].ToString();
        string[] words = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Đếm số ký tự trong văn bản
        int characterCount = words.Length;
        var minMinute = characterCount / 220;
        if (minMinute < 1)
        {
            minMinute = 1;
        }
        LinkShare = UrlExtension.WebsiteUrl + dt.Rows[0][ItemsColumns.ViLink] + RewriteExtension.Extensions;
        Image = UrlExtension.WebsiteUrl + _pic + "/" + image;
        Published = dt.Rows[0][ItemsColumns.DiDateCreated].ToString();
        Modified = dt.Rows[0][ItemsColumns.DiDateModified].ToString();
        Author = dt.Rows[0][ItemsColumns.ViAuthor].ToString();
        var brandName = SettingsExtension.GetSettingKey("KeyBrandName", _lang);
        Description = dt.Rows[0][ItemsColumns.ViDescription].ToString();
        BrandName = SettingsExtension.GetSettingKey("KeyBrandName", _lang);
        ltrInfo.Text = "<h1>"+ TitleItem + "</h1>";
        ltrInfo.Text += "<div class='author'>" + (Author.Length > 0 ? Author : brandName) + " / "+ minMinute + " min read</div>";
        ltrInfo.Text += "<div class='desc'>" + Description + "</div>";
        ltrContent.Text = content.Length > 0 ? content : _noResultText;
        //if (image.Length > 0) image += ".ashx?w=640";
        //if (MobileExtension.IsMobileView()) ltrBanner.Text = "<div class='banner'>"+ ImagesExtension.GetImage(_pic, image, TitleItem, "", false, true, content) +"</div>";
        //ltrOther.Text = GetOtherItem();
    }

    public void UpdateTotalView(string id)
    {
        //// Nếu là bot đọc thì bỏ qua
        ////Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)
        //if (Request.UserAgent != null && (Request.UserAgent.Contains("compatible;") || Request.UserAgent.Contains("facebook.com") || Request.UserAgent.Contains("ahrefs.com") || Request.UserAgent.Contains("google.com") || Request.UserAgent.Contains("bing.com") || Request.UserAgent.Contains("coccoc.com") || Request.UserAgent.Contains("apple.com"))) return;

        // Chỉ lưu 1 session 1 lần
        if (Session["Viewed" + id] != null) return;

        Items.UpdateValues("iiTotalView = iiTotalView + 1", ItemsTSql.GetById(id));

        Session["Viewed" + id] = id;
    }

}
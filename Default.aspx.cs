using System;
using System.Data;
using System.Web.UI;
using Developer.Extension;
using Developer.Keyword;
using RevosJsc;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.LanguageControl;
using RevosJsc.TSql;

public partial class _Default : Page
{
    private string _lang = Cookie.GetLanguageValueDisplay();
    private string _langRequest = "";

    #region Các querystring

    private string _p = "1";
    private string _title = "";
    private string _page = "";
    private string _rewrite = "";
    private string _key = "";

    #endregion Các querystring

    #region Các giá trị trong thẻ title, meta keywords, meta description

    private string _titleTagContent = "";
    private string _metaKewordsTagContent = "";
    private string _metaDescriptionTagContent = "";

    private string _imageShareSrc = "";
    private string _pic = "";
    private string _iid = "";
    private string _igid = "";

    #endregion Các giá trị trong thẻ title, meta keywords, meta description

    //protected override void Render(HtmlTextWriter writer)
    //{
    //    if (Request.Headers["X-MicrosoftAjax"] != "Delta=true")
    //    {
    //        var reg = new System.Text.RegularExpressions.Regex(@"<script[^>]*>[\w|\t|\r|\W]*?</script>");
    //        var sb = new System.Text.StringBuilder();
    //        var sw = new System.IO.StringWriter(sb);
    //        var hw = new HtmlTextWriter(sw);
    //        base.Render(hw);
    //        var html = sb.ToString();
    //        var myMatch = reg.Matches(html);
    //        html = reg.Replace(html, string.Empty);
    //        reg = new System.Text.RegularExpressions.Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}|(?=[\r])\s{2,}");
    //        html = reg.Replace(html, string.Empty);
    //        reg = new System.Text.RegularExpressions.Regex(@"</body>");
    //        var str = string.Empty;
    //        foreach (System.Text.RegularExpressions.Match match in myMatch)
    //        {
    //            str += match.ToString();
    //        }
    //        html = reg.Replace(html, str + "</body>");
    //        writer.Write(html);
    //    }
    //    else base.Render(writer);
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Kiểm tra nguồn người dùng
        // Kiểm tra người dùng đến từ link nào
        var previewsUrl = Request.ServerVariables["HTTP_REFERER"];
        if (previewsUrl != null)
        {
            if (previewsUrl.Contains("google.com"))
            {
                var gclid = MenuExtension.GetQueryString(Request.RawUrl, "gclid");
                var utmSource = MenuExtension.GetQueryString(Request.RawUrl, "utm_source");
                var utmMedium = MenuExtension.GetQueryString(Request.RawUrl, "utm_medium");
                var utmCampaign = MenuExtension.GetQueryString(Request.RawUrl, "utm_campaign");
                CookieExtension.SaveCookies("url_referrer", gclid.Length > 0 ? "Google Ads" : "Google search");
                if (utmSource.Length > 0) CookieExtension.SaveCookies("utm_source", utmSource);
                if (utmMedium.Length > 0) CookieExtension.SaveCookies("utm_medium", utmMedium);
                if (utmCampaign.Length > 0) CookieExtension.SaveCookies("utm_campaign", utmCampaign);
            }
            else if (previewsUrl.Contains("facebook.com")) CookieExtension.SaveCookies("url_referrer", "Facebook");
            else if (!previewsUrl.Contains(UrlExtension.WebsiteUrl)) CookieExtension.SaveCookies("url_referrer", previewsUrl);
        }
        // Trường hợp mở trong tab mới (Nếu đến từ Fb hoặc Gg thì link sẽ có tham số ?fbclid=... hoặc ?gclid=...)
        if (Request.RawUrl.Contains("?"))
        {
            var fbclid = MenuExtension.GetQueryString(Request.RawUrl.Remove(0, Request.RawUrl.IndexOf("?", StringComparison.Ordinal)), "fbclid");
            var gclid = MenuExtension.GetQueryString(Request.RawUrl.Remove(0, Request.RawUrl.IndexOf("?", StringComparison.Ordinal)), "gclid");
            var utmSource = MenuExtension.GetQueryString(Request.RawUrl.Remove(0, Request.RawUrl.IndexOf("?", StringComparison.Ordinal)), "utm_source");
            var utmMedium = MenuExtension.GetQueryString(Request.RawUrl.Remove(0, Request.RawUrl.IndexOf("?", StringComparison.Ordinal)), "utm_medium");
            var utmCampaign = MenuExtension.GetQueryString(Request.RawUrl.Remove(0, Request.RawUrl.IndexOf("?", StringComparison.Ordinal)), "utm_campaign");
            if (gclid.Length > 0)
            {
                CookieExtension.SaveCookies("url_referrer", "Google Ads");
                if (utmSource.Length > 0) CookieExtension.SaveCookies("utm_source", utmSource);
                if (utmMedium.Length > 0) CookieExtension.SaveCookies("utm_medium", utmMedium);
                if (utmCampaign.Length > 0) CookieExtension.SaveCookies("utm_campaign", utmCampaign);
            }
            else if (fbclid.Length > 0)
            {
                CookieExtension.SaveCookies("url_referrer", "Facebook");
            }
        }

        #endregion

        #region Các querystring

        if (Request.QueryString["p"] != null) _p = QueryStringExtension.GetQueryString("p");
        if (Request.QueryString["page"] != null) _page = QueryStringExtension.GetQueryString("page");
        if (Request.QueryString["rewrite"] != null) _rewrite = QueryStringExtension.GetQueryString("rewrite");
        if (Request.QueryString["title"] != null) _title = QueryStringExtension.GetQueryString("title");
        if (Request.QueryString["keyword"] != null) _key = QueryStringExtension.GetQueryString("keyword");
        if (Request.QueryString["lang"] != null) _langRequest = QueryStringExtension.GetQueryString("lang");

        //var myUri = new Uri(UrlExtension.WebsiteUrl + Request.RawUrl.Substring(1));
        //if (HttpUtility.ParseQueryString(myUri.Query).Get("q") != null) _key = HttpUtility.ParseQueryString(myUri.Query).Get("q") ?? "";
        //if (HttpUtility.ParseQueryString(myUri.Query).Get("p") != null) _p = HttpUtility.ParseQueryString(myUri.Query).Get("p") ?? "";

        if (_langRequest != _lang && _langRequest.Length > 0)
        {
            Cookie.SetLanguageValueDisplay(_langRequest);
            _lang = _langRequest;
        }
        #endregion Các querystring

        #region Chuyển hướng link

        var link = ChuyenHuongLink();
        if (link.Length > 0)
        {
            Response.Status = "301 Permanently";
            Response.AddHeader("Location", link);
            return;
        }
        #endregion

        if (IsPostBack) return;
        GetItemsOrGroupsInfoByTitle();
        if (_rewrite.Length < 1 && Session["rewrite"] != null) _rewrite = Session["rewrite"].ToString();
        GetTitleAndOtherTag();
        GetFavicon();
        GetGoogleAnalyticsCode();
    }

    #region Các phần tối ưu cho seo

    #region Chuyển hướng link
    private string ChuyenHuongLink()
    {
        if (Request.RawUrl.Equals("/")) return "";
        var condition = DataExtension.AndConditon(
            RedirectsTSql.GetByLink(Request.RawUrl),
            RedirectsTSql.GetByStatus("1")
            );
        var dt = Redirects.GetData("1", RedirectsColumns.VrLinkDestination, condition, "");
        return dt.Rows.Count > 0 ? dt.Rows[0][RedirectsColumns.VrLinkDestination].ToString().ToLower() : "";
    }
    #endregion

    #region Lấy thông tin theo Items

    /// <summary>
    /// Lấy các thông tin items, groups theo title, ưu tiên tìm items trước groups, dữ liệu sẽ lưu vào Session["dataByTitle"], đồng thời xác định igid hoặc iid và go
    /// </summary>
    private void GetItemsOrGroupsInfoByTitle()
    {
        #region Xóa các session cũ

        //Các Session lưu lại các thông tin để ko cần mất công lấy lại lần nữa tại các modul
        //Session["igid"]: igid hiện tại
        //Session["iiid"]: iiid hiện tại
        //Session["rewrite"]: RewriteExtension. hiện tại, vd: RewriteExtension.Product
        //Session["app"]: modul hiện tại: vd: RevosJsc.ProductControl.CodeApplications.Product
        //Session["apptitle"]: Tiêu đề modul hiện tại: vd: "Sản phẩm"
        //Session["dataByTitle"]: lưu dữ liệu tìm thấy theo title để khi vào trang chi tiết không cần lấy lại
        Session["rewrite"] = null;
        Session["igid"] = null; Session["iiid"] = null; Session["rewrite"] = null;
        Session["app"] = null; Session["apptitle"] = null;
        Session["dataByTitle"] = null;
        Session["dataByTitle_Category"] = null;//Lưu danh mục đang chứa items hiện tại

        #endregion Xóa các session cũ

        if (Request.QueryString["title"] != null)
        {
            #region Kiểm tra trước xem có items theo title này không

            var dt = RevosJsc.Database.Items.GetByTitle(_title, ItemsTSql.GetByStatus("1"));
            if (dt.Rows.Count > 0)
            {
                if (_lang != dt.Rows[0][ItemsColumns.ViLang].ToString())
                {
                    Cookie.SetLanguageValueDisplay(dt.Rows[0][ItemsColumns.ViLang].ToString());
                    Response.Redirect(UrlExtension.WebsiteUrl + Request.RawUrl.Substring(1));
                }
                Session["dataByTitle"] = dt;
                _iid = dt.Rows[0][ItemsColumns.IiId].ToString();
                Session["iiid"] = dt.Rows[0][ItemsColumns.IiId].ToString();
                Session["rewrite"] = GetRewriteByApp(dt.Rows[0][ItemsColumns.ViApp].ToString());

                //Lấy thông tin Groups của Items, có lưu Session["igid"]
                GetGroupsInfoByItemId(dt.Rows[0][ItemsColumns.IiId].ToString(), dt.Rows[0][ItemsColumns.ViApp].ToString());

                return;//Nếu có items rồi thì không cần kiểm tra groups nữa
            }

            #endregion Kiểm tra trước xem có items theo title này không

            #region Kiểm tra xem có groups theo title này không - chỉ kiểm tra khi không có items

            dt = Groups.GetDataByTitle(_title, GroupsTSql.GetByStatus("1"));
            if (dt.Rows.Count > 0)
            {
                if (_lang != dt.Rows[0][GroupsColumns.VgLang].ToString())
                {
                    Cookie.SetLanguageValueDisplay(dt.Rows[0][GroupsColumns.VgLang].ToString());
                    Response.Redirect(UrlExtension.WebsiteUrl + Request.RawUrl.Substring(1));
                }
                //Session["dataByTitle"] = dt;
                Session["dataByTitle_Category"] = dt;
                _igid = dt.Rows[0][GroupsColumns.IgId].ToString();
                Session["igid"] = dt.Rows[0][GroupsColumns.IgId].ToString();
                Session["rewrite"] = GetRewriteByApp(dt.Rows[0][GroupsColumns.VgApp].ToString());
            }

            #endregion Kiểm tra xem có groups theo title này không - chỉ kiểm tra khi không có items

            #region Nếu không có groups hay items thì chuyển về trang 404

            else
            {
                Response.Status = "404 Not Found";
                Response.StatusCode = 404;
                Session["rewrite"] = GetAppByRewrite(RewriteExtension.Error);
            }

            #endregion Nếu không có groups hay items thì chuyển về trang 404
        }
        else if (Request.QueryString["igid"] != null)
        {
            var dt = Groups.GetData("1", "*", DataExtension.AndConditon(GroupsTSql.GetByStatus("1"), GroupsTSql.GetById(Request.QueryString["igid"])), "");
            if (dt.Rows.Count <= 0) return;
            if (_lang != dt.Rows[0][GroupsColumns.VgLang].ToString())
            {
                Cookie.SetLanguageValueDisplay(dt.Rows[0][GroupsColumns.VgLang].ToString());
                Response.Redirect(UrlExtension.WebsiteUrl + Request.RawUrl.Substring(1));
            }
            //Session["dataByTitle"] = dt;
            Session["dataByTitle_Category"] = dt;
            _igid = dt.Rows[0][GroupsColumns.IgId].ToString();
            Session["igid"] = dt.Rows[0][GroupsColumns.IgId].ToString();
            Session["rewrite"] = GetRewriteByApp(dt.Rows[0][GroupsColumns.VgApp].ToString());
        }
        //Trường hợp vào trang chủ modul
        else if (Session["dataByTitle"] == null)
        {
            Session["rewrite"] = GetAppByRewrite(Request.QueryString["rewrite"]);
        }
    }

    private void GetGroupsInfoByItemId(string id, string viapp)
    {
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByStatus("1"),
            GroupsTSql.GetByApp(viapp),
            GroupsTSql.GetByLang(_lang),
            ItemsTSql.GetById(id)
            );
        var dt = GroupItems.GetAllData("1", "Groups.*", condition, "");
        if (dt.Rows.Count <= 0) return;
        Session["dataByTitle_Category"] = dt;
        Session["igid"] = dt.Rows[0][GroupsColumns.IgId].ToString();
    }

    #region Lấy go, pic theo app

    private string GetRewriteByApp(string app)
    {
        Session["app"] = app;

        switch (app)
        {
            case RevosJsc.AboutUsControl.CodeApplications.AboutUs:
                _rewrite = RewriteExtension.AboutUs;
                _pic = RevosJsc.AboutUsControl.FolderPic.AboutUs;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Giới thiệu");
                Session["rewrite"] = RewriteExtension.AboutUs;
                break;
            case RevosJsc.BlogControl.CodeApplications.Blog:
            case RevosJsc.BlogControl.CodeApplications.BlogGroupItem:
                _rewrite = RewriteExtension.Blog;
                _pic = RevosJsc.BlogControl.FolderPic.Blog;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Blog");
                Session["rewrite"] = RewriteExtension.Blog;
                break;
            case RevosJsc.DestinationControl.CodeApplications.Destination:
            case RevosJsc.DestinationControl.CodeApplications.DestinationGroupItem:
                _rewrite = RewriteExtension.Destination;
                _pic = RevosJsc.DestinationControl.FolderPic.Destination;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Destinations");
                Session["rewrite"] = RewriteExtension.Destination;
                break;
            case RevosJsc.ContactControl.CodeApplications.Contact:
                _rewrite = RewriteExtension.Contact;
                _pic = RevosJsc.ContactControl.FolderPic.Contact;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Liên hệ");
                Session["rewrite"] = RewriteExtension.Contact;
                break;
            case RevosJsc.CustomerControl.CodeApplications.Customer:
                _rewrite = RewriteExtension.Customer;
                _pic = RevosJsc.CustomerControl.FolderPic.Customer;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Giải pháp");
                Session["rewrite"] = RewriteExtension.Customer;
                break;
            case RevosJsc.ReviewsControl.CodeApplications.Reviews:
                _rewrite = RewriteExtension.Reviews;
                _pic = RevosJsc.ReviewsControl.FolderPic.Reviews;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Cảm nhận Khách hàng tiêu biểu");
                Session["rewrite"] = RewriteExtension.Reviews;
                break;
            case RevosJsc.CruisesControl.CodeApplications.Cruises:
            case RevosJsc.CruisesControl.CodeApplications.CruisesGroupItem:
                _rewrite = RewriteExtension.Cruises;
                _pic = RevosJsc.CruisesControl.FolderPic.Cruises;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Cruises");
                Session["rewrite"] = RewriteExtension.Cruises;
                break;
            case RevosJsc.FileLibraryControl.CodeApplications.FileLibrary:
                _rewrite = RewriteExtension.FileLibrary;
                _pic = RevosJsc.FileLibraryControl.FolderPic.FileLibrary;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Dữ liệu");
                Session["rewrite"] = RewriteExtension.FileLibrary;
                break;
            case RevosJsc.HotelControl.CodeApplications.Hotel:
                _rewrite = RewriteExtension.Hotel;
                _pic = RevosJsc.HotelControl.FolderPic.Hotel;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Hotel");
                Session["rewrite"] = RewriteExtension.Hotel;
                break;
            case RevosJsc.NewsControl.CodeApplications.News:
            case RevosJsc.NewsControl.CodeApplications.NewsGroupItem:
                _rewrite = RewriteExtension.News;
                _pic = RevosJsc.NewsControl.FolderPic.News;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Tin tức");
                Session["rewrite"] = RewriteExtension.News;
                break;
            case RevosJsc.OurTeamControl.CodeApplications.OurTeam:
                _rewrite = RewriteExtension.OurTeam;
                _pic = RevosJsc.OurTeamControl.FolderPic.OurTeam;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Our team");
                Session["rewrite"] = RewriteExtension.OurTeam;
                break;
            case RevosJsc.PhotoAlbumControl.CodeApplications.PhotoAlbum:
                _rewrite = RewriteExtension.PhotoAlbum;
                _pic = RevosJsc.PhotoAlbumControl.FolderPic.PhotoAlbum;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Thư viện");
                Session["rewrite"] = RewriteExtension.PhotoAlbum;
                break;
            case RevosJsc.ProductControl.CodeApplications.Product:
            case RevosJsc.ProductControl.CodeApplications.ProductGroupItem:
                _rewrite = RewriteExtension.Product;
                _pic = RevosJsc.ProductControl.FolderPic.Product;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Sản phẩm");
                Session["rewrite"] = RewriteExtension.Product;
                break;
            case RevosJsc.ProjectControl.CodeApplications.Project:
                _rewrite = RewriteExtension.Project;
                _pic = RevosJsc.ProjectControl.FolderPic.Project;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Dự án");
                Session["rewrite"] = RewriteExtension.Project;
                break;
            case RevosJsc.FAQControl.CodeApplications.FAQ:
                _rewrite = RewriteExtension.FAQ;
                _pic = RevosJsc.FAQControl.FolderPic.FAQ;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Hỏi đáp");
                Session["rewrite"] = RewriteExtension.FAQ;
                break;
            case RevosJsc.ServiceControl.CodeApplications.Service:
            case RevosJsc.ServiceControl.CodeApplications.ServiceGroupItem:
                _rewrite = RewriteExtension.Service;
                _pic = RevosJsc.ServiceControl.FolderPic.Service;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Thương hiệu");
                Session["rewrite"] = RewriteExtension.Service;
                break;
            case RevosJsc.TourControl.CodeApplications.Tour:
            case RevosJsc.TourControl.CodeApplications.TourGroupItem:
                _rewrite = RewriteExtension.Tour;
                _pic = RevosJsc.TourControl.FolderPic.Tour;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Nos voyages");
                Session["rewrite"] = RewriteExtension.Tour;
                break;
            case RevosJsc.VideoControl.CodeApplications.Video:
                _rewrite = RewriteExtension.Video;
                _pic = RevosJsc.VideoControl.FolderPic.Video;

                Session["apptitle"] = LanguageExtension.TranslateKeyword("Thư viện");
                Session["rewrite"] = RewriteExtension.Video;
                break;
            case RevosJsc.WebsiteControl.CodeApplications.Website:
                _rewrite = RewriteExtension.Website;
                _pic = RevosJsc.WebsiteControl.FolderPic.Website;

                Session["apptitle"] = WebsiteKeyword.Website1;
                Session["rewrite"] = RewriteExtension.Website;
                break;
            default:
                if (app == RewriteExtension.Search)
                {
                    _rewrite = RewriteExtension.Search;
                    _pic = "";

                    Session["apptitle"] = LanguageExtension.TranslateKeyword("Tìm kiếm");
                    Session["rewrite"] = RewriteExtension.Search;
                }
                break;
        }

        return _rewrite;
    }

    private string GetAppByRewrite(string go)
    {
        if (go == RewriteExtension.AboutUs)
        {
            _pic = RevosJsc.AboutUsControl.FolderPic.AboutUs;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Giới thiệu");
            Session["rewrite"] = RewriteExtension.AboutUs;
            Session["app"] = RevosJsc.AboutUsControl.CodeApplications.AboutUs;
        }
        else if (go == RewriteExtension.Blog)
        {
            _pic = RevosJsc.BlogControl.FolderPic.Blog;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Blog");
            Session["rewrite"] = RewriteExtension.Blog;
            Session["app"] = RevosJsc.BlogControl.CodeApplications.Blog;
        }
        else if (go == RewriteExtension.Contact)
        {
            _pic = RevosJsc.ContactControl.FolderPic.Contact;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Liên hệ");
            Session["rewrite"] = RewriteExtension.Contact;
            Session["app"] = RevosJsc.ContactControl.CodeApplications.Contact;
        }
        else if (go == RewriteExtension.Customer)
        {
            _pic = RevosJsc.CustomerControl.FolderPic.Customer;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Giải pháp");
            Session["rewrite"] = RewriteExtension.Customer;
            Session["app"] = RevosJsc.CustomerControl.CodeApplications.Customer;
        }
        else if (go == RewriteExtension.Destination)
        {
            _pic = RevosJsc.DestinationControl.FolderPic.Destination;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Destinations");
            Session["rewrite"] = RewriteExtension.Destination;
            Session["app"] = RevosJsc.DestinationControl.CodeApplications.Destination;
        }
        else if (go == RewriteExtension.Error)
        {
            Session["apptitle"] = LanguageExtension.TranslateKeyword("Error");
            Session["rewrite"] = RewriteExtension.Error;
        }
        else if (go == RewriteExtension.Reviews)
        {
            _pic = RevosJsc.ReviewsControl.FolderPic.Reviews;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Cảm nhận Khách hàng tiêu biểu");
            Session["rewrite"] = RewriteExtension.Reviews;
            Session["app"] = RevosJsc.ReviewsControl.CodeApplications.Reviews;
        }
        else if (go == RewriteExtension.Cruises)
        {
            _pic = RevosJsc.CruisesControl.FolderPic.Cruises;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Cruises");
            Session["rewrite"] = RewriteExtension.Cruises;
            Session["app"] = RevosJsc.CruisesControl.CodeApplications.Cruises;
        }
        else if (go == RewriteExtension.FileLibrary)
        {
            _pic = RevosJsc.FileLibraryControl.FolderPic.FileLibrary;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Dữ liệu");
            Session["rewrite"] = RewriteExtension.FileLibrary;
            Session["app"] = RevosJsc.FileLibraryControl.CodeApplications.FileLibrary;
        }
        else if (go == RewriteExtension.Hotel)
        {
            _pic = RevosJsc.HotelControl.FolderPic.Hotel;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Hotel");
            Session["rewrite"] = RewriteExtension.Hotel;
            Session["app"] = RevosJsc.HotelControl.CodeApplications.Hotel;
        }
        else if (go == RewriteExtension.News)
        {
            _pic = RevosJsc.NewsControl.FolderPic.News;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Tin tức");
            Session["rewrite"] = RewriteExtension.News;
            Session["app"] = RevosJsc.NewsControl.CodeApplications.News;
        }
        else if (go == RewriteExtension.PhotoAlbum)
        {
            _pic = RevosJsc.PhotoAlbumControl.FolderPic.PhotoAlbum;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Thư viện");
            Session["rewrite"] = RewriteExtension.PhotoAlbum;
            Session["app"] = RevosJsc.PhotoAlbumControl.CodeApplications.PhotoAlbum;
        }
        else if (go == RewriteExtension.Member)
        {
            _pic = RevosJsc.MemberControl.FolderPic.Member;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Thành viên");
            Session["rewrite"] = RewriteExtension.Member;
            Session["app"] = RevosJsc.MemberControl.CodeApplications.Member;
        }
        else if (go == RewriteExtension.OurTeam)
        {
            _pic = RevosJsc.OurTeamControl.FolderPic.OurTeam;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Our team");
            Session["rewrite"] = RewriteExtension.OurTeam;
            Session["app"] = RevosJsc.OurTeamControl.CodeApplications.OurTeam;
        }
        else if (go == RewriteExtension.PhotoAlbum)
        {
            _pic = RevosJsc.PhotoAlbumControl.FolderPic.PhotoAlbum;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("PhotoAlbum");
            Session["rewrite"] = RewriteExtension.PhotoAlbum;
            Session["app"] = RevosJsc.PhotoAlbumControl.CodeApplications.PhotoAlbum;
        }
        else if (go == RewriteExtension.Product)
        {
            _pic = RevosJsc.ProductControl.FolderPic.Product;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Sản phẩm");
            Session["rewrite"] = RewriteExtension.Product;
            Session["app"] = RevosJsc.ProductControl.CodeApplications.Product;
        }
        else if (go == RewriteExtension.Project)
        {
            _pic = RevosJsc.ProjectControl.FolderPic.Project;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Dự án");
            Session["rewrite"] = RewriteExtension.Project;
            Session["app"] = RevosJsc.ProjectControl.CodeApplications.Project;
        }
        else if (go == RewriteExtension.FAQ)
        {
            _pic = RevosJsc.FAQControl.FolderPic.FAQ;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Hỏi đáp");
            Session["rewrite"] = RewriteExtension.FAQ;
            Session["app"] = RevosJsc.FAQControl.CodeApplications.FAQ;
        }
        else if (go == RewriteExtension.Service)
        {
            _pic = RevosJsc.ServiceControl.FolderPic.Service;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Thương hiệu");
            Session["rewrite"] = RewriteExtension.Service;
            Session["app"] = RevosJsc.ServiceControl.CodeApplications.Service;
        }
        else if (go == RewriteExtension.Tour)
        {
            _pic = RevosJsc.TourControl.FolderPic.Tour;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Nos voyages");
            Session["rewrite"] = RewriteExtension.Tour;
            Session["app"] = RevosJsc.TourControl.CodeApplications.Tour;
        }
        else if (go == RewriteExtension.Video)
        {
            _pic = RevosJsc.VideoControl.FolderPic.Video;

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Thư viện");
            Session["rewrite"] = RewriteExtension.Video;
            Session["app"] = RevosJsc.VideoControl.CodeApplications.Video;
        }
        else if (go == RewriteExtension.Website)
        {
            _pic = RevosJsc.WebsiteControl.FolderPic.Website;

            Session["apptitle"] = WebsiteKeyword.Website1;
            Session["rewrite"] = RewriteExtension.Website;
            Session["app"] = RevosJsc.WebsiteControl.CodeApplications.Website;
        }
        else if (go == RewriteExtension.Search)
        {
            _pic = "";

            Session["apptitle"] = LanguageExtension.TranslateKeyword("Tìm kiếm");
            Session["rewrite"] = RewriteExtension.Search;
            Session["app"] = RewriteExtension.Search;
        }

        return go;
    }

    #endregion Lấy go, pic theo app

    #endregion Lấy thông tin theo Items

    #region Gán giá trị cho thẻ title, các thẻ meta keywords, meta description

    /// <summary>
    /// Gán giá trị cho thẻ title, các thẻ meta keywords, meta description
    /// </summary>
    private void GetTitleAndOtherTag()
    {
        #region Có groups hoặc items

        if (Session["dataByTitle"] != null)
        {
            var dt = (DataTable)Session["dataByTitle"];

            ltrMetaOther.Text = "<link rel=\"canonical\" href=\"" + UrlExtension.WebsiteUrl + dt.Rows[0][ItemsColumns.ViLink] + RewriteExtension.Extensions + "\" />";

            #region titleTagContent

            _titleTagContent = dt.Rows[0][ItemsColumns.ViMetaTitle] + (_p != "1" ? " - Trang " + _p : "");
            if (_titleTagContent == "") _titleTagContent = dt.Rows[0][ItemsColumns.ViTitle] + (_p != "1" ? " - Trang " + _p : "");

            #endregion titleTagContent

            #region metaKeywordsTagContent

            _metaKewordsTagContent = dt.Rows[0][ItemsColumns.ViMetaKeyword].ToString();
            if (_metaKewordsTagContent == "") _metaKewordsTagContent = dt.Rows[0][ItemsColumns.ViTitle].ToString();

            #endregion metaKeywordsTagContent

            #region metaDescriptionTagContent

            _metaDescriptionTagContent = dt.Rows[0][ItemsColumns.ViMetaDescription] + (_p != "1" ? " - Trang " + _p : "");
            if (_metaDescriptionTagContent == "") _metaDescriptionTagContent = dt.Rows[0][ItemsColumns.ViDescription] + (_p != "1" ? " - Trang " + _p : "");

            #endregion metaDescriptionTagContent

            #region imageShareSrc

            _imageShareSrc = GetImageShareSrc(dt.Rows[0][ItemsColumns.ViImage].ToString(), dt.Rows[0][ItemsColumns.ViContent].ToString());

            #endregion imageShareSrc
        }

        else if (Session["dataByTitle_Category"] != null)
        {
            var dt = (DataTable)Session["dataByTitle_Category"];

            ltrMetaOther.Text = "<link rel=\"canonical\" href=\"" + UrlExtension.WebsiteUrl + dt.Rows[0][GroupsColumns.VgLink] + RewriteExtension.Extensions + "\" />";

            #region titleTagContent

            _titleTagContent = dt.Rows[0][GroupsColumns.VgMetaTitle] + (_p != "1" ? " - Trang " + _p : "");
            if (_titleTagContent == "") _titleTagContent = dt.Rows[0][GroupsColumns.VgName] + (_p != "1" ? " - Trang " + _p : "");

            #endregion titleTagContent

            #region metaKeywordsTagContent

            _metaKewordsTagContent = dt.Rows[0][GroupsColumns.VgMetaKeyword].ToString();
            if (_metaKewordsTagContent == "")
                _metaKewordsTagContent = dt.Rows[0][GroupsColumns.VgName].ToString();

            #endregion metaKeywordsTagContent

            #region metaDescriptionTagContent

            _metaDescriptionTagContent = dt.Rows[0][GroupsColumns.VgMetaDescription] + (_p != "1" ? " - Trang " + _p : "");
            if (_metaDescriptionTagContent == "") _metaDescriptionTagContent = dt.Rows[0][GroupsColumns.VgDescription] + (_p != "1" ? " - Trang " + _p : "");

            #endregion metaDescriptionTagContent

            #region imageShareSrc

            _imageShareSrc = GetImageShareSrc(dt.Rows[0][GroupsColumns.VgImage].ToString(), dt.Rows[0][GroupsColumns.VgContent].ToString());
            if (_imageShareSrc == "") _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

            #endregion imageShareSrc
        }
        #endregion Có groups hoặc items

        #region else - lấy theo trang chính hoặc trang chủ

        else
        {
            ltrMetaOther.Text = "<link rel=\"canonical\" href=\"" + UrlExtension.WebsiteUrl + _rewrite + RewriteExtension.Extensions + "\" />";
            // Lấy theo trang Project
            if (_rewrite == RewriteExtension.AboutUs)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.AboutUsControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.AboutUsControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.AboutUsControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang Product
            else if(_rewrite == RewriteExtension.Product)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.ProductControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = _key + " - " + SettingsExtension.GetSettingKey(RevosJsc.ProductControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = _key + " - " + SettingsExtension.GetSettingKey(RevosJsc.ProductControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang liên hệ
            else if (_rewrite == RewriteExtension.Contact)
            {
                #region titleTagContent

                _titleTagContent = LanguageExtension.TranslateKeyword("Liên hệ");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(SettingsExtension.KeyKeywordMetaWebsite, _lang);

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(SettingsExtension.KeyDescMetatagWebsite, _lang);

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang Cruises
            else if (_rewrite == RewriteExtension.Cruises)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.CruisesControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.CruisesControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.CruisesControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang Customer
            else if (_rewrite == RewriteExtension.Customer)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.CustomerControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.CustomerControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.CustomerControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang CustomerReviews
            else if (_rewrite == RewriteExtension.Reviews)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.ReviewsControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.ReviewsControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.ReviewsControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang Destination
            else if (_rewrite == RewriteExtension.Destination)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.DestinationControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.DestinationControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.DestinationControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang FAQ
            else if (_rewrite == RewriteExtension.FAQ)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.FAQControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.FAQControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.FAQControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang FileLibrary
            else if (_rewrite == RewriteExtension.FileLibrary)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.FileLibraryControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.FileLibraryControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.FileLibraryControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang FileLibrary
            else if (_rewrite == RewriteExtension.Hotel)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.HotelControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.HotelControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.HotelControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang OurTeam
            else if (_rewrite == RewriteExtension.OurTeam)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.OurTeamControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.OurTeamControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.OurTeamControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang Project
            else if (_rewrite == RewriteExtension.Project)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.ProjectControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.ProjectControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.ProjectControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang Service
            else if (_rewrite == RewriteExtension.Service)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.ServiceControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.ServiceControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.ServiceControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang Blog
            else if (_rewrite == RewriteExtension.Blog)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.BlogControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.BlogControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.BlogControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang News
            else if (_rewrite == RewriteExtension.News)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.NewsControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "") + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.NewsControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "") + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.NewsControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "") + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang PhotoAlbum
            else if (_rewrite == RewriteExtension.PhotoAlbum)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.PhotoAlbumControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.PhotoAlbumControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.PhotoAlbumControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang Video
            else if (_rewrite == RewriteExtension.Video)
            {
                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(RevosJsc.VideoControl.SettingKey.MetaTitle, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(RevosJsc.VideoControl.SettingKey.MetaKeyword, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(RevosJsc.VideoControl.SettingKey.MetaDescription, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }

            // Lấy theo trang Search
            else if (_rewrite == RewriteExtension.Search)
            {
                #region titleTagContent

                _titleTagContent = _key + " - Tìm kiếm" + (_p != "1" ? " - Trang " + _p : "");

                #endregion titleTagContent

                ltrMetaOther.Text += "<meta name='robots' content='noindex, nofollow'/>";

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(SettingsExtension.KeyKeywordMetaWebsite, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(SettingsExtension.KeyDescMetatagWebsite, _lang) + (_p != "1" ? " - Trang " + _p : "");

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            // Lấy theo trang Error
            else if (_rewrite == RewriteExtension.Error)
            {
                #region titleTagContent

                _titleTagContent = "Page Not Found";

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(SettingsExtension.KeyKeywordMetaWebsite, _lang);

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(SettingsExtension.KeyDescMetatagWebsite, _lang);

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
            //Lấy theo trang chủ
            else
            {
                ltrMetaOther.Text = "<link rel=\"canonical\" href=\"" + UrlExtension.WebsiteUrl + "\" />";

                #region titleTagContent

                _titleTagContent = SettingsExtension.GetSettingKey(SettingsExtension.KeyTitleWebsite, _lang);

                #endregion titleTagContent

                #region metaKeywordsTagContent

                _metaKewordsTagContent = SettingsExtension.GetSettingKey(SettingsExtension.KeyKeywordMetaWebsite, _lang);

                #endregion metaKeywordsTagContent

                #region metaDescriptionTagContent

                _metaDescriptionTagContent = SettingsExtension.GetSettingKey(SettingsExtension.KeyDescMetatagWebsite, _lang);

                #endregion metaDescriptionTagContent

                #region imageShareSrc

                _imageShareSrc = UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);

                #endregion imageShareSrc
            }
        }

        #endregion else - lấy theo modul hoặc trang chủ

        #region Gán giá trị ra trang html

        ltrTitle.Text = _titleTagContent;
        ltrMetaOther.Text += "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>";
        ltrMetaOther.Text += "<meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1'>";
        ltrMetaOther.Text += "<meta name=\"keywords\" content=\"" + _metaKewordsTagContent + "\" />";
        ltrMetaOther.Text += "<meta name=\"description\" content=\"" + _metaDescriptionTagContent + "\" />";

        #endregion Gán giá trị ra trang html

        #region Các thẻ share facebook

        ltrMetaShare.Text = "<meta property=\"og:og:locale\" content=\"vi_VN\" />";
        ltrMetaShare.Text += "<meta property=\"og:type\" content=\"" + GetContentType() + "\" />";
        ltrMetaShare.Text += "<meta property=\"og:url\" content=\"" + GetUrl() + "\" />";
        ltrMetaShare.Text += "<meta property=\"og:title\" content=\"" + _titleTagContent + "\" />";
        ltrMetaShare.Text += "<meta property=\"og:image\" content=\"" + _imageShareSrc + "\" />";
        ltrMetaShare.Text += "<meta property=\"og:description\" content=\"" + _metaDescriptionTagContent + "\" />";

        #endregion Các thẻ share facebook
    }

    #endregion Gán giá trị cho thẻ title, các thẻ meta keywords, meta description

    #region GoogleAnalytics và các mã có thể gắn vào head

    private void GetGoogleAnalyticsCode()
    {
        ltrCodeOnHead.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyCodeOnHead, _lang);
        ltrCodeOnBody.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyCodeOnBody, _lang);
        ltrCodeUnderBody.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyCodeUnderBody, _lang);
    }
    #endregion

    #region Favicon

    private void GetFavicon()
    {
        ltrFavicon.Text = "<link rel=\"Shortcut icon\" href=\"" + UrlExtension.WebsiteUrl + RevosJsc.SystemWebsiteControl.FolderPic.SystemWebsite + "/" + SettingsExtension.GetSettingKey(SettingsExtension.KeyFavicon, _lang) + "\" type=\"image/x-icon\"/>";
    }

    #endregion Favicon

    #region Các thẻ meta cho share facebook

    private string GetUrl()
    {
        var s = "";
        s = (Request.Url.GetLeftPart(UriPartial.Authority) + Request.RawUrl).ToLower();
        if (s.EndsWith("default.aspx?")) s = s.Remove(s.Length - "default.aspx?".Length);
        if (s.EndsWith("default.aspx")) s = s.Remove(s.Length - "default.aspx".Length);
        return s;
    }

    private string GetContentType()
    {
        var s = "website";
        if (_rewrite == RewriteExtension.Product && _igid.Length > 0 && _iid == "") s = "category";
        else if (_rewrite == RewriteExtension.Product && _iid.Length > 0) s = "product";
        else if (_igid.Length > 0 && _iid == "") s = "object";
        else if (_iid.Length > 0) s = "article";
        else if (_rewrite == RewriteExtension.Video) s = "video.movie";
        return s;
    }

    /// <summary>
    /// Lấy đường dẫn tới ảnh nếu image trống sẽ lấy ảnh đầu tiên trong nội dung
    /// </summary>
    /// <param name="image"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    private string GetImageShareSrc(string image, string content)
    {
        const string split = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        if (image.Length < 1) return ImagesExtension.GetFirstImageInContent(content);
        if (image.IndexOf(split, StringComparison.Ordinal) > -1) image = StringExtension.LayChuoi(image, "", 1);
        return UrlExtension.WebsiteUrl + _pic + "/" + image.Replace("_HasThumb", "_HasThumb_Thumb");
    }

    #endregion Các thẻ meta cho share facebook

    #endregion Các phần tối ưu cho seo
}
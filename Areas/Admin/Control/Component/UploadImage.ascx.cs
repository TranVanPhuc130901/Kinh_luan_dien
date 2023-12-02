using System;
using System.IO;
using System.Web;
using Developer.Extension;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;
using RevosJsc.Extension;
using RevosJsc.LanguageControl;

public partial class Areas_Admin_Control_Component_UploadImage : System.Web.UI.UserControl
{
    /*
     Hướng dẫn sử dụng
    ============Tại PageLoad gọi===========================
    #region Gán app, pic cho user control upload ảnh đại diện
    UploadImage.App = app;
    UploadImage.Pic = pic;
    #endregion
    
    
    ============Gọi hàm để hiện thị ảnh khi update danh mục, sản phẩm=============
    UploadImage.Load(dt.Rows[0][GroupsColumns.VgimageColumn].ToString());
    
    
    ============Gọi hàm lúc click nút insert, update để lưu và lấy ra tên ảnh được lưu. contentDetail là nội dung để nếu cần sẽ lấy ảnh đầu tiên trong đó làm ảnh đại diện
    Insert: string image = UploadImage.Save(false, contentDetail);
    Update: string image = UploadImage.Save(true, contentDetail);
    
    ============Gọi hàm reset sau khi insert xong
    UploadImage.Reset();
     */

    #region Các thuộc tính được truyền vào từ ngoài
    #region Tên hiển thị control
    /// <summary>
    /// Tên hiển thị
    /// </summary>
    public string Text { set { ltrText.Text = value; } }
    #endregion

    #region Control - xác định dùng cho modul nào để tự động lấy thư mục pic, lấy setting
    private string _control = "";
    /// <summary>
    /// Control - xác định dùng cho trang nào để tự động lấy thư mục pic, lấy setting, ví dụ: RevosJsc.ProductControl.CodeApplication.Product. Cần gán ngay tại Page_Load
    /// </summary>
    public string Control { set { _control = value; } }
    #endregion

    #region Pic
    private string _pic = "";
    /// <summary>
    ///  Cần gán ngay tại Page_Load
    /// </summary>
    public string Pic { set { _pic = value; } }
    #endregion

    #region Tạo ảnh nhỏ
    private bool _taoAnhNho;
    /// <summary>
    ///  Cần gán ngay tại Page_Load
    /// </summary>
    public bool TaoAnhNho
    {
        set
        {
            _taoAnhNho = value;
            pnTaoAnhNho.Visible = value;
            cbTaoAnhNho.Checked = value;
        }
    }
    #endregion

    #region Giới hạn kích thước ảnh
    private bool _hanCheKichThuoc;
    /// <summary>
    ///  Cần gán ngay tại Page_Load
    /// </summary>
    public bool HanCheKichThuoc
    {
        set
        {
            _hanCheKichThuoc = value;
            pnHanCheKichThuoc.Visible = value;
            cbHanCheKichThuoc.Checked = value;
        }
    }
    #endregion

    #region Ảnh cũ - hdImage.Value
    public string HdImage { set { hdImage.Value = value; } }
    #endregion

    #region Lấy ảnh từ nội dung
    private bool _layAnhTuNoiDung;
    /// <summary>
    /// Có hiện checkbox lấy ảnh từ nội dung hay không
    /// </summary>
    public bool LayAnhTuNoiDung
    {
        set
        {
            _layAnhTuNoiDung = value;
            pnLayAnhTuNoiDung.Visible = value;
            cbLayAnhTuNoiDung.Checked = value;
        }
    }
    #endregion
    #endregion

    private readonly string _language = Cookie.GetLanguageValueAdmin();

    #region Các control lưu trữ giá trị tạm thời theo từng modul
    string _hanCheKichThuocAnh = "";
    string _hanCheKichThuocAnhMaxWidth = "";
    string _hanCheKichThuocAnhMaxHeight = "";
    string _taoAnhNhoChoAnh = "";
    string _taoAnhNhoChoAnhMaxWidth = "";
    string _taoAnhNhoChoAnhMaxHeight = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Kiểm tra xem pic, app đã được gán chưa
        if (_control == "" || _pic == "") throw new Exception("Chưa khởi tạo App, Pic. Vui lòng xem hướng dẫn tại control TempControls\\InsertForm\\UploadImage");
        #endregion

        SetPropertiesByModul();
        if (!IsPostBack)
        {
            KhoiTaoXuLyAnh();
        }
    }

    #region Gán pic, các cấu hình ảnh theo modul
    private void SetPropertiesByModul()
    {

        switch (_control)
        {
            case RevosJsc.AboutUsControl.CodeApplications.AboutUs:
                _pic = RevosJsc.AboutUsControl.FolderPic.AboutUs;

                _hanCheKichThuocAnh = RevosJsc.AboutUsControl.SettingKey.HanCheKichThuocAnhAboutUs;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.AboutUsControl.SettingKey.HanCheKichThuocAnhAboutUs_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.AboutUsControl.SettingKey.HanCheKichThuocAnhAboutUs_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.AboutUsControl.SettingKey.TaoAnhNhoChoAnhAboutUs;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.AboutUsControl.SettingKey.TaoAnhNhoChoAnhAboutUs_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.AboutUsControl.SettingKey.TaoAnhNhoChoAnhAboutUs_MaxHeight;
                break;
            case RevosJsc.BlogControl.CodeApplications.Blog:
                _pic = RevosJsc.BlogControl.FolderPic.Blog;

                _hanCheKichThuocAnh = RevosJsc.BlogControl.SettingKey.HanCheKichThuocAnhBlog;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.BlogControl.SettingKey.HanCheKichThuocAnhBlog_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.BlogControl.SettingKey.HanCheKichThuocAnhBlog_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.BlogControl.SettingKey.TaoAnhNhoChoAnhBlog;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.BlogControl.SettingKey.TaoAnhNhoChoAnhBlog_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.BlogControl.SettingKey.TaoAnhNhoChoAnhBlog_MaxHeight;
                break;


            case RevosJsc.ContactControl.CodeApplications.Contact:
                _pic = RevosJsc.ContactControl.FolderPic.Contact;

                _hanCheKichThuocAnh = RevosJsc.ContactControl.SettingKey.HanCheKichThuocAnhContact;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.ContactControl.SettingKey.HanCheKichThuocAnhContact_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.ContactControl.SettingKey.HanCheKichThuocAnhContact_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.ContactControl.SettingKey.TaoAnhNhoChoAnhContact;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.ContactControl.SettingKey.TaoAnhNhoChoAnhContact_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.ContactControl.SettingKey.TaoAnhNhoChoAnhContact_MaxHeight;
                break;


            case RevosJsc.CustomerControl.CodeApplications.Customer:
                _pic = RevosJsc.CustomerControl.FolderPic.Customer;

                _hanCheKichThuocAnh = RevosJsc.CustomerControl.SettingKey.HanCheKichThuocAnhCustomer;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.CustomerControl.SettingKey.HanCheKichThuocAnhCustomer_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.CustomerControl.SettingKey.HanCheKichThuocAnhCustomer_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.CustomerControl.SettingKey.TaoAnhNhoChoAnhCustomer;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.CustomerControl.SettingKey.TaoAnhNhoChoAnhCustomer_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.CustomerControl.SettingKey.TaoAnhNhoChoAnhCustomer_MaxHeight;
                break;


            case RevosJsc.ReviewsControl.CodeApplications.Reviews:
                _pic = RevosJsc.ReviewsControl.FolderPic.Reviews;

                _hanCheKichThuocAnh = RevosJsc.ReviewsControl.SettingKey.HanCheKichThuocAnhReviews;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.ReviewsControl.SettingKey.HanCheKichThuocAnhReviews_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.ReviewsControl.SettingKey.HanCheKichThuocAnhReviews_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.ReviewsControl.SettingKey.TaoAnhNhoChoAnhReviews;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.ReviewsControl.SettingKey.TaoAnhNhoChoAnhReviews_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.ReviewsControl.SettingKey.TaoAnhNhoChoAnhReviews_MaxHeight;
                break;

            case RevosJsc.CruisesControl.CodeApplications.Cruises:
                _pic = RevosJsc.CruisesControl.FolderPic.Cruises;

                _hanCheKichThuocAnh = RevosJsc.CruisesControl.SettingKey.HanCheKichThuocAnhCruises;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.CruisesControl.SettingKey.HanCheKichThuocAnhCruises_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.CruisesControl.SettingKey.HanCheKichThuocAnhCruises_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.CruisesControl.SettingKey.TaoAnhNhoChoAnhCruises;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.CruisesControl.SettingKey.TaoAnhNhoChoAnhCruises_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.CruisesControl.SettingKey.TaoAnhNhoChoAnhCruises_MaxHeight;
                break;


            case RevosJsc.FileLibraryControl.CodeApplications.FileLibrary:
                _pic = RevosJsc.FileLibraryControl.FolderPic.FileLibrary;

                _hanCheKichThuocAnh = RevosJsc.FileLibraryControl.SettingKey.HanCheKichThuocAnhFileLibrary;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.FileLibraryControl.SettingKey.HanCheKichThuocAnhFileLibrary_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.FileLibraryControl.SettingKey.HanCheKichThuocAnhFileLibrary_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.FileLibraryControl.SettingKey.TaoAnhNhoChoAnhFileLibrary;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.FileLibraryControl.SettingKey.TaoAnhNhoChoAnhFileLibrary_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.FileLibraryControl.SettingKey.TaoAnhNhoChoAnhFileLibrary_MaxHeight;
                break;


            case RevosJsc.HotelControl.CodeApplications.Hotel:
                _pic = RevosJsc.HotelControl.FolderPic.Hotel;

                _hanCheKichThuocAnh = RevosJsc.HotelControl.SettingKey.HanCheKichThuocAnhHotel;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.HotelControl.SettingKey.HanCheKichThuocAnhHotel_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.HotelControl.SettingKey.HanCheKichThuocAnhHotel_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.HotelControl.SettingKey.TaoAnhNhoChoAnhHotel;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.HotelControl.SettingKey.TaoAnhNhoChoAnhHotel_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.HotelControl.SettingKey.TaoAnhNhoChoAnhHotel_MaxHeight;
                break;

            case RevosJsc.MemberControl.CodeApplications.Member:
                _pic = RevosJsc.MemberControl.FolderPic.Member;

                _hanCheKichThuocAnh = RevosJsc.MemberControl.SettingKey.HanCheKichThuocAnhMember;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.MemberControl.SettingKey.HanCheKichThuocAnhMember_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.MemberControl.SettingKey.HanCheKichThuocAnhMember_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.MemberControl.SettingKey.TaoAnhNhoChoAnhMember;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.MemberControl.SettingKey.TaoAnhNhoChoAnhMember_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.MemberControl.SettingKey.TaoAnhNhoChoAnhMember_MaxHeight;
                break;

            case RevosJsc.NewsControl.CodeApplications.News:
                _pic = RevosJsc.NewsControl.FolderPic.News;

                _hanCheKichThuocAnh = RevosJsc.NewsControl.SettingKey.HanCheKichThuocAnhNews;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.NewsControl.SettingKey.HanCheKichThuocAnhNews_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.NewsControl.SettingKey.HanCheKichThuocAnhNews_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.NewsControl.SettingKey.TaoAnhNhoChoAnhNews;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.NewsControl.SettingKey.TaoAnhNhoChoAnhNews_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.NewsControl.SettingKey.TaoAnhNhoChoAnhNews_MaxHeight;
                break;

            case RevosJsc.PhotoAlbumControl.CodeApplications.PhotoAlbum:
                _pic = RevosJsc.PhotoAlbumControl.FolderPic.PhotoAlbum;

                _hanCheKichThuocAnh = RevosJsc.PhotoAlbumControl.SettingKey.HanCheKichThuocAnhPhotoAlbum;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.PhotoAlbumControl.SettingKey.HanCheKichThuocAnhPhotoAlbum_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.PhotoAlbumControl.SettingKey.HanCheKichThuocAnhPhotoAlbum_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.PhotoAlbumControl.SettingKey.TaoAnhNhoChoAnhPhotoAlbum;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.PhotoAlbumControl.SettingKey.TaoAnhNhoChoAnhPhotoAlbum_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.PhotoAlbumControl.SettingKey.TaoAnhNhoChoAnhPhotoAlbum_MaxHeight;
                break;


            case RevosJsc.ProductControl.CodeApplications.Product:
                _pic = RevosJsc.ProductControl.FolderPic.Product;

                _hanCheKichThuocAnh = RevosJsc.ProductControl.SettingKey.HanCheKichThuocAnhProduct;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.ProductControl.SettingKey.HanCheKichThuocAnhProduct_MaxHeight;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.ProductControl.SettingKey.HanCheKichThuocAnhProduct_MaxWidth;
                _taoAnhNhoChoAnh = RevosJsc.ProductControl.SettingKey.TaoAnhNhoChoAnhProduct;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.ProductControl.SettingKey.TaoAnhNhoChoAnhProduct_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.ProductControl.SettingKey.TaoAnhNhoChoAnhProduct_MaxHeight;
                break;

            case RevosJsc.FAQControl.CodeApplications.FAQ:
                _pic = RevosJsc.FAQControl.FolderPic.FAQ;

                _hanCheKichThuocAnh = RevosJsc.FAQControl.SettingKey.HanCheKichThuocAnhFAQ;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.FAQControl.SettingKey.HanCheKichThuocAnhFAQ_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.FAQControl.SettingKey.HanCheKichThuocAnhFAQ_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.FAQControl.SettingKey.TaoAnhNhoChoAnhFAQ;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.FAQControl.SettingKey.TaoAnhNhoChoAnhFAQ_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.FAQControl.SettingKey.TaoAnhNhoChoAnhFAQ_MaxHeight;
                break;

            case RevosJsc.ServiceControl.CodeApplications.Service:
                _pic = RevosJsc.ServiceControl.FolderPic.Service;

                _hanCheKichThuocAnh = RevosJsc.ServiceControl.SettingKey.HanCheKichThuocAnhService;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.ServiceControl.SettingKey.HanCheKichThuocAnhService_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.ServiceControl.SettingKey.HanCheKichThuocAnhService_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.ServiceControl.SettingKey.TaoAnhNhoChoAnhService;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.ServiceControl.SettingKey.TaoAnhNhoChoAnhService_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.ServiceControl.SettingKey.TaoAnhNhoChoAnhService_MaxHeight;
                break;
            case RevosJsc.OurTeamControl.CodeApplications.OurTeam:
                _pic = RevosJsc.OurTeamControl.FolderPic.OurTeam;

                _hanCheKichThuocAnh = RevosJsc.OurTeamControl.SettingKey.HanCheKichThuocAnhOurTeam;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.OurTeamControl.SettingKey.HanCheKichThuocAnhOurTeam_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.OurTeamControl.SettingKey.HanCheKichThuocAnhOurTeam_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.OurTeamControl.SettingKey.TaoAnhNhoChoAnhOurTeam;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.OurTeamControl.SettingKey.TaoAnhNhoChoAnhOurTeam_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.OurTeamControl.SettingKey.TaoAnhNhoChoAnhOurTeam_MaxHeight;
                break;

            case RevosJsc.TourControl.CodeApplications.Tour:
                _pic = RevosJsc.TourControl.FolderPic.Tour;

                _hanCheKichThuocAnh = RevosJsc.TourControl.SettingKey.HanCheKichThuocAnhTour;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.TourControl.SettingKey.HanCheKichThuocAnhTour_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.TourControl.SettingKey.HanCheKichThuocAnhTour_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.TourControl.SettingKey.TaoAnhNhoChoAnhTour;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.TourControl.SettingKey.TaoAnhNhoChoAnhTour_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.TourControl.SettingKey.TaoAnhNhoChoAnhTour_MaxHeight;
                break;

            case RevosJsc.ProjectControl.CodeApplications.Project:
                _pic = RevosJsc.ProjectControl.FolderPic.Project;

                _hanCheKichThuocAnh = RevosJsc.ProjectControl.SettingKey.HanCheKichThuocAnhProject;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.ProjectControl.SettingKey.HanCheKichThuocAnhProject_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.ProjectControl.SettingKey.HanCheKichThuocAnhProject_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.ProjectControl.SettingKey.TaoAnhNhoChoAnhProject;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.ProjectControl.SettingKey.TaoAnhNhoChoAnhProject_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.ProjectControl.SettingKey.TaoAnhNhoChoAnhProject_MaxHeight;
                break;

            case RevosJsc.VideoControl.CodeApplications.Video:
                _pic = RevosJsc.VideoControl.FolderPic.Video;

                _hanCheKichThuocAnh = RevosJsc.VideoControl.SettingKey.HanCheKichThuocAnhVideo;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.VideoControl.SettingKey.HanCheKichThuocAnhVideo_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.VideoControl.SettingKey.HanCheKichThuocAnhVideo_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.VideoControl.SettingKey.TaoAnhNhoChoAnhVideo;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.VideoControl.SettingKey.TaoAnhNhoChoAnhVideo_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.VideoControl.SettingKey.TaoAnhNhoChoAnhVideo_MaxHeight;
                break;

            case RevosJsc.WebsiteControl.CodeApplications.Website:
                _pic = RevosJsc.WebsiteControl.FolderPic.Website;

                _hanCheKichThuocAnh = RevosJsc.WebsiteControl.SettingKey.HanCheKichThuocAnhWebsite;
                _hanCheKichThuocAnhMaxHeight = RevosJsc.WebsiteControl.SettingKey.HanCheKichThuocAnhWebsite_MaxWidth;
                _hanCheKichThuocAnhMaxWidth = RevosJsc.WebsiteControl.SettingKey.HanCheKichThuocAnhWebsite_MaxHeight;
                _taoAnhNhoChoAnh = RevosJsc.WebsiteControl.SettingKey.TaoAnhNhoChoAnhWebsite;
                _taoAnhNhoChoAnhMaxWidth = RevosJsc.WebsiteControl.SettingKey.TaoAnhNhoChoAnhWebsite_MaxWidth;
                _taoAnhNhoChoAnhMaxHeight = RevosJsc.WebsiteControl.SettingKey.TaoAnhNhoChoAnhWebsite_MaxHeight;
                break;
        }
    }
    #endregion

    #region Khởi tạo xử lý ảnh

    private void KhoiTaoXuLyAnh()
    {
        #region Hạn chế kích thước ảnh đại diện
        cbHanCheKichThuoc.Checked = SettingsExtension.GetSettingKey(_hanCheKichThuocAnh, _language) == "1";
        tbHanCheW.Text = SettingsExtension.GetSettingKey(_hanCheKichThuocAnhMaxWidth, _language);
        tbHanCheH.Text = SettingsExtension.GetSettingKey(_hanCheKichThuocAnhMaxHeight, _language);
        #endregion

        #region Tạo ảnh nhỏ cho ảnh đại diện
        cbTaoAnhNho.Checked = SettingsExtension.GetSettingKey(_taoAnhNhoChoAnh, _language) == "1";
        tbAnhNhoW.Text = SettingsExtension.GetSettingKey(_taoAnhNhoChoAnhMaxWidth, _language);
        tbAnhNhoH.Text = SettingsExtension.GetSettingKey(_taoAnhNhoChoAnhMaxHeight, _language);
        #endregion
    }
    #endregion    

    #region Phương thức lưu ảnh, cần gọi khi insert, update
    /// <summary>
    /// Phương thức lưu ảnh khi insert, update
    /// </summary>
    /// <param name="update">True: đang ở hành động update</param>
    /// <param name="contentDetail">Nội dung để tìm ảnh đầu làm ảnh đại diện</param>
    /// <returns></returns>
    public string Save(bool update, string contentDetail)
    {
        #region Image
        var vImg = "";

        var path = Request.PhysicalApplicationPath + "/" + _pic + "/";

        #region Kiểm tra xem thư mục đã tồn tại chưa, nếu chưa -> tạo mới thư mục
        var dri = new DirectoryInfo(path);
        if (!dri.Exists) dri.Create();
        #endregion

        if (flimg.Visible)
        {
            var vImgThumb = "";
            if (flimg.PostedFile.ContentLength > 0)
            {
                var filename = flimg.FileName;
                var fileEx = filename.Substring(filename.LastIndexOf(".", StringComparison.Ordinal));

                if (ImagesExtension.ValidType(fileEx))
                {
                    var fileNotEx = StringExtension.ReplateTitle(filename.Remove(filename.LastIndexOf(".", StringComparison.Ordinal)));
                    if (fileNotEx.Length > 55) fileNotEx = fileNotEx.Remove(55);
                    var ticks = Guid.NewGuid();

                    #region Lưu ảnh đại diện theo 2 trường hợp: tạo ảnh nhỏ hoặc không.
                    //Kiểm tra xem có tạo ảnh nhỏ hay ko
                    //Nếu không tạo ảnh nhỏ, tên tệp lưu bình thường theo kiểu: tên_tệp.phần_mở_rộng
                    //Nếu tạo ảnh nhỏ, tên tệp sẽ theo kiểu: tên_tệp_HasThumb.phần_mở_rộng
                    //Khi đó tên tệp ảnh nhỏ sẽ theo kiểu:   tên_tệp_HasThumb_Thumb.phần_mở_rộng
                    //Với cách lưu tên ảnh này, khi thực hiện lưu vào csdl chỉ cần lưu tên ảnh gốc
                    //khi hiển thị chỉ cần dựa vào tên ảnh gốc để biết ảnh đó có ảnh nhỏ hay không, việc này được thực hiện bởi ImagesExtension.GetImage, lập trình không cần làm gì thêm.
                    if (cbTaoAnhNho.Checked) vImg = fileNotEx + "_" + ticks + "_HasThumb" + fileEx;
                    else vImg = fileNotEx + "_" + ticks + fileEx;
                    flimg.SaveAs(path + vImg);
                    #endregion

                    #region Hạn chế kích thước

                    if (cbHanCheKichThuoc.Checked)
                    {
                        //ImagesExtension.ResizeImage(path + vimg, "", tbHanCheW.Text, tbHanCheH.Text);
                        var image = new KalikoImage(path + vImg);
                        //// Create thumbnail by fitting
                        var thumb = image.Scale(new FitScaling(Convert.ToInt32(tbHanCheW.Text), Convert.ToInt32(tbHanCheH.Text)));
                        switch (fileEx.ToLower())
                        {
                            case ".png":
                                thumb.SavePng(path + vImg);
                                break;
                            case ".bmp":
                                thumb.SaveBmp(path + vImg);
                                break;
                            case ".gif":
                                thumb.SaveGif(path + vImg);
                                break;
                            default:
                                thumb.SaveJpg(path + vImg, 100);
                                break;
                        }
                    }

                    #endregion
                    
                    #region Tạo ảnh nhỏ
                    if (cbTaoAnhNho.Checked)
                    {
                        vImgThumb = fileNotEx + "_" + ticks + "_HasThumb_Thumb" + fileEx;
                        //ImagesExtension.ResizeImage(path + vimg, path + vimgThumb, tbAnhNhoW.Text, tbAnhNhoH.Text);

                        var image = new KalikoImage(path + vImg);
                        //// Create thumbnail by cropping
                        var thumb = image.Scale(new CropScaling(Convert.ToInt32(tbAnhNhoW.Text), Convert.ToInt32(tbAnhNhoH.Text)));
                        switch (fileEx.ToLower())
                        {
                            case ".png":
                                thumb.SavePng(path + vImgThumb);
                                break;
                            case ".bmp":
                                thumb.SaveBmp(path + vImgThumb);
                                break;
                            case ".gif":
                                thumb.SaveGif(path + vImgThumb);
                                break;
                            default:
                                thumb.SaveJpg(path + vImgThumb, 100);
                                break;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                if (cbLayAnhTuNoiDung.Visible)
                {
                    if (hdImage.Value.Length < 1 || cbLayAnhTuNoiDung.Checked)
                    //nếu không upload ảnh và cũng không có ảnh cũ -> tìm ảnh đầu tiên trong nội dung làm ảnh đại diện
                    {
                        if (hdImage.Value.Length > 0) ImagesExtension.DeleteImageWhenDeleteItem(_pic, hdImage.Value);

                        var urlImg = ImagesExtension.GetFirstImageInContent(contentDetail);
                        if (urlImg.Length > 0 && !urlImg.ToLower().StartsWith("http")) //Nếu url không phải link đầy đủ
                        {
                            var url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                            var right = HttpContext.Current.Request.Url.AbsolutePath.Remove(HttpContext.Current.Request.Url.AbsolutePath.IndexOf("/", 1, StringComparison.Ordinal) + 1);
                            if (right.StartsWith("/Areas"))
                            {
                                right = right.Substring("/Areas".Length);
                                if (right.Trim('/').Length > 0) right = "/" + right;
                            }
                            url += right;
                            var dotIndex = url.IndexOf(".", StringComparison.Ordinal);
                            if (dotIndex > -1)
                            {
                                var splitIndex = url.IndexOf("/", dotIndex, StringComparison.Ordinal);
                                if (splitIndex > -1) url = url.Remove(splitIndex);
                            }
                            if (!url.EndsWith("/")) url = url + "/";
                            urlImg = url + urlImg.Substring(1);
                        }
                        if (urlImg.Length > 0)
                        {
                            var filename = urlImg;
                            var fileEx = filename.Substring(filename.LastIndexOf(".", StringComparison.Ordinal));
                            if (ImagesExtension.ValidType(fileEx))
                            {
                                var fileNotEx = StringExtension.ReplateTitle(filename.Remove(filename.LastIndexOf(".", StringComparison.Ordinal)));
                                if (fileNotEx.Length > 55) fileNotEx = fileNotEx.Remove(55);
                                var ticks = DateTime.Now.Ticks.ToString();

                                #region Lưu ảnh đại diện theo 2 trường hợp: tạo ảnh nhỏ hoặc không.
                                //Kiểm tra xem có tạo ảnh nhỏ hay ko
                                //Nếu không tạo ảnh nhỏ, tên tệp lưu bình thường theo kiểu: tên_tệp.phần_mở_rộng
                                //Nếu tạo ảnh nhỏ, tên tệp sẽ theo kiểu: tên_tệp_HasThumb.phần_mở_rộng
                                //Khi đó tên tệp ảnh nhỏ sẽ theo kiểu:   tên_tệp_HasThumb_Thumb.phần_mở_rộng
                                //Với cách lưu tên ảnh này, khi thực hiện lưu vào csdl chỉ cần lưu tên ảnh gốc
                                //khi hiển thị chỉ cần dựa vào tên ảnh gốc để biết ảnh đó có ảnh nhỏ hay không, việc này được thực hiện bởi ImagesExtension.GetImage, lập trình không cần làm gì thêm.
                                if (cbTaoAnhNho.Checked) vImg = fileNotEx + "_" + ticks + "_HasThumb";
                                else vImg = fileNotEx + "_" + ticks;

                                if (ImagesExtension.SaveImageFromUrl(path + vImg, urlImg).Length > 0)
                                {
                                    vImg += fileEx;

                                    #region Hạn chế kích thước
                                    if (cbHanCheKichThuoc.Checked) ImagesExtension.ResizeImage(path + vImg, "", tbHanCheW.Text, tbHanCheH.Text);
                                    #endregion

                                    #region Tạo ảnh nhỏ: Thực hiện cuối để đảm bảo ảnh nhỏ cũng có con dấu
                                    if (cbTaoAnhNho.Checked)
                                    {
                                        vImgThumb = fileNotEx + "_" + ticks + "_HasThumb_Thumb" + fileEx;
                                        //ImagesExtension.ResizeImage(path + vimg, path + vimgThumb, tbAnhNhoW.Text, tbAnhNhoH.Text);
                                        var image = new KalikoImage(path + vImg);
                                        //// Create thumbnail by fitting
                                        var thumb = image.Scale(new CropScaling(Convert.ToInt32(tbAnhNhoW.Text), Convert.ToInt32(tbAnhNhoH.Text)));
                                        switch (fileEx.ToLower())
                                        {
                                            case ".png":
                                                thumb.SavePng(path + vImgThumb);
                                                break;
                                            case ".bmp":
                                                thumb.SaveBmp(path + vImgThumb);
                                                break;
                                            case ".gif":
                                                thumb.SaveGif(path + vImgThumb);
                                                break;
                                            default:
                                                thumb.SaveJpg(path + vImgThumb, 100);
                                                break;
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    vImg = "";
                                }
                                #endregion
                            }
                        }
                    }
                }
            }
        }
        #endregion

        if (!update) return vImg;
        if (vImg == "") vImg = hdImage.Value;
        else ImagesExtension.DeleteImageWhenDeleteItem(_pic, hdImage.Value);

        return vImg;
    }
    #endregion

    #region Phương thức load ra ảnh khi hiển thị thông tin cập nhật
    /// <summary>
    /// Phương thức load ra thông tin ảnh khi cập nhật
    /// </summary>
    /// <param name="imageName">Tên ảnh, thường là giá trị trường vgImage hoặc viImage</param>
    public new void Load(string imageName)
    {
        if (imageName.Length > 0)
        {
            ltimg.Text = "<a href='" + UrlExtension.WebsiteUrl + _pic + "/" + imageName + "' data-toggle='lightbox-image'>" + ImagesExtension.GetImage(_pic, imageName, "", "mw130px", false, false, "") + "</a>";
            btnDeleteCurrentImage.Visible = true;
            hdImage.Value = imageName;
        }

        if (cbLayAnhTuNoiDung.Visible)
        {
            cbLayAnhTuNoiDung.Checked = imageName == "";
        }
    }
    #endregion

    #region Phương thức reset sau khi tạo xong
    /// <summary>
    /// Phương thức reset sau khi tạo xong
    /// </summary>
    public void Reset()
    {
        ltimg.Text = "";
        hdImage.Value = "";
    }
    #endregion

    #region Sự kiện chạy khi click nút xóa ảnh
    protected void btnDeleteCurrentImage_Click(object sender, EventArgs e)
    {
        ImagesExtension.DeleteImageWhenDeleteItem(_pic, hdImage.Value);
        hdImage.Value = "";
        btnDeleteCurrentImage.Visible = false;
        ltimg.Text = "";
    }
    #endregion
}
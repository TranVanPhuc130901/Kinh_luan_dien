using System;
using System.Text;
using Developer.Config;
using Developer.Keyword;
using RevosJsc.UsersControl;

public partial class Areas_Admin_Control_Component_SidebarNav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ltrList.Text = GetListMenu();
    }

    private string GetListMenu()
    {
        var s = new StringBuilder();
        s.Append("<ul id='cm_menu_main' class='sidebar-nav'>");
        //s.Append("<li><a href='/admin'><i class='gi gi-home sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>Home</span></a></li>");

        #region AboutUs

        if (ControlConfig.ShowAboutUs)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.AboutUsControl.Link.LnkMnAboutUs() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-circle_info sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>" + AboutUsKeyword.AboutUs + "</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.AboutUsControl.Link.LnkMnAboutUsCategory() + "'>" + AboutUsKeyword.DanhSachDanhMuc + "</a></li>");
            if (AboutUsConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.AboutUsControl.Link.LnkMnAboutUsItem() + "'>" + AboutUsKeyword.DanhSachBaiViet + "</a></li>");
            if (AboutUsConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.AboutUsControl.Link.LnkMnAboutUsGroupItem() + "'>" + AboutUsKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.AboutUsControl.Link.LnkMnAboutUsCategoryCreate() + "'>" + AboutUsKeyword.ThemMoiDanhMuc + "</a></li>");
            if (AboutUsConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.AboutUsControl.Link.LnkMnAboutUsItemCreate() + "'>" + AboutUsKeyword.ThemMoiBaiViet + "</a></li>");
            if (AboutUsConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.AboutUsControl.Link.LnkMnAboutUsGroupItemCreate() + "'>" + AboutUsKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.AboutUsControl.Link.LnkMnAboutUsCategoryRec() + "'>" + AboutUsKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (AboutUsConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.AboutUsControl.Link.LnkMnAboutUsItemRec() + "'>" + AboutUsKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (AboutUsConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.AboutUsControl.Link.LnkMnAboutUsGroupItemRec() + "'>" + AboutUsKeyword.DanhSachNhomDaXoa + "</a></li>");
            if (AboutUsConfig.ShowConfig) s.Append("<li class='divider'></li>");
            if (AboutUsConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.AboutUsControl.Link.LnkMnAboutUsConfig() + "'>" + AboutUsKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Blog

        if (ControlConfig.ShowBlog)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.BlogControl.Link.LnkMnBlog() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='si si-blogger sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>"+ BlogKeyword.Blog +"</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.BlogControl.Link.LnkMnBlogCategory() + "'>"+ BlogKeyword.DanhSachDanhMuc +"</a></li>");
            if (BlogConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.BlogControl.Link.LnkMnBlogItem() + "'>"+ BlogKeyword.DanhSachBaiViet +"</a></li>");
            if (BlogConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.BlogControl.Link.LnkMnBlogGroupItem() + "'>"+ BlogKeyword.DanhSachNhom +"</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.BlogControl.Link.LnkMnBlogCategoryCreate() + "'>" + BlogKeyword.ThemMoiDanhMuc + "</a></li>");
            if (BlogConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.BlogControl.Link.LnkMnBlogItemCreate() + "'>" + BlogKeyword.ThemMoiBaiViet + "</a></li>");
            if (BlogConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.BlogControl.Link.LnkMnBlogGroupItemCreate() + "'>" + BlogKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.BlogControl.Link.LnkMnBlogCategoryRec() + "'>" + BlogKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (BlogConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.BlogControl.Link.LnkMnBlogItemRec() + "'>" + BlogKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (BlogConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.BlogControl.Link.LnkMnBlogGroupItemRec() + "'>" + BlogKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (BlogConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.BlogControl.Link.LnkMnBlogConfig() + "'>" + BlogKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Destination

        if (ControlConfig.ShowDestination)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.DestinationControl.Link.LnkMnDestination() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='fa fa-map sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>"+ DestinationKeyword.Destination +"</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.DestinationControl.Link.LnkMnDestinationCategory() + "'>" + DestinationKeyword.DanhSachDanhMuc + "</a></li>");
            if (DestinationConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.DestinationControl.Link.LnkMnDestinationItem() + "'>" + DestinationKeyword.DanhSachBaiViet + "</a></li>");
            if (DestinationConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.DestinationControl.Link.LnkMnDestinationGroupItem() + "'>" + DestinationKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.DestinationControl.Link.LnkMnDestinationCategoryCreate() + "'>" + DestinationKeyword.ThemMoiDanhMuc + "</a></li>");
            if (DestinationConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.DestinationControl.Link.LnkMnDestinationItemCreate() + "'>" + DestinationKeyword.ThemMoiBaiViet + "</a></li>");
            if (DestinationConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.DestinationControl.Link.LnkMnDestinationGroupItemCreate() + "'>" + DestinationKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.DestinationControl.Link.LnkMnDestinationCategoryRec() + "'>" + DestinationKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (DestinationConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.DestinationControl.Link.LnkMnDestinationItemRec() + "'>" + DestinationKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (DestinationConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.DestinationControl.Link.LnkMnDestinationGroupItemRec() + "'>" + DestinationKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (DestinationConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.DestinationControl.Link.LnkMnDestinationConfig() + "'>" + DestinationKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region FAQ

        if (ControlConfig.ShowFAQ)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.FAQControl.Link.LnkMnFAQCategory() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-conversation sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>"+ FAQKeyword.FAQ +"</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.FAQControl.Link.LnkMnFAQCategory() + "'>" + FAQKeyword.DanhSachDanhMuc + "</a></li>");
            if (FAQConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.FAQControl.Link.LnkMnFAQItem() + "'>" + FAQKeyword.DanhSachBaiViet + "</a></li>");
            if (FAQConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.FAQControl.Link.LnkMnFAQGroupItem() + "'>" + FAQKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.FAQControl.Link.LnkMnFAQCategoryCreate() + "'>" + FAQKeyword.ThemMoiDanhMuc + "</a></li>");
            if (FAQConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.FAQControl.Link.LnkMnFAQItemCreate() + "'>" + FAQKeyword.ThemMoiBaiViet + "</a></li>");
            if (FAQConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.FAQControl.Link.LnkMnFAQGroupItemCreate() + "'>" + FAQKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.FAQControl.Link.LnkMnFAQCategoryRec() + "'>" + FAQKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (FAQConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.FAQControl.Link.LnkMnFAQItemRec() + "'>" + FAQKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (FAQConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.FAQControl.Link.LnkMnFAQGroupItemRec() + "'>" + FAQKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (FAQConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.FAQControl.Link.LnkMnFAQConfig() + "'>" + FAQKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Filelibrary

        if (ControlConfig.ShowFilelibrary)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.FileLibraryControl.Link.LnkMnFileLibrary() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='fa fa-file-archive-o sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>" + FileLibraryKeyword.FileLibrary +"</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.FileLibraryControl.Link.LnkMnFileLibraryCategory() + "'>" + FileLibraryKeyword.DanhSachDanhMuc + "</a></li>");
            if (FileLibraryConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.FileLibraryControl.Link.LnkMnFileLibraryItem() + "'>" + FileLibraryKeyword.DanhSachBaiViet + "</a></li>");
            if (FileLibraryConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.FileLibraryControl.Link.LnkMnFileLibraryGroupItem() + "'>" + FileLibraryKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.FileLibraryControl.Link.LnkMnFileLibraryCategoryCreate() + "'>" + FileLibraryKeyword.ThemMoiDanhMuc + "</a></li>");
            if (FileLibraryConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.FileLibraryControl.Link.LnkMnFileLibraryItemCreate() + "'>" + FileLibraryKeyword.ThemMoiBaiViet + "</a></li>");
            if (FileLibraryConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.FileLibraryControl.Link.LnkMnFileLibraryGroupItemCreate() + "'>" + FileLibraryKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.FileLibraryControl.Link.LnkMnFileLibraryCategoryRec() + "'>" + FileLibraryKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (FileLibraryConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.FileLibraryControl.Link.LnkMnFileLibraryItemRec() + "'>" + FileLibraryKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (FileLibraryConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.FileLibraryControl.Link.LnkMnFileLibraryGroupItemRec() + "'>" + FileLibraryKeyword.DanhSachNhomDaXoa + "</a></li>");
            if (FileLibraryConfig.ShowConfig) s.Append("<li class='divider'></li>");
            if (FileLibraryConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.FileLibraryControl.Link.LnkMnFileLibraryConfig() + "'>" + FileLibraryKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region News

        if (ControlConfig.ShowNews)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.NewsControl.Link.LnkMnNews() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='fa fa-newspaper-o sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>"+ NewsKeyword.News +"</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.NewsControl.Link.LnkMnNewsCategory() + "'>" + NewsKeyword.DanhSachDanhMuc + "</a></li>");
            if (NewsConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.NewsControl.Link.LnkMnNewsItem() + "'>" + NewsKeyword.DanhSachBaiViet + "</a></li>");
            if (NewsConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.NewsControl.Link.LnkMnNewsGroupItem() + "'>" + NewsKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.NewsControl.Link.LnkMnNewsCategoryCreate() + "'>" + NewsKeyword.ThemMoiDanhMuc + "</a></li>");
            if (NewsConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.NewsControl.Link.LnkMnNewsItemCreate() + "'>" + NewsKeyword.ThemMoiBaiViet + "</a></li>");
            if (NewsConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.NewsControl.Link.LnkMnNewsGroupItemCreate() + "'>" + NewsKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.NewsControl.Link.LnkMnNewsCategoryRec() + "'>" + NewsKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (NewsConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.NewsControl.Link.LnkMnNewsItemRec() + "'>" + NewsKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (NewsConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.NewsControl.Link.LnkMnNewsGroupItemRec() + "'>" + NewsKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (NewsConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.NewsControl.Link.LnkMnNewsConfig() + "'>" + NewsKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region PhotoAlbum

        if (ControlConfig.ShowPhotoAlbum)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.PhotoAlbumControl.Link.LnkMnPhotoAlbum() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='fa fa-file-picture-o sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>"+ PhotoAlbumKeyword.PhotoAlbum +"</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.PhotoAlbumControl.Link.LnkMnPhotoAlbumCategory() + "'>" + PhotoAlbumKeyword.DanhSachDanhMuc + "</a></li>");
            if (PhotoAlbumConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.PhotoAlbumControl.Link.LnkMnPhotoAlbumItem() + "'>" + PhotoAlbumKeyword.DanhSachBaiViet + "</a></li>");
            if (PhotoAlbumConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.PhotoAlbumControl.Link.LnkMnPhotoAlbumGroupItem() + "'>" + PhotoAlbumKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.PhotoAlbumControl.Link.LnkMnPhotoAlbumCategoryCreate() + "'>" + PhotoAlbumKeyword.ThemMoiDanhMuc + "</a></li>");
            if (PhotoAlbumConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.PhotoAlbumControl.Link.LnkMnPhotoAlbumItemCreate() + "'>" + PhotoAlbumKeyword.ThemMoiBaiViet + "</a></li>");
            if (PhotoAlbumConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.PhotoAlbumControl.Link.LnkMnPhotoAlbumGroupItemCreate() + "'>" + PhotoAlbumKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.PhotoAlbumControl.Link.LnkMnPhotoAlbumCategoryRec() + "'>" + PhotoAlbumKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (PhotoAlbumConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.PhotoAlbumControl.Link.LnkMnPhotoAlbumItemRec() + "'>" + PhotoAlbumKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (PhotoAlbumConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.PhotoAlbumControl.Link.LnkMnPhotoAlbumGroupItemRec() + "'>" + PhotoAlbumKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (PhotoAlbumConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.PhotoAlbumControl.Link.LnkMnPhotoAlbumConfig() + "'>" + PhotoAlbumKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Project

        if (ControlConfig.ShowProject)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.ProjectControl.Link.LnkMnProject() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-projector sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>"+ ProjectKeyword.Project +"</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.ProjectControl.Link.LnkMnProjectCategory() + "'>" + ProjectKeyword.DanhSachDanhMuc + "</a></li>");
            if (ProjectConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.ProjectControl.Link.LnkMnProjectItem() + "'>" + ProjectKeyword.DanhSachBaiViet + "</a></li>");
            if (ProjectConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.ProjectControl.Link.LnkMnProjectGroupItem() + "'>" + ProjectKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.ProjectControl.Link.LnkMnProjectCategoryCreate() + "'>" + ProjectKeyword.ThemMoiDanhMuc + "</a></li>");
            if (ProjectConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.ProjectControl.Link.LnkMnProjectItemCreate() + "'>" + ProjectKeyword.ThemMoiBaiViet + "</a></li>");
            if (ProjectConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.ProjectControl.Link.LnkMnProjectGroupItemCreate() + "'>" + ProjectKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.ProjectControl.Link.LnkMnProjectCategoryRec() + "'>" + ProjectKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (ProjectConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.ProjectControl.Link.LnkMnProjectItemRec() + "'>" + ProjectKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (ProjectConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.ProjectControl.Link.LnkMnProjectGroupItemRec() + "'>" + ProjectKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (ProjectConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.ProjectControl.Link.LnkMnProjectConfig() + "'>" + ProjectKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Service

        if (ControlConfig.ShowService)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.ServiceControl.Link.LnkMnService() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='fa fa-puzzle-piece sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>" + ServiceKeyword.Service + "</span></a>");
            s.Append("<ul>");
            if (ServiceConfig.ShowCategory) s.Append("<li><a href='" + RevosJsc.ServiceControl.Link.LnkMnServiceCategory() + "'>" + ServiceKeyword.DanhSachDanhMuc + "</a></li>");
            if (ServiceConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.ServiceControl.Link.LnkMnServiceItem() + "'>" + ServiceKeyword.DanhSachBaiViet + "</a></li>");
            if (ServiceConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.ServiceControl.Link.LnkMnServiceGroupItem() + "'>" + ServiceKeyword.DanhSachNhom + "</a></li>");
            //s.Append("<li class='divider'></li>");
            if (ServiceConfig.ShowCategory) s.Append("<li><a href='" + RevosJsc.ServiceControl.Link.LnkMnServiceCategoryCreate() + "'>" + ServiceKeyword.ThemMoiDanhMuc + "</a></li>");
            if (ServiceConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.ServiceControl.Link.LnkMnServiceItemCreate() + "'>" + ServiceKeyword.ThemMoiBaiViet + "</a></li>");
            if (ServiceConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.ServiceControl.Link.LnkMnServiceGroupItemCreate() + "'>" + ServiceKeyword.ThemMoiNhom + "</a></li>");
            //s.Append("<li class='divider'></li>");
            if (ServiceConfig.ShowCategory) s.Append("<li><a href='" + RevosJsc.ServiceControl.Link.LnkMnServiceCategoryRec() + "'>" + ServiceKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (ServiceConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.ServiceControl.Link.LnkMnServiceItemRec() + "'>" + ServiceKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (ServiceConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.ServiceControl.Link.LnkMnServiceGroupItemRec() + "'>" + ServiceKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (ServiceConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.ServiceControl.Link.LnkMnServiceConfig() + "'>" + ServiceKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Reviews

        if (ControlConfig.ShowReviews)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.ReviewsControl.Link.LnkMnReviews() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='fa fa-smile-o sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>"+ ReviewsKeyword.Reviews +"</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.ReviewsControl.Link.LnkMnReviewsCategory() + "'>" + ReviewsKeyword.DanhSachDanhMuc + "</a></li>");
            if (ReviewsConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.ReviewsControl.Link.LnkMnReviewsItem() + "'>" + ReviewsKeyword.DanhSachBaiViet + "</a></li>");
            if (ReviewsConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.ReviewsControl.Link.LnkMnReviewsGroupItem() + "'>" + ReviewsKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.ReviewsControl.Link.LnkMnReviewsCategoryCreate() + "'>" + ReviewsKeyword.ThemMoiDanhMuc + "</a></li>");
            if (ReviewsConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.ReviewsControl.Link.LnkMnReviewsItemCreate() + "'>" + ReviewsKeyword.ThemMoiBaiViet + "</a></li>");
            if (ReviewsConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.ReviewsControl.Link.LnkMnReviewsGroupItemCreate() + "'>" + ReviewsKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.ReviewsControl.Link.LnkMnReviewsCategoryRec() + "'>" + ReviewsKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (ReviewsConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.ReviewsControl.Link.LnkMnReviewsItemRec() + "'>" + ReviewsKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (ReviewsConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.ReviewsControl.Link.LnkMnReviewsGroupItemRec() + "'>" + ReviewsKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (ReviewsConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.ReviewsControl.Link.LnkMnReviewsConfig() + "'>" + ReviewsKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Tour

        if (ControlConfig.ShowTour)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.TourControl.Link.LnkMnTour() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-luggage sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>"+ TourKeyword.Tour +"</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourCategory() + "'>" + TourKeyword.DanhSachDanhMuc + "</a></li>");
            if (TourConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourItem() + "'>" + TourKeyword.DanhSachBaiViet + "</a></li>");
            if (TourConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourGroupItem() + "'>" + TourKeyword.DanhSachNhom + "</a></li>");
            if (TourConfig.ShowFillterProperties) s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourFilter() + "'>" + TourKeyword.DanhSachThuocTinhLoc + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourCategoryCreate() + "'>" + TourKeyword.ThemMoiDanhMuc + "</a></li>");
            if (TourConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourItemCreate() + "'>" + TourKeyword.ThemMoiBaiViet + "</a></li>");
            if (TourConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourGroupItemCreate() + "'>" + TourKeyword.ThemMoiNhom + "</a></li>");
            if (TourConfig.ShowFillterProperties) s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourFilterCreate() + "'>" + TourKeyword.ThemMoiThuocTinhLoc + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourCategoryRec() + "'>" + TourKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (TourConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourItemRec() + "'>" + TourKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (TourConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourGroupItemRec() + "'>" + TourKeyword.DanhSachNhomDaXoa + "</a></li>");
            if (TourConfig.ShowFillterProperties) s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourFilterRec() + "'>" + TourKeyword.ThuocTinhLocDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (TourConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.TourControl.Link.LnkMnTourConfig() + "'>" + TourKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Hotel

        if (ControlConfig.ShowHotel)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.HotelControl.Link.LnkMnHotel() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-building sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>" + HotelKeyword.Hotel + "</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.HotelControl.Link.LnkMnHotelCategory() + "'>" + HotelKeyword.DanhSachDanhMuc + "</a></li>");
            if (HotelConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.HotelControl.Link.LnkMnHotelItem() + "'>" + HotelKeyword.DanhSachBaiViet + "</a></li>");
            if (HotelConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.HotelControl.Link.LnkMnHotelGroupItem() + "'>" + HotelKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.HotelControl.Link.LnkMnHotelCategoryCreate() + "'>" + HotelKeyword.ThemMoiDanhMuc + "</a></li>");
            if (HotelConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.HotelControl.Link.LnkMnHotelItemCreate() + "'>" + HotelKeyword.ThemMoiBaiViet + "</a></li>");
            if (HotelConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.HotelControl.Link.LnkMnHotelGroupItemCreate() + "'>" + HotelKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.HotelControl.Link.LnkMnHotelCategoryRec() + "'>" + HotelKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (HotelConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.HotelControl.Link.LnkMnHotelItemRec() + "'>" + HotelKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (HotelConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.HotelControl.Link.LnkMnHotelGroupItemRec() + "'>" + HotelKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (HotelConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.HotelControl.Link.LnkMnHotelConfig() + "'>" + HotelKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Cruises

        if (ControlConfig.ShowCruises)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.CruisesControl.Link.LnkMnCruises() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='fa fa-ship sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>" + CruisesKeyword.Cruises + "</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.CruisesControl.Link.LnkMnCruisesCategory() + "'>" + CruisesKeyword.DanhSachDanhMuc + "</a></li>");
            if (CruisesConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.CruisesControl.Link.LnkMnCruisesItem() + "'>" + CruisesKeyword.DanhSachBaiViet + "</a></li>");
            if (CruisesConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.CruisesControl.Link.LnkMnCruisesGroupItem() + "'>" + CruisesKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.CruisesControl.Link.LnkMnCruisesCategoryCreate() + "'>" + CruisesKeyword.ThemMoiDanhMuc + "</a></li>");
            if (CruisesConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.CruisesControl.Link.LnkMnCruisesItemCreate() + "'>" + CruisesKeyword.ThemMoiBaiViet + "</a></li>");
            if (CruisesConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.CruisesControl.Link.LnkMnCruisesGroupItemCreate() + "'>" + CruisesKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.CruisesControl.Link.LnkMnCruisesCategoryRec() + "'>" + CruisesKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (CruisesConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.CruisesControl.Link.LnkMnCruisesItemRec() + "'>" + CruisesKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (CruisesConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.CruisesControl.Link.LnkMnCruisesGroupItemRec() + "'>" + CruisesKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (CruisesConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.CruisesControl.Link.LnkMnCruisesConfig() + "'>" + CruisesKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Product

        if (ControlConfig.ShowProduct)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.ProductControl.Link.LnkMnProduct() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-shop sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>" + ProductKeyword.Product + "</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductCategory() + "'>" + ProductKeyword.DanhSachDanhMuc + "</a></li>");
            if (ProductConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductItem() + "'>" + ProductKeyword.DanhSachBaiViet + "</a></li>");
            if (ProductConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductGroupItem() + "'>" + ProductKeyword.DanhSachNhom + "</a></li>");
            if (ProductConfig.ShowFilterProperties) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductFilter() + "'>" + ProductKeyword.DanhSachThuocTinhLoc + "</a></li>");
            if (ProductConfig.ShowColor) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductColor() + "'>" + ProductKeyword.DanhSachMau + "</a></li>");
            if (ProductConfig.ShowPaymentMethod) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductPayment() + "'>" + ProductKeyword.DanhSachPhuongThucThanhToan + "</a></li>");
            if (ProductConfig.ShowBill) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductBill() + "'>" + ProductKeyword.DanhSachDonHang + "</a></li>");
            if (ProductConfig.ShowComment) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductComment() + "'>" + ProductKeyword.DanhSachBinhLuan + "</a></li>");
            //if (ProductConfig.ShowOptionUpgrade) s.Append("<li><a href='/admin?control=Product&action=OptionUpgrade'>Tùy chọn nâng cấp</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductCategoryCreate() + "'>" + ProductKeyword.ThemMoiDanhMuc + "</a></li>");
            if (ProductConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductItemCreate() + "'>" + ProductKeyword.ThemMoiBaiViet + "</a></li>");
            if (ProductConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductGroupItemCreate() + "'>" + ProductKeyword.ThemMoiNhom + "</a></li>");
            if (ProductConfig.ShowFilterProperties) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductFilterCreate() + "'>" + ProductKeyword.ThemMoiThuocTinhLoc + "</a></li>");
            if (ProductConfig.ShowColor) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductColorCreate() + "'>" + ProductKeyword.ThemMoiMau + "</a></li>");
            if (ProductConfig.ShowPaymentMethod) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductPaymentCreate() + "'>" + ProductKeyword.ThemMoiPhuongThucThanhToan + "</a></li>");
            //if (ProductConfig.ShowOptionUpgrade) s.Append("<li><a href='/admin?control=Product&action=OptionUpgradeAdd'>Thêm tùy chọn nân cấp</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductCategoryRec() + "'>" + ProductKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (ProductConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductItemRec() + "'>" + ProductKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (ProductConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductGroupItemRec() + "'>" + ProductKeyword.DanhSachNhomDaXoa + "</a></li>");
            if (ProductConfig.ShowFilterProperties) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductFilterRec() + "'>" + ProductKeyword.ThuocTinhLocDaXoa + "</a></li>");
            if (ProductConfig.ShowColor) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductColorRec() + "'>" + ProductKeyword.MauDaXoa + "</a></li>");
            if (ProductConfig.ShowPaymentMethod) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductPaymentRec() + "'>" + ProductKeyword.DanhSachPhuongThucThanhToanDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (ProductConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.ProductControl.Link.LnkMnProductConfig() + "'>" + ProductKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Video

        if (ControlConfig.ShowVideo)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.VideoControl.Link.LnkMnVideo() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-play_button sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>"+ VideoKeyword.Video +"</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.VideoControl.Link.LnkMnVideoCategory() + "'>" + VideoKeyword.DanhSachDanhMuc + "</a></li>");
            if (VideoConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.VideoControl.Link.LnkMnVideoItem() + "'>" + VideoKeyword.DanhSachBaiViet + "</a></li>");
            if (VideoConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.VideoControl.Link.LnkMnVideoGroupItem() + "'>" + VideoKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.VideoControl.Link.LnkMnVideoCategoryCreate() + "'>" + VideoKeyword.ThemMoiDanhMuc + "</a></li>");
            if (VideoConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.VideoControl.Link.LnkMnVideoItemCreate() + "'>" + VideoKeyword.ThemMoiBaiViet + "</a></li>");
            if (VideoConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.VideoControl.Link.LnkMnVideoGroupItemCreate() + "'>" + VideoKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.VideoControl.Link.LnkMnVideoCategoryRec() + "'>" + VideoKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (VideoConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.VideoControl.Link.LnkMnVideoItemRec() + "'>" + VideoKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (VideoConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.VideoControl.Link.LnkMnVideoGroupItemRec() + "'>" + VideoKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (VideoConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.VideoControl.Link.LnkMnVideoConfig() + "'>" + VideoKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Our team

        if (ControlConfig.ShowOurTeam)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.OurTeamControl.Link.LnkMnOurTeam() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-parents sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>" + OurTeamKeyword.OurTeam + "</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.OurTeamControl.Link.LnkMnOurTeamCategory() + "'>" + OurTeamKeyword.DanhSachDanhMuc + "</a></li>");
            if (OurTeamConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.OurTeamControl.Link.LnkMnOurTeamItem() + "'>" + OurTeamKeyword.DanhSachBaiViet + "</a></li>");
            if (OurTeamConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.OurTeamControl.Link.LnkMnOurTeamGroupItem() + "'>" + OurTeamKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.OurTeamControl.Link.LnkMnOurTeamCategoryCreate() + "'>" + OurTeamKeyword.ThemMoiDanhMuc + "</a></li>");
            if (OurTeamConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.OurTeamControl.Link.LnkMnOurTeamItemCreate() + "'>" + OurTeamKeyword.ThemMoiBaiViet + "</a></li>");
            if (OurTeamConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.OurTeamControl.Link.LnkMnOurTeamGroupItemCreate() + "'>" + OurTeamKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.OurTeamControl.Link.LnkMnOurTeamCategoryRec() + "'>" + OurTeamKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (OurTeamConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.OurTeamControl.Link.LnkMnOurTeamItemRec() + "'>" + OurTeamKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (OurTeamConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.OurTeamControl.Link.LnkMnOurTeamGroupItemRec() + "'>" + OurTeamKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (OurTeamConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.OurTeamControl.Link.LnkMnOurTeamConfig() + "'>" + OurTeamKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Member

        if (ControlConfig.ShowMember)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.MemberControl.Link.LnkMnMember() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='fa fa-users sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>" + MemberKeyword.Member + "</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.MemberControl.Link.LnkMnMemberItem() + "'>" + MemberKeyword.DanhSachMember + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.MemberControl.Link.LnkMnMemberItemCreate() + "'>" + MemberKeyword.ThemMoiMember + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.MemberControl.Link.LnkMnMemberItemRec() + "'>" + MemberKeyword.MemberDaXoa + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Customer

        if (ControlConfig.ShowCustomer)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.CustomerControl.Link.LnkMnCustomer() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-group sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>" + CustomerKeyword.Customer + "</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.CustomerControl.Link.LnkMnCustomerCategory() + "'>" + CustomerKeyword.DanhSachDanhMuc + "</a></li>");
            if (CustomerConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.CustomerControl.Link.LnkMnCustomerItem() + "'>" + CustomerKeyword.DanhSachBaiViet + "</a></li>");
            if (CustomerConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.CustomerControl.Link.LnkMnCustomerGroupItem() + "'>" + CustomerKeyword.DanhSachNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.CustomerControl.Link.LnkMnCustomerCategoryCreate() + "'>" + CustomerKeyword.ThemMoiDanhMuc + "</a></li>");
            if (CustomerConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.CustomerControl.Link.LnkMnCustomerItemCreate() + "'>" + CustomerKeyword.ThemMoiBaiViet + "</a></li>");
            if (CustomerConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.CustomerControl.Link.LnkMnCustomerGroupItemCreate() + "'>" + CustomerKeyword.ThemMoiNhom + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.CustomerControl.Link.LnkMnCustomerCategoryRec() + "'>" + CustomerKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            if (CustomerConfig.ShowItem) s.Append("<li><a href='" + RevosJsc.CustomerControl.Link.LnkMnCustomerItemRec() + "'>" + CustomerKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            if (CustomerConfig.ShowGroup) s.Append("<li><a href='" + RevosJsc.CustomerControl.Link.LnkMnCustomerGroupItemRec() + "'>" + CustomerKeyword.DanhSachNhomDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (CustomerConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.CustomerControl.Link.LnkMnCustomerConfig() + "'>" + CustomerKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Contact

        if (ControlConfig.ShowContactUs)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.ContactControl.Link.LnkMnContact() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='hi hi-map-marker sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>" + ContactKeyword.Contact + "</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.ContactControl.Link.LnkMnContactCategory() + "'>" + ContactKeyword.DanhSachDanhMuc + "</a></li>");
            s.Append("<li><a href='" + RevosJsc.ContactControl.Link.LnkMnContactItem() + "'>" + ContactKeyword.DanhSachLienHe + "</a></li>");
            if (ControlConfig.ShowEmail) s.Append("<li><a href='/admin?control=Contact&action=MemberNewsletter'>Member newsletter</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.ContactControl.Link.LnkMnContactCategoryCreate() + "'>" + ContactKeyword.ThemMoiDanhMuc + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.ContactControl.Link.LnkMnContactCategoryRec() + "'>" + ContactKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            s.Append("<li class='divider'></li>");
            if (ContactConfig.ShowConfig) s.Append("<li><a href='" + RevosJsc.ContactControl.Link.LnkMnContactConfig() + "'>" + CustomerKeyword.Config + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Advertistments

        if (ControlConfig.ShowAdvertistments)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.AdvertistmentsControl.Link.LnkMnAdvertistments() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-picture sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>" + AdvertistmentsKeyword.Advertistments + "</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.AdvertistmentsControl.Link.LnkMnAdvertistmentsCategory() + "'>" + AdvertistmentsKeyword.DanhSachDanhMuc + "</a></li>");
            s.Append("<li><a href='" + RevosJsc.AdvertistmentsControl.Link.LnkMnAdvertistments() + "'>" + AdvertistmentsKeyword.DanhSachBaiViet + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.AdvertistmentsControl.Link.LnkMnAdvertistmentsCategoryCreate() + "'>" + AdvertistmentsKeyword.ThemMoiDanhMuc + "</a></li>");
            s.Append("<li><a href='" + RevosJsc.AdvertistmentsControl.Link.LnkMnAdvertistmentsCreate() + "'>" + AdvertistmentsKeyword.ThemMoiBaiViet + "</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.AdvertistmentsControl.Link.LnkMnAdvertistmentsCategoryRec() + "'>" + AdvertistmentsKeyword.DanhSachDanhMucDaXoa + "</a></li>");
            s.Append("<li><a href='" + RevosJsc.AdvertistmentsControl.Link.LnkMnAdvertistmentsRec() + "'>" + AdvertistmentsKeyword.DanhSachBaiVietDaXoa + "</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Menu

        if (ControlConfig.ShowMenus)
        {
            var config = new MenusConfig();
            s.Append("<li>");
            s.Append("<a href='/Admin?control=Menus&action=Index&app=MenuMain' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-list sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>" + MenusKeyword.Menus + "</span></a>");
            s.Append("<ul>");
            for (var i = 0; i < config.Text.Length; i++)
            {
                s.Append("<li><a href='?control=Menus&action=" + RevosJsc.MenusControl.TypePage.Index + "&app=" + config.Values[i] + "'>" + config.Text[i] + @"</a></li>");
            }
            s.Append("<li class='divider'></li>");
            for (var i = 0; i < config.Text.Length; i++)
            {
                s.Append("<li><a href='?control=Menus&action=" + RevosJsc.MenusControl.TypePage.Create + "&app=" + config.Values[i] + "'>Thêm mới " + config.Text[i] + @"</a></li>");
            }
            s.Append("<li class='divider'></li>");
            for (var i = 0; i < config.Text.Length; i++)
            {
                s.Append("<li><a href='?control=Menus&action=" + RevosJsc.MenusControl.TypePage.Recycle + "&app=" + config.Values[i] + "'>" + config.Text[i] + @" đã xóa</a></li>");
            }
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        s.Append(@"
<li class='sidebar-header'>
    <span class='sidebar-header-options'><i class='gi gi-settings'></i></span>
    <span class='sidebar-header-title'>Nâng cao</span>
</li>
");
        #region Systemwebsite

        if (ControlConfig.ShowSystemwebsite)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.SystemWebsiteControl.Link.LnkInfoWebsite() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-cogwheel sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>Hệ thống</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.SystemWebsiteControl.Link.LnkInfoWebsite() + "'>Thông tin website</a></li>");
            s.Append("<li><a href='" + RevosJsc.SystemWebsiteControl.Link.LnkOptimizeSystem() + "'>Tối ưu công cụ tìm kiếm</a></li>");
            s.Append("<li><a href='" + RevosJsc.SystemWebsiteControl.Link.LnkEmail() + "'>Email hệ thống</a></li>");
            s.Append("<li><a href='" + RevosJsc.SystemWebsiteControl.Link.LnkSitemap() + "'>Tạo sitemap.xml & robots.txt</a></li>");
            s.Append("<li><a href='" + RevosJsc.SystemWebsiteControl.Link.LnkLogs() + "'>Logs</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region User

        if (ControlConfig.ShowUsers)
        {
            s.Append("<li>");
            s.Append("<a href='" + Link.LnkUsers() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-user sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>Tài khoản quản trị</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + Link.LnkUsers() + "'>Danh sách tài khoản</a></li>");
            s.Append("<li><a href='" + Link.LnkUsersCreate() + "'>Thêm mới tài khoản</a></li>");
            s.Append("<li><a href='" + Link.LnkUsersRecycle() + "'>Tài khoản đã xóa</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Language

        if (ControlConfig.ShowLanguage)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.LanguageControl.Link.LnkMnLanguage() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-flag sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>Quản lý ngôn ngữ</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.LanguageControl.Link.LnkMnLanguageNational() + "'>Danh sách ngôn ngữ</a></li>");
            s.Append("<li><a href='" + RevosJsc.LanguageControl.Link.LnkMnKeyword() + "'>Danh sách từ khóa</a></li>");
            s.Append("<li><a href='" + RevosJsc.LanguageControl.Link.LnkMnTranslate() + "'>Dịch từ khóa</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Redirect

        if (ControlConfig.ShowRedirect)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.RedirectsControl.Link.LnkRedirect() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-link sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>Chuyển hướng 301</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.RedirectsControl.Link.LnkRedirect() + "'>Danh sách link</a></li>");
            s.Append("<li><a href='" + RevosJsc.RedirectsControl.Link.LnkRedirectCreate() + "'>Thêm mới link</a></li>");
            s.Append("<li><a href='" + RevosJsc.RedirectsControl.Link.LnkRedirectImport() + "'>Import từ Excel</a></li>");
            s.Append("<li><a href='" + RevosJsc.RedirectsControl.Link.LnkRedirectRec() + "'>Danh sách link đã xóa</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        #region Website

        if (ControlConfig.ShowWebsite)
        {
            s.Append("<li>");
            s.Append("<a href='" + RevosJsc.WebsiteControl.Link.LnkMnWebsite() + "' class='sidebar-nav-menu'><i class='fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide'></i><i class='gi gi-globe sidebar-nav-icon'></i><span class='sidebar-nav-mini-hide'>"+ WebsiteKeyword.Website +"</span></a>");
            s.Append("<ul>");
            s.Append("<li><a href='" + RevosJsc.WebsiteControl.Link.LnkMnWebsite() + "'>Danh sách website</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.WebsiteControl.Link.LnkMnWebsiteCreate() + "'>Thêm mới website</a></li>");
            s.Append("<li class='divider'></li>");
            s.Append("<li><a href='" + RevosJsc.WebsiteControl.Link.LnkMnWebsiteRecycle() + "'>Website đã xóa</a></li>");
            s.Append("</ul>");
            s.Append("</li>");
        }

        #endregion

        s.Append("</ul>");
        return s.ToString();
    }
}
using System;
using Developer;
using Developer.Config;
using RevosJsc.Columns;
using RevosJsc.Extension;
using RevosJsc.UsersControl;

public partial class Areas_Admin_Control_AdminLoadControl : System.Web.UI.UserControl
{
    protected string Username = "";
    protected string Classname = "sidebar-visible-lg";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["sidebar"] != null) Classname = Request.Cookies["sidebar"].Value;
        var control = "";
        var action = "";
        if (Request.QueryString["control"] != null) control = Request.QueryString["control"];
        if (Request.QueryString["action"] != null) action = Request.QueryString["action"];
        Username = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var userRole = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuRole));
        var listRoles = new Roles();
        switch (control)
        {
            #region System
            case RevosJsc.AboutUsControl.CodeApplications.AboutUs:
                if (StringExtension.RoleInListRoles(listRoles.AboutUs, userRole) && ControlConfig.ShowAboutUs) plLoadControl.Controls.Add(LoadControl("AboutUs/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.AdvertistmentsControl.CodeApplications.Advertistments:
                if (StringExtension.RoleInListRoles(listRoles.Advertising, userRole) && ControlConfig.ShowAdvertistments) plLoadControl.Controls.Add(LoadControl("Advertistments/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.BlogControl.CodeApplications.Blog:
                if (StringExtension.RoleInListRoles(listRoles.Blog, userRole) && ControlConfig.ShowBlog) plLoadControl.Controls.Add(LoadControl("Blog/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.ContactControl.CodeApplications.Contact:
                if (StringExtension.RoleInListRoles(listRoles.ContactUs, userRole) && ControlConfig.ShowContactUs) plLoadControl.Controls.Add(LoadControl("Contact/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.CruisesControl.CodeApplications.Cruises:
                if (StringExtension.RoleInListRoles(listRoles.Cruises, userRole) && ControlConfig.ShowCruises) plLoadControl.Controls.Add(LoadControl("Cruises/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.CustomerControl.CodeApplications.Customer:
                if (StringExtension.RoleInListRoles(listRoles.Customer, userRole) && ControlConfig.ShowCustomer) plLoadControl.Controls.Add(LoadControl("Customer/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.ReviewsControl.CodeApplications.Reviews:
                if (StringExtension.RoleInListRoles(listRoles.Reviews, userRole) && ControlConfig.ShowReviews) plLoadControl.Controls.Add(LoadControl("Reviews/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.DestinationControl.CodeApplications.Destination:
                if (StringExtension.RoleInListRoles(listRoles.Destination, userRole) && ControlConfig.ShowDestination) plLoadControl.Controls.Add(LoadControl("Destination/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.FAQControl.CodeApplications.FAQ:
                if (StringExtension.RoleInListRoles(listRoles.FAQ, userRole) && ControlConfig.ShowFAQ) plLoadControl.Controls.Add(LoadControl("FAQ/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.FileLibraryControl.CodeApplications.FileLibrary:
                if (StringExtension.RoleInListRoles(listRoles.FileLibrary, userRole) && ControlConfig.ShowFilelibrary) plLoadControl.Controls.Add(LoadControl("FileLibrary/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.HotelControl.CodeApplications.Hotel:
                if (StringExtension.RoleInListRoles(listRoles.Hotel, userRole) && ControlConfig.ShowHotel) plLoadControl.Controls.Add(LoadControl("Hotel/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.LanguageControl.CodeApplications.Language:
                if (StringExtension.RoleInListRoles(listRoles.Language, userRole) && ControlConfig.ShowLanguage) plLoadControl.Controls.Add(LoadControl("Language/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.MemberControl.CodeApplications.Member:
                if (StringExtension.RoleInListRoles(listRoles.Member, userRole) && ControlConfig.ShowMember) plLoadControl.Controls.Add(LoadControl("Member/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case "Menus":
                if (StringExtension.RoleInListRoles(listRoles.Menu, userRole) && ControlConfig.ShowMenus) plLoadControl.Controls.Add(LoadControl("Menus/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.NewsControl.CodeApplications.News:
                if (StringExtension.RoleInListRoles(listRoles.News, userRole) && ControlConfig.ShowNews) plLoadControl.Controls.Add(LoadControl("News/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.PhotoAlbumControl.CodeApplications.PhotoAlbum:
                if (StringExtension.RoleInListRoles(listRoles.PhotoAlbum, userRole) && ControlConfig.ShowPhotoAlbum) plLoadControl.Controls.Add(LoadControl("PhotoAlbum/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.ProductControl.CodeApplications.Product:
                if (StringExtension.RoleInListRoles(listRoles.Product, userRole) && ControlConfig.ShowProduct) plLoadControl.Controls.Add(LoadControl("Product/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.ProjectControl.CodeApplications.Project:
                if (StringExtension.RoleInListRoles(listRoles.Project, userRole) && ControlConfig.ShowProject) plLoadControl.Controls.Add(LoadControl("Project/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.ServiceControl.CodeApplications.Service:
                if (StringExtension.RoleInListRoles(listRoles.Service, userRole) && ControlConfig.ShowService) plLoadControl.Controls.Add(LoadControl("Service/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.OurTeamControl.CodeApplications.OurTeam:
                if (StringExtension.RoleInListRoles(listRoles.OurTeam, userRole) && ControlConfig.ShowOurTeam) plLoadControl.Controls.Add(LoadControl("OurTeam/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.SystemWebsiteControl.CodeApplications.Systemwebsite:
                if (StringExtension.RoleInListRoles(listRoles.Systemwebsite, userRole) && ControlConfig.ShowSystemwebsite) plLoadControl.Controls.Add(LoadControl("SystemWebsite/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.TourControl.CodeApplications.Tour:
                if (StringExtension.RoleInListRoles(listRoles.Tour, userRole) && ControlConfig.ShowTour) plLoadControl.Controls.Add(LoadControl("Tour/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;

            case CodeApplications.Users:
                if (StringExtension.RoleInListRoles(listRoles.Users, userRole) && ControlConfig.ShowUsers || action == "LogOut") plLoadControl.Controls.Add(LoadControl("Users/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;

            case RevosJsc.VideoControl.CodeApplications.Video:
                if (StringExtension.RoleInListRoles(listRoles.Video, userRole) && ControlConfig.ShowVideo) plLoadControl.Controls.Add(LoadControl("Video/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case RevosJsc.WebsiteControl.CodeApplications.Website:
                if (StringExtension.RoleInListRoles(listRoles.Website, userRole) && ControlConfig.ShowWebsite)
                    plLoadControl.Controls.Add(LoadControl("Website/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            case "Redirects":
                if (StringExtension.RoleInListRoles(listRoles.Redirect, userRole) && ControlConfig.ShowRedirect) plLoadControl.Controls.Add(LoadControl("Redirects/LoadControl.ascx"));
                else plLoadControl.Controls.Add(LoadControl("Component/AdmCautionStop.ascx"));
                break;
            #endregion
            default:
                plLoadControl.Controls.Add(LoadControl("Home/LoadControl.ascx"));
                //plLoadControl.Controls.Add(LoadControl("Product/LoadControl.ascx"));
                //plLoadControl.Controls.Add(LoadControl("News/LoadControl.ascx"));
                break;
        }
    }
}
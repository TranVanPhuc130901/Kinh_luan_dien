using System;
using System.IO;
using System.Web.UI;
using Developer.Config;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_SystemWebsite_Action_Sitemap : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        if (cbSitemap.Checked)
        {
            var xml = "<?xml version='1.0' encoding='UTF-8'?>\r\n";
            xml += "<urlset xmlns='http://www.sitemaps.org/schemas/sitemap/0.9'>\r\n";
            xml += " <url>\r\n";
            xml += "  <loc>" + UrlExtension.WebsiteUrl.ToLower() + @"</loc>" + "\r\n";
            xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
            xml += " </url>\r\n";

            #region AboutUs

            if (ControlConfig.ShowAboutUs)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.AboutUs + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Blog

            if (ControlConfig.ShowBlog)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.Blog + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Contact

            if (ControlConfig.ShowContactUs)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.Contact + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Cruises

            if (ControlConfig.ShowCruises)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.Cruises + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Customer

            if (ControlConfig.ShowCustomer)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.Customer + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Destination

            if (ControlConfig.ShowDestination)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.Destination + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region FAQ

            if (ControlConfig.ShowFAQ)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.FAQ + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Filelibrary

            if (ControlConfig.ShowFilelibrary)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.FileLibrary + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Hotel

            if (ControlConfig.ShowHotel)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.Hotel + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region News

            if (ControlConfig.ShowNews)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.News + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region OurTeam

            if (ControlConfig.ShowOurTeam)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.OurTeam + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region PhotoAlbum

            if (ControlConfig.ShowPhotoAlbum)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.PhotoAlbum + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Product

            if (ControlConfig.ShowProduct)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.Product + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Project

            if (ControlConfig.ShowProject)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.Project + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Reviews

            if (ControlConfig.ShowReviews)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.Reviews + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Service

            if (ControlConfig.ShowService)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.Service + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Tour

            if (ControlConfig.ShowTour)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.Tour + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            #region Video

            if (ControlConfig.ShowVideo)
            {
                xml += " <url>\r\n";
                xml += "  <loc>" + UrlExtension.WebsiteUrl + RewriteExtension.Video + RewriteExtension.Extensions + @"</loc>" + "\r\n";
                xml += "  <lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
                xml += " </url>\r\n";
            }

            #endregion

            xml += GetListGroups();
            xml += GetListItems();
            xml += "</urlset>\r\n";
            File.WriteAllText(Request.PhysicalApplicationPath + "/sitemap.xml", xml);
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật sitemap.xml", logAuthor, logCreateDate);
            #endregion
        }
        if (cbRobots.Checked)
        {
            var text = "";
            text = ddlIndex.SelectedValue == "0" ? "Disallow: /" : "User-Agent: *\r\nAllow: /\r\nDisallow: /admin\r\nSitemap: " + UrlExtension.WebsiteUrl + "sitemap.xml";
            File.WriteAllText(Request.PhysicalApplicationPath + "/robots.txt", text);
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật robots.txt" + (ddlIndex.SelectedValue == "0" ? " (Disallow)" : " (Allow)"), logAuthor, logCreateDate);
            #endregion
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);
    }
    private static string GetListItems()
    {
        var s = "";
        var condition = DataExtension.OrConditon(
            ItemsTSql.GetByApp(RevosJsc.ProductControl.CodeApplications.Product),
            ItemsTSql.GetByApp(RevosJsc.AboutUsControl.CodeApplications.AboutUs),
            ItemsTSql.GetByApp(RevosJsc.BlogControl.CodeApplications.Blog),
            ItemsTSql.GetByApp(RevosJsc.CruisesControl.CodeApplications.Cruises),
            ItemsTSql.GetByApp(RevosJsc.CustomerControl.CodeApplications.Customer),
            ItemsTSql.GetByApp(RevosJsc.ReviewsControl.CodeApplications.Reviews),
            ItemsTSql.GetByApp(RevosJsc.DestinationControl.CodeApplications.Destination),
            ItemsTSql.GetByApp(RevosJsc.FAQControl.CodeApplications.FAQ),
            ItemsTSql.GetByApp(RevosJsc.NewsControl.CodeApplications.News),
            ItemsTSql.GetByApp(RevosJsc.FileLibraryControl.CodeApplications.FileLibrary),
            ItemsTSql.GetByApp(RevosJsc.PhotoAlbumControl.CodeApplications.PhotoAlbum),
            ItemsTSql.GetByApp(RevosJsc.VideoControl.CodeApplications.Video),
            ItemsTSql.GetByApp(RevosJsc.ProjectControl.CodeApplications.Project),
            ItemsTSql.GetByApp(RevosJsc.TourControl.CodeApplications.Tour),
            ItemsTSql.GetByApp(RevosJsc.HotelControl.CodeApplications.Hotel),
            ItemsTSql.GetByApp(RevosJsc.ServiceControl.CodeApplications.Service),
            ItemsTSql.GetByApp(RevosJsc.OurTeamControl.CodeApplications.OurTeam)
            );
        condition = DataExtension.AndConditon(
            ItemsTSql.GetByStatus("1"),
            condition
        );
        var dt = Items.GetData("", ItemsColumns.ViLink + "," + ItemsColumns.DiDateModified, condition, ItemsColumns.ViLink);
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i][ItemsColumns.ViLink].ToString().Length <= 0) continue;
            s += " <url>\r\n";
            s += "  <loc>" + (UrlExtension.WebsiteUrl + dt.Rows[i][ItemsColumns.ViLink] + RewriteExtension.Extensions).ToLower() + @"</loc>" + "\r\n";
            s += "  <lastmod>" + ((DateTime)dt.Rows[i][ItemsColumns.DiDateModified]).ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
            s += " </url>\r\n";
        }
        return s;
    }

    private static string GetListGroups()
    {
        var s = "";
        var condition = DataExtension.OrConditon(
            GroupsTSql.GetByApp(RevosJsc.ProductControl.CodeApplications.Product),
            GroupsTSql.GetByApp(RevosJsc.AboutUsControl.CodeApplications.AboutUs),
            GroupsTSql.GetByApp(RevosJsc.BlogControl.CodeApplications.Blog),
            GroupsTSql.GetByApp(RevosJsc.CruisesControl.CodeApplications.Cruises),
            GroupsTSql.GetByApp(RevosJsc.CustomerControl.CodeApplications.Customer),
            GroupsTSql.GetByApp(RevosJsc.ReviewsControl.CodeApplications.Reviews),
            GroupsTSql.GetByApp(RevosJsc.DestinationControl.CodeApplications.Destination),
            GroupsTSql.GetByApp(RevosJsc.FAQControl.CodeApplications.FAQ),
            GroupsTSql.GetByApp(RevosJsc.NewsControl.CodeApplications.News),
            GroupsTSql.GetByApp(RevosJsc.FileLibraryControl.CodeApplications.FileLibrary),
            GroupsTSql.GetByApp(RevosJsc.PhotoAlbumControl.CodeApplications.PhotoAlbum),
            GroupsTSql.GetByApp(RevosJsc.VideoControl.CodeApplications.Video),
            GroupsTSql.GetByApp(RevosJsc.ProjectControl.CodeApplications.Project),
            GroupsTSql.GetByApp(RevosJsc.TourControl.CodeApplications.Tour),
            GroupsTSql.GetByApp(RevosJsc.TourControl.CodeApplications.TourGroupItem),
            GroupsTSql.GetByApp(RevosJsc.HotelControl.CodeApplications.Hotel),
            GroupsTSql.GetByApp(RevosJsc.ServiceControl.CodeApplications.Service),
            GroupsTSql.GetByApp(RevosJsc.OurTeamControl.CodeApplications.OurTeam)
        );
        condition = DataExtension.AndConditon(
            GroupsTSql.GetByStatus("1"),
            condition
            );
        var dt = Groups.GetData("", GroupsColumns.VgLink + "," + GroupsColumns.DgDateModified, condition, GroupsColumns.VgLink);
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i][GroupsColumns.VgLink].ToString().Length <= 0) continue;
            s += " <url>\r\n";
            s += "  <loc>" + (UrlExtension.WebsiteUrl + dt.Rows[i][GroupsColumns.VgLink] + RewriteExtension.Extensions).ToLower() + @"</loc>" + "\r\n";
            s += "  <lastmod>" + ((DateTime)dt.Rows[i][GroupsColumns.DgDateModified]).ToString("yyyy-MM-dd") + @"</lastmod>" + "\r\n";
            s += " </url>\r\n";
        }
        return s;
    }
    protected void cbRobots_OnCheckedChanged(object sender, EventArgs e)
    {
        pnRobots.Visible = cbRobots.Checked;
    }
}
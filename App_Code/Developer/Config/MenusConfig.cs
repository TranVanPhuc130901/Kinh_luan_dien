using System.Collections.Generic;
using Developer.Extension;
using Developer.Keyword;
using RevosJsc.MenusControl;

namespace Developer.Config
{
    /// <summary>
    /// Lưu các cấu hình cho menu
    /// </summary>
    public class MenusConfig
    {
        #region Các menu

        private string[] values;
        private string[] text;

        #endregion Các menu

        #region Các modul để liệt kê khi tạo menu và chọn modul có sẵn

        private string[] textControl;
        private string[] valuesControl;
        private string[] appsControl;

        #endregion Các modul để liệt kê khi tạo menu và chọn modul có sẵn

        public MenusConfig()
        {
            #region Các menu

            text = new[]
            {
                "Menu chính",
                "Menu hỗ trợ",
                "Menu chân trang",
                "Từ khóa phổ biến"
            };
            values = new[]
            {

                CodeApplications.MenuMain,
                CodeApplications.MenuOther,
                CodeApplications.MenuBottom,
                CodeApplications.MenuTop
            };

            #endregion Các menu

            #region Các modul để liệt kê khi tạo menu và chọn modul có sẵn

            var textList = new List<string>();
            var valuesList = new List<string>();
            var appsList = new List<string>();

            textList.Add("Chọn trang");
            valuesList.Add("");
            appsList.Add("");

            textList.Add("Trang chủ");
            valuesList.Add("/");
            appsList.Add("");

            if (ControlConfig.ShowAboutUs)
            {
                textList.Add(AboutUsKeyword.AboutUs1);
                valuesList.Add("?rewrite=" + RewriteExtension.AboutUs);
                appsList.Add(RevosJsc.AboutUsControl.CodeApplications.AboutUs);
            }
            if (ControlConfig.ShowBlog)
            {
                textList.Add(BlogKeyword.Blog1);
                valuesList.Add("?rewrite=" + RewriteExtension.Blog);
                appsList.Add(RevosJsc.BlogControl.CodeApplications.Blog);
            }
            if (ControlConfig.ShowContactUs)
            {
                textList.Add(ContactKeyword.Contact1);
                valuesList.Add("?rewrite=" + RewriteExtension.Contact);
                appsList.Add(RevosJsc.ContactControl.CodeApplications.Contact);
            }
            if (ControlConfig.ShowCruises)
            {
                textList.Add(CruisesKeyword.Cruises1);
                valuesList.Add("?rewrite=" + RewriteExtension.Cruises);
                appsList.Add(RevosJsc.CruisesControl.CodeApplications.Cruises);
            }
            if (ControlConfig.ShowCustomer)
            {
                textList.Add(CustomerKeyword.Customer1);
                valuesList.Add("?rewrite=" + RewriteExtension.Customer);
                appsList.Add(RevosJsc.CustomerControl.CodeApplications.Customer);
            }
            if (ControlConfig.ShowDestination)
            {
                textList.Add(DestinationKeyword.Destination1);
                valuesList.Add("?rewrite=" + RewriteExtension.Destination);
                appsList.Add(RevosJsc.DestinationControl.CodeApplications.Destination);
            }
            if (ControlConfig.ShowFAQ)
            {
                textList.Add(FAQKeyword.FAQ1);
                valuesList.Add("?rewrite=" + RewriteExtension.FAQ);
                appsList.Add(RevosJsc.FAQControl.CodeApplications.FAQ);
            }
            if (ControlConfig.ShowFilelibrary)
            {
                textList.Add(FileLibraryKeyword.FileLibrary1);
                valuesList.Add("?rewrite=" + RewriteExtension.FileLibrary);
                appsList.Add(RevosJsc.FileLibraryControl.CodeApplications.FileLibrary);
            }
            if (ControlConfig.ShowHotel)
            {
                textList.Add(HotelKeyword.Hotel1);
                valuesList.Add("?rewrite=" + RewriteExtension.Hotel);
                appsList.Add(RevosJsc.HotelControl.CodeApplications.Hotel);
            }
            if (ControlConfig.ShowNews)
            {
                textList.Add(NewsKeyword.News1);
                valuesList.Add("?rewrite=" + RewriteExtension.News);
                appsList.Add(RevosJsc.NewsControl.CodeApplications.News);
            }
            if (ControlConfig.ShowOurTeam)
            {
                textList.Add(OurTeamKeyword.OurTeam1);
                valuesList.Add("?rewrite=" + RewriteExtension.OurTeam);
                appsList.Add(RevosJsc.OurTeamControl.CodeApplications.OurTeam);
            }
            if (ControlConfig.ShowMember)
            {
                textList.Add(MemberKeyword.Member1);
                valuesList.Add("?rewrite=" + RewriteExtension.Member);
                appsList.Add(RevosJsc.MemberControl.CodeApplications.Member);
            }
            if (ControlConfig.ShowPhotoAlbum)
            {
                textList.Add(PhotoAlbumKeyword.PhotoAlbum1);
                valuesList.Add("?rewrite=" + RewriteExtension.PhotoAlbum);
                appsList.Add(RevosJsc.PhotoAlbumControl.CodeApplications.PhotoAlbum);
            }
            if (ControlConfig.ShowProduct)
            {
                textList.Add(ProductKeyword.Product1);
                valuesList.Add("?rewrite=" + RewriteExtension.Product);
                appsList.Add(RevosJsc.ProductControl.CodeApplications.Product);
            }
            if (ControlConfig.ShowProject)
            {
                textList.Add(ProjectKeyword.Project1);
                valuesList.Add("?rewrite=" + RewriteExtension.Project);
                appsList.Add(RevosJsc.ProjectControl.CodeApplications.Project);
            }
            if (ControlConfig.ShowReviews)
            {
                textList.Add(ReviewsKeyword.Reviews1);
                valuesList.Add("?rewrite=" + RewriteExtension.Reviews);
                appsList.Add(RevosJsc.ReviewsControl.CodeApplications.Reviews);
            }
            if (ControlConfig.ShowService)
            {
                textList.Add(ServiceKeyword.Service1);
                valuesList.Add("?rewrite=" + RewriteExtension.Service);
                appsList.Add(RevosJsc.ProductControl.CodeApplications.Product);
            }
            if (ControlConfig.ShowTour)
            {
                textList.Add(TourKeyword.Tour1);
                valuesList.Add("?rewrite=" + RewriteExtension.Tour);
                appsList.Add(RevosJsc.TourControl.CodeApplications.Tour);
            }
            if (ControlConfig.ShowVideo)
            {
                textList.Add(VideoKeyword.Video1);
                valuesList.Add("?rewrite=" + RewriteExtension.Video);
                appsList.Add(RevosJsc.VideoControl.CodeApplications.Video);
            }

            textControl = textList.ToArray();
            valuesControl = valuesList.ToArray();
            appsControl = appsList.ToArray();

            #endregion Các modul để liệt kê khi tạo menu và chọn modul có sẵn
        }

        #region Các menu

        /// <summary>
        /// Danh sách tên của menu, vd: menu chính, menu trên...
        /// </summary>
        public string[] Text
        {
            get { return text; }
        }

        /// <summary>
        /// Danh sách tên của app, vd: CodeApplications.MenuMain, CodeApplications.MenuTop...
        /// </summary>
        public string[] Values
        {
            get { return values; }
        }

        #endregion Các menu

        #region Các modul để liệt kê khi tạo menu và chọn modul có sẵn

        /// <summary>
        /// Danh sách tên của modul, vd: Tin tức, Sản phẩm...
        /// </summary>
        public string[] TextControl
        {
            get { return textControl; }
        }

        /// <summary>
        /// Danh sách tên của modul, vd: "?rewrite="+RewriteExtension.Product, "?rewrite="+RewriteExtension.News...
        /// </summary>
        public string[] ValuesControl
        {
            get { return valuesControl; }
        }

        /// <summary>
        /// Danh sách app của modul, vd: CodeApplication.Product, CodeApplication.News...
        /// </summary>
        public string[] AppsControl
        {
            get { return appsControl; }
        }

        #endregion Các modul để liệt kê khi tạo menu và chọn modul có sẵn
    }
}
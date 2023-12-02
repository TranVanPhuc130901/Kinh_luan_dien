using System;
using System.Web;
using RevosJsc;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.LanguageControl;
using RevosJsc.TSql;

namespace Developer.Extension
{
    /// <summary>
    /// Summary description for RewriteExtension
    /// </summary>
    public class RewriteExtension
    {
        #region Các keyword - lưu ý không được để bằng chuỗi rỗng

        public static string Extensions = ".htm";
        public static string Homepage = "trang-chu";
        public static string AboutUs = "gioi-thieu";
        public static string Blog = "blog";
        public static string Booking = "booking";
        public static string Cart = "gio-hang";
        public static string Contact = "lien-he";
        public static string Content = "post";
        public static string Cruises = "cruises";
        public static string Customer = "customer";
        public static string Reviews = "happy-customers";
        public static string Deal = "deal";
        public static string Destination = "destinations";
        public static string Error = "error";
        public static string FileLibrary = "fileLibrary";
        public static string Forum = "forum";
        public static string Hotel = "hotel";
        public static string Member = "member";
        public static string News = "bai-viet";
        public static string OurTeam = "our-team";
        public static string PhotoAlbum = "photo-album";
        public static string Product = "san-pham";
        public static string Project = "du-an";
        public static string FAQ = "faq";
        public static string Search = "search";
        public static string Service = "thuong-hieu";
        public static string Support = "support";
        public static string Tag = "tag";
        public static string Tour = "circuits";
        public static string TrainTicket = "train-ticket";
        public static string Video = "video";
        public static string Website = "web";

        #endregion Các keyword - lưu ý không được để bằng chuỗi rỗng

        #region Các phương thức
        public static string GetLinkMenu(string vgdesc)
        {
            var rewrite = MenuExtension.GetQueryString(vgdesc, "rewrite");
            var igid = MenuExtension.GetQueryString(vgdesc, "igid");
            var iid = MenuExtension.GetQueryString(vgdesc, "iid");

            if (iid != "") return GetLinkMenuItem(iid);
            if (igid != "") return GetLinkMenuCate(igid);
            if (rewrite != "") return GetLinkMenuIndex(rewrite);
            if (vgdesc == "") vgdesc = "javascript:void(0);";
            return vgdesc;
        }

        private static readonly string Url = WebsiteUrl;
        private static string GetLinkMenuIndex(string rewrite)
        {
            return Url + rewrite.ToLower() + Extensions;
        }

        private static string GetLinkMenuCate(string igid)
        {
            return Url + GetSeoLinkByIgid(igid) + Extensions;
        }

        private static string GetLinkMenuItem(string iiId)
        {
            return Url + GetSeoLinkByIid(iiId) + Extensions;
        }

        public static string GetSeoLinkByIid(string iiId)
        {
            var dt = Items.GetData("1", ItemsColumns.ViLink, ItemsTSql.GetById(iiId), "");
            return dt.Rows.Count > 0 ? dt.Rows[0][ItemsColumns.ViLink].ToString().ToLower() : "";
        }

        public static string GetSeoLinkByIgid(string igid)
        {
            var dt = Groups.GetData("1", GroupsColumns.VgLink, GroupsTSql.GetById(igid), "");
            return dt.Rows.Count > 0 ? dt.Rows[0][GroupsColumns.VgLink].ToString().ToLower() : "";
        }
        public static string WebsiteUrl
        {
            get
            {
                var lang = Cookie.GetLanguageValueDisplay();
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
                var langname = "";
                if (lang.Equals("2")) langname = "en/";
                else if (lang.Equals("3")) langname = "jp/";
                url = url + langname;
                return url.Replace("www.", "");
            }
        }

        #endregion Các phương thức
    }
}
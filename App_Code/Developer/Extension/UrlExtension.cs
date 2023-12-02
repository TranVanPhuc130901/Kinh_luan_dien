using System;
using System.Web;

namespace Developer.Extension
{
    public class UrlExtension
    {
        /// <summary>
        /// Lấy url tự động
        /// </summary>
        public static string WebsiteUrl
        {
            get
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
                return url.Replace("www.", "");
            }
        }
        public static string RawUrl
        {
            get
            {
                var url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.RawUrl;
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
                if (url.EndsWith("/")) url = url.Substring(0, url.Length - 1);
                return url.Replace("www.", "");
            }
        }
    }
}

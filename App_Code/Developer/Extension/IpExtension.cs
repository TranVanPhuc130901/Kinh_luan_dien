using System.Web;

namespace Developer.Extension
{
    /// <summary>
    /// Thực thiện các công việc như lưu, lấy cookies...
    /// </summary>
    public class IpExtension
    {
        public static string GetIpAddress()
        {
            var context = HttpContext.Current;
            var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAddress)) return context.Request.ServerVariables["REMOTE_ADDR"];
            var addresses = ipAddress.Split(',');
            return addresses.Length != 0 ? addresses[0] : context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
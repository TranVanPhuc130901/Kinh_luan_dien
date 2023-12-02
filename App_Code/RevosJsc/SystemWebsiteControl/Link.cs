namespace RevosJsc.SystemWebsiteControl
{
    /// <summary>
    /// Summary description for Link
    /// </summary>
    public class Link
    {
        public static string LnkInfoWebsite()
        {
            return "/Admin?control=SystemWebsite&action=Infomation";
        }
        public static string LnkOptimizeSystem()
        {
            return "/Admin?control=SystemWebsite&action=OptimizeSystem";
        }
        public static string LnkEmail()
        {
            return "/Admin?control=SystemWebsite&action=Email";
        }
        public static string LnkSitemap()
        {
            return "/Admin?control=SystemWebsite&action=Sitemap";
        }
        public static string LnkLogs()
        {
            return "/Admin?control=SystemWebsite&action=Logs";
        }
    }
}
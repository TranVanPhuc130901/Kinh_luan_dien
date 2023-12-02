using RevosJsc.AdminControl;

namespace RevosJsc.WebsiteControl
{
    public class Link
    {
        public static string LnkMnWebsite()
        {
            return LinkAdmin.GoAdminControl("Website");
        }

        public static string LnkMnWebsiteCreate()
        {
            return LinkAdmin.GoAdminSubControl("Website", "Create");
        }

        public static string LnkMnWebsiteRecycle()
        {
            return LinkAdmin.GoAdminSubControl("Website", "Recycle");
        }

        public static string LnkMnWebsiteConfig()
        {
            return LinkAdmin.GoAdminSubControl("Website", "Configuration");
        }
    }
}

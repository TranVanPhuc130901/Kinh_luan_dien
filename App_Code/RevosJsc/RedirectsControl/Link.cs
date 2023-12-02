using RevosJsc.AdminControl;

namespace RevosJsc.RedirectsControl
{
    public class Link
    {
        public static string LnkRedirect()
        {
            return LinkAdmin.GoAdminControl("Redirects");
        }

        public static string LnkRedirectCreate()
        {
            return LinkAdmin.GoAdminSubControl("Redirects", "Create");
        }

        public static string LnkRedirectRec()
        {
            return LinkAdmin.GoAdminSubControl("Redirects", "Recycle");
        }

        public static string LnkRedirectImport()
        {
            return LinkAdmin.GoAdminSubControl("Redirects", "ImportExcel");
        }

    }
}

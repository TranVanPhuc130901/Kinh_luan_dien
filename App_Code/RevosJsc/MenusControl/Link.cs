using RevosJsc.AdminControl;

namespace RevosJsc.MenusControl
{
    public class Link
    {
        public static string LnkMenu(string action, string app)
        {
            return LinkAdmin.GoAdminOption("Menus", action, "app", app);
        }
        public static string LnkMenu(string action, string app, string igid)
        {
            return LinkAdmin.GoAdminMenu("Menus", action, app, igid);
        }
        public static string LnkMenuMainenu()
        {
            return LinkAdmin.GoAdminControl("Menus");
        }
        public static string LnkMenuMainenuMain()
        {
            return LinkAdmin.GoAdminControl("MenuMain");
        }

        public static string LnkMenuMainenuMainCreate()
        {
            return LinkAdmin.GoAdminSubControl("MenuMain", "create");
        }

        public static string LnkMenuMainenuMainRec()
        {
            return LinkAdmin.GoAdminSubControl("MenuMain", "recycle");
        }

        public static string LnkMenuMainenuTop()
        {
            return LinkAdmin.GoAdminControl("MenuTop");
        }

        public static string LnkMenuMainenuTopCreate()
        {
            return LinkAdmin.GoAdminSubControl("MenuTop", "create");
        }

        public static string LnkMenuMainenuTopRec()
        {
            return LinkAdmin.GoAdminSubControl("MenuTop", "recycle");
        }

        public static string LnkMenuMainenuBottom()
        {
            return LinkAdmin.GoAdminControl("MenuBottom");
        }

        public static string LnkMenuMainenuBottomCreate()
        {
            return LinkAdmin.GoAdminSubControl("MenuBottom", "create");
        }

        public static string LnkMenuMainenuBottomRec()
        {
            return LinkAdmin.GoAdminSubControl("MenuBottom", "recycle");
        }

        public static string LnkMenuMainenuOther()
        {
            return LinkAdmin.GoAdminControl("MenuOther");
        }

        public static string LnkMenuMainenuOtherCreate()
        {
            return LinkAdmin.GoAdminSubControl("MenuOther", "create");
        }

        public static string LnkMenuMainenuOtherRec()
        {
            return LinkAdmin.GoAdminSubControl("MenuOther", "recycle");
        }
    }
}

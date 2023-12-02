using RevosJsc.AdminControl;

namespace RevosJsc.LanguageControl
{
    public class Link
    {
        public static string LnkMnLanguage()
        {
            return LinkAdmin.GoAdminControl("Language");
        }

        public static string LnkMnLanguageNational()
        {
            return LinkAdmin.GoAdminSubControl("Language", TypePage.Flag);
        }

        public static string LnkMnLanguageNationalCreate()
        {
            return LinkAdmin.GoAdminSubControl("Language", TypePage.AddEditNational);
        }
        public static string LnkMnKeyword()
        {
            return LinkAdmin.GoAdminSubControl("Language", TypePage.Keyword);
        }

        public static string LnkMnKeywordCreate()
        {
            return LinkAdmin.GoAdminSubControl("Language", TypePage.AddEditKeyword);
        }
        public static string LnkMnTranslate()
        {
            return LinkAdmin.GoAdminSubControl("Language", TypePage.Translate);
        }

        public static string LnkMnTranslateCreate()
        {
            return LinkAdmin.GoAdminSubControl("Language", "AddEditTranslate");
        }
    }
}

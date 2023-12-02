using RevosJsc.AdminControl;

namespace RevosJsc.ContactControl
{
    public class Link
    {
        public static string LnkMnContact()
        {
            return LinkAdmin.GoAdminControl("Contact");
        }

        public static string LnkMnContactCategory()
        {
            return LinkAdmin.GoAdminSubControl("Contact", "Category");
        }

        public static string LnkMnContactCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Contact", "CreateCategory");
        }

        public static string LnkMnContactCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Contact", "RecycleCategory");
        }

        public static string LnkMnContactItem()
        {
            return LinkAdmin.GoAdminSubControl("Contact", "Item");
        }

        public static string LnkMnContactItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Contact", "CreateItem");
        }

        public static string LnkMnContactItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Contact", "RecycleItem");
        }

        public static string LnkMnContactConfig()
        {
            return LinkAdmin.GoAdminSubControl("Contact", "Config");
        }
    }
}

using RevosJsc.AdminControl;

namespace RevosJsc.AdvertistmentsControl
{
    public class Link
    {

        public static string LnkMnAdvertistmentsCategory()
        {
            return LinkAdmin.GoAdminSubControl("Advertistments", "Category");
        }

        public static string LnkMnAdvertistmentsCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Advertistments", "CreateCategory");
        }

        public static string LnkMnAdvertistmentsCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Advertistments", "RecycleCategory");
        }

        public static string LnkMnAdvertistments()
        {
            return LinkAdmin.GoAdminSubControl("Advertistments", "Item");
        }

        public static string LnkMnAdvertistmentsCreate()
        {
            return LinkAdmin.GoAdminSubControl("Advertistments", "CreateItem");
        }

        public static string LnkMnAdvertistmentsRec()
        {
            return LinkAdmin.GoAdminSubControl("Advertistments", "RecycleItem");
        }
    }
}

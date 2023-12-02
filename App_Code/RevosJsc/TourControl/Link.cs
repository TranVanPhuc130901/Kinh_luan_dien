using RevosJsc.AdminControl;

namespace RevosJsc.TourControl
{
    public class Link
    {
        public static string LnkMnTour()
        {
            return LinkAdmin.GoAdminControl("Tour");
        }

        public static string LnkMnTourCategory()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "Category");
        }

        public static string LnkMnTourCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "CreateCategory");
        }

        public static string LnkMnTourCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "RecycleCategory");
        }

        public static string LnkMnTourItem()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "Item");
        }

        public static string LnkMnTourItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "CreateItem");
        }

        public static string LnkMnTourItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "RecycleItem");
        }

        public static string LnkMnTourFilter()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "Filter");
        }

        public static string LnkMnTourFilterCreate()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "CreateFilter");
        }

        public static string LnkMnTourFilterRec()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "RecycleFilter");
        }

        public static string LnkMnTourProperty()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "Property");
        }

        public static string LnkMnTourPropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "CreateProperty");
        }

        public static string LnkMnTourPropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "RecycleProperty");
        }

        public static string LnkMnTourColor()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "Color");
        }

        public static string LnkMnTourColorCreate()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "CreateColor");
        }

        public static string LnkMnTourColorRec()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "RecycleColor");
        }

        public static string LnkMnTourGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "GroupItem");
        }

        public static string LnkMnTourGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "CreateGroupItem");
        }

        public static string LnkMnTourGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "RecycleGroupItem");
        }

        public static string LnkMnTourGroupCategory()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "GroupCategory");
        }

        public static string LnkMnTourGroupCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "CreateGroupCategory");
        }

        public static string LnkMnTourGroupCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "RecycleGroupCategory");
        }

        public static string LnkMnTourProvider()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "Provider");
        }

        public static string LnkMnTourProviderCreate()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "CreateProvider");
        }

        public static string LnkMnTourProviderRec()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "RecycleProvider");
        }

        public static string LnkMnTourCart()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "Cart");
        }

        public static string LnkMnTourConfig()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "Configuration");
        }

        public static string LnkMnTourComment()
        {
            return LinkAdmin.GoAdminSubControl("Tour", "Comment");
        }
    }
}

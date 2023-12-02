using RevosJsc.AdminControl;

namespace RevosJsc.DestinationControl
{
    public class Link
    {
        public static string LnkMnDestination()
        {
            return LinkAdmin.GoAdminControl("Destination");
        }

        public static string LnkMnDestinationCategory()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "Category");
        }

        public static string LnkMnDestinationCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "CreateCategory");
        }

        public static string LnkMnDestinationCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "RecycleCategory");
        }

        public static string LnkMnDestinationItem()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "Item");
        }

        public static string LnkMnDestinationItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "CreateItem");
        }

        public static string LnkMnDestinationItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "RecycleItem");
        }

        public static string LnkMnDestinationGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "GroupItem");
        }

        public static string LnkMnDestinationGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "CreateGroupItem");
        }

        public static string LnkMnDestinationGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "RecycleGroupItem");
        }

        public static string LnkMnDestinationGroupCategory()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "GroupCategory");
        }

        public static string LnkMnDestinationGroupCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "CreateGroupCategory");
        }

        public static string LnkMnDestinationGroupCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "RecycleGroupCategory");
        }

        public static string LnkMnDestinationProperty()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "Property");
        }

        public static string LnkMnDestinationPropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "CreateProperty");
        }

        public static string LnkMnDestinationPropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "RecycleProperty");
        }

        public static string LnkMnDestinationConfig()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "Configuration");
        }

        public static string LnkMnDestinationComment()
        {
            return LinkAdmin.GoAdminSubControl("Destination", "Comment");
        }
    }
}

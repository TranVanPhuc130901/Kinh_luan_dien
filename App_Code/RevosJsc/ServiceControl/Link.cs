using RevosJsc.AdminControl;

namespace RevosJsc.ServiceControl
{
    public class Link
    {
        public static string LnkMnService()
        {
            return LinkAdmin.GoAdminControl("Service");
        }

        public static string LnkMnServiceCategory()
        {
            return LinkAdmin.GoAdminSubControl("Service", "Category");
        }

        public static string LnkMnServiceCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Service", "CreateCategory");
        }

        public static string LnkMnServiceCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Service", "RecycleCategory");
        }

        public static string LnkMnServiceItem()
        {
            return LinkAdmin.GoAdminSubControl("Service", "Item");
        }

        public static string LnkMnServiceItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Service", "CreateItem");
        }

        public static string LnkMnServiceItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Service", "RecycleItem");
        }

        public static string LnkMnServiceGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("Service", "GroupItem");
        }

        public static string LnkMnServiceGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Service", "CreateGroupItem");
        }

        public static string LnkMnServiceGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Service", "RecycleGroupItem");
        }

        public static string LnkMnServiceGroupCategory()
        {
            return LinkAdmin.GoAdminSubControl("Service", "GroupCategory");
        }

        public static string LnkMnServiceGroupCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Service", "CreateGroupCategory");
        }

        public static string LnkMnServiceGroupCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Service", "RecycleGroupCategory");
        }

        public static string LnkMnServiceProperty()
        {
            return LinkAdmin.GoAdminSubControl("Service", "Property");
        }

        public static string LnkMnServicePropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("Service", "CreateProperty");
        }

        public static string LnkMnServicePropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("Service", "RecycleProperty");
        }

        public static string LnkMnServiceConfig()
        {
            return LinkAdmin.GoAdminSubControl("Service", "Configuration");
        }

        public static string LnkMnServiceComment()
        {
            return LinkAdmin.GoAdminSubControl("Service", "Comment");
        }

        public static string LnkMnServiceOrder()
        {
            return LinkAdmin.GoAdminSubControl("Service", "Order");
        }
    }
}

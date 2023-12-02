using RevosJsc.AdminControl;

namespace RevosJsc.CustomerControl
{
    public class Link
    {
        public static string LnkMnCustomer()
        {
            return LinkAdmin.GoAdminControl("Customer");
        }

        public static string LnkMnCustomerCategory()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "Category");
        }

        public static string LnkMnCustomerCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "CreateCategory");
        }

        public static string LnkMnCustomerCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "RecycleCategory");
        }

        public static string LnkMnCustomerItem()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "Item");
        }

        public static string LnkMnCustomerItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "CreateItem");
        }

        public static string LnkMnCustomerItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "RecycleItem");
        }

        public static string LnkMnCustomerGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "GroupItem");
        }

        public static string LnkMnCustomerGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "CreateGroupItem");
        }

        public static string LnkMnCustomerGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "RecycleGroupItem");
        }

        public static string LnkMnCustomerGroupCategory()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "GroupCategory");
        }

        public static string LnkMnCustomerGroupCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "CreateGroupCategory");
        }

        public static string LnkMnCustomerGroupCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "RecycleGroupCategory");
        }

        public static string LnkMnCustomerProperty()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "Property");
        }

        public static string LnkMnCustomerPropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "CreateProperty");
        }

        public static string LnkMnCustomerPropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "RecycleProperty");
        }

        public static string LnkMnCustomerConfig()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "Configuration");
        }

        public static string LnkMnCustomerComment()
        {
            return LinkAdmin.GoAdminSubControl("Customer", "Comment");
        }
    }
}

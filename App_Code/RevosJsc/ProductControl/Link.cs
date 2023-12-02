using RevosJsc.AdminControl;

namespace RevosJsc.ProductControl
{
    public class Link
    {
        public static string LnkMnProduct()
        {
            return LinkAdmin.GoAdminControl("Product");
        }

        public static string LnkMnProductBill()
        {
            return LinkAdmin.GoAdminSubControl("Product", "Bill");
        }

        public static string LnkMnProductCategory()
        {
            return LinkAdmin.GoAdminSubControl("Product", "Category");
        }

        public static string LnkMnProductCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Product", "CreateCategory");
        }

        public static string LnkMnProductCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Product", "RecycleCategory");
        }

        public static string LnkMnProductItem()
        {
            return LinkAdmin.GoAdminSubControl("Product", "Item");
        }

        public static string LnkMnProductItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Product", "CreateItem");
        }

        public static string LnkMnProductItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Product", "RecycleItem");
        }

        public static string LnkMnProductFilter()
        {
            return LinkAdmin.GoAdminSubControl("Product", "Filter");
        }

        public static string LnkMnProductFilterCreate()
        {
            return LinkAdmin.GoAdminSubControl("Product", "CreateFilter");
        }

        public static string LnkMnProductFilterRec()
        {
            return LinkAdmin.GoAdminSubControl("Product", "RecycleFilter");
        }

        public static string LnkMnProductProperty()
        {
            return LinkAdmin.GoAdminSubControl("Product", "Property");
        }

        public static string LnkMnProductPropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("Product", "CreateProperty");
        }

        public static string LnkMnProductPropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("Product", "RecycleProperty");
        }

        public static string LnkMnProductColor()
        {
            return LinkAdmin.GoAdminSubControl("Product", "Color");
        }

        public static string LnkMnProductColorCreate()
        {
            return LinkAdmin.GoAdminSubControl("Product", "CreateColor");
        }

        public static string LnkMnProductColorRec()
        {
            return LinkAdmin.GoAdminSubControl("Product", "RecycleColor");
        }

        public static string LnkMnProductGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("Product", "GroupItem");
        }

        public static string LnkMnProductGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Product", "CreateGroupItem");
        }

        public static string LnkMnProductGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Product", "RecycleGroupItem");
        }

        public static string LnkMnProductGroupCategory()
        {
            return LinkAdmin.GoAdminSubControl("Product", "GroupCategory");
        }

        public static string LnkMnProductGroupCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Product", "CreateGroupCategory");
        }

        public static string LnkMnProductGroupCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Product", "RecycleGroupCategory");
        }

        public static string LnkMnProductProvider()
        {
            return LinkAdmin.GoAdminSubControl("Product", "Provider");
        }

        public static string LnkMnProductProviderCreate()
        {
            return LinkAdmin.GoAdminSubControl("Product", "CreateProvider");
        }

        public static string LnkMnProductProviderRec()
        {
            return LinkAdmin.GoAdminSubControl("Product", "RecycleProvider");
        }

        public static string LnkMnProductCart()
        {
            return LinkAdmin.GoAdminSubControl("Product", "Cart");
        }

        public static string LnkMnProductConfig()
        {
            return LinkAdmin.GoAdminSubControl("Product", "Configuration");
        }

        public static string LnkMnProductComment()
        {
            return LinkAdmin.GoAdminSubControl("Product", "Comment");
        }
        public static string LnkMnProductPayment()
        {
            return LinkAdmin.GoAdminSubControl("Product", "Payment");
        }
        public static string LnkMnProductPaymentCreate()
        {
            return LinkAdmin.GoAdminSubControl("Product", "CreatePayment");
        }
        public static string LnkMnProductPaymentRec()
        {
            return LinkAdmin.GoAdminSubControl("Product", "RecyclePayment");
        }
    }
}

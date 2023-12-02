using RevosJsc.AdminControl;

namespace RevosJsc.ReviewsControl
{
    public class Link
    {
        public static string LnkMnReviews()
        {
            return LinkAdmin.GoAdminControl("Reviews");
        }

        public static string LnkMnReviewsCategory()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "Category");
        }

        public static string LnkMnReviewsCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "CreateCategory");
        }

        public static string LnkMnReviewsCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "RecycleCategory");
        }

        public static string LnkMnReviewsItem()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "Item");
        }

        public static string LnkMnReviewsItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "CreateItem");
        }

        public static string LnkMnReviewsItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "RecycleItem");
        }

        public static string LnkMnReviewsGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "GroupItem");
        }

        public static string LnkMnReviewsGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "CreateGroupItem");
        }

        public static string LnkMnReviewsGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "RecycleGroupItem");
        }


        public static string LnkMnReviewsProperty()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "Property");
        }

        public static string LnkMnReviewsPropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "CreateProperty");
        }

        public static string LnkMnReviewsPropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "RecycleProperty");
        }

        public static string LnkMnReviewsConfig()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "Configuration");
        }

        public static string LnkMnReviewsComment()
        {
            return LinkAdmin.GoAdminSubControl("Reviews", "Comment");
        }
    }
}

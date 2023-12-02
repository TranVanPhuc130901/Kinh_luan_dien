using RevosJsc.AdminControl;

namespace RevosJsc.BlogControl
{
    public class Link
    {
        public static string LnkMnBlog()
        {
            return LinkAdmin.GoAdminControl("Blog");
        }

        public static string LnkMnBlogCategory()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "Category");
        }

        public static string LnkMnBlogCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "CreateCategory");
        }

        public static string LnkMnBlogCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "RecycleCategory");
        }

        public static string LnkMnBlogItem()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "Item");
        }

        public static string LnkMnBlogItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "CreateItem");
        }

        public static string LnkMnBlogItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "RecycleItem");
        }

        public static string LnkMnBlogGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "GroupItem");
        }

        public static string LnkMnBlogGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "CreateGroupItem");
        }

        public static string LnkMnBlogGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "RecycleGroupItem");
        }

        public static string LnkMnBlogGroupCategory()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "GroupCategory");
        }

        public static string LnkMnBlogGroupCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "CreateGroupCategory");
        }

        public static string LnkMnBlogGroupCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "RecycleGroupCategory");
        }

        public static string LnkMnBlogProperty()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "Property");
        }

        public static string LnkMnBlogPropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "CreateProperty");
        }

        public static string LnkMnBlogPropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "RecycleProperty");
        }

        public static string LnkMnBlogConfig()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "Configuration");
        }

        public static string LnkMnBlogComment()
        {
            return LinkAdmin.GoAdminSubControl("Blog", "Comment");
        }
    }
}

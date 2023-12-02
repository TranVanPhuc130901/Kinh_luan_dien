using RevosJsc.AdminControl;

namespace RevosJsc.NewsControl
{
    public class Link
    {
        public static string LnkMnNews()
        {
            return LinkAdmin.GoAdminControl("News");
        }

        public static string LnkMnNewsCategory()
        {
            return LinkAdmin.GoAdminSubControl("News", "Category");
        }

        public static string LnkMnNewsCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("News", "CreateCategory");
        }

        public static string LnkMnNewsCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("News", "RecycleCategory");
        }

        public static string LnkMnNewsItem()
        {
            return LinkAdmin.GoAdminSubControl("News", "Item");
        }

        public static string LnkMnNewsItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("News", "CreateItem");
        }

        public static string LnkMnNewsItemRec()
        {
            return LinkAdmin.GoAdminSubControl("News", "RecycleItem");
        }

        public static string LnkMnNewsGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("News", "GroupItem");
        }

        public static string LnkMnNewsGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("News", "CreateGroupItem");
        }

        public static string LnkMnNewsGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("News", "RecycleGroupItem");
        }

        public static string LnkMnNewsProperty()
        {
            return LinkAdmin.GoAdminSubControl("News", "Property");
        }

        public static string LnkMnNewsPropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("News", "CreateProperty");
        }

        public static string LnkMnNewsPropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("News", "RecycleProperty");
        }

        public static string LnkMnNewsConfig()
        {
            return LinkAdmin.GoAdminSubControl("News", "Configuration");
        }

        public static string LnkMnNewsComment()
        {
            return LinkAdmin.GoAdminSubControl("News", "Comment");
        }
    }
}

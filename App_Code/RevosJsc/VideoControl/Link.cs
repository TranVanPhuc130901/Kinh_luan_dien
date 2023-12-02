using RevosJsc.AdminControl;

namespace RevosJsc.VideoControl
{
    public class Link
    {
        public static string LnkMnVideo()
        {
            return LinkAdmin.GoAdminControl("Video");
        }

        public static string LnkMnVideoCategory()
        {
            return LinkAdmin.GoAdminSubControl("Video", "Category");
        }

        public static string LnkMnVideoCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Video", "CreateCategory");
        }

        public static string LnkMnVideoCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Video", "RecycleCategory");
        }

        public static string LnkMnVideoItem()
        {
            return LinkAdmin.GoAdminSubControl("Video", "Item");
        }

        public static string LnkMnVideoItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Video", "CreateItem");
        }

        public static string LnkMnVideoItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Video", "RecycleItem");
        }

        public static string LnkMnVideoGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("Video", "GroupItem");
        }

        public static string LnkMnVideoGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Video", "CreateGroupItem");
        }

        public static string LnkMnVideoGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Video", "RecycleGroupItem");
        }

        public static string LnkMnVideoGroupCategory()
        {
            return LinkAdmin.GoAdminSubControl("Video", "GroupCategory");
        }

        public static string LnkMnVideoGroupCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Video", "CreateGroupCategory");
        }

        public static string LnkMnVideoGroupCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Video", "RecycleGroupCategory");
        }

        public static string LnkMnVideoProperty()
        {
            return LinkAdmin.GoAdminSubControl("Video", "Property");
        }

        public static string LnkMnVideoPropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("Video", "CreateProperty");
        }

        public static string LnkMnVideoPropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("Video", "RecycleProperty");
        }

        public static string LnkMnVideoConfig()
        {
            return LinkAdmin.GoAdminSubControl("Video", "Configuration");
        }

        public static string LnkMnVideoComment()
        {
            return LinkAdmin.GoAdminSubControl("Video", "comment");
        }
    }
}

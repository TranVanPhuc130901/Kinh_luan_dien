using RevosJsc.AdminControl;

namespace RevosJsc.PhotoAlbumControl
{
    public class Link
    {
        public static string LnkMnPhotoAlbum()
        {
            return LinkAdmin.GoAdminControl("PhotoAlbum");
        }

        public static string LnkMnPhotoAlbumCategory()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "Category");
        }

        public static string LnkMnPhotoAlbumCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "CreateCategory");
        }

        public static string LnkMnPhotoAlbumCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "RecycleCategory");
        }

        public static string LnkMnPhotoAlbumItem()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "Item");
        }

        public static string LnkMnPhotoAlbumItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "CreateItem");
        }

        public static string LnkMnPhotoAlbumItemRec()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "RecycleItem");
        }

        public static string LnkMnPhotoAlbumGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "GroupItem");
        }

        public static string LnkMnPhotoAlbumGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "CreateGroupItem");
        }

        public static string LnkMnPhotoAlbumGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "RecycleGroupItem");
        }

        public static string LnkMnPhotoAlbumGroupCategory()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "GroupCategory");
        }

        public static string LnkMnPhotoAlbumGroupCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "CreateGroupCategory");
        }

        public static string LnkMnPhotoAlbumGroupCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "RecycleGroupCategory");
        }

        public static string LnkMnPhotoAlbumProperty()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "Property");
        }

        public static string LnkMnPhotoAlbumPropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "CreateProperty");
        }

        public static string LnkMnPhotoAlbumPropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "RecycleProperty");
        }

        public static string LnkMnPhotoAlbumConfig()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "Configuration");
        }

        public static string LnkMnPhotoAlbumComment()
        {
            return LinkAdmin.GoAdminSubControl("PhotoAlbum", "Comment");
        }
    }
}

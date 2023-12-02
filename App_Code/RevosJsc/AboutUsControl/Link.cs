using RevosJsc.AdminControl;

namespace RevosJsc.AboutUsControl
{
    public class Link
    {
        public static string LnkMnAboutUs()
        {
            return LinkAdmin.GoAdminControl("AboutUs");
        }

        public static string LnkMnAboutUsCategory()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "Category");
        }

        public static string LnkMnAboutUsCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "CreateCategory");
        }

        public static string LnkMnAboutUsCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "RecycleCategory");
        }

        public static string LnkMnAboutUsItem()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "Item");
        }

        public static string LnkMnAboutUsItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "CreateItem");
        }

        public static string LnkMnAboutUsItemRec()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "RecycleItem");
        }

        public static string LnkMnAboutUsGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "GroupItem");
        }

        public static string LnkMnAboutUsGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "CreateGroupItem");
        }

        public static string LnkMnAboutUsGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "RecycleGroupItem");
        }

        public static string LnkMnAboutUsGroupCategory()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "GroupCategory");
        }

        public static string LnkMnAboutUsGroupCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "CreateGroupCategory");
        }

        public static string LnkMnAboutUsGroupCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "RecycleGroupCategory");
        }

        public static string LnkMnAboutUsProperty()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "Property");
        }

        public static string LnkMnAboutUsPropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "CreateProperty");
        }

        public static string LnkMnAboutUsPropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "RecycleProperty");
        }

        public static string LnkMnAboutUsConfig()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "Configuration");
        }

        public static string LnkMnAboutUsConfigHidden()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "ConfigurationHidden");
        }

        public static string LnkMnAboutUsComment()
        {
            return LinkAdmin.GoAdminSubControl("AboutUs", "Comment");
        }
    }
}

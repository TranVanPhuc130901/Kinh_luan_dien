using RevosJsc.AdminControl;

namespace RevosJsc.ProjectControl
{
    public class Link
    {
        public static string LnkMnProject()
        {
            return LinkAdmin.GoAdminControl("Project");
        }

        public static string LnkMnProjectCategory()
        {
            return LinkAdmin.GoAdminSubControl("Project", "Category");
        }

        public static string LnkMnProjectCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Project", "CreateCategory");
        }

        public static string LnkMnProjectCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Project", "RecycleCategory");
        }

        public static string LnkMnProjectItem()
        {
            return LinkAdmin.GoAdminSubControl("Project", "Item");
        }

        public static string LnkMnProjectItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Project", "CreateItem");
        }

        public static string LnkMnProjectItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Project", "RecycleItem");
        }

        public static string LnkMnProjectGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("Project", "GroupItem");
        }

        public static string LnkMnProjectGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Project", "CreateGroupItem");
        }

        public static string LnkMnProjectGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Project", "RecycleGroupItem");
        }

        public static string LnkMnProjectGroupCategory()
        {
            return LinkAdmin.GoAdminSubControl("Project", "GroupCategory");
        }

        public static string LnkMnProjectGroupCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Project", "CreateGroupCategory");
        }

        public static string LnkMnProjectGroupCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Project", "RecycleGroupCategory");
        }

        public static string LnkMnProjectProperty()
        {
            return LinkAdmin.GoAdminSubControl("Project", "Property");
        }

        public static string LnkMnProjectPropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("Project", "CreateProperty");
        }

        public static string LnkMnProjectPropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("Project", "RecycleProperty");
        }

        public static string LnkMnProjectConfig()
        {
            return LinkAdmin.GoAdminSubControl("Project", "Configuration");
        }

        public static string LnkMnProjectComment()
        {
            return LinkAdmin.GoAdminSubControl("Project", "Comment");
        }
    }
}

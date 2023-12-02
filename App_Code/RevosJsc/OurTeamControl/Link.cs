using RevosJsc.AdminControl;

namespace RevosJsc.OurTeamControl
{
    public class Link
    {
        public static string LnkMnOurTeam()
        {
            return LinkAdmin.GoAdminControl("OurTeam");
        }

        public static string LnkMnOurTeamCategory()
        {
            return LinkAdmin.GoAdminSubControl("OurTeam", "Category");
        }

        public static string LnkMnOurTeamCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("OurTeam", "CreateCategory");
        }

        public static string LnkMnOurTeamCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("OurTeam", "RecycleCategory");
        }

        public static string LnkMnOurTeamItem()
        {
            return LinkAdmin.GoAdminSubControl("OurTeam", "Item");
        }

        public static string LnkMnOurTeamItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("OurTeam", "CreateItem");
        }

        public static string LnkMnOurTeamItemRec()
        {
            return LinkAdmin.GoAdminSubControl("OurTeam", "RecycleItem");
        }

        public static string LnkMnOurTeamGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("OurTeam", "GroupItem");
        }

        public static string LnkMnOurTeamGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("OurTeam", "CreateGroupItem");
        }

        public static string LnkMnOurTeamGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("OurTeam", "RecycleGroupItem");
        }


        public static string LnkMnOurTeamConfig()
        {
            return LinkAdmin.GoAdminSubControl("OurTeam", "Configuration");
        }

    }
}

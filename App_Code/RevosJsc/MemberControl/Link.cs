using RevosJsc.AdminControl;

namespace RevosJsc.MemberControl
{
    public class Link
    {
        public static string LnkMnMember()
        {
            return LinkAdmin.GoAdminControl("Member");
        }

        public static string LnkMnMemberItem()
        {
            return LinkAdmin.GoAdminSubControl("Member", "Item");
        }

        public static string LnkMnMemberItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Member", "CreateItem");
        }

        public static string LnkMnMemberItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Member", "RecycleItem");
        }
        public static string LnkMnMemberNewsLetter()
        {
            return LinkAdmin.GoAdminSubControl("Member", "MemberNewsletter");
        }

        public static string LnkMnMemberConfig()
        {
            return LinkAdmin.GoAdminSubControl("Member", "Configuration");
        }
    }
}

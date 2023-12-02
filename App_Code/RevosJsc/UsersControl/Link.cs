using RevosJsc.AdminControl;

namespace RevosJsc.UsersControl
{
    public class Link
    {
        public static string LnkUsers()
        {
            return LinkAdmin.GoAdminControl("Users");
        }

        public static string LnkUsersCreate()
        {
            return LinkAdmin.GoAdminSubControl("Users", "Create");
        }
        public static string LnkUsersRecycle()
        {
            return LinkAdmin.GoAdminSubControl("Users", "Recycle");
        }
    }
}

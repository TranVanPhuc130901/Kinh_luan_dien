using System;
using RevosJsc.Columns;
using RevosJsc.Extension;
using RevosJsc.UsersControl;

public partial class Areas_Admin_Control_Users_LoadControl : System.Web.UI.UserControl
{
    private string _action = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        switch (_action)
        {
            case TypePage.Index:
                plLoadSubControl.Controls.Add(LoadControl("Control/Index.ascx"));
                break;

            case TypePage.Update:
            case TypePage.Create:
                plLoadSubControl.Controls.Add(LoadControl("Control/AddEditUser.ascx"));
                break;

            case TypePage.Recycle:
                plLoadSubControl.Controls.Add(LoadControl("Control/RecycleUser.ascx"));
                break;

            case TypePage.ChangePassword:
                plLoadSubControl.Controls.Add(LoadControl("Control/ChangePassword.ascx"));
                break;
            case TypePage.LogOut:
                CookieExtension.ClearCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
                CookieExtension.ClearCookies(SecurityExtension.BuildPassword(UsersColumns.VuRole));
                Response.Redirect("/admin");
                break;

            default:
                plLoadSubControl.Controls.Add(LoadControl("Control/Index.ascx"));
                //plLoadSubControl.Controls.Add(LoadControl("Index.ascx"));
                break;
        }
    }
}
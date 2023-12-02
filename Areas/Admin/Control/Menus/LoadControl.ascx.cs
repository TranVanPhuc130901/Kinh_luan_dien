using System;
using RevosJsc.MenusControl;

public partial class Areas_Admin_Control_Menu_LoadControl : System.Web.UI.UserControl
{
    private string _action = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        switch (_action)
        {
            case TypePage.Update:
            case TypePage.Create:
                plLoadSubControl.Controls.Add(LoadControl("AddEditMenu.ascx"));
                break;

            case TypePage.Recycle:
                plLoadSubControl.Controls.Add(LoadControl("RecycleMenu.ascx"));
                break;

            default:
                plLoadSubControl.Controls.Add(LoadControl("Index.ascx"));
                //plLoadSubControl.Controls.Add(LoadControl("Index.ascx"));
                break;
        }
    }
}
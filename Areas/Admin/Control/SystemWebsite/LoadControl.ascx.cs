using System;
using RevosJsc.SystemWebsiteControl;

public partial class Areas_Admin_Control_SystemWebsite_LoadControl : System.Web.UI.UserControl
{
    private string _action = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        switch (_action)
        {
            case TypePage.OptimizeSystem:
                plLoadSubControl.Controls.Add(LoadControl("Action/OptimizeSystem.ascx"));
                break;

            case TypePage.Email:
                plLoadSubControl.Controls.Add(LoadControl("Action/Email.ascx"));
                break;

            case TypePage.Logs:
                plLoadSubControl.Controls.Add(LoadControl("Action/Logs.ascx"));
                break;

            case TypePage.Sitemap:
                plLoadSubControl.Controls.Add(LoadControl("Action/Sitemap.ascx"));
                break;

            default:
                plLoadSubControl.Controls.Add(LoadControl("Action/Information.ascx"));
                //plLoadSubControl.Controls.Add(LoadControl("Index.ascx"));
                break;
        }
    }
}
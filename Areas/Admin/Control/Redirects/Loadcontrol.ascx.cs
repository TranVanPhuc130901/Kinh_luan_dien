using System;
using RevosJsc.RedirectsControl;

public partial class Areas_Admin_Control_Redirect_Loadcontrol : System.Web.UI.UserControl
{
    private string _action = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        switch (_action)
        {
            case TypePage.Recycle:
                plLoadSubControl.Controls.Add(LoadControl("Link/RecycleLink.ascx"));
                break;

            case TypePage.Update:
            case TypePage.Create:
                plLoadSubControl.Controls.Add(LoadControl("Link/AddEditLink.ascx"));
                break;

            case "ImportExcel"://Nhập tin từ tệp excel
                plLoadSubControl.Controls.Add(LoadControl("Tool/ImportExcel.ascx"));
                break;
            default:
                plLoadSubControl.Controls.Add(LoadControl("Link/Index.ascx"));
                //plLoadSubControl.Controls.Add(LoadControl("Index.ascx"));
                break;
        }
    }
}
using System;
using RevosJsc.MemberControl;

public partial class Areas_Admin_Control_Member_LoadControl : System.Web.UI.UserControl
{
    private string _action = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        switch (_action)
        {
            #region Item

            case TypePage.Item:
                plLoadSubControl.Controls.Add(LoadControl("Item/Index.ascx"));
                break;

            case TypePage.UpdateItem:
            case TypePage.CreateItem:
                plLoadSubControl.Controls.Add(LoadControl("Item/AddEditItem.ascx"));
                break;

            case TypePage.RecycleItem:
                plLoadSubControl.Controls.Add(LoadControl("Item/RecycleItem.ascx"));
                break;

            #endregion Item

            default:
                plLoadSubControl.Controls.Add(LoadControl("Item/Index.ascx"));
                //plLoadSubControl.Controls.Add(LoadControl("Index.ascx"));
                break;
        }
    }
}
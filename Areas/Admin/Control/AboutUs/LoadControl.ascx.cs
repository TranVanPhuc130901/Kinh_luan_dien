using System;
using RevosJsc.AboutUsControl;

public partial class Areas_Admin_Control_AboutUs_LoadControl : System.Web.UI.UserControl
{
    private string _action = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        switch (_action)
        {
            #region Category

            case TypePage.Category:
                plLoadSubControl.Controls.Add(LoadControl("Category/Index.ascx"));
                break;

            case TypePage.UpdateCategory:
            case TypePage.CreateCategory:
                plLoadSubControl.Controls.Add(LoadControl("Category/AddEditCategory.ascx"));
                break;

            case TypePage.RecycleCategory:
                plLoadSubControl.Controls.Add(LoadControl("Category/RecycleCategory.ascx"));
                break;

            #endregion Category

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

            #region Config

            case TypePage.Configuration:
                plLoadSubControl.Controls.Add(LoadControl("Config/Index.ascx"));
                break;

            #endregion Config

            #region GroupItem

            case TypePage.GroupItem:
                plLoadSubControl.Controls.Add(LoadControl("GroupItem/Index.ascx"));
                break;

            case TypePage.UpdateGroupItem:
            case TypePage.CreateGroupItem:
                plLoadSubControl.Controls.Add(LoadControl("GroupItem/AddEditGroupItem.ascx"));
                break;

            case TypePage.RecycleGroupItem:
                plLoadSubControl.Controls.Add(LoadControl("GroupItem/RecycleGroupItem.ascx"));
                break;

            #endregion GroupItem

            case "ImportGroup"://Nhập tin từ tệp excel
                plLoadSubControl.Controls.Add(LoadControl("Tool/ImportGroups.ascx"));
                break;

            case "ImportCategory"://Nhập tin từ tệp excel
                plLoadSubControl.Controls.Add(LoadControl("Tool/ImportCategory.ascx"));
                break;

            case "ImportItem"://Nhập tin từ tệp excel
                plLoadSubControl.Controls.Add(LoadControl("Tool/ImportItem.ascx"));
                break;

            default:
                plLoadSubControl.Controls.Add(LoadControl("Category/Index.ascx"));
                //plLoadSubControl.Controls.Add(LoadControl("Index.ascx"));
                break;
        }
    }
}
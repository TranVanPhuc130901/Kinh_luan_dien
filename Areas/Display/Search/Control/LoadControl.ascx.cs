using System;

public partial class Areas_Display_Search_Control_LoadControl : System.Web.UI.UserControl
{
    private string _page = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["page"] != null) _page = Request.QueryString["page"];
        Session["page"] = _page;
        switch (_page)
        {
            case "news":
                plLoadControl.Controls.Add(LoadControl("Article.ascx"));
                break;
            case "no-results":
                plLoadControl.Controls.Add(LoadControl("NoResults.ascx"));
                break;
            //case "search":
            //    plLoadControl.Controls.Add(LoadControl("Search.ascx"));
            //    break;
            default:
                plLoadControl.Controls.Add(LoadControl("Product.ascx"));
                break;
        }
    }
}
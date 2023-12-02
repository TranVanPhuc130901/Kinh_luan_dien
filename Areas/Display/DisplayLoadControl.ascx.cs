using System;
using Developer.Extension;
using RevosJsc.Extension;

public partial class Areas_Display_DisplayLoadControl : System.Web.UI.UserControl
{
    private string _rewrite = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["rewrite"] != null) _rewrite = QueryStringExtension.GetQueryString("rewrite");
        if (_rewrite.Length < 1 && Session["rewrite"] != null) _rewrite = Session["rewrite"].ToString();
        if (_rewrite == RewriteExtension.AboutUs) plLoadControl.Controls.Add(LoadControl("AboutUs/Control/LoadControl.ascx"));
        else if (_rewrite == RewriteExtension.Product) plLoadControl.Controls.Add(LoadControl("Product/Control/LoadControl.ascx"));
        else if (_rewrite == RewriteExtension.News) plLoadControl.Controls.Add(LoadControl("News/Control/LoadControl.ascx"));
        else if (_rewrite == RewriteExtension.Contact) plLoadControl.Controls.Add(LoadControl("Contact/Control/LoadControl.ascx"));
        else if (_rewrite == RewriteExtension.Search) plLoadControl.Controls.Add(LoadControl("Search/Control/LoadControl.ascx"));
        else if (_rewrite == RewriteExtension.Error) plLoadControl.Controls.Add(LoadControl("Error/Control/LoadControl.ascx"));
        else plLoadControl.Controls.Add(LoadControl("Home/Control/LoadControl.ascx"));
    }
}
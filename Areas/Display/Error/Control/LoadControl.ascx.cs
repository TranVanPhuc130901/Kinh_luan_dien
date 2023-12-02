using System;

public partial class Areas_Display_Error_Control_LoadControl : System.Web.UI.UserControl
{
    private string _igid = "";
    private string _iid = "";
    private string _page = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["page"] != null) _page = Request.QueryString["page"];

        #region title

        if (Request.QueryString["title"] != null)
        {
            if (_igid.Length < 1 && Session["igid"] != null) _igid = Session["igid"].ToString();
            if (_iid.Length < 1 && Session["iid"] != null) _iid = Session["iid"].ToString();
            if (_page.Length < 1)
            {
                if (_igid.Length > 0 && _iid.Length < 1) _page = "c";
                else _page = "d";
            }
        }

        #endregion title

        Session["page"] = _page;
        switch (_page)
        {
            case "404":
                plLoadControl.Controls.Add(LoadControl("Error404.ascx"));
                break;
            default:
                plLoadControl.Controls.Add(LoadControl("Error404.ascx"));
                break;
        }
    }
}
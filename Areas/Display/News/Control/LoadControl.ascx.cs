using System;

public partial class Areas_Display_News_Control_LoadControl : System.Web.UI.UserControl
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
            if (_iid.Length < 1 && Session["iiid"] != null) _iid = Session["iiid"].ToString();
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
            case "d":
                plLoadControl.Controls.Add(LoadControl("Detail.ascx"));
                break;
            case "c":
                plLoadControl.Controls.Add(LoadControl("Category.ascx"));
                break;
            default:
                plLoadControl.Controls.Add(LoadControl("Index.ascx"));
                break;
        }
    }
}
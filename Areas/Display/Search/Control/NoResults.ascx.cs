using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Areas_Display_Search_Control_NoResults : System.Web.UI.UserControl
{
    protected string Keyword = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["keyword"] != null) Keyword = Request.QueryString["keyword"];

    }
}
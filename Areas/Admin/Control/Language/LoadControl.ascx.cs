using System;
using RevosJsc.LanguageControl;

public partial class Areas_Admin_Control_Language_LoadControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var action = Request.QueryString["action"];
        switch (action)
        {
            case TypePage.AddEditNational:
                plLoadSubControl.Controls.Add(LoadControl("Control/AddEditLanguage.ascx"));
                break;

            case TypePage.Keyword:
                plLoadSubControl.Controls.Add(LoadControl("Keyword/Index.ascx"));
                break;
            case TypePage.AddEditKeyword:
                plLoadSubControl.Controls.Add(LoadControl("Keyword/AddEditKeyword.ascx"));
                break;

            case TypePage.Translate:
                plLoadSubControl.Controls.Add(LoadControl("Translate/Index.ascx"));
                break;

            default:
                plLoadSubControl.Controls.Add(LoadControl("Control/Index.ascx"));
                break;
        }
    }
}

using System;
using System.Web;
using System.Web.UI;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Extension;
using RevosJsc.LanguageControl;

public partial class Areas_Admin_Index : System.Web.UI.Page
{
    private readonly string _lang = Cookie.GetLanguageValueAdmin();
    private string _action = "";
    //protected override void Render(HtmlTextWriter writer)
    //{
    //    if (Request.Headers["X-MicrosoftAjax"] != "Delta=true")
    //    {
    //        var reg = new System.Text.RegularExpressions.Regex(@"<script[^>]*>[\w|\t|\r|\W]*?</script>");
    //        var sb = new System.Text.StringBuilder();
    //        var sw = new System.IO.StringWriter(sb);
    //        var hw = new HtmlTextWriter(sw);
    //        base.Render(hw);
    //        var html = sb.ToString();
    //        var mymatch = reg.Matches(html);
    //        html = reg.Replace(html, string.Empty);
    //        reg = new System.Text.RegularExpressions.Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}|(?=[\r])\s{2,}");
    //        html = reg.Replace(html, string.Empty);
    //        reg = new System.Text.RegularExpressions.Regex(@"</body>");
    //        var str = string.Empty;
    //        foreach (System.Text.RegularExpressions.Match match in mymatch)
    //        {
    //            str += match.ToString();
    //        }
    //        html = reg.Replace(html, str + "</body>");
    //        writer.Write(html);
    //    }
    //    else base.Render(writer);
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        //if (!IsPostBack) RewriteExtension.SetRewriteByLanguage(_lang);
        if (CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount)))
        {
            plLoadControl.Controls.Add(LoadControl("Control/AdminLoadControl.ascx"));
        }
        else
        {
            var urlCookie = new HttpCookie("RefererUrl") { Value = Request.RawUrl };
            if (_action != "LogOut") Response.Cookies.Add(urlCookie);
            Response.Redirect("/admin/login");
        }
    }
}
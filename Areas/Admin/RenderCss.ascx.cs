using System;
using System.IO;
using System.Text;
using System.Web.UI;

public partial class Areas_Admin_RenderCss : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var css = new StringBuilder();
        css.Append("<style>");
        string[] files = { Request.PhysicalApplicationPath + "/Areas/Admin/css/main.css", Request.PhysicalApplicationPath + "/Areas/Admin/css/themes.min.css" };
        foreach (var file in files)
        {
            var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                css.Append(streamReader.ReadToEnd());
            }
        }
        css.Append("</style>");
        ltrCss.Text = css.ToString();
        if (Request.Cookies["ThemeColor"] == null) return;
        var value = Request.Cookies["ThemeColor"].Value;
        ltrCss.Text += string.IsNullOrEmpty(value) ? "" : "<link id='theme-link' rel='stylesheet' href='" + value + "'>";
    }
}
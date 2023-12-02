using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Areas_Admin_RenderJs : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var js = new StringBuilder();
        js.Append("<script>");
        string[] files = { Request.PhysicalApplicationPath + "/Areas/Admin/js/app.min.js" };
        foreach (var file in files)
        {
            var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                js.Append(streamReader.ReadToEnd());
            }
        }
        js.Append("</script>");
        ltrJs.Text = js.ToString();
    }
}
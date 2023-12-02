using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Developer.Extension;
using RevosJsc.Extension;

public partial class RenderJs : System.Web.UI.UserControl
{
    private string _rewrite = "";
    private string _page = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["rewrite"] != null) _rewrite = QueryStringExtension.GetQueryString("rewrite");
        if (_rewrite.Length < 1 && Session["rewrite"] != null) _rewrite = Session["rewrite"].ToString();
        if (Request.QueryString["page"] != null) _page = QueryStringExtension.GetQueryString("page");
        if (_page.Length < 1 && Session["page"] != null) _page = Session["page"].ToString();
        var css = new StringBuilder();
        css.Append("<script>");

        //string[] files = { Request.PhysicalApplicationPath + "/js/Index.min.js" };
        //foreach (var file in files)
        //{
        //    var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        //    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
        //    {
        //        css.Append(streamReader.ReadToEnd());
        //    }
        //}

        var files2 = GetJsByRewrite();
        foreach (var file in files2)
        {
            if (file.Length <= 0) continue;
            var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                css.Append(streamReader.ReadToEnd());
            }
        }
        css.Append("</script>");
        ltrCss.Text = css.ToString();
    }
    private IEnumerable<string> GetJsByRewrite()
    {
        var url = Request.PhysicalApplicationPath;
        if (_rewrite.Equals(RewriteExtension.Product))
        {
            if (Session["dataByTitle"] != null) return new[] { url + "/js/jquery-3.6.0.min.js", url + "/js/fancybox/jquery.fancybox.min.js", url + "/js/owl-carousel/owl.carousel.min.js", url + "/js/Index.min.js", url + "/js/Product.min.js" };
            return new[] { url + "/js/jquery-3.6.0.min.js", url + "/js/Index.min.js", url + "/js/Product.min.js" };
        }
        if (_rewrite.Equals(RewriteExtension.Contact)) return new[] { url + "/js/jquery-3.6.0.min.js", url + "/js/fancybox/jquery.fancybox.min.js", url + "/js/Index.min.js" };
        if (_rewrite.Equals(RewriteExtension.News)) return new[] { url + "/js/jquery-3.6.0.min.js", url + "/js/owl-carousel/owl.carousel.min.js", url + "/js/Index.min.js", url + "/js/News.min.js" };
        if (_rewrite.Equals("")) return new[] { url + "/js/jquery-3.6.0.min.js", url + "/js/owl-carousel/owl.carousel.min.js", url + "/js/Index.min.js", url + "/js/Home.min.js" };
        return new[] { url + "/js/jquery-3.6.0.min.js", url + "/js/Index.min.js" };
    }
}
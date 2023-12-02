using Developer.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public partial class RenderCss : System.Web.UI.UserControl
{
    private string _rewrite = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //var watch = System.Diagnostics.Stopwatch.StartNew();
        if (Request.QueryString["rewrite"] != null) _rewrite = Request.QueryString["rewrite"];
        if (_rewrite.Length < 1 && Session["rewrite"] != null) _rewrite = Session["rewrite"].ToString();
        var css = new StringBuilder();
        css.Append("<style>");
        //string[] files = { Request.PhysicalApplicationPath + "/css/Style.min.css" };
        //foreach (var file in files)
        //{
        //    var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        //    if (fileStream.Length < 1) continue;
        //    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
        //    {
        //        css.Append(streamReader.ReadToEnd());
        //    }
        //}
        var files2 = GetCssByRewrite();

        foreach (var file in files2)
        {
            var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            if (fileStream.Length < 1) continue;
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                css.Append(streamReader.ReadToEnd());
            }
        }
        css.Append("</style>");
        ltrCss.Text = css.ToString();
        //watch.Stop();
        //Response.Write(watch.ElapsedMilliseconds);
    }
    private IEnumerable<string> GetCssByRewrite()
    {
        var url = Request.PhysicalApplicationPath;
        if (_rewrite.Equals(RewriteExtension.Product))
        {
            if (Session["dataByTitle"] != null) return new[] { url + "/js/fancybox/jquery.fancybox.min.css", url + "/js/owl-carousel/owl.carousel.min.css", Request.PhysicalApplicationPath + "/css/Style.min.css", url + "/css/Product.min.css" };
            return new[] { url + "/js/owl-carousel/owl.carousel.min.css", Request.PhysicalApplicationPath + "/css/Style.min.css", url + "/css/Product.min.css" };
        }
        if (_rewrite.Equals(RewriteExtension.AboutUs))
        {
            return new[] { Request.PhysicalApplicationPath + "/css/Style.min.css", url + "/css/AboutUs.min.css" };
        }
        if (_rewrite.Equals(RewriteExtension.News))
        {
            return new[] { url + "/js/owl-carousel/owl.carousel.min.css", Request.PhysicalApplicationPath + "/css/Style.min.css", url + "/css/News.min.css" };
        }

        if (_rewrite.Equals(RewriteExtension.Contact))
            return new[] { url + "/js/fancybox/jquery.fancybox.min.css", Request.PhysicalApplicationPath + "/css/Style.min.css", url + "/css/Contact.min.css" };

        if (_rewrite.Equals(""))
            return new[] { url + "/js/owl-carousel/owl.carousel.min.css", Request.PhysicalApplicationPath + "/css/Style.min.css", url + "/css/Home.min.css" };

        return new[] { Request.PhysicalApplicationPath + "/css/Style.min.css" };
    }
}
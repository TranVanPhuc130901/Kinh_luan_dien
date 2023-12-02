using System.Linq;
using System.Web;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using RevosJsc.Extension;

namespace Developer.Extension
{
    /// <summary>
    /// Summary description for ContentExtension
    /// </summary>
    public class ContentExtension
    {
        public static string CreateHashtag(string content)
        {
            //content = HttpUtility.HtmlDecode(content);
            var document = new HtmlDocument();
            document.LoadHtml(content);
            var fullMenu = "";
            //try
            //{
            //    var threadItems2 = document.DocumentNode.QuerySelectorAll("#gl-toc").ToList();
            //    foreach (var item in threadItems2)
            //    {
            //        item.Remove();
            //    }
            //}
            //catch
            //{
            //    // Do nothing
            //}
            try
            {
                var threadItems = document.DocumentNode.SelectNodes("//h2 | //h3").ToList();
                var menu = "";
                foreach (var item in threadItems)
                {
                    var id = StringExtension.ReplateTitle(item.InnerText).ToLower();
                    item.Attributes.Remove("id");
                    item.Attributes.Add("id", id);
                    if (item.Name.ToLower().Equals("h2")) menu += "<li><a href='#" + id + "' class='head2'>" + item.InnerText + "</a></li>";
                    else menu += "<li><a href='#" + id + "' class='head3'>" + item.InnerText + "</a></li>";
                }
                if (menu.Length <= 0) return fullMenu + document.DocumentNode.OuterHtml;
                fullMenu += "<div id='gl-toc'>";
                fullMenu += "<div class=\"title\" onclick=\"$(this).next().slideToggle(); $(this).find('.showhide').toggleClass('active');\">";
                fullMenu += "<span><i class='fas fa-list-ol'></i></span>";
                fullMenu += "<span class='showhide active'><i class='fas fa-caret-down'></i></span>";
                fullMenu += "</div>";
                fullMenu += "<div class='content'><ul>" + menu + "</ul></div>";
                fullMenu += "</div>";
            }
            catch
            {
                //
            }
            return fullMenu + document.DocumentNode.OuterHtml;
        }
    }
}
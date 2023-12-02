using System.Text.RegularExpressions;
using System.Web;

namespace RevosJsc.Extension
{
    /// <summary>
    /// Summary description for ContentExtendtions
    /// </summary>
    public class ContentExtendtions
    {
        public static string ProcessStringContent(string content, string oldContent,string folderPic)
        {
            var s = "";
            s = Regex.Replace(content, "(')+", "''");
            s = ImagesExtension.SaveContentImage(folderPic + "/images", s);
            s = ImagesExtension.SaveContentFromOtherWebsite(folderPic + "/images", s);
            //ImagesExtension.DeleteImageFromContent(folderPic, oldContent, s);                        
            return HttpUtility.HtmlDecode(s);
        }
    }
}
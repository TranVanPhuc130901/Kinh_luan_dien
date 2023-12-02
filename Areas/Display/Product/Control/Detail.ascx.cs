using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using Developer.Extension;
using Newtonsoft.Json;
using NPOI.HPSF;
using RevosJsc;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;

public partial class Areas_Display_Product_Control_Detail : System.Web.UI.UserControl
{
    protected readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _app = CodeApplications.Product;
    private readonly string _pic = FolderPic.Product;
    private readonly string _noResultText = LanguageExtension.TranslateKeyword("Updating ...");
    protected string IiId = "";
    private string _igid = "";
    protected string TitleItem = "";
    protected string Description = "";
    protected string Sku = "";
    protected string Images = "";
    protected string Price = "";
    protected string Brand = "";
    protected string LinkShare = "";
    protected string Hotline = "";
    protected string Status = "";
    protected string Guarantee = "";
    private readonly string _cProductViewed = SecurityExtension.BuildPassword("cProductViewed");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["iiid"] != null) IiId = QueryStringExtension.GetQueryString("iiid");
        if (Request.QueryString["igid"] != null) _igid = QueryStringExtension.GetQueryString("igid");

        #region title
        if (Request.QueryString["title"] != null)
        {
            //Lấy igid từ session ra vì nó đã dược lưu khi xét tại Default.aspx
            if (IiId.Length < 1 && Session["iiid"] != null) IiId = Session["iiid"].ToString();
            if (_igid.Length < 1 && Session["igid"] != null) _igid = Session["igid"].ToString();
        }
        #endregion title
        // Kiểm tra cookie xem có sản phẩm đã xem chưa
        if (CookieExtension.CheckValidCookies(_cProductViewed))
        {
            // Kiểm tra id sản phẩm đã có trong cookie chưa
            var list = CookieExtension.GetCookies(_cProductViewed);

            // Chỉ lưu 10 id để tránh tràn cookie
            var newList = "";
            for (var i = 0; i < list.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
            {
                if (i < 10) newList += "," + list.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i];
            }

            // Nếu đã có thì chuyển id lên đầu
            if (list.Contains("," + IiId))
            {
                CookieExtension.SaveCookies(_cProductViewed, "," + IiId + newList.Replace("," + IiId, ""));
            }
            // Nếu chưa có thì add thêm
            else
            {
                CookieExtension.SaveCookies(_cProductViewed, "," + IiId + newList);
            }
        }
        // Thêm mới cookie nếu chưa có
        else
        {
            CookieExtension.SaveCookies(_cProductViewed, "," + IiId);
        }
        Hotline = SettingsExtension.GetSettingKey(SettingsExtension.KeyHotLine, _lang);
        GetDataBySession();
        ltrWholesale.Text = GetWholesaleProduct();
    }
    private string GetWholesaleProduct()
    {
        var s = new StringBuilder();
        var dt = (DataTable)Session["dataByTitle"];

        var paramWholesale = dt.Rows[0][ItemsColumns.ViParam].ToString();
        var wholesaleInfo = new List<Dictionary<string, string>>();
        string[] arrPriceWholesale;
        string[] arrQuantityWholesale;
        if (paramWholesale.Length > 0)
        {
            wholesaleInfo = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(paramWholesale);
        }

        if (wholesaleInfo.Count == 0)
        {
            // Nếu biến wholesaleInfo rỗng, thì khởi tạo các mảng arrPriceWholesale và arrQuantityWholesale với kích thước bằng 0
            arrPriceWholesale = new string[0];
            arrQuantityWholesale = new string[0];
        }
        else
        {
            // Khởi tạo các mảng arrPriceWholesale và arrQuantityWholesale với kích thước bằng wholesaleInfo.Count - 1
            arrPriceWholesale = new string[wholesaleInfo.Count];
            arrQuantityWholesale = new string[wholesaleInfo.Count];

            // Lặp qua danh sách wholesaleInfo để gán giá trị cho các mảng arrPriceWholesale và arrQuantityWholesale
            for (var w = 0; w < wholesaleInfo.Count; w++)
            {
                arrPriceWholesale[w] = wholesaleInfo[w]["value"];
                arrQuantityWholesale[w] = wholesaleInfo[w]["label"];
            }
        }
        

        if (arrQuantityWholesale.Length <= 0 || string.IsNullOrEmpty(arrQuantityWholesale[0])) return s.ToString();
        s.Append("<div class='box_wholesale'>");
        s.Append("<div class='box_title-wholesale'> Mua Giá Bán Buôn / Giá Sỉ: </div>");
        for (var i = 0; i < arrQuantityWholesale.Length; i++)
        {
            s.Append("<div class='box_description-wholesale'> Mua " + arrQuantityWholesale[i] +
                     " sản phẩm chỉ với <strong> " +
                     NumberExtension.FormatNumber(arrPriceWholesale[i], true, "Liên hệ", "đ") +
                     "</strong>/1 sản phẩm,</div>");
        }
        if(arrQuantityWholesale.Length > 2)
        {
            s.Append("<div class='btn-moreWholesale'>Xem Thêm</div>");
            s.Append("<div class='tb_wholesale'>");
            s.Append("<div class='box_table'>");
            s.Append("<table class='table'>");
            s.Append("<thead class='head-tb_wholesale'><tr><th class=col1'>Số lượng</th><th class='col2'>Đơn giá</th></tr></thead><tbody class='body-tb_wholesale'>");
            for (var i = 0; i < arrQuantityWholesale.Length; i++)
            {
                s.Append("<tr><td class='row'><div class=col1'>" + arrQuantityWholesale[i] +
                         "</td><td class='col2'> " +
                         NumberExtension.FormatNumber(arrPriceWholesale[i], true, "Liên hệ", "đ") +
                         "</td></tr>");
            }
            s.Append("</tbody>");
                
            s.Append("</table>");
            s.Append("<div class='close-box_wholesale'>Thoát</div>");
            s.Append("</div>");
            s.Append("</div>");
               
        }

        s.Append("</div>");
        return s.ToString();
    }
    private void GetDataBySession()
    {
        var dt = (DataTable)Session["dataByTitle"];
        var dtG = (DataTable)Session["dataByTitle_Category"];
        UpdateTotalView(dt.Rows[0][ItemsColumns.IiId].ToString());
        var titleItem = dt.Rows[0][ItemsColumns.ViTitle].ToString();
        var price = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();
        var content = dt.Rows[0][ItemsColumns.ViContent].ToString();
        

        ltrTop.Text = "<h1>" + titleItem + "</h1><input type='hidden' id='id_pro' value='" + IiId + @"'/>";
        ltrTop.Text += "<div class='reviews_stars'><img src='/css/icon/star.svg' /><img src='/css/icon/star.svg' /><img src='/css/icon/star.svg' /><img src='/css/icon/star.svg' /><img src='/css/icon/star_half.svg' /></div>";
        ltrGallery.Text = GetSubImage();
        ltrContent.Text = content;
        ltrInfo.Text = @"
<div class='price'>
    <span>" + NumberExtension.FormatNumber(price, true, "Liên hệ", "đ") + @"</span>
</div>
";
        GetDataVariantByMasterId();
    }
    private void GetDataVariantByMasterId()
    {
        var condition = DataExtension.AndConditon(
            ItemsTSql.GetByMasterId(IiId),
            ItemsTSql.GetByStatus("1")
        );
        var dt = Items.GetData("", "*", condition, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script>");
        sb.Append("var dataVariant = [");

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var metaTitle = dt.Rows[i][ItemsColumns.ViMetaTitle].ToString();
            var metaTitleReplace = HttpUtility.UrlEncode(metaTitle);
            var metaDescription = dt.Rows[i][ItemsColumns.ViMetaDescription].ToString();
            var viImage = dt.Rows[i][ItemsColumns.ViImage].ToString();
            var titleItem = dt.Rows[i][ItemsColumns.ViTitle].ToString();
            var iiid = dt.Rows[i][ItemsColumns.IiId].ToString();
            var inventory = dt.Rows[i][ItemsColumns.Inventory].ToString();
            var mi = dt.Rows[i][ItemsColumns.MasterId].ToString();
            var imageSrc = ImagesExtension.GetImage(_pic, viImage, titleItem, "lazyload",
                true, false, "", false, false, "");
            sb.Append("{");
            sb.Append("\"id\":\"" + EscapeJsonString(iiid) + "\",");
            sb.Append("\"color\":\"" + EscapeJsonString(metaTitle) + "\",");
            sb.Append("\"colorReplace\":\"" + EscapeJsonString(metaTitleReplace) + "\",");
            sb.Append("\"size\":\"" + EscapeJsonString(metaDescription) + "\","); 
            sb.Append("\"image\":\"" + EscapeJsonString(imageSrc) + "\",");
            sb.Append("\"inventory\":\"" + EscapeJsonString(inventory) + "\",");
            sb.Append("\"mi\":\"" + EscapeJsonString(mi) + "\"");
            sb.Append("}");

            if (i < dt.Rows.Count - 1)
            {
                sb.Append(",");
            }
        }

        sb.Append("]");
        sb.Append("</script>");

        ltrData.Text = sb.ToString();
    }
    private string EscapeJsonString(string input)
    {
        // Thực hiện escape các ký tự đặc biệt trong chuỗi JSON
        // Ví dụ: nếu chuỗi có ký tự " sẽ được escape thành \"
        return input.Replace("\"", "\\\"");
    }
    private string GetColor(string list, string grName)
    {
        if (list.Equals("")) return "";
        var s = new StringBuilder();
        s.Append("<div class='check_size'>");
        s.Append(grName + ": ");
        for (var i = 0; i < list.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
        {
            var item = list.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i];
            s.Append("<input" + (i == 0 ? " checked='checked'" : "") + " name='color' id='check2" + i + "' type='radio' value='" + i + "'></input><label for='check2" + i + "'>" + item.Trim() + "</label>");
        }
        s.Append("</div>");
        return s.ToString();
    }
    private string GetSize(string list, string grName)
    {
        if (list.Equals("")) return "";
        var s = new StringBuilder();
        s.Append("<div class='check_size'>");
        s.Append(grName + ": ");
        for (var i = 0; i < list.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
        {
            var item = list.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i];
            s.Append("<input" + (i == 0 ? " checked='checked'" : "") + " name='size' id='check" + i + "' type='radio' value='" + i + "'></input><label for='check" + i + "'>" + item.Trim() + "</label>");
        }
        s.Append("</div>");
        return s.ToString();
    }
    private string GetSubImage()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            SubItemsTSql.GetByApp(CodeApplications.ProductImagesOther),
            SubItemsTSql.GetByStatus("1"),
            SubItemsTSql.GetByIiid(IiId)
        );
        var dt = Subitems.GetData("", "*", condition, SubitemsColumns.IsiSortOrder);
        if (dt.Rows.Count <= 0) return s.ToString();
        s.Append("<div class='gothiar_shop_gallery owl-carousel'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][SubitemsColumns.VsiTitle].ToString();
            var image = dt.Rows[i][SubitemsColumns.VsiImage].ToString();

            //var newImage = image;
            //if (newImage.Length > 0) newImage += ".ashx?w=498&quantity=80";
            //else newImage = "no_image.svg";

            var linkImage = UrlExtension.WebsiteUrl + _pic + "/" + image;
            s.Append(@"
        <a class='wImage'>
            " + ImagesExtension.GetImage(_pic, image, titleItem, "owl-lazy", true, false, "", false, false, "") + @"
        </a>");
        }

        s.Append("</div>");

        return s.ToString();
    }
    private void UpdateTotalView(string id)
    {
        Items.UpdateValues("iiTotalView = iiTotalView + 1", ItemsTSql.GetById(id));
    }
}
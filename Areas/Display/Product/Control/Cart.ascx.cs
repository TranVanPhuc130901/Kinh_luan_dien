using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Developer.Extension;
using Newtonsoft.Json;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;

public partial class Areas_Display_Product_Control_Cart : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _app = CodeApplications.Product;
    private readonly string _pic = FolderPic.Product;
    private readonly string _cookieCartName = SecurityExtension.BuildPassword("cGothiarCart08");

    protected void Page_Load(object sender, EventArgs e)
    {
        //ltrPaymentMethod.Text = GetPaymentMethod();
        ltrList.Text = GetList();
        //Response.Write(CookieExtension.GetCookies(SecurityExtension.BuildPassword("cProductCart")));
    }

    private string GetList()
    {

        if (!CookieExtension.CheckValidCookies(_cookieCartName)) Response.Redirect("/gio-hang-trong.htm");
        var s = new StringBuilder();
        var list = CookieExtension.GetCookies(_cookieCartName);
        //Response.Write(list);
        //return "";
        if (list.Equals("")) Response.Redirect("/gio-hang-trong.htm");
        var totalPrice = 0;
        var plus = "";
        s.Append("<ul class='listorder'>");
        for (var i = 0; i < list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
        {
            var values =
                HttpUtility.ParseQueryString(list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i]);
            var condition = DataExtension.AndConditon(DataExtension.OrConditon(ItemsTSql.GetByApp(_app),
                    ItemsTSql.GetByApp("OptionUpgrade"),
                    ItemsTSql.GetByApp("Variant")),
                    ItemsTSql.GetByLang(_lang),
                    ItemsTSql.GetByStatus("1"),
                    ItemsTSql.GetById(values["mi"])
            );
            var dt = Items.GetData("", "*", condition, "");
            if (dt.Rows.Count < 1) continue;
            var titleItem = dt.Rows[0][ItemsColumns.ViTitle].ToString();
            var image = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViImage].ToString(), "", 1);
            var price = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();
            var link =
                (UrlExtension.WebsiteUrl + dt.Rows[0][ItemsColumns.ViLink] + RewriteExtension.Extensions).ToLower();
            var app = dt.Rows[0][ItemsColumns.ViApp].ToString();
            var color = dt.Rows[0][ItemsColumns.ViMetaTitle].ToString();
            var size = dt.Rows[0][ItemsColumns.ViMetaDescription].ToString();

            // lấy số lượng nhận ưu đãi và giá ưu đãi

            var isPriceWholesale = false;

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

            if (arrQuantityWholesale.Length > 0 && !string.IsNullOrEmpty(arrQuantityWholesale[0]))
            {

                for (var q = arrQuantityWholesale.Length - 1; q >= 0; q--)
                {
                    if (int.Parse(values["q"]) < int.Parse(arrQuantityWholesale[q])) continue;
                    isPriceWholesale = true;
                    totalPrice += int.Parse(arrPriceWholesale[q]) * int.Parse(values["q"]);
                    break;
                }

            }

            if (!isPriceWholesale)
            {
                totalPrice += int.Parse(price) * int.Parse(values["q"]);
            }

            var masterId = dt.Rows[0][ItemsColumns.IiId].ToString();
            var conditionMasterId =
                DataExtension.AndConditon(ItemsTSql.GetByMasterId(masterId), ItemsTSql.GetByStatus("1"));
            var dtMaster = Items.GetData("", "*", conditionMasterId, "");
            if (dtMaster.Rows.Count <= 0) continue;
            for (var j = 0; j < dtMaster.Rows.Count; j++)
            {
                var idV = dtMaster.Rows[j][ItemsColumns.IiId].ToString();
                // Kiểm tra id sản phẩm giỏ hàng có bằng id trong cookie không
                if (idV != values["i"]) continue;
                // Lấy ra ảnh sản phẩm với id lưu trong cookie
                var imageV = dtMaster.Rows[j][ItemsColumns.ViImage].ToString();
                var colorV = dtMaster.Rows[j][ItemsColumns.ViMetaTitle].ToString();
                var sizeV = dtMaster.Rows[j][ItemsColumns.ViMetaDescription].ToString();
                var inventoryV = dtMaster.Rows[j][ItemsColumns.Inventory].ToString();
                if (price == "0") plus = "++";
                s.Append("<li>");
                s.Append("<div class='colimg'>");
                if (imageV.Length < 0)
                {
                    s.Append(@"
                                 <span>
                                " + ImagesExtension.GetImage(_pic, image, titleItem, "lazyload", true, false, "", false,
                        false, "") + @"
                                 </span>");
                }
                else
                {
                    s.Append(
                        "" + ImagesExtension.GetImage(_pic, imageV, titleItem, "", true, false, "", false) + "");
                }

                s.Append("</div>");
                s.Append("<div class='colinfo'>");
                s.Append("<div class='colinfo-left'>");
                var isWholesale = false;
                var arrPrice = "";
                if (arrQuantityWholesale.Length > 0 && !string.IsNullOrEmpty(arrQuantityWholesale[0]))
                {
                    for (int x = arrQuantityWholesale.Length - 1; x >= 0; x--)
                    {
                        if (int.Parse(values["q"]) >= int.Parse(arrQuantityWholesale[x]))
                        {
                            arrPrice = arrPriceWholesale[x];
                            isWholesale = true;
                            break;
                        }
                    }
                }

                if (isWholesale)
                {
                    s.Append(@"
                    <div class='info' data-idCart='" + values["nb"] + @"'>
                        <a href='" + link + "' title='" + titleItem + @"'>" + titleItem + @"</a>
                        <del>" + NumberExtension.FormatNumber(price, true, "Liên hệ", "đ") + @"</del>
                        <strong>" + NumberExtension.FormatNumber(arrPrice, true, "Liên hệ", "đ") + @"</strong>
                    </div>
                    ");
                }
                else
                {
                    s.Append(@"
                    <div class='info' data-idCart='" + values["nb"] + @"'>
                        <a href='" + link + "' title='" + titleItem + @"'>" + titleItem + @"</a>
                        <strong>" + NumberExtension.FormatNumber(price, true, "Liên hệ", "đ") + @"</strong>
                    </div>
                    ");
                }

                s.Append("<div class='pull-left'>");
               s.Append(GetColorAndSize(masterId, colorV, sizeV));
                s.Append("</div>");
                s.Append("<div class='choosenumber' inv='" + inventoryV + "'>");
                s.Append("<div class='abate" + (int.Parse(values["q"]) > 1 ? " active" : "") + @"'></div>");
                s.Append("<input class=\"hdQuantity number\" type=\"number\" value=\"" + values["q"] +
                         "\" min=\"1\" onchange=\"UpdateCart('" + masterId +
                         "', this.value, $(this).closest('.colinfo').find('#select-size option:selected').attr('i'))\">");
                s.Append("<div class='augment'></div>");
                s.Append("</div>");
                s.Append("</div>");

                //var option1 = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.Variant].ToString(), "", 1);
                //var option2 = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.Variant].ToString(), "", 3);
                //var optionLabel1 = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.Variant].ToString(), "", 4);
                //var optionLabel2 = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.Variant].ToString(), "", 5);
                s.Append("<div class='clearfix'>");
                s.Append("<button type=\"button\" class=\"delete\" onclick=\"RemoveItemInCart(\'" + masterId +
                         "\', $(this).closest('.colinfo').find('#select-size option:selected').attr('i'));\"><span></span>Xóa</button>");
                s.Append("</div>");
                s.Append("</div>");
                s.Append("</li>");
            }

            s.Append("</ul>");
            s.Append(@"
                    <div class='area_total'>
                        <div class='clearfix'>
                            <span>Tổng tiền:</span>
                            <span>" +
                     NumberExtension.FormatNumber(totalPrice.ToString(), true, "Liên hệ", plus + " đ") + @"</span>
                        </div>
                        <div class='shipping_store cleafix'>
                            <div class='total'>
                                <b>Cần thanh toán:</b>
                                <strong>" + NumberExtension.FormatNumber(totalPrice.ToString(), true, "Liên hệ",
                         plus + " đ") + @"</strong>
                            </div>
                        </div>
                    </div>
                    ");

        }
        return s.ToString();
    }




    private string GetColorAndSize(string masterId, string colorActive, string sizeActive)
    {

        var sc = new StringBuilder();
        var conditionSize = DataExtension.AndConditon(
               ItemsTSql.GetByMasterId(masterId),
               ItemsTSql.GetByStatus("1")
           );
        var dtSize = Items.GetData("", "*", conditionSize, "");
        if (dtSize.Rows.Count < 0) return sc.ToString();
        // check điều kiến lấy ra size
                sc.Append("<div>");
                sc.Append("Size: <select id='select-size' onchange=\"UpdateCart('" + masterId + "',  $(this).closest('.colinfo').find('.hdQuantity').val(), $('#select-size option:selected').attr('i'))\">");

                HashSet<string> uniqueSize = new HashSet<string>();

                for (int z = 0; z < dtSize.Rows.Count; z++)
                {
                   var value = dtSize.Rows[z]["viMetaDescription"].ToString();
                    var valueColor = dtSize.Rows[z][ItemsColumns.ViMetaTitle].ToString();
                    var idV = dtSize.Rows[z][ItemsColumns.IiId].ToString();
                        //if (value == sizeActive && valueColor == colorActive)
                        //{
                        //     idV = 
                        //} else
                        //{
                        //    idV = "";
                        //}
                   
                    //var srcImage = StringExtension.LayChuoi(dt.Rows[z][ItemsColumns.ViImage].ToString(), "", 1);
                    bool isSelected = (value == sizeActive);

                    // Kiểm tra giá trị đã tồn tại trong tập hợp chưa
                    if (!uniqueSize.Contains(value))
                    {
                        uniqueSize.Add(value);
                        sc.Append($"<option i='{idV}' value='{value}'{(isSelected ? " selected" : "")}>{value}</option>");
                        //s.Append($"<option value='{srcImage}'{(isSelected ? " selected" : "")}>{srcImage}</option>");
                    }
                }

                sc.Append("</select>");
                sc.Append("</div>");
   
     

        // check điều kiến lấy ra color
   
                sc.Append("<div>");
                sc.Append("Color: <select id='select-color' onchange=\"UpdateCart('" + masterId + "', $(this).closest('.colinfo').find('.hdQuantity').val(), $('#select-color option:selected').attr('i'))\">");

                HashSet<string> uniqueColor = new HashSet<string>();

                for (int z = 0; z < dtSize.Rows.Count; z++)
                {
                    var id = dtSize.Rows[z][ItemsColumns.IiId].ToString();
                    string value = dtSize.Rows[z]["viMetaTitle"].ToString();

                    //string valueReplace = HttpUtility.UrlEncode(value);
                    bool isSelected = (value == colorActive);

                    // Kiểm tra giá trị đã tồn tại trong tập hợp chưa
                    if (!uniqueColor.Contains(value))
                    {
                        uniqueColor.Add(value);

                        sc.Append($"<option i='{id}' value='{value}'{(isSelected ? " selected" : "")}'>{value}</option>");

                    }
                }

                sc.Append("</select>");
                sc.Append("</div>");


                return sc.ToString();

    }

    private string GetPaymentMethod()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(_app),
            GroupsTSql.GetByParentId("0"),
            GroupsTSql.GetByStatus("1"),
            GroupsTSql.GetByLang(_lang)
            );
        var dt = Groups.GetData("", "*", condition, GroupsColumns.IgSortOrder);
        if (dt.Rows.Count <= 0) return s.ToString();
        s.Append("<div class='pttt'>");
        s.Append("<div class='tt_form'>Phương thức thanh toán</div>");
        s.Append("<div class='note_form'>Vui lòng chọn phương thức thanh toán <span>*</span></div>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][GroupsColumns.IgId].ToString();
            s.Append(@"
<div class='chon_pttt'>
    <input id='pttt"+ id + "' value='" + dt.Rows[i][GroupsColumns.VgName] + @"' type='radio' name='PaymentMethod' " + (i == 0 ? "checked='checked'" : "") + @"/>
    <label for='pttt" + id + "'>" + dt.Rows[i][GroupsColumns.VgName] + @"</label>
</div>
<div class='cart_tt'>" + dt.Rows[i][GroupsColumns.VgContent] + @"</div>");
        }
        s.Append("</div>");
        return s.ToString();
    }

    /// <summary>
    /// Lấy ảnh của biến thể màu
    /// </summary>
    /// <param name="id">Id của màu trong select</param>
    /// <returns>Đường dẫn của hình ảnh</returns>
    private string getImageProductCart(string id)
    {
        var conditionSize = DataExtension.AndConditon(
            ItemsTSql.GetById(id),
            ItemsTSql.GetByStatus("1")
        );
        var dtColor = Items.GetData("", "ViImage, ViTitle", conditionSize, "");
        var title = dtColor.Rows[0][ItemsColumns.ViTitle].ToString();
        //var image = StringExtension.LayChuoi(dtColor.Rows[0][ItemsColumns.ViImage].ToString(), "", 1);
        var image = dtColor.Rows[0][ItemsColumns.ViImage].ToString();
        var imageSrc = _pic + "/" + image;
        return imageSrc;
    }
}
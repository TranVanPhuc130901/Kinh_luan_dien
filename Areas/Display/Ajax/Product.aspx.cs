using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Developer.Extension;
using Newtonsoft.Json;
using RevosJsc;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;

public partial class Areas_Display_Ajax_Product : System.Web.UI.Page
{
    private string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private string action = "";
    private readonly string _app = CodeApplications.Product;
    private readonly string _pic = FolderPic.Product;
    private readonly JavaScriptSerializer _js = new JavaScriptSerializer();
    private readonly string _cookieCartName = SecurityExtension.BuildPassword("cGothiarCart08");
    private string _device = "on PC";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) action = Request.QueryString["action"];
        if (Request.Form["action"] != null) action = Request.Form["action"];
        if (action == "") return;
        if (MobileExtension.IsMobileView()) _device = "on mobile";
        switch (action)
        {
            case "AddToCart":
                AddToCart();
                break;
            case "BuyNow":
                BuyNow();
                break;
            case "Order":
                Order();
                break;
            case "UpdateQuantity":
                UpdateQuantity();
                break;
            case "CountItemsInCart":
                CountItemsInCart();
                break;
            case "GetListCart":
                GetListCart();
                break;
            case "SubmitCart":
                SubmitCart();
                break;
            case "RemoveItemInCart":
                RemoveItemInCart();
                break;
            case "UpdateCart":
                UpdateCart();
                break;
            case "FilterProduct":
                FilterProduct();
                break;
            case "GetImageProductById":
                GetImageProductById();
                break;
            case "GetInventoryProduct":
                GetInventoryProduct();
                break;
        }
    }

    private void GetInventoryProduct()
    {
        var id = Request.Form["id"] ?? "";

        var condition = DataExtension.AndConditon(ItemsTSql.GetByMasterId(id), ItemsTSql.GetByStatus("1"),
            DataExtension.OrConditon(ItemsTSql.GetByApp(_app), ItemsTSql.GetByApp("OptionUpgrade"),
                ItemsTSql.GetByApp("Variant")));

        var dt = RevosJsc.Database.Items.GetData("", "*", condition, "");
        
        if(dt.Rows.Count < 0) return;
        List<string> result = new List<string>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var mau = dt.Rows[i][ItemsColumns.ViMetaTitle].ToString();
            var kichco = dt.Rows[i][ItemsColumns.ViMetaDescription].ToString();
            var tonkho = dt.Rows[i][ItemsColumns.Inventory].ToString();

            var itemData = $"{mau}, {kichco}, {tonkho} ";
            result.Add(itemData);
        }
        string[] reply = result.ToArray() ;
        Response.Output.Write(_js.Serialize(reply));
    }

    private void BuyNow()
    {
        var s = "Success";

        #region Lấy thông tin

        var mi = Request.Form["mi"] ?? "";
        var num = Request.Form["num"] ?? "";
        //var size = Request.Form["size"] ?? "";
        //var color = StringExtension.ReplateTitle(Request.Form["color"] ?? "");
        //var color = HttpUtility.UrlEncode(Request.Form["color"] ?? "");
        //var image = Request.Form["image"] ?? "";
        //var nb = Request.Form["randomNumber"] ?? "";
        var id = Request.Form["id"] ?? "";
        var inventory = Request.Form["inventory"] ?? "";
        #endregion Lấy thông tin

        #region Lấy thông tin sản phẩm

        var condition = DataExtension.AndConditon(DataExtension.OrConditon(ItemsTSql.GetByApp(_app), ItemsTSql.GetByApp("OptionUpgrade"), ItemsTSql.GetByApp("Variant")), ItemsTSql.GetByLang(_lang), ItemsTSql.GetByStatus("1"), ItemsTSql.GetById(mi));
        var dt = RevosJsc.Database.Items.GetData("1", "*", condition, "");
        if (dt.Rows.Count > 0)
        {
            #region Kiểm tra xem sản phẩm này đã có trong giỏ hàng hay chưa

            if (CookieExtension.CheckValidCookies(_cookieCartName))
            {
                // Biến kiểm tra xem sản phẩm có màu mới hay cũ
                var isNewProduct = true;
                // Kiểm tra sản phẩm có trong giỏ chưa
                var pIndex = -1;
                var list = CookieExtension.GetCookies(_cookieCartName);
                for (var i = 0; i < list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                {
                    var values = HttpUtility.ParseQueryString(list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i]);
                    if (values["i"] == id)
                    {
                        // Nếu đã có màu thì update lại sp
                        var replace = list.Replace("&i=" + values["i"] + "&q=" + values["q"] + "&mi=" + values["mi"] +"&inv=" + values["inv"], "&i=" + values["i"] + "&q=" + num +  "&mi=" + mi);
                        CookieExtension.SaveCookies(_cookieCartName, replace);

                        isNewProduct = false;
                        break;

                    }

                }
                if (isNewProduct)
                {
                    // Sản phẩm mới, thêm vào giỏ hàng
                    CookieExtension.SaveCookies(_cookieCartName, "&i=" + id + "&q=" + num + "&mi=" + mi  + "&inv=" + inventory + "," + list);
                }

            }
            // Nếu chưa có cookie thì thêm mới cookie và lưu giá trị sản phẩm đầu tiên
            else
            {
                CookieExtension.SaveCookies(_cookieCartName, "&i=" + id + "&q=" + num + "&mi=" + mi + "&inv=" + inventory);
                // ReSharper disable once PossibleNullReferenceException
                Response.Cookies[_cookieCartName].Expires = DateTime.Now.AddDays(30);
            }

            #endregion
        }
        else s = "Error";
        #endregion

        string[] reply = { s };
        Response.Output.Write(_js.Serialize(reply));
    }

    private void GetImageProductById()
    {
        var s = new StringBuilder();
        var iiid = Request.Form["id"] ?? "";
        var condition = DataExtension.AndConditon(
            SubItemsTSql.GetByApp(CodeApplications.ProductImagesOther),
            SubItemsTSql.GetByStatus("1"),
            SubItemsTSql.GetByIiid(iiid)
        );
        var dt = Subitems.GetData("5", "*", condition, SubitemsColumns.IsiSortOrder);
        if (dt.Rows.Count > 0)
        {
            s.Append("<div class='gothiar_shop_gallery1'>");
            for ( var i = 0; i < dt.Rows.Count; i++)
            {
                var titleImage = dt.Rows[i][SubitemsColumns.VsiTitle].ToString();
                var image = dt.Rows[i][SubitemsColumns.VsiImage].ToString();

                s.Append("<div class='wImage'>" + ImagesExtension.GetImage(_pic, image, titleImage, "owl-lazy", true, false, "", false, false, "") + @"</div>");
            }

            s.Append("</div>");
        } else
        {
            var dtP = RevosJsc.Database.Items.GetData("", "" + ItemsColumns.ViImage + ", " + ItemsColumns.ViTitle + "", DataExtension.AndConditon(ItemsTSql.GetByStatus("1"), ItemsTSql.GetById(iiid)), "");
            if(dtP.Rows.Count > 0)
            {
                var img = dtP.Rows[0][ItemsColumns.ViImage].ToString();
                var title = dtP.Rows[0][ItemsColumns.ViTitle].ToString();

                s.Append("<div class='gothiar_shop_gallery1'>");
                s.Append("<div class='wImage'>" + ImagesExtension.GetImage(_pic, img, title, "owl-lazy", true, false, "", false, false, "") + @"</div>");
                s.Append("</div>");
            }
        }

        string[] reply = { s.ToString() };
        Context.Response.Write(_js.Serialize(reply));
    }
    private void FilterProduct()
    {
        var gId = Request.Form["gId"] ?? "";
        var filter = Request.Form["filter"] ?? "";
        var rawUrl = Request.Form["rawUrl"] ?? "";
        var s = new StringBuilder();
        var maxItem = SettingKey.SoProductTrenTrangDanhMuc;
        var top = SettingsExtension.GetSettingKey(maxItem, _lang);
        if (top.Equals("")) top = "16";
        var condition = DataExtension.AndConditon(
            ItemsTSql.GetByApp(_app),
            ItemsTSql.GetByStatus("1"),
            filter.Length > 0 ? ItemsTSql.GetFilterAndCondition(filter.Trim(',')) : ""
            );
        condition = GroupItemsTSql.GetItemsInGroupCondition(gId, condition);
        var orderBy = ItemsColumns.IiSortOrder + ", " + ItemsColumns.DiDateCreated + " desc";
        var ds = GroupItems.GetAllDataPaging("1", top, condition, orderBy);
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];

        #region Lấy danh sách bài viết

        s.Append("<div class='product_group_items'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][ItemsColumns.ViTitle].ToString();
            var image = StringExtension.LayChuoi(dt.Rows[i][ItemsColumns.ViImage].ToString(), "", 1);
            if (image.Length > 0) image += ".ashx?w=430&quantity=60";
            //else image = "no_image.svg";
            var link = (UrlExtension.WebsiteUrl + dt.Rows[i][ItemsColumns.ViLink] + RewriteExtension.Extensions).ToLower();
            var priceNew = dt.Rows[i][ItemsColumns.FiPriceNew].ToString();
            var priceOld = dt.Rows[i][ItemsColumns.FiPriceOld].ToString();
            //var sale = Convert.ToInt32(priceOld) - Convert.ToInt32(priceNew);
            //var size = StringExtension.LayChuoi(dt.Rows[i][ItemsColumns.ViParam].ToString(), "", 1);
            s.Append(@"
<a class='item' href='" + link + "' title='" + titleItem + @"'>
    <div class='wImage'>
        " + ImagesExtension.GetImage(_pic, image, titleItem, "lazyload", false, false, "", true, true, "") + @"
    </div>
    <div class='product_info'>
        <h3 class='product_name'>" + titleItem + @"</h3>
        <div class='product_price'>
            <del class='price_old'>" + NumberExtension.FormatNumber(priceNew, true, "", "đ") + @"</del>
            <span class='price_new'>" + NumberExtension.FormatNumber(priceOld, true, "Liên hệ", "đ") + @"<i></i></span>
        </div>
        <div class='product_rating'>••••• (2 đánh giá)</div>
        <div class='product_stat'>6 màu sắc</div>
    </div>
</a>");
        }
        s.Append("</div>");

        #endregion

        #region Xuất ra phân trang

        if (Convert.ToInt32(dtPager.Rows[0]["TotalRows"]) > Convert.ToInt32(top) && dt.Rows.Count > 0)
        {
            var split = PagingCollection.SplitPagesNoRewriteDisplay(int.Parse(dtPager.Rows[0]["TotalRows"].ToString()), int.Parse(top), int.Parse("1"), rawUrl, "active", "other", "first", "last", "prev", "next", filter);
            s.Append(split);
        }

        #endregion Xuất ra phân trang
    
        string[] reply = { s.ToString() };
        Response.Output.Write(_js.Serialize(reply));
    }
    private void UpdateCart()
    {
        var s = new StringBuilder();
        var s1 = new StringBuilder();
 
        #region Lấy thông tin

        var id = Request.Form["id"] ?? "";
        var newNumber = Request.Form["newNumber"] ?? "1";
        var mi = Request.Form["mi"] ?? "";
        //var size = Request.Form["newSize"] != null ? Request.Form["newSize"] : "";
        //var color = StringExtension.ReplateTitle(Request.Form["newColor"]) != null ? StringExtension.ReplateTitle(Request.Form["newColor"]) : "";
        //var color = HttpUtility.UrlEncode(Request.Form["newColor"]) != null ? HttpUtility.UrlEncode(Request.Form["newColor"]) : "";
        //var image = Request.Form["newImage"] != null ? Request.Form["newImage"] : "";
        //var nb = Request.Form["randomNumber"] != null ? Request.Form["randomNumber"] : "";

        #endregion Lấy thông tin

        if (CookieExtension.CheckValidCookies(_cookieCartName))
        {
            //var newValue = "&i=" + id + "&q=" + newNumber + "&s=" + size + "&c=" + color + "&img=" + image + "&nb=" + nb;
            //var oldList = CookieExtension.GetCookies(_cookieCartName);
            //var list = "";
            //for (var i = 0; i < oldList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
            //{
            //    var values = HttpUtility.ParseQueryString(oldList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i]);

            //    if (!values["i"].Equals(id)) continue;
            //    if (values["nb"].Equals(nb))
            //    {
            //        list = oldList.Replace("&i=" + values["i"] + "&q=" + values["q"] + "&s=" + values["s"] + "&c=" + values["c"] + "&img=" + values["img"] + "&nb=" + values["nb"], newValue);
            //    }
            //}

            //// Gán lại vào cookie giỏ hàng
            //CookieExtension.SaveCookies(_cookieCartName, list);

            var list = "";
            var newValue = "&i=" + id + "&q=" + newNumber + "&mi=" + mi;
            var oldList = CookieExtension.GetCookies(_cookieCartName);
            var tempList = "";

            for (var i = 0; i < oldList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
            {
                var values = HttpUtility.ParseQueryString(oldList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i]);

                //if (!values["i"].Equals(id))
                //{
                //    tempList += oldList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i] + ",";
                //    continue;
                //}

                if (values["mi"].Equals(mi))
                {
                    tempList += newValue + ",";
                }
                else
                {
                    tempList += oldList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i] + ",";
                }
            }

            // Gán lại vào cookie giỏ hàng
            list = tempList.Substring(0, tempList.Length - 1);
            CookieExtension.SaveCookies(_cookieCartName, list);

            var totalPrice = 0;
            var plus = "";
            for (var i = 0; i < list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
            {
                var values = HttpUtility.ParseQueryString(list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i]);
                var condition = DataExtension.AndConditon(
                    ItemsTSql.GetByLang(_lang),
                    ItemsTSql.GetByStatus("1"),
                    ItemsTSql.GetById(values["i"])
                );
                var dt = RevosJsc.Database.Items.GetData("", "*", condition, "");
                if (dt.Rows.Count < 1) continue;
                //var titleItem = dt.Rows[0][ItemsColumns.ViTitle].ToString();
                //var image = dt.Rows[0][ItemsColumns.ViImage].ToString();
                //var link = (UrlExtension.WebsiteUrl + dt.Rows[0][ItemsColumns.ViLink] + RewriteExtension.Extensions).ToLower();
                var price = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();
                var titleItem = dt.Rows[0][ItemsColumns.ViTitle].ToString();
                var link = (UrlExtension.WebsiteUrl + dt.Rows[0][ItemsColumns.ViLink] + RewriteExtension.Extensions).ToLower();
                var subTotal = 0;
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
                        subTotal += int.Parse(arrPriceWholesale[q]) * int.Parse(values["q"]);
                        break;
                    }

                }
                if(!isPriceWholesale)
                {
                    subTotal += int.Parse(price) * int.Parse(values["q"]);
                }
                totalPrice += subTotal;
                if (price == "0") plus = "++";


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

                if (id != values["i"]) continue;
                if (isWholesale)
                {
                    s1.Append(@"
                        <a href='" + link + "' title='" + titleItem + @"'>" + titleItem + @"</a>
                        <del>" + NumberExtension.FormatNumber(price, true, "Liên hệ", "đ") + @"</del>
                        <strong>" + NumberExtension.FormatNumber(arrPrice, true, "Liên hệ", "đ") + @"</strong>
                    ");
                }
                else
                {
                    s1.Append(@"
                        <a href='" + link + "' title='" + titleItem + @"'>" + titleItem + @"</a>
                        <strong>" + NumberExtension.FormatNumber(price, true, "Liên hệ", "đ") + @"</strong>
                    ");
                }
            }

            s.Append(@"
            <div class='clearfix'>
                <span>Tổng tiền:</span>
                <span>" + NumberExtension.FormatNumber(totalPrice.ToString(), true, "Liên hệ", plus + " đ") + @"</span>
            </div>
            <div class='shipping_store cleafix'>
                <div class='total'>
                    <b>Cần thanh toán:</b>
                    <strong>" + NumberExtension.FormatNumber(totalPrice.ToString(), true, "Liên hệ", plus + " đ") + @"</strong>
                </div>
            </div>
            ");


        }
        string[] reply = { s.ToString(), s1.ToString() };
        Response.Output.Write(_js.Serialize(reply));
    }
    private void UpdateQuantity()
    {
        #region Lấy thông tin

        var iiId = Request.Form["iiId"] ?? "";
        var quantity = Request.Form["quantity"] ?? "1";

        #endregion Lấy thông tin

        if (!CookieExtension.CheckValidCookies(_cookieCartName)) return;
        var newValue = "&i=" + iiId + "&q=" + quantity;
        var oldList = CookieExtension.GetCookies(_cookieCartName);
        var list = "";
        for (var i = 0; i < oldList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
        {
            var values = HttpUtility.ParseQueryString(oldList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i]);
            if (values["i"].Equals(iiId))
            {
                list = oldList.Replace("&i=" + values["i"] + "&q=" + values["q"], newValue);
            }
        }

        // Gán lại vào cookie giỏ hàng
        CookieExtension.SaveCookies(_cookieCartName, list);
    }
    private void RemoveItemInCart()
    {
        var s1 = new StringBuilder();
        var id = Request.Form["id"];
        var newSize = Request.Form["size"];
        //var newColor = StringExtension.ReplateTitle(Request.Form["color"]);
        var newColor = HttpUtility.UrlEncode(Request.Form["color"]);
        var img = Request.Form["image"];
        var nb = Request.Form["randomNumber"] ?? "";

        if (CookieExtension.CheckValidCookies(_cookieCartName))
        {

            var oldList = CookieExtension.GetCookies(_cookieCartName);
            var list = "";
            var newValue = "";
            for (var i = 0; i < oldList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
            {
                var values = HttpUtility.ParseQueryString(oldList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i]);


                if (!values["i"].Equals(id))

                {

                    list += oldList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i] + ",";

                    continue;

                }


                if (values["nb"].Equals(nb))

                {

                    list += newValue + ",";

                }

                else

                {

                    list += oldList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i] + ",";

                }

            }

            // Gán lại vào cookie giỏ hàng
            CookieExtension.SaveCookies(_cookieCartName, list);

            var totalPrice = 0;
            var plus = "";
            if (list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length > 0)
            {
                s1.Append("<ul class='listorder'>");
                for (var i = 0; i < list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                {
                    var values = HttpUtility.ParseQueryString(list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i]);
                    var condition = DataExtension.AndConditon(
                        ItemsTSql.GetByApp(_app),
                        ItemsTSql.GetByLang(_lang),
                        ItemsTSql.GetByStatus("1"),
                        ItemsTSql.GetById(values["i"])
                    );
                    var dt = RevosJsc.Database.Items.GetData("", "*", condition, "");
                    if (dt.Rows.Count < 1) continue;
                    var titleItem = dt.Rows[0][ItemsColumns.ViTitle].ToString();
                    var image = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViImage].ToString(), "", 1);
                    var price = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();
                    var link = (UrlExtension.WebsiteUrl + dt.Rows[0][ItemsColumns.ViLink] + RewriteExtension.Extensions).ToLower();
                    var app = dt.Rows[0][ItemsColumns.ViApp].ToString();
                    var color = dt.Rows[0][ItemsColumns.ViMetaTitle].ToString();
                    var size = dt.Rows[0][ItemsColumns.ViMetaDescription].ToString();
                    var masterId = dt.Rows[0][ItemsColumns.IiId].ToString();
                    totalPrice += int.Parse(price) * int.Parse(values["q"]);
                    if (price == "0") plus = "++";

                    s1.Append("<li>");
                    s1.Append("<div class='colimg'>");
                    if (values["img"] == null || values["img"] == "")
                    {
                        s1.Append(@"
             <span>
            " + ImagesExtension.GetImage(_pic, image, titleItem, "lazyload", true, false, "", false, false, "") + @"
             </span>");

                    }
                    else
                    {
                        s1.Append($"<img src='{values["img"]}' alt='{titleItem}' />");
                    }
                    s1.Append("</div>");
                    s1.Append("<div class='colinfo'>");
                    s1.Append("<div class='colinfo-left'>");
                    s1.Append(@"
                <div class='info'>
                    <a href='" + link + "' title='" + titleItem + @"'>" + titleItem + @"</a>
                    <strong>" + NumberExtension.FormatNumber(price, true, "Liên hệ", "đ") + @"</strong>
                </div>
                ");

                    s1.Append("<span class='pull-left'>");

                    var conditionSize = DataExtension.AndConditon(
                        ItemsTSql.GetByMasterId(masterId),
                        ItemsTSql.GetByStatus("1")
                    );
                    var dtSize = RevosJsc.Database.Items.GetData("", "*", conditionSize, "");

                    // check điều kiến lấy ra size
                    if (!string.IsNullOrEmpty(values["s"]))
                    {
                        if (app == "variant")
                        {
                            s1.Append(" Size : " + size);
                        }
                        else
                        {
                            s1.Append("<div>");
                            s1.Append("Size: <select id='select-size' onchange=\"UpdateCart('" + values["i"] + "', this.value, $(this).closest('.colinfo').find('#select-color').val(), $(this).closest('.colinfo').find('.hdQuantity').val(), $(this).closest('.colinfo-left').find('#select-color option:selected').attr('imagesrc'), '" + values["nb"] + "')\">");

                            HashSet<string> uniqueSỉze = new HashSet<string>();

                            for (int z = 0; z < dtSize.Rows.Count; z++)
                            {
                                ; string value = dtSize.Rows[z]["viMetaDescription"].ToString();
                                //var srcImage = StringExtension.LayChuoi(dt.Rows[z][ItemsColumns.ViImage].ToString(), "", 1);
                                bool isSelected = (value == values["s"]);

                                // Kiểm tra giá trị đã tồn tại trong tập hợp chưa
                                if (!uniqueSỉze.Contains(value))
                                {
                                    uniqueSỉze.Add(value);
                                    s1.Append($"<option value='{value}'{(isSelected ? " selected" : "")}>{value}</option>");
                                    //s.Append($"<option value='{srcImage}'{(isSelected ? " selected" : "")}>{srcImage}</option>");
                                }
                            }

                            s1.Append("</select>");
                            s1.Append("</div>");
                        }
                    }
                    else
                    {
                        s1.Append("");
                    }

                    // check điều kiến lấy ra color
                    if (!string.IsNullOrEmpty(values["c"]))
                    {
                        if (app == "variant")
                        {
                            s1.Append(" Color : " + color);
                        }
                        else
                        {
                            s1.Append("<div>");
                            s1.Append("Color: <select id='select-color' onchange=\"UpdateCart('" + values["i"] + "', $(this).closest('.colinfo').find('#select-size').val(), this.value, $(this).closest('.colinfo').find('.hdQuantity').val(), $(this).find(':selected').attr('imagesrc'), '" + values["nb"] + "')\">");

                            HashSet<string> uniqueColor = new HashSet<string>();

                            for (int z = 0; z < dtSize.Rows.Count; z++)
                            {
                                var idC = dtSize.Rows[z][ItemsColumns.IiId].ToString();
                                string value = dtSize.Rows[z]["viMetaTitle"].ToString();
                                string valueReplace = HttpUtility.HtmlEncode(value);
                                bool isSelected = (value == HttpUtility.UrlDecode(values["c"]));

                                // Kiểm tra giá trị đã tồn tại trong tập hợp chưa
                                if (!uniqueColor.Contains(valueReplace))
                                {
                                    uniqueColor.Add(valueReplace);

                                    s1.Append($"<option value='{valueReplace}'{(isSelected ? " selected" : "")} imagesrc='{getImageProductCart(idC)}'>{value}</option>");

                                }
                            }

                            s1.Append("</select>");
                            s1.Append("</div>");
                        }
                    }
                    else
                    {
                        s1.Append("");
                    }

                    s1.Append("</span>");
                    s1.Append("<div class='choosenumber'>");
                    s1.Append("<div class='abate" + (int.Parse(values["q"]) > 1 ? " active" : "") + @"'></div>");
                    s1.Append("<input class=\"hdQuantity number\" type=\"number\" value=\"" + values["q"] + "\" min=\"1\" onchange=\"UpdateCart('" + values["i"] + "', $(this).closest('.colinfo').find('#select-size').val(), $(this).closest('.colinfo').find('#select-color').val(), this.value, $(this).closest('.colinfo').find('#select-color option:selected').attr('imagesrc'), '" + values["nb"] + "')\" disabled>");
                    s1.Append("<div class='augment'></div>");
                    s1.Append("</div>");
                    s1.Append("</div>");

                    s1.Append("<div class='clearfix'>");
                    s1.Append("<button type=\"button\" class=\"delete\" onclick=\"RemoveItemInCart(\'" + values["i"] + "\', $(this).closest('.colinfo').find('#select-size').val(), $(this).closest('.colinfo').find('#select-color').val(), $(this).closest('.colinfo').find('#select-color option:selected').attr('imagesrc'), '" + values["nb"] + "');\"><span></span>Xóa</button>");
                    s1.Append("</div>");
                    s1.Append("</div>");
                    s1.Append("</li>");
                }
                s1.Append("</ul>");

            }
            else
            {
                CookieExtension.ClearCookies(_cookieCartName);
                Response.Output.Write(_js.Serialize(""));
                return;
                //s1.Append("<div class='emptyResult'>Không có sản phẩm nào trong giỏ hàng<br/><br/><a href='/' class='btnGoHome'>Về trang chủ</a></div>");
            }
            s1.Append(@"
<div class='area_total'>
    <div class='clearfix'>
        <span>Tổng tiền:</span>
        <span>" + NumberExtension.FormatNumber(totalPrice.ToString(), true, "Liên hệ", plus + " đ") + @"</span>
    </div>
    <div class='shipping_store cleafix'>
        <div class='total'>
            <b>Cần thanh toán:</b>
            <strong>" + NumberExtension.FormatNumber(totalPrice.ToString(), true, "Liên hệ", plus + " đ") + @"</strong>
        </div>
    </div>
</div>
");
        }
        string[] reply = { s1.ToString() };
        Response.Output.Write(_js.Serialize(reply));
    }
    private void SubmitCart()
    {
        #region Lấy thông tin

        var name = Request.Form["name"] ?? "";
        var phone = Request.Form["phone"] ?? "";
        var email = Request.Form["email"] ?? "";
        var address = Request.Form["address"] ?? "";
        //var city = Request.Form["city"] ?? "";
        //var district = Request.Form["district"] ?? "";
        var message = Request.Form["message"] ?? "";
        //var payMethod = Request.Form["payMethod"] ?? "";

        #endregion Lấy thông tin

        #region Lưu thông tin khách hàng cho những lần đặt hàng sau

        CookieExtension.SaveCookies(SecurityExtension.BuildPassword("name"), name);
        CookieExtension.SaveCookies(SecurityExtension.BuildPassword("phone"), phone);
        CookieExtension.SaveCookies(SecurityExtension.BuildPassword("email"), email);
        CookieExtension.SaveCookies(SecurityExtension.BuildPassword("address"), address);

        #endregion

        var listItem = "";
        var maDonHang = "";
        var source = CookieExtension.GetCookies("url_referrer") + " - " + _device;
        if (CookieExtension.CheckValidCookies(_cookieCartName))
        {
            double total = 0;
            var id = Bills.Insert_ReturnId(_lang, "", name, phone, email, StringExtension.GhepChuoi("", address), message, StringExtension.GhepChuoi("",  source), "0", DateTime.Now.ToString(), DateTime.Now.ToString());
            maDonHang = DateTime.Now.ToString("yyMMdd") + id;
            // Cập nhật mã đơn hàng
            Bills.UpdateValues("vbCode = '" + maDonHang + "'", BillsTSql.GetById(id));
            var list = CookieExtension.GetCookies(_cookieCartName);
            var plus = "";
            var stt = 0;
            for (var i = 0; i < list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
            {
                var values = HttpUtility.ParseQueryString(list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i]);
                var condition = DataExtension.AndConditon(
                    //GroupsTSql.GetByApp(_app),
                    //GroupsTSql.GetByStatus("1"),
                    //ItemsTSql.GetByApp(_app),
                    ItemsTSql.GetByStatus("1"),
                    ItemsTSql.GetByLang(_lang),
                    ItemsTSql.GetById(values["i"])
                );
                var dt = RevosJsc.Database.Items.GetData("1", "*", condition, "");
                if (dt.Rows.Count < 1) continue;
                stt += 1;
                var price = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();
                var titleItem = dt.Rows[0][ItemsColumns.ViTitle].ToString();
                //var color = dt.Rows[0][ItemsColumns.ViMetaTitle].ToString();
                //var colorReplace = StringExtension.ReplateTitle(color);

                var subTotal = 0;

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
                        price = arrPriceWholesale[q];
                        subTotal += int.Parse(arrPriceWholesale[q]) * int.Parse(values["q"]);
                        break;
                    }

                }
                if (!isPriceWholesale)
                {
                    subTotal += int.Parse(price) * int.Parse(values["q"]);
                }

                var param = StringExtension.GhepChuoi("", HttpUtility.UrlDecode(values["s"]), HttpUtility.UrlDecode(values["c"]));

                //if(values["c"] == colorReplace)
                //{
                //     param = StringExtension.GhepChuoi("", HttpUtility.UrlDecode(values["s"]), HttpUtility.UrlDecode(color));
                //}

                BillDetails.Insert(id, titleItem, values["q"], price,
                    dt.Rows[0][ItemsColumns.FiPriceNew].ToString(), param, DateTime.Now.ToString(), DateTime.Now.ToString());
                // Lưu cookies Id đơn hàng vừa thêm 
                //var subTotal = int.Parse(price) * int.Parse(values["q"]);
                total += subTotal;
                if (price == "0") plus = "<sup>++</sup>";
                listItem += @"<tr>
    <td style='border:solid 1px #000;padding:5px 0;text-align:center'>" + stt + @"</td>
    <td style='border:solid 1px #000;padding:5px;text-align:left'>" + titleItem + @"</td>
    <td style='border:solid 1px #000;padding:5px 0;text-align:center'>" + values["q"] + @"</td>
    <td style='border:solid 1px #000;padding:5px 0;text-align:center'>" + values["s"] + @"</td>
    <td style='border:solid 1px #000;padding:5px 0;text-align:center'>" + HttpUtility.UrlDecode(values["c"]) + @"</td>
    <td style='border:solid 1px #000;padding:5px 0;text-align:center'>" + NumberExtension.FormatNumber(price, true, "Liên hệ", "đ") + @"</td>
</tr>";
            }
            listItem += @"<tr>
    <td style='border:solid 1px #000;padding:5px 0;text-align:center' colspan='2'><b>Tổng thanh toán</b></td>
    <td style='border:solid 1px #000;padding:5px 0;text-align:right' colspan='4'><b>" + NumberExtension.FormatNumber(total.ToString(), true, "Liên hệ", plus + " đ") + @"</b></td>
</tr>";
        }
        else Response.End();

        #region Gửi email thông báo đến email hệ thống
        var date = DateTime.Now.ToString();
        var subject = LanguageExtension.TranslateKeyword("Thông báo đơn đặt hàng từ") + " " + UrlExtension.WebsiteUrl + " " + date;
        var body =
@"<table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; width: 100%; max-width: 600px;' class='content'>
    <tr>
        <td align='center' bgcolor='#1bbae1' style='padding: 20px 20px 20px 20px; '>
            <div style='color: #ffffff; font-family: Arial, sans-serif; font-size: 18px; font-weight: bold;'>Đặt hàng thành công</div>
            <div style='color: #ffffff; font-family: Arial, sans-serif;'>Mã đơn hàng:" + maDonHang + @"</div>
        </td>
    </tr>
    <tr>
        <td bgcolor='#f2f2f2' style='padding:20px 10px 5px; color: #555555; font-family: Arial, sans-serif;'>
            Xin chào! Bạn có một đơn đặt hàng trên website: <a href='" + UrlExtension.WebsiteUrl + @"'>" + UrlExtension.WebsiteUrl + @"</a>
        </td>
    </tr>
    <tr>
        <td bgcolor='#f2f2f2' style='padding:5px 10px; color: #555555; font-family: Arial, sans-serif;font-size: 15px; line-height: 30px;'>
            Thông tin khách hàng
        </td>
    </tr>
    <tr>
        <td bgcolor='#f2f2f2' style='padding:5px 10px; color: #555555; font-family: Arial, sans-serif;'>
            Họ tên: " + name + @"
        </td>
    </tr>
    <tr>
        <td bgcolor='#f2f2f2' style='padding:5px 10px; color: #555555; font-family: Arial, sans-serif;'>
            Điện thoại: " + phone + @"
        </td>
    </tr>
    <tr>
        <td bgcolor='#f2f2f2' style='padding:5px 10px; color: #555555; font-family: Arial, sans-serif;'>
            Email: " + email + @"
        </td>
    </tr>
    <tr>
        <td bgcolor='#f2f2f2' style='padding:5px 10px; color: #555555; font-family: Arial, sans-serif;'>
            Địa chỉ nhận hàng (Dành cho Ship COD): " + address  + @"
        </td>
    </tr>
    
    <tr>
        <td bgcolor='#f2f2f2' style='padding:5px 10px; color: #555555; font-family: Arial, sans-serif;border-bottom: 1px solid #f6f6f6;'>
            Ghi chú: " + message + @"
        </td>
    </tr>
    <tr>
        <td bgcolor='#f2f2f2' style='padding: 5px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 15px; line-height: 30px;'>
            Thông tin đơn hàng
        </td>
    </tr>
    <tr>
        <td bgcolor='#f2f2f2' style='padding: 5px 10px;'>
            <table style='border-collapse:collapse;width:100%'>
                <tr>
                    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>STT</th>
                    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>Tên sản phẩm</th>
                    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>Số lượng</th>
                    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>Size</th>
                    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>Màu sắc</th>
                    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>Đơn giá</th>
                </tr>
                " + listItem + @"
            </table>
        </td>
    </tr>
    <tr>
        <td bgcolor='#f2f2f2' style='padding:10px; color: #555555; font-family: Arial, sans-serif;'>
            " + LanguageExtension.TranslateKeyword("Để trao đổi thêm về yêu cầu này, vui lòng chọn Trả lời tất cả email (reply all) hoặc liên hệ hotline " + SettingsExtension.GetSettingKey(SettingsExtension.KeyHotLine, _lang) + ". Trân trọng cảm ơn!") + @"
        </td>
    </tr>
    <tr>
        <td align='center' bgcolor='#dddddd' style='padding: 15px 10px 15px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 12px; line-height: 18px;'>
            <b>Gothiar</b>
        </td>
    </tr>
</table>";
        var emailKhac = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailManager, _lang);
        var emailNhan = "";
        var emailCc = new List<string>();
        for (var i = 0; i < emailKhac.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
        {
            var item = emailKhac.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i];
            if (i == 0 && email.Equals("")) emailNhan = item;
            else emailCc.Add(item);
        }

        if (email.Length > 0) emailNhan = email;
        EmailExtension.SendEmail(emailNhan, subject, body, emailCc.ToArray());

        #endregion

        #region Xóa cookie giỏ hàng

        CookieExtension.ClearCookies(_cookieCartName);

        #endregion

        Response.End();
    }
    private string GetVariantById(string value)
    {
        var param = "";
        var dt = Subitems.GetData("1", "*", DataExtension.AndConditon(SubItemsTSql.GetById(value), SubItemsTSql.GetByStatus("1")), "");
        if (dt.Rows.Count <= 0) return param.Length > 0 ? param : "";
        param = dt.Rows[0][SubitemsColumns.VsiTitle].ToString();

        return param.Length > 0 ? param : "";
    }
    private string GetColorById(string value)
    {
        var param = "";
        var dt = Subitems.GetData("1", "*", DataExtension.AndConditon(SubItemsTSql.GetById(value), SubItemsTSql.GetByStatus("1")), "");
        if (dt.Rows.Count > 0)
        {
            param = dt.Rows[0][SubitemsColumns.VsiParam].ToString();
        }

        return param.Length > 0 ? GroupsExtension.GetNameByIgid(param) : "";
    }
    private void AddToCart()
    {
        var s = "Success";

        #region Lấy thông tin

        var id = Request.Form["id"] ?? "";
        var num = Request.Form["num"] ?? "";
        var size = Request.Form["size"] ?? "";
        //var color = StringExtension.ReplateTitle(Request.Form["color"]) ?? "";
        var color = HttpUtility.UrlEncode(Request.Form["color"]) ?? "";
        var image = Request.Form["image"] ?? "";
        var nb = Request.Form["randomNumber"] ?? "";
        #endregion Lấy thông tin

        #region Lấy thông tin sản phẩm

        var condition = DataExtension.AndConditon(DataExtension.OrConditon(ItemsTSql.GetByApp(_app), ItemsTSql.GetByApp("OptionUpgrade"), ItemsTSql.GetByApp("Variant")), ItemsTSql.GetByLang(_lang), ItemsTSql.GetByStatus("1"), ItemsTSql.GetById(id));
        var dt = RevosJsc.Database.Items.GetData("1", "*", condition, "");
        if (dt.Rows.Count > 0)
        {
            #region Kiểm tra xem sản phẩm này đã có trong giỏ hàng hay chưa

            if (CookieExtension.CheckValidCookies(_cookieCartName))
            {
                // Biến kiểm tra xem sản phẩm có màu mới hay cũ
                var isNewProduct = true;
                // Kiểm tra sản phẩm có trong giỏ chưa
                var pIndex = -1;
                var list = CookieExtension.GetCookies(_cookieCartName);
                for (var i = 0; i < list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                {
                    var values = HttpUtility.ParseQueryString(list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i]);
                    if (values["i"] == id && values["c"] == color)
                    {
                        // Nếu đã có màu thì update lại sp
                            var replace = list.Replace("&i=" + values["i"] + "&q=" + values["q"] + "&s=" + values["s"] + "&c=" + values["c"] + "&img=" + values["img"] + "&nb=" + values["nb"], "&i=" + values["i"] + "&q=" + values["q"] + "&s=" + size + "&c=" + color + "&img=" + image + "&nb=" + values["nb"]);
                            CookieExtension.SaveCookies(_cookieCartName, replace);

                            isNewProduct = false;
                        break;
                        
                    }

                }
                if (isNewProduct)
                {
                    // Sản phẩm mới, thêm vào giỏ hàng
                    CookieExtension.SaveCookies(_cookieCartName, "&i=" + id + "&q=" + num + "&s=" + size + "&c=" + color + "&img=" + image + "&nb=" + nb + "," + list);
                }

            }
            // Nếu chưa có cookie thì thêm mới cookie và lưu giá trị sản phẩm đầu tiên
            else
            {
                CookieExtension.SaveCookies(_cookieCartName, "&i=" + id + "&q=" + num + "&s=" + size + "&c=" + color + "&img=" + image + "&nb=" + nb );
                // ReSharper disable once PossibleNullReferenceException
                Response.Cookies[_cookieCartName].Expires = DateTime.Now.AddDays(30);
            }

            #endregion
        }
        else s = "Error";
        #endregion

        string[] reply = { s };
        Response.Output.Write(_js.Serialize(reply));
    }
    private void Order()
    {
        var s = "Đặt hàng thành công!";

        #region Lấy thông tin

        var id = Request.Form["id"] ?? "";
        var name = Request.Form["name"] ?? "";
        var tel = Request.Form["tel"] ?? "";
        var email = Request.Form["email"] ?? "";
        var address = Request.Form["address"] ?? "";
        var message = Request.Form["message"] ?? "";
        var source = CookieExtension.GetCookies("url_referrer") + " - " + _device;

        #endregion Lấy thông tin

        #region Lưu thông tin vào bảng liên hệ
        ContactDetails.Insert(name, email, tel, address, "Thông tin KH đặt hàng", message, id, DateTime.Now.ToString(), "0");
        #endregion

        #region Gửi email thông báo đến email hệ thống

        var date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        var subject = "Bạn có một yêu cầu đặt hàng từ " + UrlExtension.WebsiteUrl + " " + date;
        var body = @"
<div style='width: 100%;margin: auto; font-family: Arial'>
    <table style='width: 100%; border-spacing: 0;border-collapse: collapse;border-right:1px solid'>
        <tbody>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Sản phẩm</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + ProductExtension.GetProductNameById(id) + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Link</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + Request.UrlReferrer + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Họ tên</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + name + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Điện thoại</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + tel + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Email</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + email + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Địa chỉ</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + address + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Ghi chú</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + message + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Nguồn</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + source + @"</td>
            </tr>
        </tbody>
    </table>
</div>";

        var emailManager = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailManager, _lang);
        var emailReceived = "";
        var emailCc = new List<string>();
        for (var i = 0; i < emailManager.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
        {
            var item = emailManager.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i];
            if (i == 0) emailReceived = item;
            else emailCc.Add(item);
        }
        EmailExtension.SendEmail(emailReceived, subject, body, emailCc.ToArray());

        #endregion Gửi email thông báo đến email hệ thống

        string[] reply = { s };
        Response.Output.Write(_js.Serialize(reply));
    }

    private void GetListCart()
    {
        var s = new StringBuilder();
        var list = CookieExtension.GetCookies(_cookieCartName);
        var totalPrice = 0;
        var plus = "";
        for (var i = 0; i < list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
        {
            var values = HttpUtility.ParseQueryString(list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i]);
            var condition = DataExtension.AndConditon(
                //DataExtension.OrConditon(ItemsTSql.GetByApp(_app), ItemsTSql.GetByApp("OptionUpgrade")),
                ItemsTSql.GetByStatus("1"),
                ItemsTSql.GetByLang(_lang),
                "Items.iiId = '" + values["i"] + "'"
            );
            var dt = RevosJsc.Database.Items.GetData("1", "*", condition, "");
            if (dt.Rows.Count < 1) continue;
            var iiId = dt.Rows[0][ItemsColumns.IiId].ToString();
            var titleItem = dt.Rows[0][ItemsColumns.ViTitle].ToString();
            var image = dt.Rows[0][ItemsColumns.ViImage].ToString();
            if (dt.Rows[0][ItemsColumns.ViApp].ToString().Equals("Variant")) image = GetParentImage(dt.Rows[0][ItemsColumns.ViTag].ToString());
            if (image.Contains(StringExtension.SpecialCharactersKeyword.ParamsSpilitItems)) image = StringExtension.LayChuoi(image, "", 1);
            var price = dt.Rows[0][ItemsColumns.FiPriceNew].ToString();
            var link = UrlExtension.WebsiteUrl + dt.Rows[0][ItemsColumns.ViLink] + RewriteExtension.Extensions;
            var pricePlus = "0";
            var subTotalPrice = (int.Parse(price) + int.Parse(pricePlus)) * int.Parse(values["q"]);
            totalPrice += subTotalPrice;
            if (price == "0") plus = "<sup>++</sup>";
            s.Append(@"
<div class='item'>
    <div class='col1'>
        <a a href='javascript://' onclick='RemoveItem(" + iiId + @")' class='delete'></a>
        <a href='" + link + "' title='" + titleItem + @"' class='wImage0'>
            " + ImagesExtension.GetImage(_pic, image, titleItem, "", false, false, "") + @"
        </a>
        <span class='title'>" + titleItem + @"</span>
    </div>
    <div class='col2'>" + NumberExtension.FormatNumber(price, true, "Liên hệ", "đ") + @"</div>
    <div class='col3'>
        <div class='number'>
            <input type='number' onchange='UpdateQuantity("+ iiId + ",this.value)' value='" + values["q"] + @"' min='1' />
            <span class='up' onclick='upNumber(this)'></span>
            <span class='down' onclick='downNumber(this)'></span>
        </div>
    </div>
    <div class='col4'>" + NumberExtension.FormatNumber(subTotalPrice.ToString(), true, "Liên hệ", "đ") + @"</div>
</div>");
        }

        if (s.Length > 0) s.Append(@"<div class='total'>
        <span>Tổng thanh toán</span>
        <b>" + NumberExtension.FormatNumber(totalPrice.ToString(), true, "Liên hệ", plus + " đ") + @"</b>
    </div>");
        else s.Append("<div class='emptyCart'><p>Không có sản phẩm nào trong giỏ hàng của bạn.</p><p><a href='/' class='viewMore'>Tiếp tục mua sắm</a></p></div>");
        Response.Write(s);
    }

    private string GetParentImage(string id)
    {
        var dt = RevosJsc.Database.Items.GetData("1", "*", ItemsTSql.GetById(id), "");
        return dt.Rows.Count > 0 ? dt.Rows[0][ItemsColumns.ViImage].ToString() : "";
    }

    private void CountItemsInCart()
    {
        var quantity = 0;
        var list = CookieExtension.GetCookies(_cookieCartName);

        for (var i = 0; i < list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
        {
            var values = HttpUtility.ParseQueryString(list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i]);
            var condition = DataExtension.AndConditon(
                ItemsTSql.GetByLang(_lang),
                ItemsTSql.GetByStatus("1"),
                ItemsTSql.GetById(values["i"])
            );
            var dt = RevosJsc.Database.Items.GetData("", "*", condition, "");
            if (dt.Rows.Count < 1) continue;
            quantity += int.Parse(values["q"]);
        }
        string[] reply = { quantity.ToString(), list};
        Context.Response.Write(_js.Serialize(reply));
    }
    private string GetListOther(string iiId, string type)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(_app),
            GroupsTSql.GetByStatus("1"),
            ItemsTSql.GetByApp(_app),
            ItemsTSql.GetByCode(type),
            ItemsTSql.GetByStatus("1")
        );
        var dt = GroupItems.GetAllData("", "*", condition, ItemsColumns.IiSortOrder);
        if (dt.Rows.Count < 2) return "";
        s.Append("<select onchange='UpdateItem(this.value," + iiId + ")'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][ItemsColumns.IiId].ToString();
            var memory = StringExtension.LayChuoi(dt.Rows[i][ItemsColumns.ViParam].ToString(), "", 2);
            s.Append("<option " + (iiId.Equals(id) ? "selected" : "") + " value='" + id + "'>" + memory + "</option>");
        }

        s.Append("</select>");
        return s.ToString();
    }
    private string GetListVariant(string iiId, string variant, ref string priceNew)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            SubItemsTSql.GetByIiid(iiId),
            SubItemsTSql.GetByApp("Formality"),
            SubItemsTSql.GetByStatus("1")
        );
        var dt = Subitems.GetData("", "*", condition, SubitemsColumns.IsiSortOrder);
        if (dt.Rows.Count < 1) return "";
        s.Append("<select onchange='UpdateSize(this.value," + iiId + ")'>");
        if (variant.Equals("")) s.Append("<option>Chọn hình thức</option>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][SubitemsColumns.IsiId].ToString();
            var name = dt.Rows[i][SubitemsColumns.VsiTitle].ToString();
            var price = dt.Rows[i][SubitemsColumns.FsiPriceOld].ToString();
            if (variant.Equals(id)) priceNew = price;
            s.Append("<option " + (variant.Equals(id) ? "selected" : "") + " value='" + id + "'>" + name + "</option>");
        }
        s.Append("</select>");
        return s.ToString();
    }
    private string GetListColor(string iiId, string color, ref string priceNew)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            SubItemsTSql.GetByApp(CodeApplications.ProductImagesOther),
            SubItemsTSql.GetByIiid(iiId),
            SubItemsTSql.GetByStatus("1"),
            "vsiParam <> ''"
        );
        var dt = Subitems.GetData("", "*", condition, SubitemsColumns.IsiSortOrder);
        if (dt.Rows.Count <= 0) return s.ToString();
        var listColorId = ",";
        s.Append("<select onchange='UpdateColor(this.value," + iiId + ")'>");
        if (color.Equals("")) s.Append("<option>Chọn màu sắc</option>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var colorId = dt.Rows[i][SubitemsColumns.VsiParam].ToString();
            if (listColorId.Contains("," + colorId + ",")) continue;
            listColorId += colorId + ",";
            var price = dt.Rows[i][SubitemsColumns.FsiPriceNew].ToString();
            var name = GroupsExtension.GetNameByIgid(colorId);
            var id = dt.Rows[i][SubitemsColumns.IsiId].ToString();
            if (color.Equals(id)) priceNew = price;
            s.Append("<option " + (color.Equals(id) ? "selected" : "") + " value='" + id + "'>" + name + "</option>");
        }
        s.Append("</select>");
        return s.ToString();
    }
    private string getImageProductCart(string id)
    {
        var conditionSize = DataExtension.AndConditon(
            ItemsTSql.GetById(id),
            ItemsTSql.GetByStatus("1")
        );
        var dtColor = RevosJsc.Database.Items.GetData("", "ViImage, ViTitle", conditionSize, "");
        var title = dtColor.Rows[0][ItemsColumns.ViTitle].ToString();
        //var image = StringExtension.LayChuoi(dtColor.Rows[0][ItemsColumns.ViImage].ToString(), "", 1);
        var image = dtColor.Rows[0][ItemsColumns.ViImage].ToString();
        var imageSrc = _pic + "/" + image;
        return imageSrc;
    }
}
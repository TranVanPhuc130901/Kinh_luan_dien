using System;
using System.Text;
using System.Web;
using Developer.Extension;
using RevosJsc;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;

public partial class Areas_Display_Search_Control_Product : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _pic = FolderPic.Product;
    private readonly string _app = CodeApplications.Product;
    private readonly string _maxItem = SettingKey.SoProductTrenTrangDanhMuc;
    private readonly string _noResultText = LanguageExtension.TranslateKeyword("Updating ...");
    private string _p = "1";
    private string _key = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = QueryStringExtension.GetQueryString("p");
        if (Request.QueryString["keyword"] != null) _key = Request.QueryString["keyword"];
        
        ltrList.Text = GetList();
    }
    
    private string GetList()
    {
        var s = new StringBuilder();
        var top = SettingsExtension.GetSettingKey(_maxItem, _lang);
        if (top.Equals("")) top = "8";
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(_app),
            GroupsTSql.GetByStatus("1"),
            GroupsTSql.GetByLang(_lang),
            ItemsTSql.GetByApp(_app),
            ItemsTSql.GetByStatus("1"),
            _key.Length > 0 ? ProductExtension.FullTextSearch(_key) : ""
            );
        var orderBy = ItemsColumns.IiSortOrder + ", " + ItemsColumns.DiDateCreated + " desc";
        var ds = GroupItems.GetAllDataPaging(_p, top, condition, orderBy);
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        //if (dt.Rows.Count < 1) return "";

        #region Lấy danh sách bài viết

        s.Append("<div class='result_search'> Kết quả tìm kiếm (" + _key + " - " + NumberExtension.FormatNumber(dtPager.Rows[0]["TotalRows"].ToString())  +  " sản phẩm )</div>");

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
        <div class='product_rating reviews_stars'><img src='/css/icon/star.svg' /><img src='/css/icon/star.svg' /><img src='/css/icon/star.svg' /><img src='/css/icon/star.svg' /><img src='/css/icon/star_half.svg' /></div>
        <div class='product_stat'>6 màu sắc</div>
    </div>
</a>");
        }
        s.Append("</div>");

        #endregion

        #region Xuất ra phân trang

        if (Convert.ToInt32(dtPager.Rows[0]["TotalRows"]) <= Convert.ToInt32(top) || dt.Rows.Count < 1) return s.ToString();
        if (_key.Length > 0)
        {
            var split = PagingCollection.SpilitPagesNoRewriteDisplay(int.Parse(dtPager.Rows[0]["TotalRows"].ToString()), int.Parse(top), int.Parse(_p), "?rewrite=search&page=product&keyword=" + _key, "active", "trangkhac", "dau", "cuoi", "prev", "next");
            s.Append(split);

        }
        else
        {
            s.Append("<div class='emptyresult'>" + _noResultText + "</div>");
            //var split = PagingCollection.SplitPages(int.Parse(dtPager.Rows[0]["TotalRows"].ToString()), int.Parse(top), int.Parse(_p), "?rewrite=search&page=product&keyword=" + _key, "active", "other", "first", "last", "prev", "next");
            //s.Append(split);
        }

        #endregion Xuất ra phân trang
        s.Append("</div>");

        return s.ToString();
    }
}
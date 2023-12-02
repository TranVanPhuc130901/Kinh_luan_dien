using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;


public partial class Areas_Display_Product_Control_Category : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _app = CodeApplications.Product;
    private readonly string _pic = FolderPic.Product;
    private readonly string _maxItem = SettingKey.SoProductTrenTrangDanhMuc;
    //private readonly string _noResultText = "Bài viết đang được cập nhật ...";
    private string _p = "1";
    private string _filter = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = QueryStringExtension.GetQueryString("p");
        if (Request.QueryString["filter"] != null) _filter = QueryStringExtension.GetQueryString("filter");
        if (_filter.Equals(""))
        {
            var myUri = new Uri(UrlExtension.WebsiteUrl + Request.RawUrl.Substring(1));
            _filter = HttpUtility.ParseQueryString(myUri.Query).Get("filter") ?? "";
        }
        GetGroup();
    }

    protected void GetGroup()
    {
        var s = new StringBuilder();
        var dt = (DataTable)Session["dataByTitle_Category"];
        if (dt.Rows.Count <= 0) return;
        //var nameG = dt.Rows[0][GroupsColumns.VgName].ToString();
        //var bannerG = StringExtension.LayChuoi(dt.Rows[0][GroupsColumns.VgImage].ToString(), "", 2);
        //var linkBanner = dt.Rows[0][GroupsColumns.VgParam].ToString();
        s.Append("<h1>" + dt.Rows[0][GroupsColumns.VgName] + "</h1>");
        s.Append("<input id='hd_group_id' type='hidden' value='" + dt.Rows[0][GroupsColumns.IgId] + "' />");
        s.Append("<input id='hd_raw_url' type='hidden' value='" + dt.Rows[0][GroupsColumns.VgLink].ToString().ToLower() + "' />");
        s.Append("<div class='desc'>" + dt.Rows[0][GroupsColumns.VgDescription].ToString().Replace("\n","<br/>") + "</div>");
        ltrTop.Text = s.ToString();
        ltrList.Text = GetList(dt.Rows[0][GroupsColumns.VgGenealogy].ToString());
    }


    private string GetList(string genealogy)
    {
        var s = new StringBuilder();
        var top = SettingsExtension.GetSettingKey(_maxItem, _lang);
        if (top.Equals("")) top = "10";
        var condition = DataExtension.AndConditon(
            ItemsTSql.GetByApp(_app),
            ItemsTSql.GetByStatus("1"),
            _filter.Length > 0 ? ItemsTSql.GetFilterAndCondition(_filter.Trim(',')) : ""
            );
        condition = GroupItemsTSql.GetItemsInGroupByGenealogy(genealogy, condition);
        var orderBy = ItemsColumns.IiSortOrder + ", " + ItemsColumns.DiDateCreated + " desc";
        if (Session["OrderProduct"] != null) orderBy = Session["OrderProduct"].ToString();
        var ds = GroupItems.GetAllDataPaging(_p, top, condition, orderBy);
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        //if (dt.Rows.Count < 1) return "";

        #region Lấy danh sách bài viết

        s.Append("<div class='product_group_items'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var iiid = dt.Rows[i][ItemsColumns.IiId].ToString();

            
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
        <div class='product_stat'>"+ ProductExtension.GetCountColor(iiid) +  @" Màu sắc</div>
    </div>
</a>");
        }
        s.Append("</div>");

        #endregion

        #region Xuất ra phân trang

        if (Convert.ToInt32(dtPager.Rows[0]["TotalRows"]) <= Convert.ToInt32(top) || dt.Rows.Count < 1) return s.ToString();
        if (_filter.Length > 0)
        {
            var split = PagingCollection.SplitPagesNoRewriteDisplay(int.Parse(dtPager.Rows[0]["TotalRows"].ToString()), int.Parse(top), int.Parse(_p), Request.QueryString["title"], "active", "other", "first", "last", "prev", "next", _filter);
            s.Append(split);

        }
        else
        {
            var split = PagingCollection.SplitPages(int.Parse(dtPager.Rows[0]["TotalRows"].ToString()), int.Parse(top), int.Parse(_p), Request.QueryString["title"], "active", "other", "first", "last", "prev", "next");
            s.Append(split);
        }

        #endregion Xuất ra phân trang

        return s.ToString();
    }
}
using System;
using System.Data;
using System.Text;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;

public partial class Areas_Display_Product_SubControl_SubProductOther : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _app = CodeApplications.Product;
    private readonly string _pic = FolderPic.Product;
    private readonly string _maxItem = SettingKey.SoProductKhacTrenMotTrang;
    private string _id = "";
    private string _gid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["iiid"] != null) _id = QueryStringExtension.GetQueryString("iiid");
        if (Request.QueryString["igid"] != null) _gid = QueryStringExtension.GetQueryString("igid");

        #region title
        if (Request.QueryString["title"] != null)
        {
            //Lấy igid từ session ra vì nó đã dược lưu khi xét tại Default.aspx
            if (_id.Length < 1 && Session["iiid"] != null) _id = Session["iiid"].ToString();
            if (_gid.Length < 1 && Session["igid"] != null) _gid = Session["igid"].ToString();
        }
        #endregion title
        ltrList.Text = GetOtherItem();
    }

    private void GetDataBySession()
    {
        var s = new StringBuilder();
        var dt = (DataTable)Session["dataByTitle"];
        if (dt.Rows.Count < 1) return;
    }

    private string GetOtherItem()
    {
        var s = new StringBuilder();
        var top = SettingsExtension.GetSettingKey(_maxItem, _lang);
        if (top.Equals("")) top = "4";
        var condition = DataExtension.AndConditon(
            ItemsTSql.GetByApp(_app),
            ItemsTSql.GetByStatus("1"),
            " Items.iiId <> " + _id + " "
            );
        condition = GroupItemsTSql.GetItemsInGroupCondition(_gid, condition);
        var dt = GroupItems.GetAllData(top, "*", condition, ItemsColumns.IiSortOrder + "," + ItemsColumns.DiDateCreated + " DESC");
        if (dt.Rows.Count < 1) return "";

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
         <div class='product_stat'>" + ProductExtension.GetCountColor(iiid) + @" Màu sắc</div>
    </div>
</a>");
        }
        s.Append("</div>");

        #endregion

        return s.ToString();
    }
}
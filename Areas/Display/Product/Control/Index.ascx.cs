using System;
using System.Collections.Generic;
using System.Text;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;


public partial class Areas_Display_Product_Control_Index : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _maxItem = SettingKey.SoProductTrenTrangChu;
    private readonly string _app = CodeApplications.Product;
    private readonly string _pic = FolderPic.Product;
    //private readonly string _noResultText = "Bài viết đang được cập nhật ...";
    protected void Page_Load(object sender, EventArgs e)
    {
        ltrList.Text = GetGroup();
    }

    protected string GetGroup()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(_app),
            GroupsTSql.GetByParentId("0"),
            GroupsTSql.GetByStatus("1")
            );
        var dt = Groups.GetData("", "*", condition, GroupsColumns.IgSortOrder);
        if (dt.Rows.Count <= 0) return s.ToString();
        s.Append("<div class='product_groups'>");
        for (var i = 0; i< dt.Rows.Count; i++)
        {
            var nameG = dt.Rows[i][GroupsColumns.VgName].ToString();
            var image = StringExtension.LayChuoi(dt.Rows[i][GroupsColumns.VgImage].ToString(), "", 1);
            if (image.Length > 0) image += "";
            var link = UrlExtension.WebsiteUrl + dt.Rows[i][GroupsColumns.VgLink].ToString().ToLower() + RewriteExtension.Extensions;
            var totalRows = 0;
            var list = GetList(dt.Rows[i][GroupsColumns.VgGenealogy].ToString(), ref totalRows);
            if (totalRows < 1) continue;
            s.Append(@"<div class='heading'><a href='" + link + "' title='" + nameG + @"'><h2>" + nameG + @"</h2><span>" + totalRows + @" sản phẩm</span></a></div>");
            s.Append(list);
        }
        s.Append("</div>");
        return s.ToString();
    }
    private string GetList(string genealogy, ref int totalRows)
    {
        var s = new StringBuilder();
        var top = SettingsExtension.GetSettingKey(_maxItem, _lang);
        if (top.Equals("")) top = "10";
        var condition = DataExtension.AndConditon(
            ItemsTSql.GetByApp(_app),
            ItemsTSql.GetByStatus("1")
            );
        condition = GroupItemsTSql.GetItemsInGroupByGenealogy(genealogy, condition);
        var orderBy = ItemsColumns.IiSortOrder + ", " + ItemsColumns.DiDateCreated + " desc";
        if (Session["OrderProduct"] != null) orderBy = Session["OrderProduct"].ToString();
        var dt = GroupItems.GetAllData(top, "*", condition, orderBy);
        totalRows = dt.Rows.Count;

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
        <div class='product_stat'> "+ ProductExtension.GetCountColor(iiid)+@" màu sắc</div>
    </div>
</a>");
        }
        s.Append("</div>");

        #endregion

        return s.ToString();
    }

   
}
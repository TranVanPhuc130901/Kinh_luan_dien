using System;
using System.Text;
using System.Web.UI.WebControls;
using Developer.Config;
using Developer.Extension;
using RevosJsc.ProductControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Product_Item_OptionUpgrade : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected readonly string Control = CodeApplications.Product;
    private readonly string _app = "OptionUpgrade";
    protected readonly string Pic = FolderPic.Product;
    private string _p = "1";
    private string _numberShowItem = "10";
    private string _key = "";
    private string _sortCookiesName = SortKey.SortProductItems + "02";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = Request.QueryString["p"];
        if (Request.QueryString["key"] != null) _key = QueryStringExtension.GetQueryString("key");
        if (Request.QueryString["NumberShowItem"] != null) _numberShowItem = QueryStringExtension.GetQueryString("NumberShowItem");
        txtTitle.Text = _key;
        if (IsPostBack) return;
        ddlShowNumber.SelectedValue = _numberShowItem;
        GetList("");
    }


    private void GetList(string order)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            ItemsTSql.GetByLang(_lang),
            ItemsTSql.GetByApp(_app)
            );
        if (txtTitle.Text.Length > 0)
        {
            condition += " AND " + ProductExtension.FullTextSearch(txtTitle.Text);
        }
        var orderBy = order.Length > 0 ? order : CookieExtension.GetCookiesSort(_sortCookiesName);
        var ds = Items.GetDataPaging(_p, _numberShowItem, condition, orderBy);
        if (ds.Tables.Count < 1) return;
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        #region Lấy ra danh sách bài viết

        if (dt.Rows.Count < 1) return;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][ItemsColumns.IiId].ToString();
            var itemTitle = dt.Rows[i][ItemsColumns.ViTitle].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var status = dt.Rows[i][ItemsColumns.IiStatus].ToString();
            var sortOrder = dt.Rows[i][ItemsColumns.IiSortOrder].ToString();
            var image = dt.Rows[i][ItemsColumns.ViImage].ToString();
            s.Append("<div id='item" + id + "' class='item inner'>");
            s.Append("<div class=\"cot1 text-center\"><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + "' /></div>");
            s.Append("<div class=\"cot2\">" + ImagesExtension.GetImage(Pic, image, itemTitle, "w90px mr5", true, false, "") + dt.Rows[i][ItemsColumns.ViTitle] + " (+"+ NumberExtension.FormatNumber(dt.Rows[i][ItemsColumns.FiPriceNew].ToString(), true, "...", "đ") +")</div>");
            s.Append("<div class=\"cot3 text-center\">" + ((DateTime)dt.Rows[i][ItemsColumns.DiDateCreated]).ToString("dd-MM-yyyy HH:mm") + "</div>");
            s.Append("<div class=\"cot4 text-center\">" + NumberExtension.FormatNumber(dt.Rows[i][ItemsColumns.IiTotalView].ToString()) + "</div>");
            s.Append("<div class=\"cot5 text-center\"><input class='form-control text-center' id='TbOrder" + id + "' type='number' min='0' value='" + sortOrder + "' onchange='UpdateOrderItems(" + id + ",this.value)' /></div>");
            s.Append("<div class=\"cot6 text-center\"><label class='switch switch-primary'><input onchange='OnOffItems(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></div>");

            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href='" + LinkAdmin.GoAdminOption(CodeApplications.Product, "OptionUpgradeEdit", "iiid", id) + "' title='Chỉnh sửa' class='btn btn-default'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteRecItem('" + Control + "','" + Pic + "','" + id + "','" + itemTitle + "')\" title=\"Xóa item\" class=\"btn btn-danger\"><i class=\"fa fa-times\"></i></a>");
            s.Append("</div>");
            s.Append("</div>");
        }
        ltrList.Text = s.ToString();

        #endregion

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), Convert.ToInt16(_numberShowItem), Convert.ToInt32(_p), LinkAdmin.UrlAdmin(CodeApplications.Product, "OptionUpgrade", "", _key, ddlShowNumber.SelectedValue), "active", "normal", "first", "last", "preview", "next");
        var from = int.Parse(_numberShowItem) * (int.Parse(_p) - 1) + 1;
        var to = dt.Rows.Count + int.Parse(_numberShowItem) * (int.Parse(_p) - 1);
        ltrPaging.Text = @"
<div class='col-sm-5 hidden-xs'>
    <div class='dataTables_info' id='ecom-products_info' role='status' aria-live='polite'>
        <strong>" + from + "</strong> - <strong>" + to + "</strong> of <strong>" + NumberExtension.FormatNumber(dtPager.Rows[0]["TotalRows"].ToString()) + @"</strong>
    </div>
</div>
<div class='col-sm-7 col-xs-12 clearfix'>
    <div class='dataTables_paginate paging_bootstrap' id='ecom-products_paginate'>
        " + split + @"
    </div>
</div>
";
        #endregion
    }
    protected void btSearch_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(LinkAdmin.UrlAdmin(_app, "OptionUpgrade", "", txtTitle.Text, ddlShowNumber.SelectedValue));
    }

    protected void lbtTitle_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(ItemsColumns.ViTitle, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtCreateDate_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(ItemsColumns.DiDateCreated, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtTotalview_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(ItemsColumns.IiTotalView, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtOrder_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(ItemsColumns.IiSortOrder, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtStatus_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(ItemsColumns.IiStatus, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }
}
using System;
using System.Text;
using Developer.Extension;
using RevosJsc.ProductControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Product_Bill_Index : System.Web.UI.UserControl
{
    private readonly string _control = CodeApplications.Product;
    private string _p = "1";
    private string _numberShowItem = "10";
    private string _key = "";
    private string _code = "";
    private string _sortCookiesName = SortKey.SortProductBill;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = Request.QueryString["p"];
        if (Request.QueryString["key"] != null) _key = QueryStringExtension.GetQueryString("key");
        if (Request.QueryString["code"] != null) _code = QueryStringExtension.GetQueryString("code");
        if (Request.QueryString["NumberShowItem"] != null) _numberShowItem = QueryStringExtension.GetQueryString("NumberShowItem");
        txtTitle.Text = _key;
        txtCode.Text = _code;
        if (IsPostBack) return;
        ddlShowNumber.SelectedValue = _numberShowItem;
        GetList("");
    }
    private string LinkBillDetails(string id)
    {
        return "/Areas/Admin/PopUp/Bill/ShowBillDetails.aspx?control=" + _control + "&iiid=" + id;
    }
    private string LinkBillDetails2(string id)
    {
        return "/Areas/Admin/PopUp/Bill/EditBillDetails.aspx?control=" + _control + "&billId=" + id;
    }

    private void GetList(string order)
    {
        var s = new StringBuilder();
        var condition = "";
        if (txtTitle.Text.Length > 0)
        {
            condition = SearchTSql.GetSearchMathedCondition(txtTitle.Text, BillsColumns.VbName, BillsColumns.VbEmail, BillsColumns.VbPhone);
        }
        if (txtCode.Text.Length > 0)
        {
            condition = DataExtension.AndConditon(condition, BillsTSql.GetByCode(txtCode.Text));
        }
        var orderBy = order.Length > 0 ? order : CookieExtension.GetCookiesSort(_sortCookiesName);
        var ds = Bills.GetBillPagging(_p, _numberShowItem, condition, orderBy);
        if (ds.Tables.Count < 1) return;
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        #region Lấy ra danh sách bài viết

        if (dt.Rows.Count < 1) return;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][BillsColumns.IbId].ToString();
            var status = dt.Rows[i][BillsColumns.IbStatus].ToString();
            var itemTitle = dt.Rows[i][BillsColumns.VbCode].ToString();
            var comment = dt.Rows[i]["vbComment"].ToString();
            var payment = StringExtension.LayChuoi(dt.Rows[i]["vbParam"].ToString(), "", 1);
            var address = dt.Rows[i]["vbAddress"].ToString();
            s.Append("<div id='item" + id + "' class='item inner'>");
            s.Append("<div class=\"cot1 text-center\"><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + "' /></div>");
            s.Append("<div class=\"cot2\">");
            s.Append("<b>Mã đơn hàng</b>: " + itemTitle + "<br/>");
            s.Append("<b>Họ tên</b>: " + dt.Rows[i][BillsColumns.VbName].ToString().Replace("-/-", " ") + "<br/>");
            s.Append("<b>Email</b>: " + dt.Rows[i][BillsColumns.VbEmail] + "<br/>");
            s.Append("<b>Điện thoại</b>: " + dt.Rows[i][BillsColumns.VbPhone] + "<br/>");
            s.Append("<b>Địa chỉ</b>: " + StringExtension.LayChuoi(address, "", 1) + " - " + StringExtension.LayChuoi(address, "", 2) + " - " + StringExtension.LayChuoi(address, "", 3) + "<br/>");
            s.Append("</div>");
            s.Append("<div class=\"cot2\">");
            //s.Append("<b>PTTT</b>: " + payment + "<br/>");
            s.Append(comment.Length > 0 ? comment.Replace("\n", "<br/>") : "");
            s.Append("</div>");
            s.Append("<div class=\"cot3 text-center\">" + ((DateTime)dt.Rows[i][BillsColumns.DbDateCreated]).ToString("dd-MM-yyyy HH:mm") + "</div>");
            s.Append("<div class=\"cot6 text-center\"><label class='switch switch-primary'><input onchange='OnOffBill(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></div>");
            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"javascript:NewWindow_('" + LinkBillDetails(id) + "','ImageList','800','500','yes','yes');\" title=\"Xem chi tiết\" class=\"btn btn-primary\"><i class=\"fa fa-info-circle\"></i></a> ");
            s.Append("<a href=\"javascript:DeleteBill('" + id + "','" + itemTitle + "')\" title=\"Xóa\" class=\"btn btn-danger\"><i class=\"fa fa-times\"></i></a>");
            s.Append("</div>");
            s.Append("</div>");
        }
        ltrList.Text = s.ToString();

        #endregion

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var keyAndCode = _key + "&code=" + _code;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), Convert.ToInt16(_numberShowItem), Convert.ToInt32(_p), LinkAdmin.UrlAdmin(_control, TypePage.Bill, "", keyAndCode, ddlShowNumber.SelectedValue), "active", "normal", "first", "last", "preview", "next");
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
        var keyAndCode = txtTitle.Text + "&code=" + txtCode.Text;
        Response.Redirect(LinkAdmin.UrlAdmin(_control, TypePage.Bill, "", keyAndCode, ddlShowNumber.SelectedValue));
    }
    protected void lbtCreateDate_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(BillsColumns.DbDateCreated, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtStatus_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(BillsColumns.IbStatus, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }
}
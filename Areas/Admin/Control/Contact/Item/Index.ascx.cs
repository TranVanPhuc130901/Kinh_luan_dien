using System;
using System.Text;
using Developer.Extension;
using RevosJsc.ContactControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Contact_Item_Index : System.Web.UI.UserControl
{
    private string _control = CodeApplications.Contact;
    private string _p = "1";
    private string _numberShowItem = "10";
    private string _key = "";
    private string _param = "";
    private string _sortCookiesName = SortKey.SortContactItems;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = Request.QueryString["p"];
        if (Request.QueryString["key"] != null) _key = QueryStringExtension.GetQueryString("key");
        if (Request.QueryString["param"] != null) _param = QueryStringExtension.GetQueryString("param");
        if (Request.QueryString["NumberShowItem"] != null) _numberShowItem = QueryStringExtension.GetQueryString("NumberShowItem");
        txtTitle.Text = _key;
        if (IsPostBack) return;
        ddlShowNumber.SelectedValue = _numberShowItem;
        ddlCategory.SelectedValue = _param;
        GetList("");
    }

    private string UrlAdmin()
    {
        return "/admin?control=Contact&action=Item&key=" + txtTitle.Text + "&NumberShowItem=" + ddlShowNumber.SelectedValue + "&param=" + ddlCategory.SelectedValue;
    }
    private string LinkContactDetail(string icdId, string subApp)
    {
        return "/Areas/Admin/PopUp/Contact/ShowContactDetail.aspx?control=" + _control + "&subapp=" + subApp + "&id=" + icdId;
    }
    private void GetList(string order)
    {
        var s = new StringBuilder();
        var condition = "";
        if (txtTitle.Text.Length > 0)
        {
            condition = SearchTSql.GetSearchMathedCondition(txtTitle.Text, "vcdName", "vcdEmail", "vcdPhone");
        }
        if (_param.Length > 0) condition = DataExtension.AndConditon(condition, "vcdSubject = N'" + _param + "'");
        var orderBy = "";
        orderBy = order.Length > 0 ? order : CookieExtension.GetCookiesSort(_sortCookiesName);
        var ds = ContactDetails.GetDataPaging(_p, _numberShowItem, condition, orderBy);
        if (ds.Tables.Count < 1) return;
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];

        #region Lấy ra danh sách bài viết

        if (dt.Rows.Count < 1) return;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i]["icdId"].ToString();
            var itemTitle = dt.Rows[i]["vcdName"].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var status = dt.Rows[i]["icdStatus"].ToString();
            var param = dt.Rows[i]["vcdParam"].ToString();
            var type = dt.Rows[i]["vcdSubject"].ToString();
            s.Append("<div id='item" + id + "' class='item inner'>");
            s.Append("<div class=\"cot1 text-center\"><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + "' /></div>");
            s.Append("<div class=\"cot2\">");
            s.Append("<b>Họ tên</b>: " + dt.Rows[i]["vcdName"] + "<br/>");
            s.Append("<b>Email</b>: " + dt.Rows[i]["vcdEmail"] + "<br/>");
            s.Append("<b>Điện thoại</b>: " + dt.Rows[i]["vcdPhone"] + "<br/>");
            s.Append("<b>Địa chỉ</b>: " + dt.Rows[i]["vcdAddress"] + "<br/>");
            s.Append("</div>");
            s.Append("<div class=\"cot2\">");
            s.Append("<b>" + GetSubjectBySubject(type) + "</b><br/>");
            s.Append(dt.Rows[i]["vcdContent"].ToString().Replace("\n", "<br/>"));
            s.Append("</div>");
            s.Append("<div class=\"cot3 text-center\">" + ((DateTime)dt.Rows[i]["dcdDateCreated"]).ToString("dd-MM-yyyy HH:mm") + "</div>");
            s.Append("<div class=\"cot6 text-center\"><label class='switch switch-primary'><input onchange='OnOffContactDetail(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></div>");
            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"javascript:NewWindow_('" + LinkContactDetail(id, type) + "','ImageList','800','500','yes','yes');\" title='Xem chi tiết' class='btn btn-primary'><i class='fa fa-info-circle'></i></a> ");
            s.Append("<a href=\"javascript:DeleteContactDetail('" + id + "','" + itemTitle + "')\" title=\"Xóa\" class=\"btn btn-danger\"><i class=\"fa fa-times\"></i></a>");
            s.Append("</div>");
            s.Append("</div>");
        }
        ltrList.Text = s.ToString();

        #endregion

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), Convert.ToInt16(_numberShowItem), Convert.ToInt32(_p), UrlAdmin(), "active", "normal", "first", "last", "preview", "next");
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
        Response.Redirect(UrlAdmin());
    }
    protected void lbtCreateDate_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort("dcdDateCreated", _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }
    private string GetSubjectBySubject(string subject)
    {
        if (subject.Equals("BookingTour")) return "Đặt tour tùy chọn";
        if (subject.Equals("RequestCallBack")) return "Yêu cầu gọi lại";
        if (subject.Equals("RequestCallBack2")) return "Yêu cầu tư vấn tour";
        if (subject.Equals("Contact2")) return "Yêu cầu tư vấn tour";
        if (subject.Equals("RequestQuote")) return "Yêu cầu báo giá";
        return "Liên hệ - Góp ý";
    }
}
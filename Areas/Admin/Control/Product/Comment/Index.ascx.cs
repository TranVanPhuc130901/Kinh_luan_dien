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

public partial class Areas_Admin_Control_Product_Comment_Index : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected readonly string Control = CodeApplications.Product;
    private readonly string _app = CodeApplications.ProductComment;
    private readonly string _subapp = CodeApplications.ProductImagesOther;
    protected readonly string Pic = FolderPic.Product;
    private string _p = "1";
    private string _numberShowItem = "10";
    private string _igid = "";
    private string _key = "";
    private string _sortCookiesName = SortKey.SortProductComment;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = Request.QueryString["p"];
        if (Request.QueryString["key"] != null) _key = QueryStringExtension.GetQueryString("key");
        if (Request.QueryString["igid"] != null) _igid = QueryStringExtension.GetQueryString("igid");
        if (Request.QueryString["NumberShowItem"] != null) _numberShowItem = QueryStringExtension.GetQueryString("NumberShowItem");
        txtTitle.Text = _key;
        if (IsPostBack) return;
        GetGroupInDdl();
        ddlShowNumber.SelectedValue = _numberShowItem;
        GetList("");
    }

    private void GetGroupInDdl()
    {
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByLang(_lang), 
            GroupsTSql.GetByApp(Control), 
            GroupsTSql.GetByStatus("1"),
            ItemsTSql.GetByApp(Control),
            ItemsTSql.GetByLang(_lang),
            ItemsTSql.GetByStatus("1")
            );
        var dt = GroupItems.GetAllData("", "*", condition, ItemsColumns.ViTitle);
        ddlCategory.Items.Clear();
        ddlCategory.Items.Add(new ListItem("Lọc theo sản phẩm", ""));
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            ddlCategory.Items.Add(new ListItem(dt.Rows[i][ItemsColumns.ViTitle].ToString(), dt.Rows[i][ItemsColumns.IiId].ToString()));
        }
        ddlCategory.SelectedValue = _igid;
    }
    private void GetList(string order)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            DataExtension.OrConditon(SubItemsTSql.GetByApp(_app), SubItemsTSql.GetByApp(_app + "2")),
            SubItemsTSql.GetByLang(_lang)
            );
        if (_igid.Length > 0) condition += " AND " + SubItemsTSql.GetByIiid(_igid);
        if (txtTitle.Text.Length > 0)
        {
            condition += " AND " + SearchTSql.GetSearchMathedCondition(txtTitle.Text, SubitemsColumns.VsiTitle, SubitemsColumns.VsiDescription, SubitemsColumns.VsiImage);
        }
        var orderBy = "";
        orderBy = order.Length > 0 ? order : CookieExtension.GetCookiesSort(_sortCookiesName);
        var ds = Subitems.GetDataPaging(_p, _numberShowItem, condition, orderBy);
        if (ds.Tables.Count < 1) return;
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        #region Lấy ra danh sách bài viết

        if (dt.Rows.Count < 1) return;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][SubitemsColumns.IsiId].ToString();
            var iiId = dt.Rows[i][SubitemsColumns.IiId].ToString();
            var itemTitle = dt.Rows[i][SubitemsColumns.VsiTitle].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var status = dt.Rows[i][SubitemsColumns.IsiStatus].ToString();
            var comment = dt.Rows[i][SubitemsColumns.VsiContent].ToString();
            var email = dt.Rows[i][SubitemsColumns.VsiDescription].ToString();
            var phone = dt.Rows[i][SubitemsColumns.VsiImage].ToString();
            s.Append("<div id='item" + id + "' class='item inner'>");
            s.Append("<div class=\"cot1 text-center\"><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + "' /></div>");
            s.Append("<div class=\"cot2\"><b>Họ tên</b>: " + itemTitle + "<br/><b>Email</b>: " + email + "<br/><b>Điện thoại</b>: " + phone + "<br/>" + comment + "</div>");
            s.Append("<div class=\"cot3 text-center\">" + ((DateTime)dt.Rows[i][SubitemsColumns.DsiDateCreated]).ToString("dd-MM-yyyy HH:mm") + "</div>");
            s.Append("<div class=\"cot6 text-center\"><label class='switch switch-primary'><input onchange='OnOffSubItems(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></div>");

            s.Append("<div class=\"cot7 btn-group-sm text-center\">");

            s.Append("<a target='_blank' href='" + dt.Rows[i][SubitemsColumns.VsiParam] + "' title='Xem bài viết' class='btn btn-default'><i class='fa fa-eye'></i></a> ");
            s.Append("<a href=\"javascript:NewWindow_('/Areas/Admin/PopUp/Items/ReplyComment.aspx?control=" + Control + "&iiid=" + iiId + "&isiid=" + id + "','ImageList','800','500','yes','yes');\" title='Trả lời bình luận' class='btn btn-default'><i class='fa fa-reply'></i></a> ");
            s.Append("<a href=\"javascript:DeleteSubItem('" + Request.RawUrl + "','" + Pic + "','" + id + "','" + itemTitle + "')\" title=\"Xóa item\" class=\"btn btn-danger\"><i class=\"fa fa-times\"></i></a>");

            s.Append("</div>");
            s.Append("</div>");
        }

        ltrList.Text = s.ToString();

        #endregion

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), Convert.ToInt16(_numberShowItem), Convert.ToInt32(_p), LinkAdmin.UrlAdmin(Control, TypePage.Comment, ddlCategory.SelectedValue, _key, ddlShowNumber.SelectedValue), "active", "normal", "first", "last", "preview", "next");
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
</div>";
        #endregion
    }
    protected void btSearch_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(LinkAdmin.UrlAdmin(Control, TypePage.Comment, ddlCategory.SelectedValue, txtTitle.Text, ddlShowNumber.SelectedValue));
    }

    protected void lbtTitle_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(SubitemsColumns.VsiTitle, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtCreateDate_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(SubitemsColumns.DsiDateCreated, _sortCookiesName);
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
        var order = CookieExtension.SetCookiesSort(SubitemsColumns.IsiStatus, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }
}
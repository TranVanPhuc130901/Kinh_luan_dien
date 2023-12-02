using System;
using System.Text;
using System.Web.UI.WebControls;
using Developer.Extension;
using RevosJsc.AdvertisingControl;
using RevosJsc.AdminControl;
using RevosJsc.AdvertistmentsControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Advertising_Item_Index : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _control = CodeApplications.Advertistments;
    protected readonly string Pic = FolderPic.Advertistments;
    private string _p = "1";
    private string _numberShowItem = "10";
    protected string IapID = "";
    private string _key = "";
    private string _sortCookiesName = SortKey.SortAdvertisingItems;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = Request.QueryString["p"];
        if (Request.QueryString["key"] != null) _key = QueryStringExtension.GetQueryString("key");
        if (Request.QueryString["iapid"] != null) IapID = QueryStringExtension.GetQueryString("iapid");
        if (Request.QueryString["NumberShowItem"] != null) _numberShowItem = QueryStringExtension.GetQueryString("NumberShowItem");
        txtTitle.Text = _key;
        if (IsPostBack) return;
        GetGroupInDdl();
        ddlShowNumber.SelectedValue = _numberShowItem;
        GetList("");
    }

    private void GetGroupInDdl()
    {
        var condition = DataExtension.AndConditon(AdvertistmentPositionsTSql.GetByLang(_lang), " iapStatus <> 2 ");
        var dt = AdvertistmentPositions.GetData("", "*", condition, "");
        ddlCategory.Items.Clear();
        ddlCategory.Items.Add(new ListItem("Lọc theo danh mục", ""));
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            ddlCategory.Items.Add(new ListItem(dt.Rows[i][AdvertistmentPositionsColumns.VapName].ToString(), dt.Rows[i][AdvertistmentPositionsColumns.IapId].ToString()));
        }
        ddlCategory.SelectedValue = IapID;
    }

    private void GetList(string order)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            IapID.Length > 0 ? AdvertistmentPositionsTSql.GetById(IapID): "",
            AdvertistmentsTSql.GetByLang(_lang),
            "iaStatus <> 2",
            "iapStatus <> 2"
            );
        if (txtTitle.Text.Length > 0)
        {
            condition += " AND " + SearchTSql.GetSearchMathedCondition(txtTitle.Text, AdvertistmentsColumns.VaTitle);
        }
        var orderBy = order.Length > 0 ? order : CookieExtension.GetCookiesSort(_sortCookiesName);
        var ds = Advertistments.GetAllDataPaging(_p, _numberShowItem, condition, orderBy);
        if (ds.Tables.Count < 1) return;
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        #region Lấy ra danh sách bài viết

        if (dt.Rows.Count < 1) return;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][AdvertistmentsColumns.IaId].ToString();
            var itemTitle = dt.Rows[i][AdvertistmentsColumns.VaTitle].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var status = dt.Rows[i][AdvertistmentsColumns.IaStatus].ToString();
            var sortOrder = dt.Rows[i][AdvertistmentsColumns.IaSortOrder].ToString();
            s.Append("<div id='item" + id + "' class='item inner'>");
            s.Append("<div class=\"cot1 text-center\"><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + "' /></div>");
            s.Append("<div class=\"cot2\">" + ImagesExtension.GetImage(Pic, dt.Rows[i][AdvertistmentsColumns.VaImage].ToString(), itemTitle, "w90px mr5", true, false, "") + dt.Rows[i][AdvertistmentsColumns.VaTitle] + "</div>");
            s.Append("<div class=\"cot3 text-center\">" + ((DateTime)dt.Rows[i][AdvertistmentsColumns.DaDateCreated]).ToString("dd-MM-yyyy HH:mm") + "</div>");
            s.Append("<div class=\"cot5 text-center\"><input class='form-control text-center' id='TbOrder" + id + "' type='number' min='0' value='" + sortOrder + "' onchange='UpdateOrderAdvertistment(" + id + ",this.value)' /></div>");
            s.Append("<div class=\"cot6 text-center\"><label class='switch switch-primary'><input onchange='OnOffAdvertistment(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></div>");

            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href='" + LinkAdmin.GoAdminOption(CodeApplications.Advertistments, TypePage.UpdateItem, "iaid", id) + "' title='Chỉnh sửa' class='btn btn-default'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteAdvertistment('" + id + "','" + itemTitle + "')\" title=\"Xóa item\" class=\"btn btn-warning\"><i class=\"fa fa-times\"></i></a>");
            s.Append("</div>");
            s.Append("</div>");
        }
        ltrList.Text = s.ToString();

        #endregion

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), Convert.ToInt16(_numberShowItem), Convert.ToInt32(_p), "/admin?control=Advertistments&action=Item&iapid=" + ddlCategory.SelectedValue + "&key="+ _key +"&NumberShowItem=" + ddlShowNumber.SelectedValue, "active", "normal", "first", "last", "preview", "next");
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
        Response.Redirect("/admin?control=Advertistments&action=Item&iapid="+ ddlCategory.SelectedValue + "&key="+ txtTitle.Text +"&NumberShowItem="+ ddlShowNumber.SelectedValue);
    }
    protected void lbtTitle_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(AdvertistmentsColumns.VaTitle, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtCreateDate_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(AdvertistmentsColumns.DaDateCreated, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtOrder_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(AdvertistmentsColumns.IaSortOrder, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtStatus_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(AdvertistmentsColumns.IaStatus, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }
}
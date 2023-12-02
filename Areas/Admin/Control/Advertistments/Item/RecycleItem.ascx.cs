using System;
using System.Text;
using System.Web.UI.WebControls;
using Developer.Extension;
using RevosJsc.AdvertisingControl;
using RevosJsc.AdvertistmentsControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Advertising_Item_RecycleItem : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected readonly string Control = CodeApplications.Advertistments;
    private readonly string _app = CodeApplications.Advertistments;
    protected readonly string Pic = FolderPic.Advertistments;
    private string _p = "1";
    private string _numberShowItem = "10";
    private string _iapid = "";
    private string _key = "";
    private string _sortCookiesName = SortKey.SortAdvertisingItems;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = Request.QueryString["p"];
        if (Request.QueryString["key"] != null) _key = QueryStringExtension.GetQueryString("key");
        if (Request.QueryString["iapid"] != null) _iapid = QueryStringExtension.GetQueryString("iapid");
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
        ddlCategory.SelectedValue = _iapid;
    }
    private void GetList(string order)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            _iapid.Length > 0 ? AdvertistmentPositionsTSql.GetById(_iapid) : "",
            AdvertistmentsTSql.GetByLang(_lang),
            AdvertistmentsTSql.GetByStatus("2")
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
            s.Append("<div id='item-" + id + "' class='item inner'>");
            s.Append("<div class=\"cot1 text-center\"><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + "' /></div>");
            s.Append("<div class=\"cot2\">" + ImagesExtension.GetImage(Pic, dt.Rows[i][AdvertistmentsColumns.VaImage].ToString(), itemTitle, "w90px mr5", true, false, "") + dt.Rows[i][AdvertistmentsColumns.VaTitle] + "</div>");
            s.Append("<div class=\"cot3 text-center\">" + ((DateTime)dt.Rows[i][AdvertistmentsColumns.DaDateCreated]).ToString("dd-MM-yyyy HH:mm") + "</div>");

            s.Append("<div class='cot5 btn-group-sm text-center'>");
            s.Append("<a href=\"javascript:RestoreAdvertistment('" + id + "','" + itemTitle + "')\"; title='Khôi phục' class='btn btn-success'><i class='gi gi-refresh'></i></a> ");
            s.Append("<a href=\"javascript:DeleteRecAdvertistment('" + id + "','" + itemTitle + "')\" title=\"Xóa item\" class=\"btn btn-danger\"><i class=\"fa fa-times\"></i></a>");
            s.Append("</div>");
            s.Append("</div>");
        }
        ltrList.Text = s.ToString();

        #endregion

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), Convert.ToInt16(_numberShowItem), Convert.ToInt32(_p), "/admin?control=Advertistments&action=RecycleItem&iapid=" + ddlCategory.SelectedValue + "&key=" + _key + "&NumberShowItem=" + ddlShowNumber.SelectedValue, "active", "normal", "first", "last", "preview", "next");
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
        Response.Redirect("/admin?control=Advertistments&action=RecycleItem&iapid=" + ddlCategory.SelectedValue + "&key=" + txtTitle.Text + "&NumberShowItem=" + ddlShowNumber.SelectedValue);
    }

}
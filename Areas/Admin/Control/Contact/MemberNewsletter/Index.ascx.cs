using System;
using System.Text;
using Developer.Extension;
using RevosJsc.MemberControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Member_NewsLetter_Index : System.Web.UI.UserControl
{
    //private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected readonly string Control = "Contact";
    protected readonly string Action = "MemberNewsletter";
    private readonly string _app = CodeApplications.MemberNewsletter;
    protected readonly string Pic = FolderPic.Member;
    private string _p = "1";
    private string _numberShowItem = "20";
    private string _key = "";
    private string _other = "";
    private string _sortCookiesName = SortKey.SortMemberItems;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = Request.QueryString["p"];
        if (Request.QueryString["key"] != null) _key = QueryStringExtension.GetQueryString("key");
        if (Request.QueryString["other"] != null) _other = QueryStringExtension.GetQueryString("other");
        if (Request.QueryString["NumberShowItem"] != null) _numberShowItem = QueryStringExtension.GetQueryString("NumberShowItem");
        txtTitle.Text = _key;
        txtOther.Text = _other;
        if (IsPostBack) return;
        ddlShowNumber.SelectedValue = _numberShowItem;
        GetList("");
    }

    private void GetList(string order)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            MembersTSql.GetByApp(_app),
            "Members.imStatus <> 2"
            );
        if (txtTitle.Text.Length > 0)
        {
            condition += " AND " + SearchTSql.GetSearchMathedCondition(txtTitle.Text, MembersColumns.VmAccount);
        }
        if (txtOther.Text.Length > 0)
        {
            condition += " AND " + SearchTSql.GetSearchMathedCondition(txtOther.Text, MembersColumns.VmEmail, MembersColumns.VmPhone);
        }
        var orderBy = "";
        orderBy = order.Length > 0 ? order : CookieExtension.GetCookiesSort(_sortCookiesName);
        var ds = Members.GetDataPaging(_p, _numberShowItem, condition, orderBy);
        if (ds.Tables.Count < 1) return;
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        #region Lấy ra danh sách bài viết

        if (dt.Rows.Count < 1) return;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][MembersColumns.ImId].ToString();
            var itemTitle = dt.Rows[i][MembersColumns.VmEmail].ToString();
            var status = dt.Rows[i][MembersColumns.ImStatus].ToString();
            var image = dt.Rows[i][MembersColumns.VmImage].ToString();
            s.Append("<div id='item-" + id + "' class='item inner'>");
            s.Append("<div class=\"cot1 text-center\"><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + "' /></div>");
            s.Append("<div class=\"cot2\">" + itemTitle + "</div>");
            s.Append("<div class=\"cot3 text-center\">" + ((DateTime)dt.Rows[i][MembersColumns.DmDateCreated]).ToString("dd-MM-yyyy HH:mm") + "</div>");
            s.Append("<div class=\"cot6 text-center\"><label class='switch switch-primary'><input onchange='OnOffMember(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></div>");
            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"javascript:DeleteRecMember('" + Action + "','" + id + "','" + itemTitle + "','" + Pic + "')\" title=\"Xóa item\" class=\"btn btn-danger\"><i class=\"fa fa-times\"></i></a>");
            s.Append("</div>");
            s.Append("</div>");
        }
        ltrList.Text = s.ToString();

        #endregion

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var fullKey = _key + "&other=" + _other;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), Convert.ToInt16(_numberShowItem), Convert.ToInt32(_p), LinkAdmin.UrlAdmin(Control, Action, "", fullKey, ddlShowNumber.SelectedValue), "active", "normal", "first", "last", "preview", "next");
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
        var fullKey = txtTitle.Text + "&other=" + txtOther.Text;
        Response.Redirect(LinkAdmin.UrlAdmin(Control, Action, "", fullKey, ddlShowNumber.SelectedValue));
    }
    protected void lbtTitle_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(MembersColumns.VmAccount, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtCreateDate_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(MembersColumns.DmDateCreated, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtDateOfBirth_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(MembersColumns.DmBirthday, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtStatus_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(MembersColumns.ImStatus, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

}
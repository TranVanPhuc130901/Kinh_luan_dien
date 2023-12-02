using System;
using System.Text;
using Developer.Extension;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.RedirectsControl;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Redirect_Link_Index : System.Web.UI.UserControl
{
    private string _p = "1";
    private string _numberShowItem = "10";
    private string _link = "", _des = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = Request.QueryString["p"];
        if (Request.QueryString["NumberShowItem"] != null) _numberShowItem = QueryStringExtension.GetQueryString("NumberShowItem");
        if (Request.QueryString["link"] != null) _link = Request.QueryString["link"];
        if (Request.QueryString["des"] != null) _des = Request.QueryString["des"];
        if (IsPostBack) return;
        tbLink.Text = _link;
        tbDes.Text = _des;
        ddlShowNumber.SelectedValue = _numberShowItem;
        GetList();
    }
    private void GetList()
    {
        var s = new StringBuilder();
        var condition = "IrStatus <> 2";
        if (tbLink.Text.Length > 0)
        {
            condition += " AND " + SearchTSql.GetSearchMathedCondition(tbLink.Text, RedirectsColumns.VrLink);
        }
        if (tbDes.Text.Length > 0)
        {
            condition += " AND " + SearchTSql.GetSearchMathedCondition(tbDes.Text, RedirectsColumns.VrLinkDestination);
        }
        var ds = Redirects.GetDataPaging(_p, _numberShowItem, condition, "");
        if (ds.Tables.Count < 1) return;
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        #region Lấy ra danh sách bài viết

        if (dt.Rows.Count < 1) return;
        s.Append("<tbody>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][RedirectsColumns.IrId].ToString();
            var vrLink = dt.Rows[i][RedirectsColumns.VrLink].ToString();
            var status = dt.Rows[i][RedirectsColumns.IrStatus].ToString();
            s.Append(@"
<tr id='item"+ id + @"'>
    <td class='text-center'><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + @"' /></td>
    <td><a target='_blank' href='" + dt.Rows[i][RedirectsColumns.VrLink] + @"'>" + dt.Rows[i][RedirectsColumns.VrLink] + @"</a> <i class='fa fa-caret-right'></i> " + dt.Rows[i][RedirectsColumns.VrLinkDestination] + @"</td>
    <td class='text-center'><label class='switch switch-primary'><input onchange='OnOffRedirect(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") +@"><span></span></label></td>
    <td class='text-center'>
        <div class='btn-group-sm'>
            <a target='_blank' href='" + dt.Rows[i][RedirectsColumns.VrLink] + @"' title='Xem nhanh' class='btn btn-default'><i class='fa fa-eye'></i></a>
            <a href='" + LinkAdmin.GoAdminOption(CodeApplications.Redirects, TypePage.Update, "id", id) + @"' title='Chỉnh sửa' class='btn btn-default'><i class='fa fa-pencil'></i></a>
            <a href=javascript:DeleteRedirect(" + id + ",'" + vrLink + @"') title='Xóa link' class='btn btn-warning'><i class='fa fa-times'></i></a>
        </div>
    </td>
</tr>");
        }
        s.Append("</tbody>");
        ltrList.Text = s.ToString();

        #endregion

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), Convert.ToInt16(_numberShowItem), Convert.ToInt32(_p), LinkAdmin.UrlAdminRedirect(tbLink.Text, tbDes.Text, ddlShowNumber.SelectedValue), "active", "normal", "first", "last", "preview", "next");
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
        Search();
    }

    protected void ddlShowNumber_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        Response.Redirect(LinkAdmin.UrlAdminRedirect(tbLink.Text, tbDes.Text, ddlShowNumber.SelectedValue));
    }
}
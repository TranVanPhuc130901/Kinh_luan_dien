using System;
using System.Text;
using Developer.Extension;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.SystemWebsiteControl;

public partial class Areas_Admin_Control_SystemWebsite_Action_Logs : System.Web.UI.UserControl
{
    private string _p = "1";
    private readonly string _ni = "20";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = Request.QueryString["p"];
        if (IsPostBack) return;
        GetList();
    }
    private void GetList()
    {
        var s = new StringBuilder();
        var ds = Logs.GetDataPaging(_p, _ni, "", "");
        if (ds.Tables.Count < 1) return;
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        #region Lấy ra danh sách bài viết

        if (dt.Rows.Count < 1) return;
        s.Append("<tbody>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            s.Append(@"
<tr class='"+ (i % 2 == 0 ? "odd":"even") +@"'>
    <td>"+ dt.Rows[i][LogsColumns.VlDescription] + @" - <a target='_blank' href='" + dt.Rows[i][LogsColumns.VlUrl] + @"'>" + dt.Rows[i][LogsColumns.VlUrl] + @"</a></td>
</tr>");
        }
        s.Append("</tbody>");
        ltrList.Text = s.ToString();

        #endregion

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), Convert.ToInt16(_ni), Convert.ToInt32(_p), LinkAdmin.UrlAdmin(CodeApplications.Systemwebsite, TypePage.Logs, "", "", _ni), "active", "normal", "first", "last", "preview", "next");
        var from = int.Parse(_ni) * (int.Parse(_p) - 1) + 1;
        var to = dt.Rows.Count + int.Parse(_ni) * (int.Parse(_p) - 1);
        ltrPaging.Text = ltrPaging2.Text = @"
<div class='col-sm-5 hidden-xs'>
    <div class='dataTables_info' id='ecom-products_info' role='status' aria-live='polite'><strong>" + from +"</strong> - <strong>"+ to +"</strong> of <strong>"+ NumberExtension.FormatNumber(dtPager.Rows[0]["TotalRows"].ToString()) +@"</strong></div>
</div>
<div class='col-sm-7 col-xs-12 clearfix'>
    <div class='dataTables_paginate paging_bootstrap' id='ecom-products_paginate'>
        "+ split + @"
    </div>
</div>
";

        #endregion
    }
}
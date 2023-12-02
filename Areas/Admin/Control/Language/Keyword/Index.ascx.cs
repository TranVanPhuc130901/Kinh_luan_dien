using System;
using System.Text;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.LanguageControl;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Language_Keyword_Index : System.Web.UI.UserControl
{
    private string _key = "";
    private readonly string _sortCookiesName = SortKey.SortKeyword;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["key"] != null) _key = QueryStringExtension.GetQueryString("key");
        txtTitle.Text = _key;
        if (IsPostBack) return;

        GetList("");
    }

    private void GetList(string order)
    {
        var s = new StringBuilder();
        var condition = "";
        if (txtTitle.Text.Length > 0)
        {
            condition = SearchTSql.GetSearchMathedCondition(txtTitle.Text, KeywordsColumns.VkTitle);
        }
        var orderBy = order.Length > 0 ? order : CookieExtension.GetCookiesSort(_sortCookiesName);
        var dt = Keywords.GetData("", "*", condition, orderBy);
        #region Lấy ra danh sách bài viết

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][KeywordsColumns.IkId].ToString();
            //var desc = dt.Rows[i][KeywordColumns.VkDescription].ToString();
            var itemTitle = dt.Rows[i][KeywordsColumns.VkTitle].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            s.Append("<div id='item" + id + "' class='item inner'>");
            s.Append("<div class=\"cot1 text-center\"><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + "' /></div>");
            s.Append("<div class=\"cot2\">" + dt.Rows[i][KeywordsColumns.VkTitle] + "</div>");
            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href='" + LinkAdmin.GoAdminOption(CodeApplications.Language, TypePage.AddEditKeyword, "ikid", id) + "' title='Chỉnh sửa' class='btn btn-default'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteKeyword('" + id + "','" + itemTitle + "')\" title=\"Xóa item\" class=\"btn btn-danger\"><i class=\"fa fa-times\"></i></a>");
            s.Append("</div>");
            s.Append("</div>");
        }

        #endregion Lấy ra danh sách bài viết

        ltrList.Text = s.ToString();
    }

    protected void btSearch_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(LinkAdmin.UrlAdmin(CodeApplications.Language, TypePage.Keyword, ddlCategory.SelectedValue, txtTitle.Text, ddlShowNumber.SelectedValue));
    }

    protected void lbtTitle_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(KeywordsColumns.VkTitle, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }
}
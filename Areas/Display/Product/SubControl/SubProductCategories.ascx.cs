using System;
using System.Text;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;

public partial class Areas_Display_Product_SubControl_SubProductCategories : System.Web.UI.UserControl
{
    private string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private string _app = CodeApplications.Product;
    private string _igid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["igid"] != null) _igid = Request.QueryString["igid"];
        #region title

        if (Request.QueryString["title"] != null)
        {
            //Lấy igid từ session ra vì nó đã dược lưu khi xét tại Default.aspx
            if (_igid.Length < 1 && Session["igid"] != null) _igid = Session["igid"].ToString();
        }

        #endregion title
        ltrList.Text = GetAllGroups();
        if (ltrList.Text == "") Visible = false;
    }

    private string GetAllGroups()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByLang(_lang),
            GroupsTSql.GetByApp(_app),
            GroupsTSql.GetByParentId("0"),
            GroupsTSql.GetByStatus("1")
            );
        var dt = Groups.GetData("", "*", condition, GroupsColumns.IgSortOrder);
        if (dt.Rows.Count < 1) return "";
        s.Append("<div class='product_categories'>");
        s.Append("<div class='label active'>Danh mục</div>");
        s.Append("<ul>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][GroupsColumns.VgName].ToString();
            var link = (UrlExtension.WebsiteUrl + dt.Rows[i][GroupsColumns.VgLink] + RewriteExtension.Extensions).ToLower();
            s.Append("<li class='" + (dt.Rows[i][GroupsColumns.IgId].ToString() == _igid ? "active" : "nm") + "'><a href='" + link + "' title='" + titleItem + "'>" + titleItem + "</a></li>");
        }
        s.Append("</ul>");
        s.Append("</div>");
        return s.ToString();
    }
}
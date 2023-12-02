using System;
using System.Text;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.AboutUsControl;
using RevosJsc.TSql;

public partial class Areas_Display_News_SubControl_SubNewsCate : System.Web.UI.UserControl
{
    private string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private string _app = CodeApplications.AboutUs;
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
        s.Append("<ul class='about_us_categories'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][GroupsColumns.VgName].ToString();
            var link = (UrlExtension.WebsiteUrl + dt.Rows[i][GroupsColumns.VgLink] + RewriteExtension.Extensions).ToLower();
            var active = dt.Rows[i][GroupsColumns.IgId].ToString() == _igid;
            s.Append("<li class='" + (active ? "active" : "nm") + "'><a" + (active ? "" : " href='" + link + "'") + " title='" + titleItem + "'>" + titleItem + "</a></li>");
        }
        s.Append("</ul>");
        return s.ToString();
    }
}
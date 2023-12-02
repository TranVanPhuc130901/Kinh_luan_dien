using System;
using System.Text;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.MenusControl;
using RevosJsc.TSql;

public partial class Areas_Display_Component_MenuMain : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _app = CodeApplications.MenuMain;
    private readonly string _pic = FolderPic.Menus;
    private string _igid = "";
    private string _rewrite = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["igid"] != null) _igid = Request.QueryString["igid"];
        if (Request.QueryString["rewrite"] != null) _rewrite = Request.QueryString["rewrite"];
        if (_rewrite == "" && Session["rewrite"] != null) _rewrite = StringExtension.RemoveSqlInjectionChars(Session["rewrite"].ToString());
        #region title
        if (Request.QueryString["title"] != null)
        {
            if (_igid.Length < 1 && Session["igid"] != null) _igid = Session["igid"].ToString();
        }
        #endregion title
        ltrList.Text = GetList();
    }

    private string GetList()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            MenusTSql.GetByApp(_app),
            MenusTSql.GetByParentId("0"),
            MenusTSql.GetByStatus("1")
        );
        var dt = Menus.GetData("", "*", condition, MenusColumns.ImnSortOrder);
        if (dt.Rows.Count < 1) return "";
        s.Append("<ul class='navigation_primary'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var name = dt.Rows[i][MenusColumns.VmnName].ToString();
            var image = dt.Rows[i][MenusColumns.VmnImage].ToString();
            if (image.Length > 0) image = image + ".ashx?w=20";
            var link = RewriteExtension.GetLinkMenu(dt.Rows[i][MenusColumns.VmnLink].ToString());
            var target = dt.Rows[i][MenusColumns.ImnTarget].ToString().Equals("1") ? "target='_blank'" : "target='_self'";
            var id = MenuExtension.GetQueryString(dt.Rows[i][MenusColumns.VmnLink].ToString(), "igid");
            var rw = MenuExtension.GetQueryString(dt.Rows[i][MenusColumns.VmnLink].ToString(), "rewrite");
            var active = "";
            var list = GetSub(dt.Rows[i][MenusColumns.ImnId].ToString(), ref active);
            s.Append("<li class='" + (id == _igid && _igid.Length > 0 || active == "current-menu-item" || rw == _rewrite && _igid == "" && id == "" && _rewrite!= "" ? "current-menu-item" : "") + "'>");
            s.Append("<a href='" + link + "' " + target + ">" + name + "</a>");
            s.Append(list);
            s.Append("</li>");
        }
        s.Append("</ul>");
        return s.ToString();
    }

    private string GetSub(string parent, ref string active)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            MenusTSql.GetByApp(_app),
            MenusTSql.GetByStatus("1"),
            MenusTSql.GetByParentId(parent)
        );
        var dt = Menus.GetData("", "*", condition, MenusColumns.ImnSortOrder);
        if (dt.Rows.Count < 1) return s.ToString();
        s.Append("<ul class='navigation_second'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var name = dt.Rows[i][MenusColumns.VmnName].ToString();
            var link = RewriteExtension.GetLinkMenu(dt.Rows[i][MenusColumns.VmnLink].ToString());
            var target = dt.Rows[i][MenusColumns.ImnTarget].ToString().Equals("1") ? "target='_blank'" : "target='_self'";
            var id = MenuExtension.GetQueryString(dt.Rows[i][MenusColumns.VmnLink].ToString(), "igid");
            var rw = MenuExtension.GetQueryString(dt.Rows[i][MenusColumns.VmnLink].ToString(), "rewrite");
            if (id == _igid && _igid.Length > 0 || id == "" && rw == _rewrite && _igid.Length > 0) active = "active";
            var list = GetSub2(dt.Rows[i][MenusColumns.ImnId].ToString());
            s.Append("<li>");
            s.Append("<a href='" + link + "' " + target + ">" + name + "</a>");
            s.Append(list);
            s.Append("</li>");
        }
        s.Append("</ul>");
        return s.ToString();
    }
    private string GetSub2(string parent)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            MenusTSql.GetByApp(_app),
            MenusTSql.GetByStatus("1"),
            MenusTSql.GetByParentId(parent)
        );
        var dt = Menus.GetData("", "*", condition, MenusColumns.ImnSortOrder);
        if (dt.Rows.Count < 1) return s.ToString();
        s.Append("<ul class='navigation_tertiary'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var name = dt.Rows[i][MenusColumns.VmnName].ToString();
            var link = RewriteExtension.GetLinkMenu(dt.Rows[i][MenusColumns.VmnLink].ToString());
            var target = dt.Rows[i][MenusColumns.ImnTarget].ToString().Equals("1") ? "target='_blank'" : "target='_self'";
            s.Append("<li>");
            s.Append("<a href='" + link + "' " + target + ">" + name + "</a>");
            s.Append("</li>");
        }
        s.Append("</ul>");
        return s.ToString();
    }
}
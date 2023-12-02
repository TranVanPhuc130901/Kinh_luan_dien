using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.MenusControl;
using RevosJsc.TSql;

public partial class Areas_Display_Component_ProductCategories : System.Web.UI.UserControl
{
    private readonly string _pic = FolderPic.Menus;
    private readonly string _app = CodeApplications.MenuOther;
    protected string Rewrite = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["rewrite"] != null) Rewrite = QueryStringExtension.GetQueryString("rewrite");
        if (Rewrite.Length < 1 && Session["rewrite"] != null) Rewrite = Session["rewrite"].ToString();
        ltrList.Text = GetList();
        if (ltrList.Text == "") Visible = false;
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
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var name = dt.Rows[i][MenusColumns.VmnName].ToString();
            var image = dt.Rows[i][MenusColumns.VmnImage].ToString();
            if (image.Length > 0) image += ".ashx?w=25&quantity=80";
            var link = RewriteExtension.GetLinkMenu(dt.Rows[i][MenusColumns.VmnLink].ToString());
            var target = dt.Rows[i][MenusColumns.ImnTarget].ToString().Equals("1") ? "target='_blank'" : "target='_self'";
            var list = GetSubMenu(dt.Rows[i][MenusColumns.ImnId].ToString());
            s.Append(list.Length > 0 ? "<li class='hasSub' onclick='toggleThis(this)'>" : "<li>");
            s.Append("<a href='" + link + "' " + target + ">");
            s.Append("<span class='wImage0'>" + ImagesExtension.GetImage(_pic, image, name, "lazy", false, false, "", false, true) + "</span>");
            s.Append("<span class='title'>" + name + "</span>");
            s.Append("</a>");
            s.Append(list);
            s.Append("</li>");
        }
        return s.ToString();
    }

    private string GetSubMenu(string parent)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            MenusTSql.GetByApp(_app),
            MenusTSql.GetByParentId(parent),
            MenusTSql.GetByStatus("1")
        );
        var dt = Menus.GetData("", "*", condition, MenusColumns.ImnSortOrder);
        if (dt.Rows.Count < 1) return "";
        s.Append("<div class='subCate'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var name = dt.Rows[i][MenusColumns.VmnName].ToString();
            var link = RewriteExtension.GetLinkMenu(dt.Rows[i][MenusColumns.VmnLink].ToString());
            var target = dt.Rows[i][MenusColumns.ImnTarget].ToString().Equals("1") ? "target='_blank'" : "target='_self'";
            s.Append("<a href='" + link + "' " + target + ">" + name + "</a>");
        }
        s.Append("</div>");
        return s.ToString();
    }
}
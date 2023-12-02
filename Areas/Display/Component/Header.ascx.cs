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
using RevosJsc.MenusControl;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Display_Component_Header : System.Web.UI.UserControl
{
    private readonly string _app = CodeApplications.MenuTop;


    protected string Rewrite = "";

    protected string Key = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["rewrite"] != null) Rewrite = QueryStringExtension.GetQueryString("rewrite");
        if (Rewrite.Length < 1 && Session["rewrite"] != null) Rewrite = Session["rewrite"].ToString();


        ltrPopularSearch.Text = GetList();
    }

    // Lấy danh sách MenuTop(Popular search)
    private string GetList()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            MenusTSql.GetByApp(_app),
            MenusTSql.GetByStatus("1")
        );

        var dt = Menus.GetData("", "*", condition, MenusColumns.ImnSortOrder);
        if (dt.Rows.Count < 1) return "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            var name = dt.Rows[i][MenusColumns.VmnName].ToString();
            var image = dt.Rows[i][MenusColumns.VmnImage].ToString();
            if (image.Length > 0) image = image + ".ashx?w=20";
            var link = RewriteExtension.GetLinkMenu(dt.Rows[i][MenusColumns.VmnLink].ToString());
            var target = dt.Rows[i][MenusColumns.ImnTarget].ToString().Equals("1") ? "target='_blank'" : "target='_self'";
            var id = MenuExtension.GetQueryString(dt.Rows[i][MenusColumns.VmnLink].ToString(), "igid");
            var rw = MenuExtension.GetQueryString(dt.Rows[i][MenusColumns.VmnLink].ToString(), "rewrite");
            var active = "";
            s.Append("<a class='item-popular' href="+ link + " " + target + ">" + name + "</a>");
        }

        return s.ToString();
    }

}
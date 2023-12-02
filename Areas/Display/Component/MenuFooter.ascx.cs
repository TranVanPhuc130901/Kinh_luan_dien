using System;
using System.Text;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.MenusControl;
using RevosJsc.TSql;

public partial class Areas_Display_Component_MenuFooter : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _app = CodeApplications.MenuBottom;
    private readonly string _pic = RevosJsc.AdvertistmentsControl.FolderPic.Advertistments;

    protected void Page_Load(object sender, EventArgs e)
    {
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
        s.Append("<div class='inner'>");
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
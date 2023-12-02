using System;
using System.Text;
using RevosJsc.AdminControl;
using RevosJsc.MenusControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Menu_RecycleMenu : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected string LinkCreate = "";
    protected string App = "";
    protected string Pic = FolderPic.Menus;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["app"] != null) App = Request.QueryString["app"];
        LinkCreate = LinkAdmin.GoAdminOption("Menus", TypePage.Create, "app", App);
        if (IsPostBack) return;
        ltrList.Text = GetCate();
    }
    private string GetCate()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            MenusTSql.GetByApp(App),
            MenusTSql.GetByLang(_lang),
            MenusTSql.GetByStatus("2")
        );
        var dt = Menus.GetData("", "*", condition, "VmnGenealogy");

        if (dt.Rows.Count <= 0) return "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][MenusColumns.ImnId].ToString();
            var name = dt.Rows[i][MenusColumns.VmnName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            s.Append(dt.Rows[i][MenusColumns.VmnName]);
            s.Append("</div>");

            s.Append("<div class=\"cot3\">" + GetRoadMenu(id) + @"</div>");

            s.Append("<div class=\"cot5 btn-group-sm text-center\">");
            s.Append("<a href=\"javascript:RestoreMenu('" + App + "','" + id + "','" + name + "')\"; title='Khôi phục' class='btn btn-success'><i class='gi gi-refresh'></i></a> ");
            s.Append("<a href=\"javascript:DeleteRecMenu('" + App + "','" + id + "','" + name + "','"+ Pic + "');\" title='Xóa vĩnh viễn' class='btn btn-danger'><i class='fa fa-times'></i></a>");
            s.Append("</div>");
            s.Append("</div>");
            s.Append("</div>");

            //igidParent = id;
            //s.Append("<div id=\"" + id + "\"  style=\"display:none\">");
            //s.Append(GetSubCate(id));
            //s.Append("</div>");

        }
        return s.ToString();
    }
    private string GetRoadMenu(string igid)
    {
        var str = new StringBuilder();
        var fields = DataExtension.GetListColumns(MenusColumns.ImnParentId, MenusColumns.VmnName);
        var condition = DataExtension.AndConditon(MenusTSql.GetByApp(App), MenusTSql.GetById(igid));
        var current = "";
        var dt = Menus.GetData("1", fields, condition, "");
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][MenusColumns.ImnParentId].ToString().Equals("0"))
            {
                str.Append(dt.Rows[0][MenusColumns.VmnName] + " / ");
            }
            else
            {
                str.Append(GetRoadMenu(dt.Rows[0][MenusColumns.ImnParentId].ToString()));
                current = dt.Rows[0][MenusColumns.VmnName].ToString();
            }
        }
        else
        {
            str.Append("/");
        }
        str.Append(current);
        return str.ToString();
    }
}
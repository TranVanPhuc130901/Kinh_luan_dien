using System;
using System.Text;
using RevosJsc.MenusControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Menu_Index : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private string _control = "Menus";
    private string _txtLevel = "";
    protected string LinkCreate = "";
    protected string App = "";

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
            MenusTSql.GetByParentId("0"),
            " imnStatus <> '2' "
        );
        var dt = Menus.GetData("", "*", condition, MenusColumns.ImnSortOrder + "," + MenusColumns.VmnName);

        if (dt.Rows.Count <= 0) return "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][MenusColumns.ImnId].ToString();
            var name = dt.Rows[i][MenusColumns.VmnName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var order = dt.Rows[i][MenusColumns.ImnSortOrder].ToString();
            var status = dt.Rows[i][MenusColumns.ImnStatus].ToString();
            var subtotal = "";
            var list = GetSubCate(id, ref subtotal);
            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            if (list.Length > 0)
            {
                s.Append("<a id=\"showhide" + id + "\" href=\"javascript:void(0)\" onclick=\"ShowHideGroup('" + id + "');\"><i class='hi hi-plus'></i></a> ");
            }
            else s.Append("<i class='hi hi-plus' style='color:transparent'></i>");
            s.Append(dt.Rows[i][MenusColumns.VmnName]);
            s.Append("</div>");

            s.Append("<div class=\"cot4 text-center\">" + subtotal + @"</div>");

            s.Append("<div class=\"cot5 text-center\"><input class='form-control text-center' id=\"TbOrder" + id + "\" type=\"number\" min=\"0\" value=\"" + order + "\" onchange=\"UpdateOrderMenu(" + id + ",this.value)\" /></div>");

            s.Append("<div class=\"cot6 text-center\"><label class=\"switch switch-primary\"><input onchange=\"OnOffMenu(" + id + ")\" type=\"checkbox\" " + (status.Equals("1") ? "checked" : "") + @"><span></span></label></div>");

            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"" + LinkAdmin.GoAdminMenu(_control, TypePage.Update, App, id) + "\" title=\"Chỉnh sửa\" class='btn btn-default'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteMenu('" + App + @"','" + id + @"','" + name + "')\"; title='Xóa danh mục' class='btn btn-warning'><i class='fa fa-times'></i></a>");
            s.Append("</div>");
            s.Append("</div>");
            if (list.Length > 0) s.Append(list);
            s.Append("</div>");

            //igidParent = id;
            //s.Append("<div id=\"" + id + "\"  style=\"display:none\">");
            //s.Append(GetSubCate(id));
            //s.Append("</div>");
        
        }
        return s.ToString();
    }
    public string GetSubCate(string parentId, ref string subtotal)
    {
        var condition = DataExtension.AndConditon(
            MenusTSql.GetByApp(App),
            MenusTSql.GetByLang(_lang),
            MenusTSql.GetByParentId(parentId),
            " imnStatus <> '2' "
            );
        var dt = Menus.GetData("", "*", condition, MenusColumns.ImnSortOrder + "," + MenusColumns.VmnName);
        subtotal = dt.Rows.Count.ToString();
        var s = new StringBuilder();
        if (dt.Rows.Count <= 0) return "";
        s.Append("<div id='"+ parentId + "' style='display:none'>");
        _txtLevel += "...";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][MenusColumns.ImnId].ToString();
            var name = dt.Rows[i][MenusColumns.VmnName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var order = dt.Rows[i][MenusColumns.ImnSortOrder].ToString();
            var status = dt.Rows[i][MenusColumns.ImnStatus].ToString();
            var subtotal2 = "";
            var list = GetSubCate(id, ref subtotal2);

            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + parentId + "-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + parentId + "-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            if (list.Length > 0)
            {
                s.Append("<a id=\"showhide" + id + "\" href=\"javascript:void(0)\" onclick=\"ShowHideGroup('" + id + "');\"><i class='hi hi-plus'></i></a> ");
            }
            else s.Append("<i class='hi hi-plus' style='color:transparent'></i>");
            s.Append(_txtLevel + " " + dt.Rows[i][MenusColumns.VmnName]);
            s.Append("</div>");

            s.Append("<div class=\"cot4 text-center\">" + subtotal2 + @"</div>");

            s.Append("<div class=\"cot5 text-center\"><input class='form-control text-center' id=\"tb_sort_order" + id + "\" type=\"number\" min=\"0\" value=\"" + order + "\" onchange=\"UpdateOrderMenu(" + id + ",this.value)\" /></div>");

            s.Append("<div class=\"cot6 text-center\"><label class=\"switch switch-primary\"><input onchange=\"OnOffMenu(" + id + ")\" type=\"checkbox\" " + (status.Equals("1") ? "checked" : "") + @"><span></span></label></div>");

            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"" + LinkAdmin.GoAdminMenu(_control, TypePage.Update, App, id) + "\" title=\"Chỉnh sửa\" class='btn btn-default'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteMenu('" + App + @"','" + id + @"','" + name + "')\"; title='Xóa danh mục' class='btn btn-warning'><i class='fa fa-times'></i></a>");
            s.Append("</div>");
            s.Append("</div>");
            if (list.Length > 0) s.Append(list);
            s.Append("</div>");

        }
        s.Append("</div>");
        _txtLevel = _txtLevel.Remove(_txtLevel.Length - 3);
        return s.ToString();
    }
}
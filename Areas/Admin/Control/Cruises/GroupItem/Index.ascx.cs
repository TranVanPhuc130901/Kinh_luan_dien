using System;
using System.Text;
using RevosJsc.CruisesControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Cruises_GroupItem_Index : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected readonly string Control = CodeApplications.Cruises;
    private const string App = CodeApplications.CruisesGroupItem;
    protected readonly string Action = TypePage.GroupItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        ltrList.Text = GetCate();
    }
    private string LinkAddItemToGroup(string igid, string genealogy)
    {
        return "/Areas/Admin/PopUp/GroupItems/AddItemsForGroup.aspx?control=" + Control + "&igid=" + igid + "&genealogy=" + genealogy;
    }
    private string GetCate()
    {
        var s = new StringBuilder();
        var fields = DataExtension.GetListColumns(
            GroupsColumns.IgId,
            GroupsColumns.VgName,
            GroupsColumns.IgSortOrder,
            GroupsColumns.IgStatus,
            GroupsColumns.VgParam,
            GroupsColumns.VgGenealogy
            );
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(App),
            GroupsTSql.GetByLang(_lang),
            GroupsTSql.GetByParentId("0"),
            " igStatus <> '2' "
        );
        var dt = Groups.GetData("", fields, condition, GroupsColumns.IgSortOrder + "," + GroupsColumns.VgName);

        if (dt.Rows.Count <= 0) return "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][GroupsColumns.IgId].ToString();
            var name = dt.Rows[i][GroupsColumns.VgName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var order = dt.Rows[i][GroupsColumns.IgSortOrder].ToString();
            var status = dt.Rows[i][GroupsColumns.IgStatus].ToString();
            var genealogy = dt.Rows[i][GroupsColumns.VgGenealogy].ToString();
            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            s.Append(dt.Rows[i][GroupsColumns.VgName]);
            s.Append("</div>");

            s.Append("<div class=\"cot3 text-center\">" + GroupsExtension.CountItemInGroup(id) + @"</div>");

            s.Append("<div class=\"cot5 text-center\"><input class='form-control text-center' id=\"TbOrder" + id + "\" type=\"number\" min=\"0\" value=\"" + order + "\" onchange=\"UpdateOrderGroup('" + Control + "','" + Action + "'," + id + ",this.value)\" /></div>");

            s.Append("<div class=\"cot6 text-center\"><label class=\"switch switch-primary\"><input onchange=\"OnOffGroup('" + Control + "','" + Action + "'," + id + ")\" type=\"checkbox\" " + (status.Equals("1") ? "checked" : "") + @"><span></span></label></div>");

            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"javascript:NewWindow_('" + LinkAddItemToGroup(id, genealogy) + "','ImageList','800','500','yes','yes');\" title='Thêm bài viết' class='btn btn-default'><i class='fa fa-exchange'></i></a> ");
            s.Append("<a href=\"" + LinkAdmin.GoAdminCategory(Control, TypePage.UpdateGroupItem, id) + "\" title=\"Chỉnh sửa\" class=\"btn btn-default\"><i class=\"fa fa-pencil\"></i></a> ");
            s.Append("<a href=\"javascript:DeleteGroup('" + Control + "','" + TypePage.GroupItem + "','" + id + @"','" + name + "')\"; title=\"Xóa\" class=\"btn btn-warning\"><i class=\"fa fa-times\"></i></a>");
            s.Append("</div>");
            s.Append("</div>");
            s.Append("</div>");
        }
        return s.ToString();
    }
}
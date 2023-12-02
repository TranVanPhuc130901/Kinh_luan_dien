using System;
using System.Text;
using RevosJsc.TourControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Filter_Category_Index : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private string _txtLevel = "";
    protected readonly string Control = CodeApplications.Tour;
    protected readonly string Action = TypePage.Filter;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        ltrList.Text = GetCate();
    }
    private string GetCate()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            FilterTSql.GetByLang(_lang),
            FilterTSql.GetByParentId("0"),
            " ifStatus <> '2' "
        );
        var dt = Filter.GetData("", "*", condition, FilterColumns.IfSortOrder + "," + FilterColumns.VfName);

        if (dt.Rows.Count <= 0) return "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][FilterColumns.IfId].ToString();
            var name = dt.Rows[i][FilterColumns.VfName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var order = dt.Rows[i][FilterColumns.IfSortOrder].ToString();
            var status = dt.Rows[i][FilterColumns.IfStatus].ToString();
            var list = GetSubCate(id);
            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            if (list.Length > 0)
            {
                s.Append("<a id=\"showhide" + id + "\" href=\"javascript:void(0)\" onclick=\"ShowHideGroup('" + id + "');\"><i class='hi hi-plus'></i></a> ");
            }
            else s.Append("<i class='hi hi-plus' style='color:transparent'></i>");
            s.Append(dt.Rows[i][FilterColumns.VfName]);
            s.Append("</div>");

            s.Append("<div class=\"cot4 text-center\">" + FilterExtension.CountChildFilter(id) + @"</div>");

            s.Append("<div class=\"cot5 text-center\"><input class='form-control text-center' id=\"TbOrder" + id + "\" type=\"number\" min=\"0\" value=\"" + order + "\" onchange=\"UpdateOrderFilter('" + Control + "','" + Action + "'," + id + ",this.value)\" /></div>");

            s.Append("<div class=\"cot6 text-center\"><label class=\"switch switch-primary\"><input onchange=\"OnOffFilter('" + Control + "','" + Action + "'," + id + ")\" type=\"checkbox\" " + (status.Equals("1") ? "checked" : "") + @"><span></span></label></div>");

            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"" + LinkAdmin.GoAdminOption(Control, TypePage.UpdateFilter, "ifid", id) + "\" title=\"Chỉnh sửa\" class='btn btn-default'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteFilter('" + Control + "','" + Action + "','" + id + @"','" + name + "')\"; title='Xóa danh mục' class='btn btn-warning'><i class='fa fa-times'></i></a>");
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
    public string GetSubCate(string parentId)
    {
        var condition = DataExtension.AndConditon(
            FilterTSql.GetByLang(_lang),
            FilterTSql.GetByParentId(parentId),
            " ifStatus <> '2' "
            );
        var dt = Filter.GetData("", "*", condition, FilterColumns.IfSortOrder);
        var s = new StringBuilder();
        if (dt.Rows.Count <= 0) return "";
        s.Append("<div id='"+ parentId + "' style='display:none'>");
        _txtLevel += "...";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][FilterColumns.IfId].ToString();
            var name = dt.Rows[i][FilterColumns.VfName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var order = dt.Rows[i][FilterColumns.IfSortOrder].ToString();
            var status = dt.Rows[i][FilterColumns.IfStatus].ToString();
            var list = GetSubCate(id);

            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + parentId + "-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + parentId + "-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            if (list.Length > 0)
            {
                s.Append("<a id=\"showhide" + id + "\" href=\"javascript:void(0)\" onclick=\"ShowHideGroup('" + id + "');\"><i class='hi hi-plus'></i></a> ");
            }
            else s.Append("<i class='hi hi-plus' style='color:transparent'></i>");
            s.Append(_txtLevel + " " + dt.Rows[i][FilterColumns.VfName]);
            s.Append("</div>");

            s.Append("<div class=\"cot4 text-center\">" + FilterExtension.CountChildFilter(id) + @"</div>");

            s.Append("<div class=\"cot5 text-center\"><input class='form-control text-center' id=\"tb_sort_order" + id + "\" type=\"number\" min=\"0\" value=\"" + order + "\" onchange=\"UpdateOrderFilter('" + Control + "','" + Action + "'," + id + ",this.value)\" /></div>");

            s.Append("<div class=\"cot6 text-center\"><label class=\"switch switch-primary\"><input onchange=\"OnOffFilter('" + Control + "','" + Action + "'," + id + ")\" type=\"checkbox\" " + (status.Equals("1") ? "checked" : "") + @"><span></span></label></div>");

            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"" + LinkAdmin.GoAdminOption(Control, TypePage.UpdateFilter, "ifid", id) + "\" title=\"Chỉnh sửa\" class='btn btn-default'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteFilter('" + Control + "','" + Action + "','" + id + "','" + name + "')\"; title='Xóa danh mục' class='btn btn-warning'><i class='fa fa-times'></i></a>");
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
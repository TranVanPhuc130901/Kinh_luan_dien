using System;
using System.Text;
using RevosJsc.ContactControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Contact_Category_Index : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private string _txtLevel = "";
    protected readonly string Control = CodeApplications.Contact;
    protected readonly string Action = TypePage.Category;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        ltrList.Text = GetCate();
    }
    private string GetCate()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            ContactsTSql.GetByLang(_lang),
            ContactsTSql.GetByParentId("0"),
            " icStatus <> '2' "
        );
        var dt = Contacts.GetData("", "*", condition, ContactsColumns.IcSortOrder + "," + ContactsColumns.VcName);

        if (dt.Rows.Count <= 0) return "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][ContactsColumns.IcId].ToString();
            var name = dt.Rows[i][ContactsColumns.VcName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var order = dt.Rows[i][ContactsColumns.IcSortOrder].ToString();
            var status = dt.Rows[i][ContactsColumns.IcStatus].ToString();
            var total = "";
            var list = GetSubCate(id, ref total);
            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            if (list.Length > 0) s.Append("<a id=\"showhide" + id + "\" href=\"javascript:void(0)\" onclick=\"ShowHideGroup('" + id + "');\"><i class='hi hi-plus'></i></a> ");
            else s.Append("<i class='hi hi-plus' style='color:transparent'></i>");
            s.Append(dt.Rows[i][ContactsColumns.VcName]);
            s.Append("</div>");

            s.Append("<div class=\"cot4 text-center\">" + total + @"</div>");

            s.Append("<div class=\"cot5 text-center\"><input class='form-control text-center' id=\"TbOrder" + id + "\" type=\"number\" min=\"0\" value=\"" + order + "\" onchange=\"UpdateOrderContact(" + id + ",this.value)\" /></div>");

            s.Append("<div class=\"cot6 text-center\"><label class=\"switch switch-primary\"><input onchange=\"OnOffContact(" + id + ")\" type=\"checkbox\" " + (status.Equals("1") ? "checked" : "") + @"><span></span></label></div>");

            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"" + LinkAdmin.GoAdminOption(Control, TypePage.UpdateCategory, "icid", id) + "\" title=\"Chỉnh sửa\" class='btn btn-default'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteContact('" + id + @"','" + name + "')\"; title='Xóa danh mục' class='btn btn-warning'><i class='fa fa-times'></i></a>");
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
    public string GetSubCate(string parentId, ref string total)
    {
        var condition = DataExtension.AndConditon(
            ContactsTSql.GetByLang(_lang),
            ContactsTSql.GetByParentId(parentId),
            " icStatus <> 2 "
            );
        var dt = Contacts.GetData("", "*", condition, ContactsColumns.IcSortOrder);
        var s = new StringBuilder();
        total = dt.Rows.Count.ToString();
        if (dt.Rows.Count <= 0) return "";
        s.Append("<div id='"+ parentId + "' style='display:none'>");
        _txtLevel += "...";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][ContactsColumns.IcId].ToString();
            var name = dt.Rows[i][ContactsColumns.VcName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var order = dt.Rows[i][ContactsColumns.IcSortOrder].ToString();
            var status = dt.Rows[i][ContactsColumns.IcStatus].ToString();
            var totalsub = "";
            var list = GetSubCate(id, ref totalsub);

            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + parentId + "-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + parentId + "-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            if (list.Length > 0) s.Append("<a id=\"showhide" + id + "\" href=\"javascript:void(0)\" onclick=\"ShowHideGroup('" + id + "');\"><i class='hi hi-plus'></i></a> ");
            else s.Append("<i class='hi hi-plus' style='color:transparent'></i>");
            s.Append(_txtLevel + " " + dt.Rows[i][ContactsColumns.VcName]);
            s.Append("</div>");

            s.Append("<div class=\"cot4 text-center\">" + totalsub + @"</div>");

            s.Append("<div class=\"cot5 text-center\"><input class='form-control text-center' id=\"tb_sort_order" + id + "\" type=\"number\" min=\"0\" value=\"" + order + "\" onchange=\"UpdateOrderContact(" + id + ",this.value)\" /></div>");

            s.Append("<div class=\"cot6 text-center\"><label class=\"switch switch-primary\"><input onchange=\"OnOffContact(" + id + ")\" type=\"checkbox\" " + (status.Equals("1") ? "checked" : "") + @"><span></span></label></div>");

            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"" + LinkAdmin.GoAdminOption(Control, TypePage.UpdateCategory, "icid", id) + "\" title=\"Chỉnh sửa\" class='btn btn-default'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteContact('" + id + "','" + name + "')\"; title='Xóa danh mục' class='btn btn-warning'><i class='fa fa-times'></i></a>");
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
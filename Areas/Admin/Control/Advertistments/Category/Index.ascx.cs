using System;
using System.Text;
using RevosJsc.AdminControl;
using RevosJsc.AdvertistmentsControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Advertistments_Category_Index : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected readonly string Control = CodeApplications.Advertistments;
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
            AdvertistmentPositionsTSql.GetByLang(_lang),
            " iapStatus <> 2 "
        );
        var dt = AdvertistmentPositions.GetData("", "*", condition, AdvertistmentPositionsColumns.IapSortOrder);

        if (dt.Rows.Count <= 0) return "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][AdvertistmentPositionsColumns.IapId].ToString();
            var name = dt.Rows[i][AdvertistmentPositionsColumns.VapName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var order = dt.Rows[i][AdvertistmentPositionsColumns.IapSortOrder].ToString();
            var status = dt.Rows[i][AdvertistmentPositionsColumns.IapStatus].ToString();
            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            s.Append(dt.Rows[i][AdvertistmentPositionsColumns.VapName]);
            s.Append("</div>");

            s.Append("<div class=\"cot3 text-center\">" + CountItem(id) + @"</div>");

            s.Append("<div class=\"cot5 text-center\"><input class='form-control text-center' id=\"TbOrder" + id + "\" type=\"number\" min=\"0\" value=\"" + order + "\" onchange=\"UpdateOrderAdvertistmentPosition(" + id + ",this.value)\" /></div>");

            s.Append("<div class=\"cot6 text-center\"><label class=\"switch switch-primary\"><input onchange=\"OnOffAdvertistmentPosition(" + id + ")\" type=\"checkbox\" " + (status.Equals("1") ? "checked" : "") + @"><span></span></label></div>");

            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"" + LinkAdmin.GoAdminOption(Control, TypePage.UpdateCategory, "iapid", id) + "\" title=\"Chỉnh sửa\" class=\"btn btn-default\"><i class=\"fa fa-pencil\"></i></a> ");
            s.Append("<a href=\"javascript:DeleteAdvertistmentPosition('" + id + @"','" + name + "')\"; title=\"Xóa\" class=\"btn btn-warning\"><i class=\"fa fa-times\"></i></a>");
            s.Append("</div>");
            s.Append("</div>");
            s.Append("</div>");
        }
        return s.ToString();
    }

    private static string CountItem(string id)
    {
        var dt = Advertistments.GetData("", AdvertistmentsColumns.IaId, AdvertistmentsTSql.GetByPositionId(id), "");
        return dt.Rows.Count.ToString();
    }
}
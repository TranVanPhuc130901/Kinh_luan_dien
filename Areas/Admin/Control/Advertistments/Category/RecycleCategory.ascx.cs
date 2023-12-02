using System;
using System.Text;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Advertistments_Category_RecycleCategory : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
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
            AdvertistmentPositionsTSql.GetByStatus("2")
        );
        var dt = AdvertistmentPositions.GetData("", "*", condition, "");

        if (dt.Rows.Count <= 0) return "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][AdvertistmentPositionsColumns.IapId].ToString();
            var name = dt.Rows[i][AdvertistmentPositionsColumns.VapName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            s.Append(dt.Rows[i][AdvertistmentPositionsColumns.VapName]);
            s.Append("</div>");

            s.Append("<div class=\"cot4 text-center\">" + CountItem(id) + @"</div>");
     
            s.Append("<div class=\"cot5 btn-group-sm text-center\">");
            s.Append("<a href=\"javascript:RestoreAdvertistmentPosition('" + id + "','" + name + "')\"; title='Khôi phục' class='btn btn-success'><i class='gi gi-refresh'></i></a> ");
            s.Append("<a href=\"javascript:DeleteRecAdvertistmentPosition('" + id + "','" + name + "');\" title='Xóa vĩnh viễn' class='btn btn-danger'><i class='fa fa-times'></i></a>");
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
    private static string CountItem(string id)
    {
        var dt = Advertistments.GetData("", AdvertistmentsColumns.IaId, AdvertistmentsTSql.GetByPositionId(id), "");
        return dt.Rows.Count.ToString();
    }
}
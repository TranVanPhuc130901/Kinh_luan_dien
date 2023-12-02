using System;
using System.Text;
using RevosJsc.TourControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Tour_Filter_RecycleFilter : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected readonly string Control = CodeApplications.Tour;
    private readonly string _app = CodeApplications.Tour;
    protected readonly string Pic = FolderPic.Tour;
    private readonly string _typePage = TypePage.RecycleCategory;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        ltrList.Text = GetGroups();
    }
    private string GetGroups()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            FilterTSql.GetByLang(_lang),
            FilterTSql.GetByStatus("2")
        );
        var dt = Filter.GetData("", "*", condition, "VfGenealogy");

        if (dt.Rows.Count <= 0) return "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][FilterColumns.IfId].ToString();
            var name = dt.Rows[i][FilterColumns.VfName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            s.Append(dt.Rows[i][FilterColumns.VfName]);
            s.Append("</div>");

            s.Append("<div class=\"cot3\">" + GetRoadGroup(id) + @"</div>");

            s.Append("<div class=\"cot4 text-center\">" + FilterExtension.CountAllChild(id) + @"</div>");

            s.Append("<div class=\"cot5 btn-group-sm text-center\">");
            s.Append("<a href=\"javascript:RestoreFilter('" + Control + "','" + _typePage + "','" + id + "','" + name + "');\" title='Khôi phục' class='btn btn-success'><i class='gi gi-refresh'></i></a> ");
            s.Append("<a href=\"javascript:DeleteRecFilter('" + Control + "','" + id + "','" + name + "','"+ Pic + "');\" title='Xóa vĩnh viễn' class='btn btn-danger'><i class='fa fa-times'></i></a>");
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
    private string GetRoadGroup(string igid)
    {
        var str = new StringBuilder();
        var fields = DataExtension.GetListColumns(FilterColumns.IfParentId, FilterColumns.VfName);
        var current = "";
        var dt = Filter.GetData("1", fields, FilterTSql.GetById(igid), "");
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][FilterColumns.IfParentId].ToString().Equals("0"))
            {
                str.Append(dt.Rows[0][FilterColumns.VfName] + " / ");
            }
            else
            {
                str.Append(GetRoadGroup(dt.Rows[0][FilterColumns.IfParentId].ToString()));
                current = dt.Rows[0][FilterColumns.VfName].ToString();
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
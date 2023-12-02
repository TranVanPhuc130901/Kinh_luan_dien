using System;
using System.Text;
using RevosJsc.DestinationControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Destination_Category_RecycleCategory : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected readonly string Control = CodeApplications.Destination;
    private readonly string _app = CodeApplications.Destination;
    protected readonly string Pic = FolderPic.Destination;
    private readonly string _typePage = TypePage.RecycleCategory;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        ltrList.Text = GetCate();
    }
    private string GetCate()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(_app),
            GroupsTSql.GetByLang(_lang),
            GroupsTSql.GetByStatus("2")
        );
        var dt = Groups.GetData("", "*", condition, "VgGenealogy");

        if (dt.Rows.Count <= 0) return "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][GroupsColumns.IgId].ToString();
            var name = dt.Rows[i][GroupsColumns.VgName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            s.Append(dt.Rows[i][GroupsColumns.VgName]);
            s.Append("</div>");

            s.Append("<div class=\"cot3\">" + GetRoadGroup(id) + @"</div>");

            s.Append("<div class=\"cot4 text-center\">" + GroupsExtension.CountAllChildCategory(id) + @"</div>");
            s.Append("<div class=\"cot4 text-center\">" + GroupsExtension.CountAllItemInGroup(id) + @"</div>");


            s.Append("<div class=\"cot5 btn-group-sm text-center\">");
            s.Append("<a href=\"javascript:RestoreGroup('" + Control + "','" + _typePage + "','" + id + "','" + name + "')\"; title='Khôi phục' class='btn btn-success'><i class='gi gi-refresh'></i></a> ");
            s.Append("<a href=\"javascript:DeleteRecGroup('" + Control + "','" + _app + "','" + id + "','" + name + "','"+ Pic + "');\" title='Xóa vĩnh viễn' class='btn btn-danger'><i class='fa fa-times'></i></a>");
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
        var fields = DataExtension.GetListColumns(GroupsColumns.IgParentId, GroupsColumns.VgName);
        var condition = DataExtension.AndConditon(GroupsTSql.GetByApp(_app), GroupsTSql.GetById(igid));
        var current = "";
        var dt = Groups.GetData("1", fields, condition, "");
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][GroupsColumns.IgParentId].ToString().Equals("0"))
            {
                str.Append(dt.Rows[0][GroupsColumns.VgName] + " / ");
            }
            else
            {
                str.Append(GetRoadGroup(dt.Rows[0][GroupsColumns.IgParentId].ToString()));
                current = dt.Rows[0][GroupsColumns.VgName].ToString();
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
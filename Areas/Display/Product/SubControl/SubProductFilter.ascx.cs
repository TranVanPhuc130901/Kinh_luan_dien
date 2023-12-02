using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;

public partial class Areas_Display_Product_SubControl_SubProductFilter : System.Web.UI.UserControl
{
    private string _app = CodeApplications.ProductFilterProperties;
    private string _igid = "";
    private string _filter = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["igid"] != null) _igid = Request.QueryString["igid"];
        if (Request.QueryString["filter"] != null) _filter = Request.QueryString["filter"];
        if (_filter.Equals(""))
        {
            var myUri = new Uri(UrlExtension.WebsiteUrl + Request.RawUrl.Substring(1));
            _filter = HttpUtility.ParseQueryString(myUri.Query).Get("filter") ?? "";
        }

        if (Session["dataByTitle_Category"] != null)
        {
            var dt = (DataTable)Session["dataByTitle_Category"];
            if (dt.Rows.Count <= 0) return;
            ltrList.Text = GetList(dt.Rows[0][GroupsColumns.VgParam].ToString());
            var link = UrlExtension.WebsiteUrl + dt.Rows[0][GroupsColumns.VgLink] + RewriteExtension.Extensions;
        }
        else if (_igid.Length > 0)
        {
            var dt = Groups.GetData("1", "*", GroupsTSql.GetById(_igid), "");
            if (dt.Rows.Count <= 0) return;
            ltrList.Text = GetList(dt.Rows[0][GroupsColumns.VgParam].ToString());
        }
        //else Visible = false;
        
    }

    private string GetList(string listId)
    {
        if (listId.Equals("")) return "";
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            FilterTSql.GetByStatus("1"),
            FilterTSql.GetByLevel("1"),
            "ifId IN ("+ listId.Trim(',') +")"
            );
        var dt = Filter.GetData("", "*", condition, FilterColumns.IfSortOrder);
        if (dt.Rows.Count <= 0) return s.ToString();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var active = false;
            var list = GetSubFilter(dt.Rows[i][FilterColumns.IfId].ToString(), ref active);
            if (list.Length <= 0) continue;
            s.Append("<div class='product_categories'>");
            s.Append("<div class='label'>" + dt.Rows[i][FilterColumns.VfName] + "</div>");
            s.Append("<ul class='product_filters' style='" + (active ? "": "display:none") + "'>");
            s.Append(list);
            s.Append("</ul>");
            s.Append("</div>");
        }
        return s.ToString();
    }

    private string GetSubFilter(string parent, ref bool active)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            FilterTSql.GetByStatus("1"),
            FilterTSql.GetByParentId(parent)
        );
        var dt = Filter.GetData("", "*", condition, FilterColumns.IfSortOrder);
        if (dt.Rows.Count <= 0) return s.ToString();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][FilterColumns.IfId].ToString();
            var name = dt.Rows[i][FilterColumns.VfName].ToString();
            var current = "";

            if (("," + _filter + ",").Contains("," + id + ","))
            {
                active = true;
                current = "checked='checked'";
            }
            s.Append("<li>");
            s.Append("<input type='checkbox' value='" + id + "' id='" + id + "' name='year_range' " + current + ">");
            s.Append("<label for='" + id + "'>"+ name + "</label>");
            s.Append("</li>");
        }
        return s.ToString();
    }

    private string GetListCurrent(string link)
    {
        if (_filter.Equals("")) return "";
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            FilterTSql.GetByStatus("1"),
            "ifId IN (" + _filter.Trim(',') + ")"
        );
        var dt = Filter.GetData("", "*", condition, FilterColumns.IfSortOrder);
        if (dt.Rows.Count <= 0) return s.ToString();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            s.Append("<a class='sub' ref='nofollow' href='javascript://' data-id='" + dt.Rows[i][FilterColumns.IfId] + "'>" + dt.Rows[i][FilterColumns.VfName] + "</a>");
        }

        s.Append("<a class='clear_all' href='" + link + "'>Xóa tất cả</a>");
        return s.ToString();
    }

}
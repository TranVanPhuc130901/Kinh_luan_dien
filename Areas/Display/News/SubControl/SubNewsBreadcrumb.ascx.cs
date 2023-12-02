using System;
using System.Data;
using System.Text;
using Developer.Extension;
using Newtonsoft.Json.Linq;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Display_News_SubControl_SubNewsBreadcrumb : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private string _rewrite = "";
    private string _appTitle = "";
    private string _page = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Lấy thông tin tên, rewrite như Sản phẩm, san-pham. Thông tin này đã được xử lý lưu vào session tại Default.aspx
        if (Session["rewrite"] != null) _rewrite = StringExtension.RemoveSqlInjectionChars(Session["rewrite"].ToString());
        if (Session["apptitle"] != null) _appTitle = StringExtension.RemoveSqlInjectionChars(Session["apptitle"].ToString());
        if (Session["page"] != null) _page = StringExtension.RemoveSqlInjectionChars(Session["page"].ToString());
        #endregion

        ltrList.Text = GetRoads();
    }

    private string GetRoads()
    {
        var s = new StringBuilder();

        #region Road trang chủ modul

        var link = UrlExtension.WebsiteUrl + _rewrite + RewriteExtension.Extensions;
        if (_rewrite == RewriteExtension.Search || _rewrite == RewriteExtension.Contact || _rewrite == RewriteExtension.Error) s.Append("<li><a href='javascript://' title='" + _appTitle + "'>" + _appTitle + "</a></li>");
        else if (_rewrite != RewriteExtension.AboutUs) s.Append("<li><a href='" + (link.Contains(UrlExtension.WebsiteUrl + Request.RawUrl.Substring(1)) ? "javascript:void(0);" : link) + "' title='" + _appTitle + "'>" + _appTitle + "</a></li>");

        #endregion

        #region Road danh mục

        var dt = new DataTable();
        if (Session["dataByTitle_Category"] != null) dt = (DataTable)Session["dataByTitle_Category"];
        var listId = "";
        if (dt.Rows.Count > 0)
        {
            listId = dt.Rows[0][GroupsColumns.VgGenealogy].ToString();
            s.Append(GetGroupRoads(listId));
            //link = UrlExtension.WebsiteUrl + dt.Rows[0][GroupsColumns.VgLink].ToString().ToLower() + RewriteExtension.Extensions;
            //s.Append("<a href='" + (link.Equals(UrlExtension.WebsiteUrl + Request.RawUrl.Substring(1)) ? "javascript:void(0);" : link) + "' title='" + dt.Rows[0][GroupsColumns.VgName] + "'>" + dt.Rows[0][GroupsColumns.VgName] + "</a>");
        }

        #endregion

        #region Road chi tiết

        //if (Session["igid"] == null || Session["iiid"] == null || Session["dataByTitle"] == null) return s.ToString();
        //dt = (DataTable)Session["dataByTitle"];
        //if (dt.Rows.Count > 0)
        //{
        //    var newPost = listId.Split(',');
        //    var name = dt.Rows[0][ItemsColumns.ViTitle].ToString();
        //    var linkItem = UrlExtension.WebsiteUrl + dt.Rows[0][ItemsColumns.ViLink].ToString().ToLower() + RewriteExtension.Extensions;
        //    //j.Append("{'@type': 'ListItem','position': '" + (newPost.Length + 1) + "','name': '" + name.Replace("'","") + "','item': '" + linkItem + "'},");
        //    //s.Append("<span>" + dt.Rows[0][ItemsColumns.ViTitle] + "</span>");
        //}

        #endregion

        return s.ToString();
    }

    private string GetGroupRoads(string igParentId)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            "CHARINDEX(','+CAST(" + GroupsColumns.IgId + " AS varchar)+',','" + igParentId + "')>0"//Lấy danh sách cha của group hiện tại
            );
        var orderBy = "LEN(" + GroupsColumns.VgGenealogy + ")";//Order theo chiều dài của trường danh sách cha để groups cha hiện trước
        var fields = DataExtension.GetListColumns(GroupsColumns.VgName, GroupsColumns.VgLink, GroupsColumns.VgApp, GroupsColumns.IgLevel);
        var dt = Groups.GetData("", fields, condition, orderBy);
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var name = dt.Rows[i][GroupsColumns.VgName].ToString();
            var level = int.Parse(dt.Rows[i][GroupsColumns.IgLevel].ToString()) + 2;
            var link = UrlExtension.WebsiteUrl + dt.Rows[i][GroupsColumns.VgLink].ToString().ToLower() + RewriteExtension.Extensions;
            s.Append("<li><a href='" + ((UrlExtension.WebsiteUrl + Request.RawUrl.Substring(1)).Contains(link) ? "javascript:void(0);" : link) + "' title='" + name + "'>" + name + "</a></li>");
        }
        return s.ToString();
    }
}
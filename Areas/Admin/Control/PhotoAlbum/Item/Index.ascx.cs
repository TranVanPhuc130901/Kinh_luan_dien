using System;
using System.Text;
using System.Web.UI.WebControls;
using Developer.Config;
using Developer.Extension;
using RevosJsc.PhotoAlbumControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_PhotoAlbum_Item_Index : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected readonly string Control = CodeApplications.PhotoAlbum;
    private readonly string _app = CodeApplications.PhotoAlbum;
    private readonly string _subapp = CodeApplications.PhotoAlbumImagesOther;
    protected readonly string Pic = FolderPic.PhotoAlbum;
    private string _p = "1";
    private string _numberShowItem = "10";
    private string _igid = "";
    private string _key = "";
    private string _sortCookiesName = SortKey.SortPhotoAlbumItems;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null) _p = Request.QueryString["p"];
        if (Request.QueryString["key"] != null) _key = QueryStringExtension.GetQueryString("key");
        if (Request.QueryString["igid"] != null) _igid = QueryStringExtension.GetQueryString("igid");
        if (Request.QueryString["NumberShowItem"] != null) _numberShowItem = QueryStringExtension.GetQueryString("NumberShowItem");
        txtTitle.Text = _key;
        if (IsPostBack) return;
        GetGroupInDdl();
        ddlShowNumber.SelectedValue = _numberShowItem;
    }

    private void GetGroupInDdl()
    {
        var condition = DataExtension.AndConditon(GroupsTSql.GetByLang(_lang), GroupsTSql.GetByApp(_app), " igStatus <> '2' ");
        var dt = Groups.GetAllGroups("*", condition, "");
        ddlCategory.Items.Clear();
        ddlCategory.Items.Add(new ListItem("Lọc theo danh mục", ""));
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            ddlCategory.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i]["igLevel"].ToString()) + dt.Rows[i]["vgName"], dt.Rows[i]["igId"].ToString()));
        }
        ddlCategory.SelectedValue = _igid;
        GetList("");
    }
    private void GetList(string order)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            GroupsTSql.GetByApp(_app),
            GroupsTSql.GetByLang(_lang),
            ItemsTSql.GetByApp(_app),
            "Items.iiStatus <> 2",
            "Groups.igStatus <> 2"
            );
        if (_igid.Length > 0) condition = GroupItemsTSql.GetItemsInGroupCondition(_igid, condition);
        if (txtTitle.Text.Length > 0)
        {
            condition += " AND " + SearchTSql.GetSearchMathedCondition(txtTitle.Text, ItemsColumns.ViTitle);
        }
        var orderBy = "";
        orderBy = order.Length > 0 ? order : CookieExtension.GetCookiesSort(_sortCookiesName);
        var ds = GroupItems.GetAllDataPaging(_p, _numberShowItem, condition, orderBy);
        if (ds.Tables.Count < 1) return;
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        #region Lấy ra danh sách bài viết

        if (dt.Rows.Count < 1) return;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][ItemsColumns.IiId].ToString();
            var itemTitle = dt.Rows[i][ItemsColumns.ViTitle].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var status = dt.Rows[i][ItemsColumns.IiStatus].ToString();
            var sortOrder = dt.Rows[i][ItemsColumns.IiSortOrder].ToString();
            s.Append("<div id='item" + id + "' class='item inner'>");
            s.Append("<div class=\"cot1 text-center\"><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + "' /></div>");
            s.Append("<div class=\"cot2\">" + ImagesExtension.GetImage(Pic, dt.Rows[i][ItemsColumns.ViImage].ToString(), itemTitle, "w90px mr5", true, false, "") + dt.Rows[i][ItemsColumns.ViTitle] + "</div>");
            s.Append("<div class=\"cot3 text-center\">" + ((DateTime)dt.Rows[i][ItemsColumns.DiDateCreated]).ToString("dd-MM-yyyy HH:mm") + "</div>");
            s.Append("<div class=\"cot4 text-center\">" + NumberExtension.FormatNumber(dt.Rows[i][ItemsColumns.IiTotalView].ToString()) + "</div>");
            s.Append("<div class=\"cot5 text-center\"><input class='form-control text-center' id='TbOrder" + id + "' type='number' min='0' value='" + sortOrder + "' onchange='UpdateOrderItems(" + id + ",this.value)' /></div>");
            s.Append("<div class=\"cot6 text-center\"><label class='switch switch-primary'><input onchange='OnOffItems(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></div>");

            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            if (PhotoAlbumConfig.ShowMorePictures) s.Append("<a target='_blank' href=\"/Areas/Admin/PopUp/Items/AddImageToItem.aspx?control=" + Control + "&subapp=" + _subapp + "&pic=" + Pic + "&iiid=" + id +"\" title='Thêm bài viết' class='btn btn-default'><i class='fa fa-picture-o'></i></a> ");
            s.Append("<a target='_blank' href='" + dt.Rows[i][ItemsColumns.ViLink] + RewriteExtension.Extensions + "' title='Xem nhanh' class='btn btn-default'><i class='fa fa-eye'></i></a> ");
            s.Append("<a href='" + LinkAdmin.GoAdminOption(CodeApplications.PhotoAlbum, TypePage.UpdateItem, "iiid", id) + "' title='Chỉnh sửa' class='btn btn-default'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteItem('"+ Control + "','" + id + "','" + itemTitle + "')\" title=\"Xóa item\" class=\"btn btn-warning\"><i class=\"fa fa-times\"></i></a>");
            s.Append("</div>");
            s.Append("</div>");
        }
        ltrList.Text = s.ToString();

        #endregion

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), Convert.ToInt16(_numberShowItem), Convert.ToInt32(_p), LinkAdmin.UrlAdmin(_app, TypePage.Item, ddlCategory.SelectedValue, _key, ddlShowNumber.SelectedValue), "active", "normal", "first", "last", "preview", "next");
        var from = int.Parse(_numberShowItem) * (int.Parse(_p) - 1) + 1;
        var to = dt.Rows.Count + int.Parse(_numberShowItem) * (int.Parse(_p) - 1);
        ltrPaging.Text = @"
<div class='col-sm-5 hidden-xs'>
    <div class='dataTables_info' id='ecom-products_info' role='status' aria-live='polite'>
        <strong>" + from + "</strong> - <strong>" + to + "</strong> of <strong>" + NumberExtension.FormatNumber(dtPager.Rows[0]["TotalRows"].ToString()) + @"</strong>
    </div>
</div>
<div class='col-sm-7 col-xs-12 clearfix'>
    <div class='dataTables_paginate paging_bootstrap' id='ecom-products_paginate'>
        " + split + @"
    </div>
</div>
";
        #endregion
    }
    protected void btSearch_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(LinkAdmin.UrlAdmin(_app, TypePage.Item, ddlCategory.SelectedValue, txtTitle.Text, ddlShowNumber.SelectedValue));
    }

    protected void lbtTitle_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(ItemsColumns.ViTitle, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtCreateDate_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(ItemsColumns.DiDateCreated, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtTotalview_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(ItemsColumns.IiTotalView, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtOrder_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(ItemsColumns.IiSortOrder, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }

    protected void lbtStatus_Click(object sender, EventArgs e)
    {
        //Lưu vào cookies
        var order = CookieExtension.SetCookiesSort(ItemsColumns.IiStatus, _sortCookiesName);
        //Gọi hàm lấy dữ liệu theo kiểu sắp xếp hiện tại
        GetList(order);
    }
}
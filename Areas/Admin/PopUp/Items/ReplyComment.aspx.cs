using System;
using System.IO;
using System.Text;
using System.Web.UI;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;

public partial class Areas_Admin_PopUp_Items_ReplyComment : System.Web.UI.Page
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private string _control = "";
    private string _iiId = "";
    private string _isiId = "";
    private string _pic = "";
    private string _app = CodeApplications.ProductComment;
    private string _app2 = CodeApplications.ProductComment + "2";
    private string _p = "1";
    private string _id = "";
    private bool _edit = true;
    private string _link = "";
    private string _oldAuthor = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) Response.Redirect(UrlExtension.WebsiteUrl);
        if (Request.QueryString["control"] != null) _control = Request.QueryString["control"];
        if (Request.QueryString["iiid"] != null) _iiId = Request.QueryString["iiid"];
        if (Request.QueryString["isiid"] != null) _isiId = Request.QueryString["isiid"];
        if (Request.QueryString["pic"] != null) _pic = Request.QueryString["pic"];
        if (Request.QueryString["p"] != null) _p = QueryStringExtension.GetQueryString("p");
        if (Request.QueryString["id"] != null) _id = QueryStringExtension.GetQueryString("id");
        if (_id.Equals("")) _edit = false;
        ltrTitle.Text = "Thêm ảnh";
        if (IsPostBack) return;
        GetItemsInfo();
        GetUserInfo();
        if (_edit) GetSubItems();
        GetList();
    }

    private void GetUserInfo()
    {
        var dt = Users.GetData("1", "*", UsersTSql.GetByAccount(CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))), "");
        if (dt.Rows.Count <= 0) return;
        txtTitle.Text = dt.Rows[0][UsersColumns.VuFirstName] + " " + dt.Rows[0][UsersColumns.VuLastName];
        txtEmail.Text = dt.Rows[0][UsersColumns.VuEmail].ToString();
        txtPhone.Text = dt.Rows[0][UsersColumns.VuPhoneNumber].ToString();
    }

    private void GetItemsInfo()
    {
        if (_isiId.Equals("")) return;
        var dt = Subitems.GetData("1", "*", SubItemsTSql.GetById(_isiId), "");
        if (dt.Rows.Count <= 0) return;
        ltrTitle.Text = "Trả lời bình luận của " + dt.Rows[0][SubitemsColumns.VsiTitle];
        ltrInfo.Text = "<b>" + dt.Rows[0][SubitemsColumns.VsiTitle] + ":</b> " + dt.Rows[0][SubitemsColumns.VsiContent];
        _link = dt.Rows[0][SubitemsColumns.VsiParam].ToString();
        _oldAuthor = dt.Rows[0][SubitemsColumns.FsiPriceOld].ToString();
    }

    private void GetSubItems()
    {
        var dt = Subitems.GetData("1", "*", SubItemsTSql.GetById(_id), "");
        if (dt.Rows.Count <= 0) return;
        txtTitle.Text = dt.Rows[0][SubitemsColumns.VsiTitle].ToString();
        txtEmail.Text = dt.Rows[0][SubitemsColumns.VsiDescription].ToString();
        txtPhone.Text = dt.Rows[0][SubitemsColumns.VsiImage].ToString();
        txtContent.Text = dt.Rows[0][SubitemsColumns.VsiContent].ToString();
        btSubmit.Text = "Cập nhật";
    }
    private void GetList()
    {
        var s = new StringBuilder();
        const int top = 10;
        var condition = DataExtension.AndConditon(
            SubItemsTSql.GetByApp(_app2),
            SubItemsTSql.GetByIiid(_iiId),
            SubItemsTSql.GetBySortOrder(_isiId),
            "isiStatus <> 2"
            );

        var ds = Subitems.GetDataPaging(_p, top.ToString(), condition, SubitemsColumns.DsiDateCreated + " DESC");
        if (ds.Tables.Count <= 0) return;
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        if (dt.Rows.Count < 1) return;

        #region Lấy ra danh sách bài viết

        s.Append("<table class='table table-bordered table-striped table-vcenter'><tbody>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][SubitemsColumns.IsiId].ToString();
            var iiId = dt.Rows[i][SubitemsColumns.IiId].ToString();
            var itemTitle = dt.Rows[i][SubitemsColumns.VsiTitle].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var status = dt.Rows[i][SubitemsColumns.IsiStatus].ToString();
            var comment = dt.Rows[i][SubitemsColumns.VsiContent].ToString();
            var email = dt.Rows[i][SubitemsColumns.VsiDescription].ToString();
            var phone = dt.Rows[i][SubitemsColumns.VsiImage].ToString();
            s.Append("<tr id='item" + id + "'>");
            s.Append("<td><b>Họ tên</b>: " + itemTitle + "<br/><b>Email</b>: " + email + "<br/><b>Điện thoại</b>: " + phone + "<br/>" + comment + "</td>");
            s.Append("<td class='text-center'><label class='switch switch-primary'><input onchange='OnOffSubItems(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></td>");
            s.Append("<td class='text-center btn-group-xs'>");
            s.Append("<a href='/Areas/Admin/PopUp/Items/ReplyComment.aspx?control=" + _control + "&iiid=" + _iiId + "&isiid=" + _isiId + "&id=" + id + @"' class='btn btn-default' title='Sửa'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteSubItem('"+ Request.RawUrl +"','"+ _pic +"','"+ id +"','"+ itemTitle + "')\" class=\"btn btn-danger\" title='Xóa'><i class='fa fa-times'></i></a>");
            s.Append("</td>");
            s.Append("</tr>");
        }
        s.Append("</tbody></table>");

        #endregion Lấy ra danh sách bài viết

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), top, Convert.ToInt32(_p), "/Areas/Admin/PopUp/Items/AddImageToItem.aspx?control=" + _control + "&iiid=" + _iiId + "&isiid=" + _isiId, "active", "normal", "first", "last", "preview", "next");
        ltrPaging.Text = @"
<div class='dataTables_paginate paging_bootstrap' id='ecom-products_paginate'>
    " + split + @"
</div>";
        #endregion Xuất ra phân trang

        ltrList.Text = s.ToString();
    }
    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        if (_edit)
        {
            Subitems.Update(_iiId, _lang, _app2, txtTitle.Text, txtEmail.Text, txtContent.Text, txtPhone.Text, _link, _oldAuthor, "", _isiId, "1", DateTime.Now.ToString(), DateTime.Now.ToString(), _id);
        }

        else
        {
            Subitems.Insert(_iiId, _lang, _app2, txtTitle.Text, txtEmail.Text, txtContent.Text, txtPhone.Text, _link, "1", "", _isiId, "1", DateTime.Now.ToString(), DateTime.Now.ToString());
        }

        #region After Insert/Update
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo: " + txtTitle.Text + "', {type: 'success'});});", true);

        #endregion
        Response.Redirect("/Areas/Admin/PopUp/Items/ReplyComment.aspx?control=" + _control + "&iiid=" + _iiId + "&isiid=" + _isiId);
    }
}
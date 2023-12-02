using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Developer.Config;
using Developer.Extension;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_PopUp_Items_AddFormalityToItem : System.Web.UI.Page
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private string _control = "";
    private string _iiId = "";
    private string _isiId = "";
    private string _pic = "";
    private string _subApp = "";
    private string _p = "1";
    private bool _edit = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) Response.Redirect(UrlExtension.WebsiteUrl);
        if (Request.QueryString["control"] != null) _control = Request.QueryString["control"];
        if (Request.QueryString["iiid"] != null) _iiId = Request.QueryString["iiid"];
        if (Request.QueryString["isiid"] != null) _isiId = Request.QueryString["isiid"];
        if (Request.QueryString["pic"] != null) _pic = Request.QueryString["pic"];
        if (Request.QueryString["subapp"] != null) _subApp = Request.QueryString["subapp"];
        if (Request.QueryString["p"] != null) _p = QueryStringExtension.GetQueryString("p");
        if (_isiId.Equals("")) _edit = false;
        btnCancel.Attributes["href"] = "/Areas/Admin/PopUp/Items/AddFormalityToItem.aspx?control=" + _control + "&subapp=" + _subApp + "&pic=" + _pic + "&iiid=" + _iiId;
        ltrTitle.Text = "Thêm hình thức";
        if (IsPostBack) return;
        GetItemsInfo();
        if (_edit) GetSubItems();
        GetList();
    }


    private void GetItemsInfo()
    {
        if (_iiId.Equals("")) return;
        var dt = RevosJsc.Database.Items.GetData("1", ItemsColumns.ViTitle, ItemsTSql.GetById(_iiId), "");
        if (dt.Rows.Count > 0)
        {
            ltrTitle.Text = "Thêm tùy chọn hình thức cho: " + dt.Rows[0][ItemsColumns.ViTitle];
        }
    }

    private void GetSubItems()
    {
        cbContiue.Visible = false;
        btSubmit.Text = "Cập nhật";
        var dt = Subitems.GetData("1", "*", SubItemsTSql.GetById(_isiId), "");
        if (dt.Rows.Count <= 0) return;
        txtTitle.Text = dt.Rows[0][SubitemsColumns.VsiTitle].ToString();
        txtOrder.Text = dt.Rows[0][SubitemsColumns.IsiSortOrder].ToString();
        txtPriceOld.Text = dt.Rows[0][SubitemsColumns.FsiPriceOld].ToString();
        ltrPriceOld.Text = NumberExtension.FormatNumber(dt.Rows[0][SubitemsColumns.FsiPriceOld].ToString());
        cbStatus.Checked = dt.Rows[0][SubitemsColumns.IsiStatus].Equals(1);
    }
    private void GetList()
    {
        var s = new StringBuilder();
        const int top = 10;
        var condition = DataExtension.AndConditon(
            SubItemsTSql.GetByApp(_subApp),
            SubItemsTSql.GetByIiid(_iiId),
            "isiStatus <> 2"
            );

        var ds = Subitems.GetDataPaging(_p, top.ToString(), condition, SubitemsColumns.IsiSortOrder);
        if (ds.Tables.Count <= 0) return;
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        if (dt.Rows.Count < 1) return;
        #region Lấy ra danh sách bài viết

        s.Append("<table class='table table-bordered table-striped table-vcenter'><tbody>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][SubitemsColumns.IsiId].ToString();
            var titleItem = dt.Rows[i][SubitemsColumns.VsiTitle].ToString();
            var image = dt.Rows[i][SubitemsColumns.VsiImage].ToString();
            var price = dt.Rows[i][SubitemsColumns.FsiPriceOld].ToString();
            var status = dt.Rows[i][SubitemsColumns.IsiStatus].ToString();
            s.Append("<tr id='item" + id + "'>");
            s.Append("<td>" + titleItem + "</td>");
            s.Append("<td>" + NumberExtension.FormatNumber(price, true, "0", "đ") + "</td>");
            s.Append("<td class='text-center'><label class='switch switch-primary'><input onchange='OnOffSubItems(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></td>");
            s.Append("<td class='text-center btn-group-xs'>");
            s.Append("<a href='/Areas/Admin/PopUp/Items/AddFormalityToItem.aspx?control=" + _control + "&subapp=" + _subApp + "&pic=" + _pic + "&iiid=" + _iiId + "&isiid=" + id + @"' class='btn btn-default' title='Sửa'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteSubItem('"+ Request.RawUrl +"','"+ _pic +"','"+ id +"','"+ titleItem +"')\" class=\"btn btn-danger\" title='Xóa'><i class='fa fa-times'></i></a>");
            s.Append("</td>");
            s.Append("</tr>");
        }
        s.Append("</tbody></table>");
        #endregion Lấy ra danh sách bài viết

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), top, Convert.ToInt32(_p), "/Areas/Admin/PopUp/Items/AddImageToItem.aspx?control=" + _control + "&subapp=" + _subApp + "&pic=" + _pic + "&iiid=" + _iiId, "active", "normal", "first", "last", "preview", "next");
        ltrPaging.Text = @"
<div class='dataTables_paginate paging_bootstrap' id='ecom-products_paginate'>
    " + split + @"
</div>
";
        #endregion Xuất ra phân trang

        ltrList.Text = s.ToString();
    }
    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        #region Lưu bản ghi SubItems
        var status = "0";
        if (cbStatus.Checked) status = "1";

        if (_edit)
        {
            Subitems.Update(_iiId, _lang, _subApp, txtTitle.Text, "", "", "", "", txtPriceOld.Text, "", txtOrder.Text, status, DateTime.Now.ToString(), DateTime.Now.ToString(), _isiId);
        }
        else
        {
            Subitems.Insert(_iiId, _lang, _subApp, txtTitle.Text, "", "", "", "", txtPriceOld.Text, "", txtOrder.Text, status, DateTime.Now.ToString(), DateTime.Now.ToString());
        }
        #endregion

        #region After Insert/Update
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo: " + txtTitle.Text + "', {type: 'success'});});", true);

        #endregion
        if (cbContiue.Checked)
        {
            try
            {
                txtOrder.Text = (Convert.ToInt32(txtOrder.Text) + 1).ToString();
            }
            catch
            {
                // do nothing
            }
            txtTitle.Text = "";
            txtTitle.Focus();
            GetList();
        }
        else
        {
            Response.Redirect("/Areas/Admin/PopUp/Items/AddFormalityToItem.aspx?control=" + _control + "&subapp=" + _subApp + "&pic=" + _pic + "&iiid=" + _iiId);
        }
    }
}
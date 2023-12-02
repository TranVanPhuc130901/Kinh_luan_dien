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

public partial class Areas_Admin_PopUp_Items_AddVariantsForItem : System.Web.UI.Page
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private string _control = "";
    private string _iiId = "";
    private string _isiId = "";
    private string _pic = "";
    private string _subApp = "Variant";
    private string _p = "1";
    private bool _edit = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) Response.Redirect(UrlExtension.WebsiteUrl);
        if (Request.QueryString["control"] != null) _control = Request.QueryString["control"];
        if (Request.QueryString["iiid"] != null) _iiId = Request.QueryString["iiid"];
        if (Request.QueryString["isiid"] != null) _isiId = Request.QueryString["isiid"];
        if (Request.QueryString["p"] != null) _p = QueryStringExtension.GetQueryString("p");
        if (_isiId.Equals("")) _edit = false;
        btnCancel.Attributes["href"] = "/Areas/Admin/PopUp/Items/AddVariantsForItem.aspx?control=" + _control + "&iiid=" + _iiId;
        ltrTitle.Text = "Thêm ảnh";
        if (IsPostBack) return;
        GetItemInfo();
        if (_edit) GetSubItems();
        GetList();
    }


    private void GetItemInfo()
    {
        if (_iiId.Equals("")) return;
        var dt = RevosJsc.Database.Items.GetData("1", ItemsColumns.ViTitle, ItemsTSql.GetById(_iiId), "");
        if (dt.Rows.Count > 0)
        {
            ltrTitle.Text = "Thêm biến thể cho: " + dt.Rows[0][ItemsColumns.ViTitle];
        }
    }

    private void GetSubItems()
    {
        cbContiue.Visible = false;
        btSubmit.Text = "Cập nhật";
        var dt = RevosJsc.Database.Items.GetData("1", "*", ItemsTSql.GetById(_isiId), "");
        if (dt.Rows.Count <= 0) return;
        txtTitle.Text = dt.Rows[0][ItemsColumns.ViTitle].ToString();
        txtDescription.Text = dt.Rows[0][ItemsColumns.ViDescription].ToString();
        txtOrder.Text = dt.Rows[0][ItemsColumns.IiSortOrder].ToString();
        txtPriceOld.Text = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();
        ltrPriceOld.Text = NumberExtension.FormatNumber(dt.Rows[0][ItemsColumns.FiPriceOld].ToString());
        txtPriceNew.Text = dt.Rows[0][ItemsColumns.FiPriceNew].ToString();
        ltrPriceNew.Text = NumberExtension.FormatNumber(dt.Rows[0][ItemsColumns.FiPriceNew].ToString());
        cbStatus.Checked = dt.Rows[0][ItemsColumns.IiStatus].Equals(1);

        TextBox1.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViContent].ToString(), "", 5);
        TextBox2.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViContent].ToString(), "", 6);
        TextBox3.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViContent].ToString(), "", 7);
        TextBox4.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViContent].ToString(), "", 8);
        TextBox5.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViContent].ToString(), "", 9);
        TextBox6.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViContent].ToString(), "", 10);
        TextBox7.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViContent].ToString(), "", 11);
    }
    private void GetList()
    {
        var s = new StringBuilder();
        const int top = 10;
        var condition = DataExtension.AndConditon(
            ItemsTSql.GetByApp(_subApp),
            ItemsTSql.GetByTag(_iiId)
            );

        var ds = RevosJsc.Database.Items.GetDataPaging(_p, top.ToString(), condition, ItemsColumns.IiSortOrder);
        if (ds.Tables.Count <= 0) return;
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        if (dt.Rows.Count < 1) return;

        #region Lấy ra danh sách bài viết

        s.Append("<table class='table table-bordered table-striped table-vcenter'><tbody>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][ItemsColumns.IiId].ToString();
            var titleItem = dt.Rows[i][ItemsColumns.ViTitle].ToString();
            var status = dt.Rows[i][ItemsColumns.IiStatus].ToString();
            s.Append("<tr id='item" + id + "'>");
            s.Append("<td>" + titleItem + "</td>");
            s.Append("<td class='text-center'><label class='switch switch-primary'><input onchange='OnOffItems(6)(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></td>");
            s.Append("<td class='text-center btn-group-xs'>");
            s.Append("<a href='/Areas/Admin/PopUp/Items/AddVariantsForItem.aspx?control=" + _control + "&iiid=" + _iiId + "&isiid=" + id + @"' class='btn btn-default' title='Sửa'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteRecItem('" + _control +"','"+ _pic +"','"+ id +"','"+ StringExtension.ReplateTitle(titleItem) +"')\" class=\"btn btn-danger\" title='Xóa'><i class='fa fa-times'></i></a>");
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

        var fullContent = StringExtension.GhepChuoi("", "", "", "", "", TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text);

        if (_edit)
        {
            RevosJsc.Database.Items.Update(_lang, _subApp, "", txtTitle.Text, txtDescription.Text, fullContent, "", "", "", "", "", _iiId, "", txtPriceOld.Text, txtPriceNew.Text, "", "", txtOrder.Text, DateTime.Now.ToString(), DateTime.Now.ToString(), status, _isiId);
        }
        else
        {
            RevosJsc.Database.Items.Insert(_lang, _subApp, "", txtTitle.Text, txtDescription.Text, fullContent, "", "", "", "", "", _iiId, "", txtPriceOld.Text, txtPriceNew.Text, "", "", txtOrder.Text, DateTime.Now.ToString(), DateTime.Now.ToString(), status);
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
            Response.Redirect("/Areas/Admin/PopUp/Items/AddVariantsForItem.aspx?control=" + _control + "&iiid=" + _iiId);
        }
    }
}
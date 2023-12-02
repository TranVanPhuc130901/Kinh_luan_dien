using System;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Developer.Extension;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using RevosJsc.Columns;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;

public partial class Areas_Admin_PopUp_Items_AddVariantsToItems : Page
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private string _control = "";
    private string _iId = "";
    private string _sId = "";
    private string _pic = FolderPic.Product;
    private string _subApp = CodeApplications.Variant;
    private readonly string _subImage = CodeApplications.ProductImagesOther;
    private string _p = "1";
    private bool _edit;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) Response.Redirect(UrlExtension.WebsiteUrl);
        if (Request.QueryString["control"] != null) _control = Request.QueryString["control"];
        if (Request.QueryString["id"] != null) _iId = Request.QueryString["id"];
        if (Request.QueryString["sid"] != null) _sId = Request.QueryString["sid"];
        if (Request.QueryString["pic"] != null) _pic = Request.QueryString["pic"];
        if (Request.QueryString["subapp"] != null) _subApp = Request.QueryString["subapp"];
        if (Request.QueryString["p"] != null) _p = Request.QueryString["p"];
        if (_sId.Length > 0) _edit = true;
        btnCancel.Attributes["href"] = "/Areas/Admin/PopUp/Items/AddVariantsToItems.aspx?id=" + _iId;
        //ltrNote.Text = "<div class='text-warning'>Lưu ý: Có thể chọn nhiều ảnh cùng 1 lúc</div>";
        ltrTitle.Text = "Thêm biến thể";
        if (IsPostBack) return;
        GetItemsInfo();
        if (_edit) GetSubItems();
        GetList();
    }
    private void GetItemsInfo()
    {
        if (_iId.Equals("")) return;
        var dt = RevosJsc.Database.Items.GetData("1", "*", ItemsTSql.GetById(_iId), "");
        if (dt.Rows.Count <= 0) return;
        ltrTitle.Text = "Thêm biến thể cho: " + dt.Rows[0][ItemsColumns.ViTitle];
        var variants = dt.Rows[0][ItemsColumns.Variant].ToString();

        if (variants.Length <= 0) return;
        dynamic contentJson = JsonConvert.DeserializeObject(variants);
        if (contentJson == null) return;

        int countRows = contentJson.Count;

        rptOption.DataSource = new int[countRows];
        rptOption.DataBind();
        switch (countRows)
        {
            case 2:
                {
                    var ar1 = contentJson[0].value.ToString();
                    var ar2 = contentJson[1].value.ToString();
                    var lbl1 = contentJson[0].label.ToString();
                    var lbl2 = contentJson[1].label.ToString();
                    var item = rptOption.Items[0];
                    var dropdown = item.FindControl("ddlOption") as DropDownList;
                    var label = item.FindControl("ltrTitle") as Literal;
                    if (label != null) label.Text = lbl1;
                    if (dropdown != null)
                    {
                        dropdown.Items.Clear();
                        foreach (var text in ar1.Split(','))
                        {
                            dropdown.Items.Add(new ListItem(text, text));
                        }
                    }

                    item = rptOption.Items[1];
                    dropdown = item.FindControl("ddlOption") as DropDownList;
                    label = item.FindControl("ltrTitle") as Literal;
                    if (label != null) label.Text = lbl2;
                    if (dropdown != null)
                    {
                        dropdown.Items.Clear();
                        foreach (var text in ar2.Split(','))
                        {
                            dropdown.Items.Add(new ListItem(text, text));
                        }
                    }
                    break;
                }

            default:
                {
                    var ar1 = contentJson[0].value.ToString();
                    var lbl1 = contentJson[0].label.ToString();
                    var item = rptOption.Items[0];
                    var dropdown = item.FindControl("ddlOption") as DropDownList;
                    var label = item.FindControl("ltrTitle") as Literal;
                    if (label != null) label.Text = lbl1;
                    if (dropdown != null)
                    {
                        dropdown.Items.Clear();
                        foreach (var text in ar1.Split(','))
                        {
                            dropdown.Items.Add(new ListItem(text, text));
                        }
                    }
                    break;
                }
        }
        if (_edit) return;
        txtCode.Text = dt.Rows[0][ItemsColumns.ViCode].ToString();
        txtPrice.Text = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();
        ltrPriceOld.Text = NumberExtension.FormatNumber(dt.Rows[0][ItemsColumns.FiPriceOld].ToString());
        txtPrice2.Text = dt.Rows[0][ItemsColumns.FiPriceNew].ToString();
        txtTitle.Text = dt.Rows[0][ItemsColumns.ViTitle].ToString();
    }
    private void GetSubItems()
    {
        cbContiue.Visible = false;
        btSubmit.Text = "Cập nhật";
        //ltrNote.Visible = false;
        var dt = RevosJsc.Database.Items.GetData("1", "*", ItemsTSql.GetById(_sId), "");
        if (dt.Rows.Count <= 0) return;
        txtTitle.Text = dt.Rows[0][ItemsColumns.ViTitle].ToString();
        txtCode.Text = dt.Rows[0][ItemsColumns.ViCode].ToString();
        txtPrice.Text = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();
        ltrPriceOld.Text = NumberExtension.FormatNumber(dt.Rows[0][ItemsColumns.FiPriceOld].ToString());
        txtPrice2.Text = dt.Rows[0][ItemsColumns.FiPriceNew].ToString();
        txtQuantity.Text = dt.Rows[0][ItemsColumns.Inventory].ToString();
        HdPromotionStartDate.Value = dt.Rows[0][ItemsColumns.PromotionStartDate].ToString();
        HdPromotionEndDate.Value = dt.Rows[0][ItemsColumns.PromotionEndDate].ToString();
        hdCreateDate.Value = dt.Rows[0][ItemsColumns.DiDateCreated].ToString();
        var image = dt.Rows[0][ItemsColumns.ViImage].ToString();
        hdImage.Value = image;
        ltimg.Text = "<a href='" + UrlExtension.WebsiteUrl + _pic + "/" + image + "' data-toggle='lightbox-image'>" + ImagesExtension.GetImage(_pic, image, txtTitle.Text, "w130px", false, false, "") + "</a>";
        if (image.Length > 0) lnk_delete_Image_current.Visible = true;
        var color = dt.Rows[0][ItemsColumns.ViMetaTitle].ToString();
        var size = dt.Rows[0][ItemsColumns.ViMetaDescription].ToString();
        //ddlType.SelectedValue = dt.Rows[0][SubitemsColumns.VsiParam].ToString();
        cbStatus.Checked = dt.Rows[0][ItemsColumns.IiStatus].Equals(1);

        for (var i = 0; i < rptOption.Items.Count; i++)
        {
            var dropdown = rptOption.Items[i].FindControl("ddlOption") as DropDownList;
            if (dropdown != null && i == 0) dropdown.SelectedValue = color;
            if (dropdown != null && i == 1) dropdown.SelectedValue = size;
        }
    }
    private void GetList()
    {
        var s = new StringBuilder();
        var top = "10";
        var condition = DataExtension.AndConditon(
            ItemsTSql.GetByApp(_subApp),
            ItemsTSql.GetByMasterId(_iId)
            );

        var ds = RevosJsc.Database.Items.GetDataPaging(_p, top, condition, "iiId");
        var dt = ds.Tables[0];
        var dtPager = ds.Tables[1];
        if (dt.Rows.Count < 1) return;

        #region Lấy ra danh sách bài viết

        s.Append("<table class='table table-bordered table-striped table-vcenter'><tbody>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][ItemsColumns.IiId].ToString();
            var titleItem = dt.Rows[i][ItemsColumns.ViTitle].ToString();
            var color = dt.Rows[i][ItemsColumns.ViMetaTitle].ToString();
            var size = dt.Rows[i][ItemsColumns.ViMetaDescription].ToString();
            if (color.Length > 0) titleItem += " - " + color;
            if (size.Length > 0) titleItem += " - " + size;
            var image = dt.Rows[i][ItemsColumns.ViImage].ToString();
            if (image.Length > 0) image += ".ashx?w=50";
            var status = dt.Rows[i][ItemsColumns.IiStatus].ToString();
            var code = dt.Rows[i][ItemsColumns.ViCode].ToString();
            var price = dt.Rows[i][ItemsColumns.FiPriceOld].ToString();
            var salePrice = dt.Rows[i][ItemsColumns.FiPriceNew].ToString();
            var startDate = (DateTime)dt.Rows[i][ItemsColumns.PromotionStartDate];
            var endDate = (DateTime)dt.Rows[i][ItemsColumns.PromotionEndDate];
            s.Append("<tr id='item" + id + "'>");
            s.Append("<td>" + ImagesExtension.GetImage(_pic, image, titleItem, "", false, false, "") + " " + titleItem + " ("+ code + ")</td>");
            s.Append("<td class='text-center'>" + NumberExtension.FormatNumber(price, true, "Liên hệ", "đ") + "</td>");
            s.Append("<td class='text-center'><label class='switch switch-primary'><input onchange='OnOffItems(" + id + ","+ status +")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></td>");
            s.Append("<td class='text-center btn-group-xs'>");
            s.Append("<a href='/Areas/Admin/PopUp/Items/AddVariantsToItems.aspx?id=" + _iId + "&sid=" + id + "&p="+ _p +"' class='btn btn-default' title='Sửa'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:NewWindow_('/Areas/Admin/PopUp/Items/AddImageToItem.aspx?control=" + _subApp + "&subapp=" + _subImage + "&pic=" + _pic + "&iiid=" + id + "','ImageList','800','500','yes','yes');\" title='Thêm ảnh' class='btn btn-default'><i class='fa fa-picture-o'></i></a> ");
            s.Append("<a href=\"javascript:DeleteRecItem('"+ _control + "','"+ _pic +"','" + id + "','" + HttpUtility.HtmlEncode(titleItem) + "')\" class=\"btn btn-danger\" title='Xóa'><i class='fa fa-times'></i></a>");
            s.Append("</td>");
            s.Append("</tr>");
        }
        s.Append("</tbody></table>");

        #endregion Lấy ra danh sách bài viết

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), Convert.ToInt32(top), Convert.ToInt32(_p), "?control=Product&id=" + _iId, "active", "normal", "first", "last", "preview", "next");
        var from = int.Parse(top) * (int.Parse(_p) - 1) + 1;
        var to = dt.Rows.Count + int.Parse(top) * (int.Parse(_p) - 1);
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
</div>";
        #endregion

        ltrList.Text = s.ToString();
    }


    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        var path = Request.PhysicalApplicationPath + "/" + _pic + "/";

        #region Kiểm tra xem thư mục đã tồn tại chưa, nếu chưa -> tạo mới thư mục
        var dri = new DirectoryInfo(path);
        if (!dri.Exists) dri.Create();
        #endregion

        #region Xử lý hình ảnh
        var vImg = "";
        // ReSharper disable once PossibleNullReferenceException
        if (Request.Files[0].ContentLength > 0)
        {
            var filename = Request.Files[0].FileName;
            var fileEx = filename.Substring(filename.LastIndexOf(".", StringComparison.Ordinal));

            if (ImagesExtension.ValidType(fileEx))
            {
                var fileNotEx = Guid.NewGuid().ToString();
                if (fileNotEx.Length > 50) fileNotEx = fileNotEx.Remove(50);

                #region Lưu ảnh

                vImg = fileNotEx + fileEx;
                Request.Files[0].SaveAs(path + vImg);

                #endregion
            }
        }

        #endregion

        #region Lưu bản ghi Items
        var status = "0";
        if (cbStatus.Checked) status = "1";
        var value1 = "";
        var value2 = "";
        var label1 = "";
        var label2 = "";
        for (var i = 0; i < rptOption.Items.Count; i ++)
        {
            var item = rptOption.Items[i].FindControl("ddlOption") as DropDownList;
            var label = rptOption.Items[i].FindControl("ltrTitle") as Literal;
            switch (i)
            {
                case 0:
                    value1 = item != null ? item.SelectedValue : "";
                    label2 = label != null ? label.Text : "";
                    break;
                case 1:
                    value2 = item != null ? item.SelectedValue : "";
                    label2 = label != null ? label.Text : "";
                    break;
            }
        }

        if (_edit)
        {
            if (vImg == "") vImg = hdImage.Value;
            else ImagesExtension.DeleteImageWhenDeleteItem(_pic, hdImage.Value);
            var variant = "[{\"label\":\"" + label1 + "\",\"value\":\"" + value1 + "\"},{\"label\":\"" + label2 + "\",\"value\":\"" + value2 + "\"}]";
            RevosJsc.Database.Items.UpdateProduct(_lang, _subApp, txtCode.Text, txtTitle.Text, "", "", vImg, "", value1.Trim(), "", value2.Trim(), "", "", txtPrice.Text, txtPrice2.Text, "", "", "", DateTime.Now.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture), status, HdPromotionStartDate.Value, HdPromotionEndDate.Value, variant, _iId, txtQuantity.Text, _sId);
        }
        else
        {
            var variant = "[{\"label\":\"" + label1 + "\",\"value\":\"" + value1 + "\"},{\"label\":\"" + label2 + "\",\"value\":\"" + value2 + "\"}]";
            RevosJsc.Database.Items.InsertProduct(_lang, _subApp, txtCode.Text, txtTitle.Text, "", "", vImg, "", value1.Trim(), "", value2.Trim(), "", "", txtPrice.Text, txtPrice2.Text, "", "", "", DateTime.Now.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture), status, HdPromotionStartDate.Value, HdPromotionEndDate.Value, variant,_iId , txtQuantity.Text);
        }
        #endregion

        #region After Insert/Update
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo: ', {type: 'success'});});", true);

        if (cbContiue.Checked)
        {
            //try
            //{
            //    txtOrder.Text = (Convert.ToInt32(txtOrder.Text) + 1).ToString();
            //}
            //catch
            //{
            //    // do nothing
            //}
            //txtTitle.Text = "";
            //txtTitle.Focus();
            GetList();
        }
        else
        {
            Response.Redirect("/Areas/Admin/PopUp/Items/AddVariantsToItems.aspx?id=" + _iId + "&p=" + _p);
        }

        #endregion

    }

    #region Sự kiện chạy khi click nút xóa ảnh
    protected void lnk_delete_Image_current_Click(object sender, EventArgs e)
    {
        ImagesExtension.DeleteImageWhenDeleteItem(_pic, hdImage.Value);
        hdImage.Value = "";
        lnk_delete_Image_current.Visible = false;
        ltimg.Text = "";
    }
    #endregion
}
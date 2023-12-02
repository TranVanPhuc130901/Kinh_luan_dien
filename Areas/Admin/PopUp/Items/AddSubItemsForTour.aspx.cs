using System;
using System.IO;
using System.Text;
using System.Web.UI;
using Developer.Extension;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_PopUp_Items_AddSubItemsForTour : System.Web.UI.Page
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
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) return;
        if (Request.QueryString["control"] != null) _control = Request.QueryString["control"];
        if (Request.QueryString["iiid"] != null) _iiId = Request.QueryString["iiid"];
        if (Request.QueryString["isiid"] != null) _isiId = Request.QueryString["isiid"];
        if (Request.QueryString["pic"] != null) _pic = Request.QueryString["pic"];
        if (Request.QueryString["subapp"] != null) _subApp = Request.QueryString["subapp"];
        if (Request.QueryString["p"] != null) _p = QueryStringExtension.GetQueryString("p");
        if (_isiId.Equals("")) _edit = false;
        btnCancel.Attributes["href"] = "/Areas/Admin/PopUp/Items/AddSubItemsForTour.aspx?control=" + _control + "&subapp=" + _subApp + "&pic=" + _pic + "&iiid=" + _iiId;
        ltrNote.Text = "<div class='text-warning'>Lưu ý: Có thể chọn nhiều ảnh cùng 1 lúc</div>";
        ltrTitle.Text = "Thêm ảnh";
        if (IsPostBack) return;
        GetImageConfig();
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
            ltrTitle.Text = "Thêm ảnh cho: " + dt.Rows[0][ItemsColumns.ViTitle];
        }
    }

    private void GetImageConfig()
    {
        cbHanCheKichThuoc.Checked = SettingsExtension.GetSettingKey(_control + "HanCheKichThuocAnhKhac2", _lang).Equals("1");
        tbHanCheW.Text = SettingsExtension.GetSettingKey(_control + "HanCheKichThuocAnhKhac_MaxWidth2", _lang);
        tbHanCheH.Text = SettingsExtension.GetSettingKey(_control + "HanCheKichThuocAnhKhac_MaxHeight2", _lang);

        cbTaoAnhNho.Checked = SettingsExtension.GetSettingKey(_control + "TaoAnhNhoChoAnhKhac2", _lang).Equals("1");
        tbAnhNhoW.Text = SettingsExtension.GetSettingKey(_control + "TaoAnhNhoChoAnhKhac_MaxWidth2", _lang);
        tbAnhNhoH.Text = SettingsExtension.GetSettingKey(_control + "TaoAnhNhoChoAnhKhac_MaxHeight2", _lang);
    }

    private void GetSubItems()
    {
        cbContiue.Visible = false;
        flimg.Attributes.Remove("required");
        flimg.AllowMultiple = false;
        ltrNote.Visible = false;
        btSubmit.Text = "Cập nhật";
        var dt = Subitems.GetData("1", "*", SubItemsTSql.GetById(_isiId), "");
        if (dt.Rows.Count <= 0) return;
        txtTitle.Text = dt.Rows[0][SubitemsColumns.VsiTitle].ToString();
        txtDesc.Text = dt.Rows[0][SubitemsColumns.VsiDescription].ToString();
        ddlStar.SelectedValue = dt.Rows[0][SubitemsColumns.FsiPriceOld].ToString();
        txtOrder.Text = dt.Rows[0][SubitemsColumns.IsiSortOrder].ToString();
        ltimg.Text = "<a href='" + UrlExtension.WebsiteUrl + _pic + "/" + dt.Rows[0][SubitemsColumns.VsiImage] + "' data-toggle='lightbox-image'>" + ImagesExtension.GetImage(_pic, dt.Rows[0][SubitemsColumns.VsiImage].ToString(), "", "w130px", false, false, "") + "</a>";
        hdImage.Value = dt.Rows[0][SubitemsColumns.VsiImage].ToString();
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
            var status = dt.Rows[i][SubitemsColumns.IsiStatus].ToString();
            s.Append("<tr id='item" + id + "'>");
            s.Append("<td>");
            s.Append("<a href='/" + _pic + "/" + image + @"' data-toggle='lightbox-image'>" + ImagesExtension.GetImage(_pic, image, titleItem, "mr5 w90px", true, true, "") + titleItem + "</a>");
            s.Append("</td>");
            s.Append("<td class='text-center'><label class='switch switch-primary'><input onchange='OnOffSubItems(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></td>");
            s.Append("<td class='text-center btn-group-xs'>");
            s.Append("<a href='/Areas/Admin/PopUp/Items/AddSubItemsForTour.aspx?control=" + _control + "&subapp=" + _subApp + "&pic=" + _pic + "&iiid=" + _iiId + "&isiid=" + id + @"' class='btn btn-default' title='Sửa'><i class='fa fa-pencil'></i></a> ");
            s.Append("<a href=\"javascript:DeleteSubItem('"+ Request.RawUrl +"','"+ _pic +"','"+ id +"','"+ titleItem +"')\" class=\"btn btn-danger\" title='Xóa'><i class='fa fa-times'></i></a>");
            s.Append("</td>");
            s.Append("</tr>");
        }
        s.Append("</tbody></table>");
        #endregion Lấy ra danh sách bài viết

        #region Xuất ra phân trang

        if (dtPager.Rows.Count <= 0 && dt.Rows.Count <= 0) return;
        var split = PagingCollection.SpilitPagesNoRewrite(Convert.ToInt32(dtPager.Rows[0]["TotalRows"]), top, Convert.ToInt32(_p), "/Areas/Admin/PopUp/Items/AddSubItemsForTour.aspx?control=" + _control + "&subapp=" + _subApp + "&pic=" + _pic + "&iiid=" + _iiId, "active", "normal", "first", "last", "preview", "next");
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
        #region Lưu cấu hình ảnh

        if (cbSaveConfig.Checked)
        {
            #region Hạn chế kích thước ảnh đại diện
            SettingsExtension.SetOtherSettingKey(_control + "HanCheKichThuocAnhKhac2", cbHanCheKichThuoc.Checked ? "1" : "0", _lang);
            SettingsExtension.SetOtherSettingKey(_control + "HanCheKichThuocAnhKhac_MaxWidth2", tbHanCheW.Text, _lang);
            SettingsExtension.SetOtherSettingKey(_control + "HanCheKichThuocAnhKhac_MaxHeight2", tbHanCheH.Text, _lang);
            #endregion

            #region Tạo ảnh nhỏ cho ảnh đại diện
            SettingsExtension.SetOtherSettingKey(_control + "TaoAnhNhoChoAnhKhac2", cbTaoAnhNho.Checked ? "1" : "0", _lang);
            SettingsExtension.SetOtherSettingKey(_control + "TaoAnhNhoChoAnhKhac_MaxWidth2", tbAnhNhoW.Text, _lang);
            SettingsExtension.SetOtherSettingKey(_control + "TaoAnhNhoChoAnhKhac_MaxHeight2", tbAnhNhoH.Text, _lang);
            #endregion
        }

        #endregion
        var path = Request.PhysicalApplicationPath + "/" + _pic + "/";

        #region Kiểm tra xem thư mục đã tồn tại chưa, nếu chưa -> tạo mới thư mục
        var dri = new DirectoryInfo(path);
        if (!dri.Exists) dri.Create();
        #endregion

        var listImage = Request.Files;
        for (var i = 0; i < listImage.Count; i++)
        {
            #region Xử lý hình ảnh
            var vImg = "";
            if (listImage[i].ContentLength > 0)
            {
                var filename = listImage[i].FileName;
                var fileEx = filename.Substring(filename.LastIndexOf(".", StringComparison.Ordinal));

                if (ImagesExtension.ValidType(fileEx))
                {
                    var fileNotEx = StringExtension.ReplateTitle(filename.Remove(filename.LastIndexOf(".", StringComparison.Ordinal)));
                    if (fileNotEx.Length > 50) fileNotEx = fileNotEx.Remove(50);
                    var ticks = DateTime.Now.Ticks.ToString();

                    #region Lưu ảnh

                    //Kiểm tra xem có tạo ảnh nhỏ hay ko
                    //Nếu không tạo ảnh nhỏ, tên tệp lưu bình thường theo kiểu: tên_tệp.phần_mở_rộng
                    //Nếu tạo ảnh nhỏ, tên tệp sẽ theo kiểu: tên_tệp_HasThumb.phần_mở_rộng
                    //Khi đó tên tệp ảnh nhỏ sẽ theo kiểu:   tên_tệp_HasThumb_Thumb.phần_mở_rộng
                    //Với cách lưu tên ảnh này, khi thực hiện lưu vào csdl chỉ cần lưu tên ảnh gốc
                    //khi hiển thị chỉ cần dựa vào tên ảnh gốc để biết ảnh đó có ảnh nhỏ hay không, việc này được thực hiện bởi ImagesExtension.GetImage, lập trình không cần làm gì thêm.
                    if (cbTaoAnhNho.Checked) vImg = fileNotEx + "_" + ticks + "_HasThumb" + fileEx;
                    else vImg = fileNotEx + "_" + ticks + fileEx;
                    listImage[i].SaveAs(path + vImg);

                    #endregion

                    #region Hạn chế kích thước

                    if (cbHanCheKichThuoc.Checked && tbHanCheW.Text.Length > 0 && tbHanCheH.Text.Length > 0)
                    {
                        //ImagesExtension.ResizeImage(path + vimg, "", tbHanCheW.Text, tbHanCheH.Text);
                        var image = new KalikoImage(path + vImg);
                        if (image.Width > int.Parse(tbHanCheW.Text) || image.Height > int.Parse(tbHanCheH.Text))
                        {
                            // Fitting
                            var thumb = image.Scale(new FitScaling(Convert.ToInt32(tbHanCheW.Text), Convert.ToInt32(tbHanCheH.Text)));
                            if (fileEx.ToLower() == ".png") thumb.SavePng(path + vImg);
                            else if (fileEx.ToLower() == ".bmp") thumb.SaveBmp(path + vImg);
                            else if (fileEx.ToLower() == ".gif") thumb.SaveGif(path + vImg);
                            else thumb.SaveJpg(path + vImg, 90);
                        }
                    }
                    #endregion

                    #region Tạo ảnh nhỏ

                    if (cbTaoAnhNho.Checked && tbAnhNhoW.Text.Length > 0 && tbAnhNhoH.Text.Length > 0)
                    {
                        var vimgThumb = fileNotEx + "_" + ticks + "_HasThumb_Thumb" + fileEx;
                        //ImagesExtension.ResizeImage(path + vimg, path + vimgThumb, tbAnhNhoW.Text, tbAnhNhoH.Text);

                        var image = new KalikoImage(path + vImg);
                        // Create thumbnail by Croping
                        var thumb = image.Scale(new FitScaling(Convert.ToInt32(tbAnhNhoW.Text), Convert.ToInt32(tbAnhNhoH.Text)));
                        if (fileEx.ToLower() == ".png") thumb.SavePng(path + vimgThumb);
                        else if (fileEx.ToLower() == ".bmp") thumb.SaveBmp(path + vimgThumb);
                        else if (fileEx.ToLower() == ".gif") thumb.SaveGif(path + vimgThumb);
                        else thumb.SaveJpg(path + vimgThumb, 90);
                    }

                    #endregion
                }
            }

            #endregion

            #region Lưu bản ghi SubItems
            var status = "0";
            if (cbStatus.Checked) status = "1";

            if (_edit)
            {
                if (vImg == "") vImg = hdImage.Value;
                else ImagesExtension.DeleteImageWhenDeleteItem(_pic, hdImage.Value);
                Subitems.Update(_iiId, _lang, _subApp, txtTitle.Text, txtDesc.Text, "", vImg, "", ddlStar.SelectedValue, "0", txtOrder.Text, status, DateTime.Now.ToString(), DateTime.Now.ToString(), _isiId);
            }
            else
            {
                Subitems.Insert(_iiId, _lang, _subApp, txtTitle.Text + (i > 0 ? " " + i : ""), txtDesc.Text, "", vImg, "", ddlStar.SelectedValue, "0", (int.Parse(txtOrder.Text) + i).ToString(), status, DateTime.Now.ToString(), DateTime.Now.ToString());
            }
            #endregion

            #region After Insert/Update
            ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo: " + txtTitle.Text + "', {type: 'success'});});", true);

            #endregion
        }

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
            Response.Redirect("/Areas/Admin/PopUp/Items/AddSubItemsForTour.aspx?control=" + _control + "&subapp=" + _subApp + "&pic=" + _pic + "&iiid=" + _iiId);
        }
    }
}
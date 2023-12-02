using System;
using System.Web;
using System.Web.UI;
using Developer.Keyword;
using RevosJsc.ProductControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;

public partial class Areas_Admin_Control_Product_Config_Index : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetSettingKey();
    }

    private void GetSettingKey()
    {
        txtIndex.Text = SettingsExtension.GetSettingKey(SettingKey.SoProductTrenTrangChu, _lang);
        txtCategory.Text = SettingsExtension.GetSettingKey(SettingKey.SoProductTrenTrangDanhMuc, _lang);
        txtOther.Text = SettingsExtension.GetSettingKey(SettingKey.SoProductKhacTrenMotTrang, _lang);

        txtMetaTitle.Text = SettingsExtension.GetSettingKey(SettingKey.MetaTitle, _lang);
        txtMetaKeyword.Text = SettingsExtension.GetSettingKey(SettingKey.MetaKeyword, _lang);
        txtMetaDescription.Text = SettingsExtension.GetSettingKey(SettingKey.MetaDescription, _lang);

        txt_content3.Text = SettingsExtension.GetSettingKey("KeyThongBaoHoanThanhDatHang", _lang);
        txt_content4.Text = SettingsExtension.GetSettingKey("TieuDeTrangSanPham", _lang);
        txt_content5.Text = SettingsExtension.GetSettingKey("NoiDungTrangSanPham", _lang);
    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        SettingsExtension.SetOtherSettingKey(SettingKey.SoProductTrenTrangChu, txtIndex.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.SoProductTrenTrangDanhMuc, txtCategory.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.SoProductKhacTrenMotTrang, txtOther.Text, _lang);

        SettingsExtension.SetOtherSettingKey(SettingKey.MetaTitle, txtMetaTitle.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.MetaKeyword, txtMetaKeyword.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.MetaDescription, txtMetaDescription.Text, _lang);

        SettingsExtension.SetOtherSettingKey("KeyThongBaoHoanThanhDatHang", txt_content3.Text, _lang);
        SettingsExtension.SetOtherSettingKey("TieuDeTrangSanPham", txt_content4.Text, _lang);
        SettingsExtension.SetOtherSettingKey("NoiDungTrangSanPham", txt_content5.Text, _lang);

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật cấu hình " + ProductKeyword.Product, logAuthor, logCreateDate);
        #endregion

        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);
        GetSettingKey();
    }
}
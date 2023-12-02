using System;
using System.Web.UI;
using Developer.Keyword;
using RevosJsc.DestinationControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;

public partial class Areas_Admin_Control_Destination_Config_Index : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetSettingKey();
    }

    private void GetSettingKey()
    {
        txtIndex.Text = SettingsExtension.GetSettingKey(SettingKey.SoDestinationTrenTrangChu, _lang);
        txtCategory.Text = SettingsExtension.GetSettingKey(SettingKey.SoDestinationTrenTrangDanhMuc, _lang);
        txtOther.Text = SettingsExtension.GetSettingKey(SettingKey.SoDestinationKhacTrenMotTrang, _lang);

        txtMetaTitle.Text = SettingsExtension.GetSettingKey(SettingKey.MetaTitle, _lang);
        txtMetaKeyword.Text = SettingsExtension.GetSettingKey(SettingKey.MetaKeyword, _lang);
        txtMetaDescription.Text = SettingsExtension.GetSettingKey(SettingKey.MetaDescription, _lang);

        cbHanCheKichThuoc.Checked = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhDestination, _lang).Equals("1");
        tbHanCheW.Text = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhDestination_MaxWidth, _lang);
        tbHanCheH.Text = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhDestination_MaxHeight, _lang);

        cbTaoAnhNho.Checked = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhDestination, _lang).Equals("1");
        tbAnhNhoW.Text = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhDestination_MaxWidth, _lang);
        tbAnhNhoH.Text = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhDestination_MaxHeight, _lang);
    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        SettingsExtension.SetOtherSettingKey(SettingKey.SoDestinationTrenTrangChu, txtIndex.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.SoDestinationTrenTrangDanhMuc, txtCategory.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.SoDestinationKhacTrenMotTrang, txtOther.Text, _lang);

        SettingsExtension.SetOtherSettingKey(SettingKey.MetaTitle, txtMetaTitle.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.MetaKeyword, txtMetaKeyword.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.MetaDescription, txtMetaDescription.Text, _lang);

        #region Hạn chế kích thước ảnh đại diện
        SettingsExtension.SetOtherSettingKey(SettingKey.HanCheKichThuocAnhDestination, cbHanCheKichThuoc.Checked ? "1" : "0", _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.HanCheKichThuocAnhDestination_MaxWidth, tbHanCheW.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.HanCheKichThuocAnhDestination_MaxHeight, tbHanCheH.Text, _lang);
        #endregion

        #region Tạo ảnh nhỏ cho ảnh đại diện
        SettingsExtension.SetOtherSettingKey(SettingKey.TaoAnhNhoChoAnhDestination, cbTaoAnhNho.Checked ? "1" : "0", _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.TaoAnhNhoChoAnhDestination_MaxWidth, tbAnhNhoW.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.TaoAnhNhoChoAnhDestination_MaxHeight, tbAnhNhoH.Text, _lang);
        #endregion

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật cấu hình " + DestinationKeyword.Destination, logAuthor, logCreateDate);
        #endregion

        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);
        GetSettingKey();
    }
}
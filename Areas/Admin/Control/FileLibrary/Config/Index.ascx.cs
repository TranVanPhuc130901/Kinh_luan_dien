using System;
using System.Web.UI;
using Developer.Keyword;
using RevosJsc.FileLibraryControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;

public partial class Areas_Admin_Control_FileLibrary_Config_Index : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetSettingKey();
    }

    private void GetSettingKey()
    {
        txtIndex.Text = SettingsExtension.GetSettingKey(SettingKey.SoFileLibraryTrenTrangChu, _lang);
        txtCategory.Text = SettingsExtension.GetSettingKey(SettingKey.SoFileLibraryTrenTrangDanhMuc, _lang);
        txtOther.Text = SettingsExtension.GetSettingKey(SettingKey.SoFileLibraryKhacTrenMotTrang, _lang);

        txtMetaTitle.Text = SettingsExtension.GetSettingKey(SettingKey.MetaTitle, _lang);
        txtMetaKeyword.Text = SettingsExtension.GetSettingKey(SettingKey.MetaKeyword, _lang);
        txtMetaDescription.Text = SettingsExtension.GetSettingKey(SettingKey.MetaDescription, _lang);

        cbHanCheKichThuoc.Checked = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhFileLibrary, _lang).Equals("1");
        tbHanCheW.Text = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhFileLibrary_MaxWidth, _lang);
        tbHanCheH.Text = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhFileLibrary_MaxHeight, _lang);

        cbTaoAnhNho.Checked = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhFileLibrary, _lang).Equals("1");
        tbAnhNhoW.Text = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhFileLibrary_MaxWidth, _lang);
        tbAnhNhoH.Text = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhFileLibrary_MaxHeight, _lang);
    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        SettingsExtension.SetOtherSettingKey(SettingKey.SoFileLibraryTrenTrangChu, txtIndex.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.SoFileLibraryTrenTrangDanhMuc, txtCategory.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.SoFileLibraryKhacTrenMotTrang, txtOther.Text, _lang);

        SettingsExtension.SetOtherSettingKey(SettingKey.MetaTitle, txtMetaTitle.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.MetaKeyword, txtMetaKeyword.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.MetaDescription, txtMetaDescription.Text, _lang);

        #region Hạn chế kích thước ảnh đại diện
        SettingsExtension.SetOtherSettingKey(SettingKey.HanCheKichThuocAnhFileLibrary, cbHanCheKichThuoc.Checked ? "1" : "0", _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.HanCheKichThuocAnhFileLibrary_MaxWidth, tbHanCheW.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.HanCheKichThuocAnhFileLibrary_MaxHeight, tbHanCheH.Text, _lang);
        #endregion

        #region Tạo ảnh nhỏ cho ảnh đại diện
        SettingsExtension.SetOtherSettingKey(SettingKey.TaoAnhNhoChoAnhFileLibrary, cbTaoAnhNho.Checked ? "1" : "0", _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.TaoAnhNhoChoAnhFileLibrary_MaxWidth, tbAnhNhoW.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.TaoAnhNhoChoAnhFileLibrary_MaxHeight, tbAnhNhoH.Text, _lang);
        #endregion

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật cấu hình " + FileLibraryKeyword.FileLibrary, logAuthor, logCreateDate);
        #endregion

        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);
        GetSettingKey();
    }
}
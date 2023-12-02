﻿using System;
using System.Web.UI;
using Developer.Keyword;
using RevosJsc.VideoControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using System.Web;

public partial class Areas_Admin_Control_Video_Config_Index : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetSettingKey();
    }

    private void GetSettingKey()
    {
        txtIndex.Text = SettingsExtension.GetSettingKey(SettingKey.SoVideoTrenTrangChu, _lang);
        txtCategory.Text = SettingsExtension.GetSettingKey(SettingKey.SoVideoTrenTrangDanhMuc, _lang);
        txtOther.Text = SettingsExtension.GetSettingKey(SettingKey.SoVideoKhacTrenMotTrang, _lang);

        txtMetaTitle.Text = SettingsExtension.GetSettingKey(SettingKey.MetaTitle, _lang);
        txtMetaKeyword.Text = SettingsExtension.GetSettingKey(SettingKey.MetaKeyword, _lang);
        txtMetaDescription.Text = SettingsExtension.GetSettingKey(SettingKey.MetaDescription, _lang);

        cbHanCheKichThuoc.Checked = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhVideo, _lang).Equals("1");
        tbHanCheW.Text = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhVideo_MaxWidth, _lang);
        tbHanCheH.Text = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhVideo_MaxHeight, _lang);

        cbTaoAnhNho.Checked = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhVideo, _lang).Equals("1");
        tbAnhNhoW.Text = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhVideo_MaxWidth, _lang);
        tbAnhNhoH.Text = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhVideo_MaxHeight, _lang);

        txtEmbed.Text = SettingsExtension.GetSettingKey("VideoTrangChu", _lang);
    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        SettingsExtension.SetOtherSettingKey(SettingKey.SoVideoTrenTrangChu, txtIndex.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.SoVideoTrenTrangDanhMuc, txtCategory.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.SoVideoKhacTrenMotTrang, txtOther.Text, _lang);

        SettingsExtension.SetOtherSettingKey(SettingKey.MetaTitle, txtMetaTitle.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.MetaKeyword, txtMetaKeyword.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.MetaDescription, txtMetaDescription.Text, _lang);
        SettingsExtension.SetOtherSettingKey("VideoTrangChu", HttpUtility.HtmlDecode(txtEmbed.Text), _lang);

        #region Hạn chế kích thước ảnh đại diện
        SettingsExtension.SetOtherSettingKey(SettingKey.HanCheKichThuocAnhVideo, cbHanCheKichThuoc.Checked ? "1" : "0", _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.HanCheKichThuocAnhVideo_MaxWidth, tbHanCheW.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.HanCheKichThuocAnhVideo_MaxHeight, tbHanCheH.Text, _lang);
        #endregion

        #region Tạo ảnh nhỏ cho ảnh đại diện
        SettingsExtension.SetOtherSettingKey(SettingKey.TaoAnhNhoChoAnhVideo, cbTaoAnhNho.Checked ? "1" : "0", _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.TaoAnhNhoChoAnhVideo_MaxWidth, tbAnhNhoW.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingKey.TaoAnhNhoChoAnhVideo_MaxHeight, tbAnhNhoH.Text, _lang);
        #endregion

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật cấu hình " + VideoKeyword.Video, logAuthor, logCreateDate);
        #endregion

        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);
        GetSettingKey();
    }
}
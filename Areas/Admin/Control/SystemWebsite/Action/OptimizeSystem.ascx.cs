using System;
using System.Web.UI;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;

public partial class Areas_Admin_Control_SystemWebsite_Action_OptimizeSystem : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        GetSettingKey();
    }

    private void GetSettingKey()
    {
        textTagTitle.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyTitleWebsite, _lang);
        textTagKeyword.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyKeywordMetaWebsite, _lang);
        textTagDescription.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyDescMetatagWebsite, _lang);
        txtHead.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyCodeOnHead, _lang);
        txtBody.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyCodeOnBody, _lang);
        txtBodyBottom.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyCodeUnderBody, _lang);
    }
    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyTitleWebsite, textTagTitle.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyKeywordMetaWebsite, textTagKeyword.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyDescMetatagWebsite, textTagDescription.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyCodeOnHead, txtHead.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyCodeOnBody, txtBody.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyCodeUnderBody, txtBodyBottom.Text, _lang);
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật Tối ưu công cụ tìm kiếm", logAuthor, logCreateDate);
        #endregion
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);
    }

}
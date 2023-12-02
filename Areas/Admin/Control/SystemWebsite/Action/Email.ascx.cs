using System;
using System.Web.UI;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;

public partial class Areas_Admin_Control_SystemWebsite_Action_Email : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetSettingKey();
    }
    private void GetSettingKey()
    {
        tbEmail.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailSystem, _lang);
        tbEmailOther.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailManager, _lang);
        hdOldPass.Value = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailSystemPassword, _lang);
        if (hdOldPass.Value.Length > 0) tbPassword.Attributes["placeholder"] = "Đã nhập mật khẩu";
        else tbPassword.Attributes["placeholder"] = "Chưa nhập mật khẩu";

    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyEmailSystem, tbEmail.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyEmailManager, tbEmailOther.Text, _lang);
        var password = tbPassword.Text;
        if (password.Length < 1) password = hdOldPass.Value;
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyEmailSystemPassword, password, _lang);
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật Email hệ thống", logAuthor, logCreateDate);
        #endregion
        GetSettingKey();
    }
}
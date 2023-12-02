using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Extension;
using Developer.Keyword;
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
        txt_contentx.Text = SettingsExtension.GetSettingKey("NoiDungTrangLienHe", _lang);
        txt_content.Text = SettingsExtension.GetSettingKey("ThongBaoHoanThanhGuiLienHe", _lang);
    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        SettingsExtension.SetOtherSettingKey("ThongBaoHoanThanhGuiLienHe", HttpUtility.HtmlDecode(txt_content.Text), _lang);
        SettingsExtension.SetOtherSettingKey("NoiDungTrangLienHe", HttpUtility.HtmlDecode(txt_contentx.Text), _lang);

        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật cấu hình " + ContactKeyword.Contact, logAuthor, logCreateDate);
        #endregion

        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);

        GetSettingKey();
    }
}
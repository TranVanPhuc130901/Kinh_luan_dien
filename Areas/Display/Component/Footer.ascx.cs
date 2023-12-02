using System;
using System.Diagnostics;
using System.Text;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.ContactControl;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Display_Component_Footer : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    protected string Copyright = "";
    //protected string Hotline2 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetSocial();
    }


    private void GetSocial()
    {
        Copyright = SettingsExtension.GetSettingKey(SettingsExtension.KeyContentFooterWebsite, _lang);
        //Hotline2 = SettingsExtension.GetSettingKey(SettingsExtension.KeyHotLine + "2", _lang);
        //var zalo = SettingsExtension.GetSettingKey(SettingsExtension.KeyLinkedIn, _lang);
        //var skype = SettingsExtension.GetSettingKey(SettingsExtension.KeyRss, _lang);
        //var facebook = SettingsExtension.GetSettingKey(SettingsExtension.KeyLinkFanpageFacebook, _lang);
        //var twitter = SettingsExtension.GetSettingKey(SettingsExtension.KeyTitleWebsite, _lang);
        //var pinterest = SettingsExtension.GetSettingKey(SettingsExtension.KeyPinterest, _lang);
        //var instagram = SettingsExtension.GetSettingKey(SettingsExtension.KeyInstagram, _lang);
        //var youtube = SettingsExtension.GetSettingKey(SettingsExtension.KeyLinkYoutubeChanel, _lang);
        //var linkedIn = SettingsExtension.GetSettingKey(SettingsExtension.KeyLinkedIn, _lang);
        //var email = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailWebsite, _lang);

        //        ltrStatistic.Text = @"
        //<div class='item'>
        //    <p class='lh22'>
        //        <span class='fwb dib' style='width: 115px'>Đang online</span><span class='pr10'>:</span><span>"+ NumberExtension.FormatNumber(OnlineActiveUsers.OnlineUsersInstance.OnlineUsers.UsersCount.ToString()) + @"</span>
        //    </p>
        //    <p class='lh22'>
        //        <span class='fwb dib' style='width: 115px'>Tổng truy cập</span><span class='pr10'>:</span><span>"+ NumberExtension.FormatNumber(SettingsExtension.GetSettingKey(SettingsExtension.KeyTotalView, _lang)) +@"</span>
        //    </p>
        //</div>";
        //ltrCopyright.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyContentFooterWebsite, _lang);
    }
}
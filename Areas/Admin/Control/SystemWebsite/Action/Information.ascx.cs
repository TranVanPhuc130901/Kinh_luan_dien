using System;
using System.IO;
using System.Web.UI;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.SystemWebsiteControl;

public partial class Areas_Admin_Control_SystemWebsite_Action_Information : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private string _pic = FolderPic.SystemWebsite;
    protected void Page_Load(object sender, EventArgs e)
    {
        GetSettingKey();
    }

    private void GetSettingKey()
    {
        txtHotline.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyHotLine, _lang);
        txtHotline2.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyHotLine + "2", _lang);
        txtPhone.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyPhoneContact, _lang);
        txtEmail.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailWebsite, _lang);
        txtFanpageFb.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyLinkFanpageFacebook, _lang);
        txtFanpageTw.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyLinkFanpageTwitter, _lang);
        txtYoutube.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyLinkYoutubeChanel, _lang);
        txtOpenTime.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyOpenTime, _lang);
        txtPinterest.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyPinterest, _lang);
        txtInstagram.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyInstagram, _lang);
        txtRss.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyRss, _lang);
        txtCpr.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyContentFooterWebsite, _lang);
        txtAdd.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyCompanyAddress, _lang);
        txtLinkedIn.Text = SettingsExtension.GetSettingKey(SettingsExtension.KeyLinkedIn, _lang);
        txtBrand.Text = SettingsExtension.GetSettingKey("KeyBrandName", _lang);

        hd_img1.Value = SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, _lang);
        if (hd_img1.Value.Length > 0) ltrimgShare.Text = "<div class='pb10'><img src='/" + FolderPic.SystemWebsite + "/" + hd_img1.Value + "' class='mw300px' /></div>";
        hd_img2.Value = SettingsExtension.GetSettingKey(SettingsExtension.KeyFavicon, _lang);
        if (hd_img2.Value.Length > 0) ltrimgFavicon.Text = "<div class='pb10'><img src='/" + FolderPic.SystemWebsite + "/" + hd_img2.Value + "' class='mw300px' /></div>";

    }
    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        #region Kiểm tra xem thư mục chứa ảnh đã tồn tại chưa, nếu chưa -> tạo mới thư mục

        var path = Request.PhysicalApplicationPath + "/" + _pic + "/";
        var dri = new DirectoryInfo(path);
        if (!dri.Exists) dri.Create();

        #endregion

        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyHotLine, txtHotline.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyHotLine + "2", txtHotline2.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyPhoneContact, txtPhone.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyEmailWebsite, txtEmail.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyLinkFanpageFacebook, txtFanpageFb.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyLinkFanpageTwitter, txtFanpageTw.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyLinkYoutubeChanel, txtYoutube.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyOpenTime, txtOpenTime.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyPinterest, txtPinterest.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyInstagram, txtInstagram.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyRss, txtRss.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyContentFooterWebsite, txtCpr.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyCompanyAddress, txtAdd.Text, _lang);
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyLinkedIn, txtLinkedIn.Text, _lang);
        SettingsExtension.SetOtherSettingKey("KeyBrandName", txtBrand.Text, _lang);

        #region Image Image share

        var vimg = "";
        if (flimgShare.FileName.Length > 0 && flimgShare.PostedFile.ContentLength > 0)
        {
            var filename = "";
            filename = Path.GetFileName(flimgShare.PostedFile.FileName);
            var fileex = "";

            fileex = Path.GetExtension(filename).ToLower();
            if (fileex == ".jpg" || fileex == ".gif" || fileex == ".png" || fileex == ".bmp")
            {
                var fileNotEx = Path.GetFileNameWithoutExtension(flimgShare.PostedFile.FileName);
                vimg = StringExtension.ReplateTitle(fileNotEx) + DateTime.Now.Ticks + fileex;
                flimgShare.SaveAs(Request.PhysicalApplicationPath + "/" + _pic + "/" + vimg);
            }
        }

        #endregion

        if (vimg.Equals(""))
        {
            vimg = hd_img1.Value;
        }
        if (!vimg.Equals(hd_img1.Value))
        {
            ImagesExtension.DeleteImageWhenDeleteItem(_pic, hd_img1.Value);
        }
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyImageShareHomepage, vimg, _lang);

        #region Favicon

        var favicon = "";
        if (flimgFavicon.FileName.Length > 0 && flimgFavicon.PostedFile.ContentLength > 0)
        {
            var filename = "";
            filename = Path.GetFileName(flimgFavicon.PostedFile.FileName);
            var fileex = "";
            fileex = Path.GetExtension(filename).ToLower();
            if (fileex == ".ico" || fileex == ".png")
            {
                var fileNotEx = Path.GetFileNameWithoutExtension(flimgFavicon.PostedFile.FileName);
                favicon = fileNotEx + DateTime.Now.Ticks + fileex;
                flimgFavicon.SaveAs(Request.PhysicalApplicationPath + "/" + _pic + "/" + favicon);
            }
        }

        #endregion

        if (favicon.Equals(""))
        {
            favicon = hd_img2.Value;
        }
        if (!favicon.Equals(hd_img2.Value))
        {
            ImagesExtension.DeleteImageWhenDeleteItem(_pic, hd_img2.Value);
        }
        SettingsExtension.SetOtherSettingKey(SettingsExtension.KeyFavicon, favicon, _lang);

        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật Thông tin website", logAuthor, logCreateDate);
        #endregion
        GetSettingKey();
    }
}
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.TSql;

namespace RevosJsc.Extension
{
    public class SettingsExtension
    {
        public const string KeyCodeOnBody = "PropertyCodeOnBody";
        public const string KeyCodeOnHead = "PropertyCodeOnHead";
        public const string KeyCodeUnderBody = "PropertyCodeUnderBody";
        public const string KeyContentContactWebsite = "PropertyContentContactWebsite";
        public const string KeyContentContactWebsiteBottom = "PropertyContentContactWebsite_bottom";
        public const string KeyCompanyAddress = "PropertyCompanyAddress";
        public const string KeyContentFooterWebsite = "PropertyContentFooterWebsite";
        public const string KeyDescMetatagWebsite = "PropertyDescMetatagWebsite";
        public const string KeyEmailManager = "PropertyEmailManager";
        public const string KeyEmailSystem = "PropertyEmailSystem";
        public const string KeyEmailSystemPassword = "PropertyEmailSystemPassword";
        public const string KeyEmailWebsite = "PropertyEmailWebsite";
        public const string KeyFavicon = "PropertyFavicon";
        public const string KeyHienThiPopupTaiTrangChu = "OtherSettingHienThiPopupTaiTrangChu";
        public const string KeyHotLine = "PropertyHotLine";
        public const string KeyRss = "PropertyRss";
        public const string KeyImageShareHomepage = "PropertyImageShareHomepage";
        public const string KeyKeywordMetaWebsite = "PropertyKeywordMetaWebsite";
        public const string KeyLinkedIn = "PropertyLinkedIn";
        public const string KeyLinkFanpageFacebook = "PropertyLinkFanpageFacebook";
        public const string KeyLinkFanpageTwitter = "PropertyLinkFanpageTwitter";
        public const string KeyLinkGooglePlus = "PropertyLinkGooglePlus";
        public const string KeyLinkYoutubeChanel = "PropertyLinkYoutubeChanel";
        public const string KeyNhacNenWebsite = "PropertyNhacNenWebsite";
        public const string KeyNoiDungPopupTaiTrangChu = "OtherSettingNoiDungPopupTaiTrangChu";
        public const string KeyOpenTime = "PropertyOpenTime";
        public const string KeyPinterest = "PropertyPinterest";
        public const string KeyInstagram = "PropertyInstagram";
        public const string KeyPhoneContact = "PropertyPhoneContact";
        public const string KeyThongBaoSauKhiGuiLienHe = "OtherSettingThongBaoSauKhiGuiLienHe";
        public const string KeyTitleWebsite = "PropertyTitleWebsite";
        public const string KeyTotalView = "PropertyTotalView";

        public static void SetOtherSettingKey(string key, string value, string language)
        {
            var dt = Settings.GetData("", SettingsColumns.VsValue, DataExtension.AndConditon(SettingsTSql.GetBykey(key), SettingsTSql.GetByLang(language)), "");
            if (dt.Rows.Count > 0)
            {
                //Cap nhat                    
                Settings.Update(key, value, language);
            }
            else Settings.Insert(key, value, language);
        }

        public static string GetSettingKey(string key, string language)
        {
            var dt = Settings.GetData("1", SettingsColumns.VsValue, DataExtension.AndConditon(SettingsTSql.GetBykey(key), SettingsTSql.GetByLang(language)), "");
            return dt.Rows.Count > 0 ? dt.Rows[0][SettingsColumns.VsValue].ToString() : "";
        }
    }
}

using System;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.ContactControl;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Display_Contact_Control_LoadControl : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    protected void Page_Load(object sender, EventArgs e)
    {
        //GetContact();
        //ltrContent.Text = SettingsExtension.GetSettingKey("ThongBaoHoanThanhGuiLienHe", _lang);
    }
    private void GetContact()
    {
        var condition = DataExtension.AndConditon(
            ContactsTSql.GetByStatus("1"),
            ContactsTSql.GetByParentId("0"),
            ContactsTSql.GetByLang(_lang)
        );
        var dt = Contacts.GetData("1", "*", condition, ContactsColumns.IcSortOrder);
        if (dt.Rows.Count < 1) return;
        var name = dt.Rows[0][ContactsColumns.VcName].ToString();
        //var image = dt.Rows[0][ContactsColumns.VcImage].ToString();
        var add = dt.Rows[0][ContactsColumns.VcAddress].ToString();
        var hotline = dt.Rows[0][ContactsColumns.VcHotline].ToString();
        var email = dt.Rows[0][ContactsColumns.VcEmail].ToString();
        ltrContact.Text = @"
<b>"+ name + @"</b>
<span>Địa chỉ: " + add + @"</span>
<span>Hotline: <a href='tel:" + StringExtension.RemoveCharsInPhoneNumber(hotline) + "'>" + hotline + @"</a></span>
<span>Email: <a href='mailto:" + email + "'>" + email + @"</a></span>
<span>Website: " + UrlExtension.WebsiteUrl + @"</span>";
        //ltrMap.Text = dt.Rows[0][ContactsColumns.VcMap].ToString();
    }
}
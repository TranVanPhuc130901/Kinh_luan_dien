using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Text;
using System.Web.Script.Serialization;
using Developer.Extension;
using NPOI.OpenXmlFormats.Wordprocessing;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Display_Ajax_Contact : System.Web.UI.Page
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private string _action = "";
    private readonly JavaScriptSerializer _js = new JavaScriptSerializer();
    private string _device = "on PC";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (Request.Form["action"] != null) _action = Request.Form["action"];
        if (_action == "") return;
        if (MobileExtension.IsMobileView()) _device = "on mobile";
        switch (_action)
        {
            case "MakeAnAppointment":
                MakeAnAppointment();
                break;
            case "RegisterEmail":
                RegisterEmail();
                break;
            case "SendContact":
                SendContact();
                break;
            case "OpenHotline":
                OpenHotline();
                break;
            case "dangkynhanlogo":
                Dangkynhanlogo();
                break;
            case "dangkynhanlogo2":
                Dangkynhanlogo2();
                break;
            case "GetMap":
                GetMap();
                break;
        }
    }

    private void MakeAnAppointment()
    {
        #region Lấy thông tin

        var name = Request.Form["name"] ?? "";
        var phone = Request.Form["phone"] ?? "";
        var source = CookieExtension.GetCookies("url_referrer") + " - " + _device;
        var link = Request.Headers["Referer"] ?? "";

        #endregion Lấy thông tin

        #region Thêm liên hệ
        ContactDetails.Insert(name, "", phone, "", "Appointment", "", source, DateTime.Now.ToString(), "0");
        #endregion Thêm

        #region Gửi email thông báo đến email hệ thống

        var date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        var subject = "Bạn có một yêu cầu tư vấn sản phẩm từ " + UrlExtension.WebsiteUrl + " " + date;
        var body = @"
<div style='width: 100%;margin: auto; font-family: Arial'>
    <table style='width: 100%; border-spacing: 0;border-collapse: collapse;border-right:1px solid'>
        <tbody>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Họ tên</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + name + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Điện thoại</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + phone + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Link</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + link + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Nguồn</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + source + @"</td>
            </tr>
        </tbody>
    </table>
</div>";

        var emailManager = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailManager, _lang);
        var emailReceived = "";
        var emailCc = new List<string>();
        for (var i = 0; i < emailManager.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
        {
            var item = emailManager.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i];
            if (i == 0) emailReceived = item;
            else emailCc.Add(item);
        }
        EmailExtension.SendEmail(emailReceived, subject, body, emailCc.ToArray());

        #endregion Gửi email thông báo đến email hệ thống
    }

    private void RegisterEmail()
    {
        var txtEmail = Request.Form["email"] ?? "";
        if (txtEmail.Equals("")) Response.Redirect(UrlExtension.WebsiteUrl + RewriteExtension.Contact + RewriteExtension.Extensions);
        var exist = ExistedEmail(txtEmail, RevosJsc.MemberControl.CodeApplications.MemberNewsletter);
        if (exist)
        {

            string[] reply = { "Email " + txtEmail + " đã được sử dụng, hãy nhập một email khác!" };
            Response.Output.Write(_js.Serialize(reply));
        }
        else{

            #region Thêm tài khoản

            //Thêm tài khoản
            Members.Insert(RevosJsc.MemberControl.CodeApplications.MemberNewsletter, "", "", "", "", "", "", txtEmail, "", "", "", "", "", "", "", "", "", "1", DateTime.Now.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture), "", "", "", "");

            #endregion Thêm tài khoản

            #region Gửi email thông báo đến email hệ thống

            var date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            var subject = "Thông báo từ " + UrlExtension.WebsiteUrl + " " + date;
            var body = @"
<div style='font:bold 14px Arial;padding-bottom:15px'>Xin chào! Bạn có đăng ký nhận tin khuyến mãi từ " + UrlExtension.WebsiteUrl + @"</div>
<ul>
    <li>Email: " + txtEmail + @"</li>
    <li>Gửi lúc: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + @"</li>
</ul>";
            // Chỉ thông báo đến admin
            var emailReceived = "";
            var emailManager = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailManager, _lang);
            var emailCc = new List<string>();
            for (var i = 0; i < emailManager.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
            {
                var item = emailManager.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i];
                if (i == 0) emailReceived = item;
                else emailCc.Add(item);
            }
            if (emailReceived.Length > 0) EmailExtension.SendEmail(emailReceived, subject, body, emailCc.ToArray());

            #endregion Gửi email thông báo đến email hệ thống

            string[] reply = { "Đăng ký thành công email " + txtEmail };
            Response.Output.Write(_js.Serialize(reply));
        }
    }

    private bool ExistedEmail(string email, string app)
    {
        var condition = DataExtension.AndConditon(
            MembersTSql.GetByEmail(email),
            MembersTSql.GetByApp(RevosJsc.MemberControl.CodeApplications.MemberNewsletter)
        );
        var dt = Members.GetData("", MembersColumns.ImId, condition, "");
        return dt.Rows.Count > 0;
    }

    private void GetMap()
    {
        var id = Request.Params["id"] ?? "";
        var dt = Contacts.GetData("1", "*", ContactsTSql.GetById(id), "");
        Response.Write(dt.Rows.Count > 0 ? dt.Rows[0][ContactsColumns.VcMap].ToString() : "");
    }

    private void Dangkynhanlogo2()
    {
        #region Lấy thông tin

        var name = Request.Form["name"];
        var email = Request.Form["email"];
        var phone = Request.Form["phone"];
        var subject = Request.Form["subject"];
        var link = Request.Form["link"];
        var source = CookieExtension.GetCookies("url_referrer") + " - " + _device;

        #endregion

        #region Thêm liên hệ
        ContactDetails.Insert(name, email, phone, link, "Dangkynhanlogo2", subject, source, DateTime.Now.ToString(), "0");
        #endregion Thêm

        #region Gửi email thông báo đến email hệ thống

        var date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        var subjectEmail = "Bạn có một liên hệ từ " + UrlExtension.WebsiteUrl + " " + date;
        var body = @"
<div style='width: 100%;margin: auto; font-family: Arial'>
    <table style='width: 100%; border-spacing: 0;border-collapse: collapse;border-right:1px solid'>
        <tbody>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Họ tên</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + name + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Email</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + email + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Điện thoại</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + phone + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Nội dung</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + subject.Replace("\n", "<br/>") + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Link bài viết</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'><a href='" + link + "' title='" + link + "'>" + link + @"</a></td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Nguồn</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + source + @"</td>
            </tr>
        </tbody>
    </table>
</div>
";

        var emailManager = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailManager, _lang);
        var emailReceived = "";
        var emailCc = new List<string>();
        for (var i = 0; i < emailManager.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
        {
            var item = emailManager.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i];
            if (i == 0) emailReceived = item;
            else emailCc.Add(item);
        }
        EmailExtension.SendEmail(emailReceived, subjectEmail, body, emailCc.ToArray());

        #endregion Gửi email thông báo đến email hệ thống

        Response.End();
    }
    private void Dangkynhanlogo()
    {
        #region Lấy thông tin

        var name = Request.Form["name"];
        var email = Request.Form["email"];
        var phone = Request.Form["phone"];
        var subject = Request.Form["subject"];
        var link = Request.Form["link"];
        var source = CookieExtension.GetCookies("url_referrer") + " - " + _device;

        #endregion

        #region Thêm liên hệ
        ContactDetails.Insert(name, email, phone, link, "Dangkynhanlogo", subject, source, DateTime.Now.ToString(), "0");
        #endregion Thêm

        #region Gửi email thông báo đến email hệ thống

        var date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        var subjectEmail = "Bạn có một liên hệ từ " + UrlExtension.WebsiteUrl + " " + date;
        var body = @"
<div style='width: 100%;margin: auto; font-family: Arial'>
    <table style='width: 100%; border-spacing: 0;border-collapse: collapse;border-right:1px solid'>
        <tbody>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Họ tên</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + name + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Email</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + email + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Điện thoại</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + phone + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Nội dung</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + subject.Replace("\n", "<br/>") + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Link bài viết</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'><a href='" + link + "' title='" + link + "'>" + link + @"</a></td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Nguồn</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + source + @"</td>
            </tr>
        </tbody>
    </table>
</div>
";

        var emailManager = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailManager, _lang);
        var emailReceived = "";
        var emailCc = new List<string>();
        for (var i = 0; i < emailManager.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
        {
            var item = emailManager.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i];
            if (i == 0) emailReceived = item;
            else emailCc.Add(item);
        }
        EmailExtension.SendEmail(emailReceived, subjectEmail, body, emailCc.ToArray());

        #endregion Gửi email thông báo đến email hệ thống

        Response.End();
    }
    private void SendContact()
    {
        #region Lấy thông tin

        var name = Request.Form["name"] ?? "";
        var phone = Request.Form["phone"] ?? "";
        var email = Request.Form["email"] ?? "";
        var message = Request.Form["message"] ?? "";
        var source = CookieExtension.GetCookies("url_referrer") + " - " + _device;

        #endregion Lấy thông tin

        #region Thêm liên hệ
        ContactDetails.Insert(name, email, phone, "", "Contact", message, source, DateTime.Now.ToString(), "0");
        #endregion Thêm

        #region Gửi email thông báo đến email hệ thống

        var date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        var subject = "Bạn có một liên hệ - góp ý từ " + UrlExtension.WebsiteUrl + " " + date;
        var body = @"
<div style='width: 100%;margin: auto; font-family: Arial'>
    <table style='width: 100%; border-spacing: 0;border-collapse: collapse;border-right:1px solid'>
        <tbody>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Họ tên</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + name + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Điện thoại</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + phone + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Email</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + email + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Nội dung</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + message.Replace("\n", "<br/>") + @"</td>
            </tr>
            <tr>
                <td style='padding: 10px; width: 25%;border: 1px solid #d2d2d2'>Nguồn</td>
                <td style='padding: 10px; width: 75%;border: 1px solid #d2d2d2'>" + source + @"</td>
            </tr>
        </tbody>
    </table>
</div>";

        var emailManager = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailManager, _lang);
        var emailReceived = "";
        var emailCc = new List<string>();
        for (var i = 0; i < emailManager.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
        {
            var item = emailManager.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[i];
            if (i == 0) emailReceived = item;
            else emailCc.Add(item);
        }
        EmailExtension.SendEmail(emailReceived, subject, body, emailCc.ToArray());

        #endregion Gửi email thông báo đến email hệ thống
    }
    private void GetMapContent()
    {
        var s = new StringBuilder();

        #region Lấy thông tin

        var condition = DataExtension.AndConditon(
            ContactsTSql.GetByLang(_lang),
            ContactsTSql.GetByStatus("1"),
            ContactsTSql.GetByParentId("0")
            );
        var dt = Contacts.GetData("1", "*", condition, ContactsColumns.IcSortOrder);
        if (dt.Rows.Count > 0)
        {
            s.Append("<div id='lightboxx'></div>");
            s.Append("<div id='map_popup'><div class='inner'><a class='btnClose' href='javascript:closeMap();'>Đóng lại ( X )</a>" + dt.Rows[0][ContactsColumns.VcMap] + "</div></div>");
        }

        #endregion Lấy thông tin

        string[] reply = { s.ToString() };
        Response.Output.Write(_js.Serialize(reply));
    }

    #region Xử lý lượt click
    private void OpenHotline()
    {
        var time = DateTime.Now.ToString("dd/MM/yyyy");
        var dtx = GetClick("", "*", "Time = '" + time + "'", "");
        if (dtx.Rows.Count > 0)
        {
            UpdateClick("Value = Value + 1", "Time = '" + time + "'");
            // Bổ sung: Lưu chi tiết lượt click của người dùng đến từ đâu
            if (CookieExtension.CheckValidCookies("url_referrer"))
            {
                if (CookieExtension.GetCookies("url_referrer").Equals("Google search")) UpdateClick("SEO = SEO + 1", "Time = '" + time + "'");
                else if (CookieExtension.GetCookies("url_referrer").Equals("Google Ads")) UpdateClick("Adwords = Adwords + 1", "Time = '" + time + "'");
                else if (CookieExtension.GetCookies("url_referrer").Equals("Facebook")) UpdateClick("Facebook = Facebook + 1", "Time = '" + time + "'");
                else UpdateClick("Other = Other + 1", "Time = '" + time + "'");
            }
            else
            {
                UpdateClick("Unknow = Unknow + 1", "Time = '" + time + "'");
            }
        }
        else
        {
            var seo = "0";
            var adwords = "0";
            var facebook = "0";
            var other = "0";
            var unknow = "0";
            if (CookieExtension.CheckValidCookies("url_referrer"))
            {
                if (CookieExtension.GetCookies("url_referrer").Equals("Google search")) seo = "1";
                else if (CookieExtension.GetCookies("url_referrer").Equals("Google Ads")) adwords = "1";
                else if (CookieExtension.GetCookies("url_referrer").Equals("Facebook")) facebook = "1";
                else other = "1";
            }
            else
            {
                unknow = "1";
            }
            InsertClick("", time, "1", seo, adwords, facebook, other, unknow);
        }

        const string key = "SoLanClickMuaHangTrucTiep";
        var dt = Settings.GetData("", SettingsColumns.VsValue, SettingsTSql.GetBykey(key), "");
        if (dt.Rows.Count > 0)
        {
            Settings.UpdateValues("VsValue = VsValue + 1", SettingsTSql.GetBykey(key));
        }
        else
        {
            Settings.Insert(key, "1", _lang);
        }
        Response.End();
    }

    #region Inserts

    private static void InsertClick(string ip, string time, string value, string seo, string adwords, string facebook, string other, string unknow)
    {
        var cmd = new OleDbCommand(" INSERT INTO Click values(?,?,?,?,?,?,?,?,?) ") { CommandType = CommandType.Text };
        cmd.Parameters.AddWithValue("@IP", ip);
        cmd.Parameters.AddWithValue("@Time", time);
        cmd.Parameters.AddWithValue("@Value", value);
        cmd.Parameters.AddWithValue("@SEO", seo);
        cmd.Parameters.AddWithValue("@Adwords", adwords);
        cmd.Parameters.AddWithValue("@Facebook", facebook);
        cmd.Parameters.AddWithValue("@Other", other);
        cmd.Parameters.AddWithValue("@Unknow", unknow);
        cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
        SqlDatabase.ExecuteNoneQuery(cmd);
    }

    #endregion Inserts

    #region Update

    private static void UpdateClick(string values, string condition)
    {
        if (values.Equals("")) return;
        SqlDatabase.ExecuteNoneQuery(new OleDbCommand(" UPDATE Click SET " + values + " WHERE " + condition + " "));
    }

    #endregion Update

    #region Get data

    public static DataTable GetClick(string top, string fields, string condition, string orderby)
    {
        if (fields.Equals("")) return new DataTable("dt");
        if (!top.Equals("")) top = "TOP " + top;
        if (!orderby.Equals("")) orderby = " ORDER BY " + orderby;
        var cmd = new OleDbCommand(" SELECT " + top + fields + " FROM Click WHERE " + condition + orderby);
        return SqlDatabase.GetData(cmd);
    }

    #endregion Get data

    #endregion
}
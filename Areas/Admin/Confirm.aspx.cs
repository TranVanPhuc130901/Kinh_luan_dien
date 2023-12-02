using System;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Confirm : System.Web.UI.Page
{
    private string account = "";
    private string confirm = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["account"] != null) account = QueryStringExtension.GetQueryString("account");
        if (Request.QueryString["confirm"] != null) confirm = QueryStringExtension.GetQueryString("confirm");
        if (IsPostBack) return;
        var condition = DataExtension.AndConditon(
            UsersTSql.GetByAccount(account),
            UsersTSql.GetByVerificationCode(confirm)
        );
        if (account.Equals("") || confirm.Equals(""))
        {
            ltrNotification.Text = @"
<b>Khôi phục mật khẩu thất bại</b><br/><br/>
Mã xác nhận hoặc đường dẫn đã được kích hoạt, nếu bạn chưa nhận được mật khẩu mới vui lòng thử lại tính năng khôi phục mật khẩu.
";
            return;
        }
        var dt = Users.GetData("1", "*", condition, "");
        if (dt.Rows.Count > 0)
        {
            var rnd = new Random();
            var newpass = rnd.Next(111111, 999999);
            var fields = new[] { UsersColumns.VuPassword, UsersColumns.VuVerificationCode };
            var values = new[] { SecurityExtension.BuildPassword(newpass.ToString()), "" };
            Users.UpdateValues(DataExtension.UpdateValues(fields, values), UsersTSql.GetById(dt.Rows[0][UsersColumns.IuId].ToString()));
            var contentEmail = @"
<table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; width: 100%; max-width: 600px;' class='content'>
    <tr>
        <td align='center' style='padding: 20px 20px 20px 20px; color: #ffffff; font-family: Arial, sans-serif; font-size: 36px; font-weight: bold;'>
            <img src='" + UrlExtension.WebsiteUrl + @"Areas/Admin/img/icon120.png' alt='Revos Viet Nam' width='120' height='120' style='display:block;' />
        </td>
    </tr>
    <tr>
        <td bgcolor='#f9f9f9' style='padding: 20px 20px 0 20px; color: #555555; font-family: Arial, sans-serif; '>
            Hệ thống đã hoàn tất yêu cầu khôi phục mật khẩu quản trị tại website " + UrlExtension.WebsiteUrl + @"<br/><br/>
            Thông tin mật khẩu mới như sau:<br/><br/>
            <ul>
                <li>Tài khoản: <b>" + account + @"</b></li>
                <li>Mật khẩu mới: <b>" + newpass + @"</b></li>
            </ul>
        </td>
    </tr>
    <tr>
        <td align='center' bgcolor='#dddddd' style='padding: 15px 10px 15px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 12px; line-height: 18px;'>
            <b>Revos CMS</b><br/>Revos Viet Nam ,.JSC
        </td>
    </tr>
</table>
";
            EmailExtension.SendEmail(dt.Rows[0][UsersColumns.VuEmail].ToString(), "Hoàn thành khôi phục mật khẩu quản trị website: " + UrlExtension.WebsiteUrl, contentEmail);
            ltrNotification.Text = @"
<b>Hoàn thành khôi phục mật khẩu quản trị website</b><br/><br/>
Hệ thống đã gửi mật khẩu mới tới email của bạn, vui lòng kiểm tra email để nhận mật khẩu mới.
";
        }
        else
        {
            ltrNotification.Text = @"
<b>Khôi phục mật khẩu thất bại</b><br/><br/>
Mã xác nhận hoặc đường dẫn đã được kích hoạt, nếu bạn chưa nhận được mật khẩu mới vui lòng thử lại tính năng khôi phục mật khẩu.
";
        }
    }
}
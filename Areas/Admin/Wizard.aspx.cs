using System;
using System.IO;
using System.Linq;
using Developer;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Wizard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var dt = LanguageNational.GetData("1", "*", "", "");
        if (dt.Rows.Count > 0)
        {
            progress_wizard.Visible = false;
            ltrTitle.Text = "<small class='text-danger'>Website đã được khởi tạo</small>";
            Response.Redirect("/");
        }
        else ltrTitle.Text = "<small>Easy setup</small>";
    }

    protected void next3_OnClick(object sender, EventArgs e)
    {
        var vimg = "";
        if (flFlag.PostedFile.ContentLength > 0)
        {
            var filename = flFlag.FileName;
            var fileex = filename.Substring(filename.LastIndexOf(".", StringComparison.Ordinal));
            var path = Request.PhysicalApplicationPath + "/" + RevosJsc.LanguageControl.FolderPic.Language + "/";

            #region Kiểm tra xem thư mục đã tồn tại chưa, nếu chưa -> tạo mới thư mục

            var dri = new DirectoryInfo(path);
            if (!dri.Exists) dri.Create();

            #endregion

            if (ImagesExtension.ValidType(fileex))
            {
                var fileNotEx = StringExtension.ReplateTitle(filename.Remove(filename.LastIndexOf(".", StringComparison.Ordinal) - 1));
                if (fileNotEx.Length > 9) fileNotEx = fileNotEx.Remove(9);
                var ticks = DateTime.Now.Ticks.ToString();

                #region Lưu ảnh

                var vimgThumb = fileNotEx + "_" + ticks + fileex;
                vimg = vimgThumb;
                flFlag.SaveAs(path + vimgThumb);

                #endregion
            }
        }
        LanguageNational.Insert(txtLanguage.Text, vimg, "1", "1");
        var listRoles = new Roles();
        var roles = listRoles.Values.Aggregate(StringExtension.SpecialCharactersKeyword.ParamsSpilitRole, (current, x) => current + x + StringExtension.SpecialCharactersKeyword.ParamsSpilitRole);
        const string account = "admin";
        // Kiểm tra bảng user có tài khoản admin chưa
        var dt = Users.GetData("1", "*", UsersTSql.GetByAccount(account), "");
        if (dt.Rows.Count < 1)
        {
            // Nếu chưa có thì tạo tài khoản admin
            Users.Insert(roles, account, txtPassword.Text, "", "Admin", "", "", "", txtEmail.Text, "", "", "", "", "1", DateTime.Now.ToString(), DateTime.Now.ToString(), DateTime.Now.AddYears(1000).ToString(), "");
        }
        else
        {
            // Nếu đã có tài khoản admin thì update password mới
            string[] fields = { UsersColumns.VuPassword, UsersColumns.VuEmail };
            string[] values = { SecurityExtension.BuildPassword(txtPassword.Text), txtEmail.Text };
            Users.UpdateValues(DataExtension.UpdateValues(fields, values), UsersTSql.GetByAccount(account));
        }

        #region Tạo cookie

        CookieExtension.SaveCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount), "admin");
        CookieExtension.SaveCookies(SecurityExtension.BuildPassword(UsersColumns.VuRole), roles);

        #endregion
        Response.Redirect("/admin");
    }
}
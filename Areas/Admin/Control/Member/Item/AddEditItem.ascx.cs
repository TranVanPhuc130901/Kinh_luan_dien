using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Keyword;
using RevosJsc.MemberControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Member_Item_AddEditItem : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _app = CodeApplications.Member;
    private readonly string _pic = FolderPic.Member;

    private string _imid = "";
    private string _action = "";
    private bool _insert;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (_action.Equals(TypePage.CreateItem)) _insert = true;
        if (Request.QueryString["imid"] != null) _imid = Request.QueryString["imid"];

        #region Gán app, pic cho user control upload ảnh đại diện
        UploadImage.Control = _app;
        UploadImage.Pic = _pic;
        UploadImage.Text = "Ảnh đại diện";
        UploadImage.LayAnhTuNoiDung = false;
        UploadImage.TaoAnhNho = false;
        UploadImage.HanCheKichThuoc = false;
        #endregion

        if (IsPostBack) return;
        InitialControlsValue(_insert);
    }

    private string LinkRedrect()
    {
        return LinkAdmin.GoAdminSubControl(CodeApplications.Member, TypePage.Item);
    }

    private void InitialControlsValue(bool isInsert)
    {
        #region update
        if (!isInsert)
        {
            ltrTitle.Text = MemberKeyword.CapNhatMember;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var dt = Members.GetData("1", "*", MembersTSql.GetById(_imid), "");
            txtAccount.Text = dt.Rows[0][MembersColumns.VmAccount].ToString();
            txtAccount.Enabled = false;
            txtPassword.Attributes["placeholder"] = "Bỏ trống nếu không muốn thay đổi mật khẩu";
            txtPassword2.Attributes["placeholder"] = "Bỏ trống nếu không muốn thay đổi mật khẩu";
            txtFirstName.Text = dt.Rows[0][MembersColumns.VmFirstName].ToString();
            txtLastName.Text = dt.Rows[0][MembersColumns.VmLastName].ToString();
            UploadImage.Load(dt.Rows[0][MembersColumns.VmImage].ToString());
            txtAdd.Text = dt.Rows[0][MembersColumns.VmAddress].ToString();
            txtPhone.Text = dt.Rows[0][MembersColumns.VmPhone].ToString();
            txtEmail.Text = dt.Rows[0][MembersColumns.VmEmail].ToString();

            txtBirthDay.Text = ((DateTime)dt.Rows[0][MembersColumns.DmBirthday]).ToString("yyyy-MM-ddTHH:mm");
            txtDate.Text = ((DateTime)dt.Rows[0][MembersColumns.DmDateCreated]).ToString("yyyy-MM-ddTHH:mm");

            cbStatus.Checked = dt.Rows[0][MembersColumns.ImStatus].Equals(1);
            HdAccount.Value = dt.Rows[0][MembersColumns.VmAccount].ToString();
            HdPassword.Value = dt.Rows[0][MembersColumns.VmPassword].ToString();
            HdEmail.Value = dt.Rows[0][MembersColumns.VmEmail].ToString();
        }
        #endregion

        #region  insert
        else
        {
            ltrTitle.Text = MemberKeyword.ThemMoiMember;
            txtBirthDay.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            btSubmit.Text = "Thêm mới";
            txtAccount.Focus();
        }
        #endregion
    }
    private bool CheckExistedAcount(string acount)
    {
        var condition = MembersTSql.GetByAccount(acount) + " AND " + MembersTSql.GetByApp(_app);
        var dt = Members.GetData("1", MembersColumns.ImId, condition, "");
        return dt.Rows.Count > 0;
    }
    private bool CheckExistedEmail(string email)
    {
        var condition = MembersTSql.GetByEmail(email) + " AND " + MembersTSql.GetByApp(_app);
        var dt = Members.GetData("1", MembersColumns.ImId, condition, "");
        return dt.Rows.Count > 0;
    }
    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        #region Status
        var status = "0";
        if (cbStatus.Checked) status = "1";
        #endregion

        #region Insert
        if (_insert)
        {
            // Nếu là insert thì kiểm tra account đã tồn tại chưa
            if (CheckExistedAcount(txtAccount.Text))
            {
                ltrNote2.Text = "<div class='text-danger'>Tài khoản này đã tồn tại, Vui lòng chọn tài khoản khác.</div>";
                txtAccount.Focus();
                return;
            }
            if (CheckExistedEmail(txtEmail.Text))
            {
                ltrNote2.Text = "<div class='text-danger'>Email này đã được sử dụng, Vui lòng chọn email khác.</div>";
                txtEmail.Focus();
                return;
            }

            var image = UploadImage.Save(false, "");
            Members.Insert(_app, txtAccount.Text, txtPassword.Text, txtFirstName.Text, txtLastName.Text, txtAdd.Text, txtPhone.Text, txtEmail.Text, txtBirthDay.Text.Replace("T", " "), "", "", "", "", "", image, "", "", status, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), DateTime.Now.ToString(), "", "", "");
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " thêm mới member " + txtAccount.Text, logAuthor, logCreateDate);
            #endregion
        }
        #endregion

        #region Update
        else
        {
            if (HdEmail.Value != txtEmail.Text && CheckExistedEmail(txtEmail.Text))
            {
                ltrNote2.Text = "<div class='text-danger'>Email này đã được sử dụng, Vui lòng chọn email khác.</div>";
                txtEmail.Focus();
                return;
            }
            var image = UploadImage.Save(true, "");
            var newPassword = SecurityExtension.BuildPassword(HdPassword.Value);
            if (txtPassword.Text != "") newPassword = SecurityExtension.BuildPassword(txtPassword.Text);
            Members.Update(_app, HdAccount.Value, newPassword, txtFirstName.Text, txtLastName.Text, txtAdd.Text, txtPhone.Text, txtEmail.Text, txtBirthDay.Text.Replace("T", " "), "", "", "", "", "", image, "", "", status, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), DateTime.Now.ToString(), "", "", "", _imid);

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật member " + txtAccount.Text, logAuthor, logCreateDate);
            #endregion
        }
        #endregion

        #region After Insert/Update
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo: " + txtAccount.Text + "', {type: 'success'});});", true);

        if (cbContiue.Checked)
        {
            ResetControls();
        }
        else
        {
            Response.Redirect(LinkRedrect());
        }
        #endregion
    }

    private void ResetControls()
    {
        #region Reset các textbox, textbox nào có chứa css class là NotReset thì sẽ không bị reset
        foreach (Control control in form.Controls)
        {
            var textBox = control as TextBox;
            if (textBox != null) if (!textBox.CssClass.Contains("not-reset")) textBox.Text = "";
            var hiddenField = control as HiddenField;
            if (hiddenField != null) hiddenField.Value = "";
        }
        #endregion

        UploadImage.Reset();
        txtDate.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");

        txtAccount.Focus();
    }

}
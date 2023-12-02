using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Developer.Config;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;
using RevosJsc.UsersControl;

public partial class Areas_Admin_Control_Users_Control_AddEditUser : UserControl
{
    private readonly string _app = CodeApplications.Users;
    private readonly string _pic = FolderPic.Users;
    private string _action = "";
    private bool _isInsert;
    private string _iuId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (Request.QueryString["id"] != null) _iuId = Request.QueryString["id"];
        if (_action.Equals(TypePage.Create)) _isInsert = true;
        #region Gán app, pic cho user control upload ảnh đại diện
        UploadImageUsers.Control = _app;
        UploadImageUsers.Pic = _pic;
        UploadImageUsers.Text = "Ảnh đại diện";
        UploadImageUsers.LayAnhTuNoiDung = false;
        UploadImageUsers.TaoAnhNho = false;
        UploadImageUsers.HanCheKichThuoc = false;
        #endregion
        if (IsPostBack) return;
        LoadListRoles();
        InitialControlsValue(_isInsert);
    }
    private void InitialControlsValue(bool insert)
    {
        #region update

        if (!insert)
        {
            btSubmit.Text = "Cập nhật";
            chkContiue.Visible = false;
            var dt = Users.GetData("1", "*", UsersTSql.GetById(_iuId), "");
            #region Roles
            var roleDescription = dt.Rows[0][UsersColumns.VuRole].ToString();
            for (var i = 0; i < cblRole.Items.Count; i++)
            {
                cblRole.Items[i].Selected = StringExtension.RoleInListRoles(cblRole.Items[i].Value, roleDescription);
            }
            #endregion
            txtUsername.Text = dt.Rows[0][UsersColumns.VuAccount].ToString();
            txtUsername.ReadOnly = true;
            //txtPassword.ReadOnly = true;
            //txtPassword2.ReadOnly = true;
            txtPassword.Attributes["placeholder"] = "Bỏ trống nếu không muốn thay đổi mật khẩu";
            txtPassword2.Attributes["placeholder"] = "Bỏ trống nếu không muốn thay đổi mật khẩu";
            txtFirstName.Text = dt.Rows[0][UsersColumns.VuFirstName].ToString();
            txtLastName.Text = dt.Rows[0][UsersColumns.VuLastName].ToString();
            txtEmail.Text = dt.Rows[0][UsersColumns.VuEmail].ToString();
            txtPhone.Text = dt.Rows[0][UsersColumns.VuPhoneNumber].ToString();
            txtAdd.Text = dt.Rows[0][UsersColumns.VuAddress].ToString();
            txtDate.Text = dt.Rows[0][UsersColumns.DuDateCreated].ToString();
            txtDesc.Text = dt.Rows[0][UsersColumns.VuParam].ToString();
            hdExpiration.Value = dt.Rows[0][UsersColumns.DuExpirationDate].ToString();
            hdId.Value = dt.Rows[0][UsersColumns.IuId].ToString();
            hdRole.Value = dt.Rows[0][UsersColumns.VuRole].ToString();
            hdUsername.Value = dt.Rows[0][UsersColumns.VuAccount].ToString();
            UploadImageUsers.Load(dt.Rows[0][UsersColumns.VuImage].ToString());
            #region status
            chkStatus.Checked = dt.Rows[0][UsersColumns.IuStatus].Equals(1);
            #endregion status
        }

        #endregion update

        #region insert

        else
        {
            txtDate.Text = DateTime.Now.ToString();
            txtUsername.Focus();
        }

        #endregion insert
    }
    private void LoadListRoles()
    {
        var listRoles = new Developer.Roles();
        if (ControlConfig.ShowAboutUs) cblRole.Items.Add(new ListItem(listRoles.Text[0], listRoles.Values[0]));
        if (ControlConfig.ShowAdvertistments) cblRole.Items.Add(new ListItem(listRoles.Text[1], listRoles.Values[1]));
        if (ControlConfig.ShowBlog) cblRole.Items.Add(new ListItem(listRoles.Text[2], listRoles.Values[2]));
        if (ControlConfig.ShowContactUs) cblRole.Items.Add(new ListItem(listRoles.Text[3], listRoles.Values[3]));
        if (ControlConfig.ShowCruises) cblRole.Items.Add(new ListItem(listRoles.Text[4], listRoles.Values[4]));
        if (ControlConfig.ShowCustomer) cblRole.Items.Add(new ListItem(listRoles.Text[5], listRoles.Values[5]));
        if (ControlConfig.ShowReviews) cblRole.Items.Add(new ListItem(listRoles.Text[6], listRoles.Values[6]));
        if (ControlConfig.ShowDestination) cblRole.Items.Add(new ListItem(listRoles.Text[7], listRoles.Values[7]));
        if (ControlConfig.ShowFAQ) cblRole.Items.Add(new ListItem(listRoles.Text[8], listRoles.Values[8]));
        if (ControlConfig.ShowFilelibrary) cblRole.Items.Add(new ListItem(listRoles.Text[9], listRoles.Values[9]));
        if (ControlConfig.ShowHotel) cblRole.Items.Add(new ListItem(listRoles.Text[10], listRoles.Values[10]));
        if (ControlConfig.ShowLanguage) cblRole.Items.Add(new ListItem(listRoles.Text[11], listRoles.Values[11]));
        if (ControlConfig.ShowMember) cblRole.Items.Add(new ListItem(listRoles.Text[12], listRoles.Values[12]));
        if (ControlConfig.ShowMenus) cblRole.Items.Add(new ListItem(listRoles.Text[13], listRoles.Values[13]));
        if (ControlConfig.ShowNews) cblRole.Items.Add(new ListItem(listRoles.Text[14], listRoles.Values[14]));
        if (ControlConfig.ShowPhotoAlbum) cblRole.Items.Add(new ListItem(listRoles.Text[15], listRoles.Values[15]));
        if (ControlConfig.ShowProduct) cblRole.Items.Add(new ListItem(listRoles.Text[16], listRoles.Values[16]));
        if (ControlConfig.ShowProject) cblRole.Items.Add(new ListItem(listRoles.Text[17], listRoles.Values[17]));
        if (ControlConfig.ShowService) cblRole.Items.Add(new ListItem(listRoles.Text[18], listRoles.Values[18]));
        if (ControlConfig.ShowOurTeam) cblRole.Items.Add(new ListItem(listRoles.Text[19], listRoles.Values[19]));
        if (ControlConfig.ShowSystemwebsite) cblRole.Items.Add(new ListItem(listRoles.Text[20], listRoles.Values[20]));
        if (ControlConfig.ShowTour) cblRole.Items.Add(new ListItem(listRoles.Text[21], listRoles.Values[21]));
        if (ControlConfig.ShowUsers) cblRole.Items.Add(new ListItem(listRoles.Text[22], listRoles.Values[22]));
        if (ControlConfig.ShowVideo) cblRole.Items.Add(new ListItem(listRoles.Text[23], listRoles.Values[23]));
        if (ControlConfig.ShowWebsite) cblRole.Items.Add(new ListItem(listRoles.Text[24], listRoles.Values[24]));
        if (ControlConfig.ShowRedirect) cblRole.Items.Add(new ListItem(listRoles.Text[25], listRoles.Values[25]));
     
    }
    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        #region Check Role

        const string paramsSpilitRole = StringExtension.SpecialCharactersKeyword.ParamsSpilitRole;
        var roleDescription = paramsSpilitRole;
        for (var i = 0; i < cblRole.Items.Count; i++)
        {
            if (cblRole.Items[i].Selected) roleDescription += cblRole.Items[i].Value + paramsSpilitRole;
        }

        #endregion Check Role

        #region Status
        var status = "0";
        if (chkStatus.Checked) status = "1";
        #endregion

        #region Insert

        if (_isInsert)
        {
            if (ExistedUser(txtUsername.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Tài khoản này đã được sử dụng, vui lòng chọn tên tài khoản khác.');", true);
                return;
            }
            if (txtPassword.Text != txtPassword2.Text)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Nhập lại mật khẩu không chính xác, vui lòng thử lại.');", true);
                return;
            }

            var image = UploadImageUsers.Save(false, "");
            Users.Insert(roleDescription, txtUsername.Text.Trim(), txtPassword.Text, "", txtFirstName.Text, txtLastName.Text, txtAdd.Text, txtPhone.Text.Trim(), txtEmail.Text, image, "", "", "", status, DateTime.Now.ToString(), DateTime.Now.ToString(), DateTime.Now.AddYears(1000).ToString(), "");

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " tạo mới tài khoản quản trị: " + txtUsername.Text, logAuthor, logCreateDate);
            #endregion
        }

        #endregion Insert

        #region Update

        else
        {
            var image = UploadImageUsers.Save(true, "");
            Users.Update(hdId.Value, roleDescription, "", txtFirstName.Text, txtLastName.Text, txtAdd.Text, txtPhone.Text, txtEmail.Text, image, "", "", "", status, txtDate.Text, DateTime.Now.ToString(), hdExpiration.Value, txtDesc.Text);

            if (txtPassword.Text.Length > 0)
            {
                string[] fields = { UsersColumns.VuPassword };
                string[] values = { SecurityExtension.BuildPassword(txtPassword.Text) };
                Users.UpdateValues(DataExtension.UpdateValues(fields, values), UsersTSql.GetById(_iuId));
            }

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật thông tin tài khoản quản trị: " + hdUsername.Value, logAuthor, logCreateDate);
            #endregion

            ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);
        }

        #endregion Update

        #region After Insert/Update

        if (chkContiue.Checked)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo: " + txtUsername.Text + "', {type: 'success'});});", true);
            ResetControls(this);
        }
        else
        {
            Response.Redirect(Link.LnkUsers());
        }

        #endregion After Insert/Update
    }

    public bool ExistedUser(string username)
    {
        var dt = Users.GetData("", UsersColumns.IuId, UsersTSql.GetByAccount(username), "");
        return dt.Rows.Count > 0;
    }
    private void ResetControls(Control control)
    {
        #region Reset các textbox, textbox nào có chứa css class là not-reset thì sẽ không bị reset
        foreach (Control c in control.Controls)
        {
            var box = c as TextBox;
            if (box != null) if (box.CssClass != "not-reset") box.Text = "";
            var field = c as HiddenField;
            if (field != null) field.Value = "";
            ResetControls(c);
        }
        #endregion

        txtDate.Text = DateTime.Now.ToString();
        txtUsername.Focus();
    }
}
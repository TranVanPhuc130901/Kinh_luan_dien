using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.RedirectsControl;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Redirect_Link_AddEditLink : UserControl
{
    private readonly string _app = CodeApplications.Redirects;
    private string _action = "";
    private string _irid = "";
    private bool _isInsert;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (Request.QueryString["id"] != null) _irid = Request.QueryString["id"];
        if (_action.Equals(TypePage.Create)) _isInsert = true;
        if (IsPostBack) return;
        InitialControlsValue(_isInsert);
    }

    private void InitialControlsValue(bool insert)
    {
        #region update

        if (!insert)
        {
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var dt = Redirects.GetData("1", "*", RedirectsTSql.GetById(_irid), RedirectsColumns.IrId);
            if (dt.Rows.Count < 1) return;

            txtLink.Text = dt.Rows[0][RedirectsColumns.VrLink].ToString();
            hdOldLink.Value = dt.Rows[0][RedirectsColumns.VrLink].ToString();
            txtLinkDestination.Text = dt.Rows[0][RedirectsColumns.VrLinkDestination].ToString();
            txtDate.Text = dt.Rows[0][RedirectsColumns.DrDateCreated].ToString();

            #region status
            cbStatus.Checked = dt.Rows[0][RedirectsColumns.IrStatus].Equals(1);
            #endregion status
        }

        #endregion update

        #region insert

        else
        {
            txtDate.Text = DateTime.Now.ToString();
            txtLink.Focus();
        }

        #endregion insert
    }
    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        #region Status
        var status = "0";
        if (cbStatus.Checked) status = "1";
        #endregion

        #region Insert

        if (_isInsert)
        {
            // Kiểm tra nếu đã có link rồi thì không thêm nữa
            var dt = Redirects.GetData("1", "*", RedirectsTSql.GetByLink(txtLink.Text), "");
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Link đã tồn tại, vui lòng kiểm tra lại trong danh sách chuyển hướng', {type: 'danger'});});", true);
                return;
            }
            Redirects.Insert(txtLink.Text, txtLinkDestination.Text, txtDate.Text, DateTime.Now.ToString(), status);

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " tạo mới chuyển hướng link: " + txtLink.Text, logAuthor, logCreateDate);
            #endregion
        }

        #endregion

        #region Update

        else
        {
            Redirects.Update(txtLink.Text, txtLinkDestination.Text, txtDate.Text, DateTime.Now.ToString(), status, _irid);
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật chuyển hướng link: " + hdOldLink.Value, logAuthor, logCreateDate);
            #endregion
            ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);
        }

        #endregion

        #region After Insert/Update

        if (cbContiue.Checked)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo: " + txtLink.Text + "', {type: 'success'});});", true);
            ResetControls(this);
        }
        else
        {
            Response.Redirect(LinkAdmin.GoAdminControl(_app));
        }

        #endregion After Insert/Update
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
        txtLink.Focus();
    }
}
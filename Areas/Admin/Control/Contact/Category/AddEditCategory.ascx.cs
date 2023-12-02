using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Keyword;
using RevosJsc.ContactControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Contact_Category_AddEditCategory : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _app = CodeApplications.Contact;
    private readonly string _pic = FolderPic.Contact;

    private string _icid = "";
    private string _parent = "0";
    private string _action = "";
    private bool _insert;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (_action.Equals(TypePage.CreateCategory)) _insert = true;
        if (Request.QueryString["icid"] != null) _icid = Request.QueryString["icid"];
        if (Request.QueryString["parent"] != null) _parent = Request.QueryString["parent"];

        #region Gán app, pic cho user control upload ảnh đại diện
        UploadImage.Control = _app;
        UploadImage.Pic = _pic;
        UploadImage.Text = "Ảnh đại diện";
        UploadImage.LayAnhTuNoiDung = false;
        UploadImage.TaoAnhNho = false;
        UploadImage.HanCheKichThuoc = false;
        #endregion

        #region Gán đường dẫn cho ckeditor
        foreach (Control control in form.Controls)
        {
            var ckEditorControl = control as CKEditorControl;
            if (ckEditorControl != null) ckEditorControl.FilebrowserImageBrowseUrl = "/Ckeditor/ckfinder/ckfinder.aspx?type=Images&path=" + _pic;
        }
        #endregion

        //if (IsPostBack) return;
        GetGroupInDdl();
        InitialControlsValue(_insert);
    }

    private string LinkRedrect()
    {
        return LinkAdmin.GoAdminSubControl(CodeApplications.Contact, TypePage.Category, ddlCategory.SelectedValue);
    }

    private void GetGroupInDdl()
    {
        var condition = DataExtension.AndConditon(ContactsTSql.GetByLang(_lang), " icStatus <> 2 ", " icId <> '"+ _icid + "' ");
        var dt = Contacts.GetAllContact("*", condition, "");
        ddlCategory.Items.Clear();
        ddlCategory.Items.Add(new ListItem("Danh mục gốc", "0"));
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            ddlCategory.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i][ContactsColumns.IcLevel].ToString()) + dt.Rows[i][ContactsColumns.VcName], dt.Rows[i][ContactsColumns.IcId].ToString()));
        }
        ddlCategory.SelectedValue = _parent;
    }

    private void InitialControlsValue(bool isInsert)
    {
        #region update
        if (!isInsert)
        {
            ltrTitle.Text = ContactKeyword.CapNhatDanhMuc;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var condition = ContactsTSql.GetById(_icid);
            var dt = Contacts.GetData("1", "*", condition, "");

            txtTitle.Text = dt.Rows[0][ContactsColumns.VcName].ToString();
            HdTitle.Value = dt.Rows[0][ContactsColumns.VcName].ToString();
            txtAddress.Text = dt.Rows[0][ContactsColumns.VcAddress].ToString();
            txtPhone.Text = dt.Rows[0][ContactsColumns.VcPhone].ToString();
            txtHotline.Text = dt.Rows[0][ContactsColumns.VcHotline].ToString();
            txtEmail.Text = dt.Rows[0][ContactsColumns.VcEmail].ToString();
            txtMap.Text = dt.Rows[0][ContactsColumns.VcMap].ToString();
            UploadImage.Load(dt.Rows[0][ContactsColumns.VcImage].ToString());
            txtParam.Text = dt.Rows[0][ContactsColumns.VcParam].ToString();
            txtOrder.Text = dt.Rows[0][ContactsColumns.IcSortOrder].ToString();
            txtDate.Text = ((DateTime)dt.Rows[0][ContactsColumns.DcDateCreated]).ToString("yyyy-MM-ddTHH:mm");

            cbStatus.Checked = dt.Rows[0][ContactsColumns.IcStatus].Equals(1);
            ddlCategory.SelectedValue = dt.Rows[0][ContactsColumns.IcParentId].ToString();
        }
        #endregion

        #region  insert
        else
        {
            ltrTitle.Text = ContactKeyword.ThemMoiDanhMuc;
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            btSubmit.Text = "Thêm mới";
            txtTitle.Focus();
        }
        #endregion
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
            var image = UploadImage.Save(false, "");
            Contacts.Insert(ddlCategory.SelectedValue, txtOrder.Text, status, _lang, txtTitle.Text, txtAddress.Text, txtPhone.Text, txtHotline.Text, txtEmail.Text, txtMap.Text, image, txtParam.Text, txtDate.Text.Replace("T", " "), DateTime.Now.ToString());
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " thêm mới: " + txtTitle.Text, logAuthor, logCreateDate);
            #endregion
        }
        #endregion

        #region Update
        else
        {
            var image = UploadImage.Save(true, "");
            Contacts.Update(ddlCategory.SelectedValue, txtOrder.Text, status, _lang, txtTitle.Text, txtAddress.Text, txtPhone.Text, txtHotline.Text, txtEmail.Text, txtMap.Text, image, txtParam.Text, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), _icid);
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật: " + txtTitle.Text, logAuthor, logCreateDate);
            #endregion
        }
        #endregion

        #region After Insert/Update
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo: " + txtTitle.Text + "', {type: 'success'});});", true);

        if (cbContiue.Checked)
        {
            ResetControls();
            GetGroupInDdl();
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

        _parent = ddlCategory.SelectedValue;
        UploadImage.Reset();
        txtDate.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
        try
        {
            txtOrder.Text = (Convert.ToInt32(txtOrder.Text) + 1).ToString();
        }
        catch
        {
            // do nothing
        }

        txtTitle.Focus();
    }

}
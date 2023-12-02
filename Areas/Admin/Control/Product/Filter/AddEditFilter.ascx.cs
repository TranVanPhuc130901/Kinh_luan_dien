using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Extension;
using Developer.Keyword;
using RevosJsc.ProductControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Product_Filter_AddEditFilter : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _app = CodeApplications.Product;
    private readonly string _pic = FolderPic.Product;

    private string _ifid = "";
    private string _parent = "0";
    private string _action = "";
    private bool _insert;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (_action.Equals(TypePage.CreateFilter)) _insert = true;
        if (Request.QueryString["ifid"] != null) _ifid = Request.QueryString["ifid"];
        if (Request.QueryString["parent"] != null) _parent = Request.QueryString["parent"];

        #region Gán app, pic cho user control upload ảnh đại diện
        UploadImage.Control = _app;
        UploadImage.Pic = _pic;
        UploadImage.Text = "Ảnh đại diện";
        UploadImage.LayAnhTuNoiDung = false;
        UploadImage.TaoAnhNho = false;
        UploadImage.HanCheKichThuoc = false;
        // Ẩn ảnh đại diện
        UploadImage.Visible = false;
        #endregion

        #region Gán đường dẫn cho ckeditor

        foreach (Control control in form.Controls)
        {
            var ckEditorControl = control as CKEditorControl;
            if (ckEditorControl != null) ckEditorControl.FilebrowserImageBrowseUrl = UrlExtension.WebsiteUrl + "ckeditor/ckfinder/ckfinder.aspx?type=Images&path=" + _pic;
        }

        #endregion Gán đường dẫn cho ckeditor

        //if (IsPostBack) return;
        GetGroupInDdl();
        InitialControlsValue(_insert);
    }

    private string LinkRedrect()
    {
        return LinkAdmin.GoAdminSubControl(CodeApplications.Product, TypePage.Filter, ddlCategory.SelectedValue);
    }

    private void GetGroupInDdl()
    {
        var condition = DataExtension.AndConditon(FilterTSql.GetByLang(_lang), " ifStatus <> '2' ", " ifId <> '"+ _ifid + "' ");
        var dt = Filter.GetAllData("*", condition, "");
        ddlCategory.Items.Clear();
        ddlCategory.Items.Add(new ListItem("Danh mục gốc", "0"));
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            ddlCategory.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i][FilterColumns.IfLevel].ToString()) + dt.Rows[i][FilterColumns.VfName], dt.Rows[i][FilterColumns.IfId].ToString()));
        }
        ddlCategory.SelectedValue = _parent;
    }

    private void InitialControlsValue(bool isInsert)
    {
        #region update
        if (!isInsert)
        {
            ltrTitle.Text = ProductKeyword.CapNhatDanhMuc;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var condition = FilterTSql.GetById(_ifid);
            var dt = Filter.GetData("1", "*", condition, "");

            txtTitle.Text = dt.Rows[0][FilterColumns.VfName].ToString();
            HdTitle.Value = dt.Rows[0][FilterColumns.VfName].ToString();
            UploadImage.Load(dt.Rows[0][FilterColumns.VfImage].ToString());

            txtOrder.Text = dt.Rows[0][FilterColumns.IfSortOrder].ToString();
            txtDesc.Text = dt.Rows[0][FilterColumns.VfDescription].ToString();
            txtDate.Text = ((DateTime)dt.Rows[0][FilterColumns.DfDateCreated]).ToString("yyyy-MM-ddTHH:mm");
            ddlType.SelectedValue = dt.Rows[0][FilterColumns.IfType].ToString();
            cbStatus.Checked = dt.Rows[0][FilterColumns.IfStatus].Equals(1);
            ddlCategory.SelectedValue = dt.Rows[0][FilterColumns.IfParentId].ToString();
            ddlParam.SelectedValue = dt.Rows[0][FilterColumns.VfParam].ToString();
        }
        #endregion

        #region  insert
        else
        {
            ltrTitle.Text = ProductKeyword.ThemMoiDanhMuc;
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
            Filter.Insert(ddlCategory.SelectedValue, _lang, txtOrder.Text, txtTitle.Text, txtDesc.Text, image, ddlParam.SelectedValue, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), ddlType.SelectedValue, status);
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
            Filter.Update(ddlCategory.SelectedValue, _lang, txtOrder.Text, txtTitle.Text, txtDesc.Text, image, ddlParam.SelectedValue, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), ddlType.SelectedValue, status, _ifid);
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
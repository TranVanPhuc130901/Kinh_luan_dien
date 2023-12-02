using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Extension;
using Developer.Keyword;
using RevosJsc.AdminControl;
using RevosJsc.AdvertistmentsControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;
using Advertistments = RevosJsc.Database.Advertistments;

public partial class Areas_Admin_Control_Advertising_Item_AddEditItem : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _app = CodeApplications.Advertistments;
    private readonly string _pic = FolderPic.Advertistments;

    private string _iaid = "";
    private string _iapid = "";
    private string _action = "";
    private bool _insert;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (_action.Equals(TypePage.CreateItem)) _insert = true;
        if (Request.QueryString["iaid"] != null) _iaid = Request.QueryString["iaid"];
        if (Request.QueryString["iapid"] != null) _iapid = Request.QueryString["iapid"];

        #region Gán app, pic cho user control upload ảnh đại diện
        UploadImage.Control = _app;
        UploadImage.Pic = _pic;
        UploadImage.Text = "Ảnh quảng cáo";
        UploadImage.LayAnhTuNoiDung = false;
        UploadImage.TaoAnhNho = false;
        UploadImage.HanCheKichThuoc = false;
        #endregion
        #region Gán đường dẫn cho ckeditor

        foreach (Control control in form.Controls)
        {
            var ckEditorControl = control as CKEditorControl;
            if (ckEditorControl != null) ckEditorControl.FilebrowserImageBrowseUrl = UrlExtension.WebsiteUrl + "ckeditor/ckfinder/ckfinder.aspx?type=Images&path=" + _pic;
        }

        #endregion Gán đường dẫn cho ckeditor

        if (IsPostBack) return;
        GetGroupInDdl();
        InitialControlsValue(_insert);
    }

    private string LinkRedrect()
    {
        return LinkAdmin.GoAdminOption(CodeApplications.Advertistments, TypePage.Item, "iapid", ddlCategory.SelectedValue);
    }

    private void GetGroupInDdl()
    {
        var condition = DataExtension.AndConditon(AdvertistmentPositionsTSql.GetByLang(_lang), " iapStatus <> 2 ");
        var dt = AdvertistmentPositions.GetData("", "*", condition, "");
        ddlCategory.Items.Clear();
        if (dt.Rows.Count < 1)
        {
            ddlCategory.CssClass = "form-control";
            ddlCategory.Items.Add(new ListItem("Vui lòng tạo danh mục trước khi thêm bài viết", ""));
        }
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            ddlCategory.Items.Add(new ListItem(dt.Rows[i][AdvertistmentPositionsColumns.VapName].ToString(), dt.Rows[i][AdvertistmentPositionsColumns.IapId].ToString()));
        }
    }

    private void InitialControlsValue(bool isInsert)
    {
        #region update
        if (!isInsert)
        {
            ltrTitle.Text = AdvertistmentsKeyword.CapNhatBaiViet;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var dt = Advertistments.GetData("1", "*", AdvertistmentsTSql.GetById(_iaid), "");

            txtTitle.Text = dt.Rows[0][AdvertistmentsColumns.VaTitle].ToString();
            HdTitle.Value = dt.Rows[0][AdvertistmentsColumns.VaTitle].ToString();
            UploadImage.Load(dt.Rows[0][AdvertistmentsColumns.VaImage].ToString());
            txt_content.Text = dt.Rows[0][AdvertistmentsColumns.VaParam].ToString();
            txtOrder.Text = dt.Rows[0][AdvertistmentsColumns.IaSortOrder].ToString();
            txtDesc.Text = dt.Rows[0][AdvertistmentsColumns.VaDescription].ToString();
            txtDate.Text = ((DateTime)dt.Rows[0][AdvertistmentsColumns.DaDateCreated]).ToString("yyyy-MM-ddTHH:mm");

            #region Link

            txtLink.Text = dt.Rows[0][AdvertistmentsColumns.VaLink].ToString();
            ddlOpenOption.SelectedValue = dt.Rows[0][AdvertistmentsColumns.IaTarget].ToString();

            #endregion

            cbStatus.Checked = dt.Rows[0][AdvertistmentsColumns.IaStatus].Equals(1);
            ddlCategory.SelectedValue = dt.Rows[0][AdvertistmentsColumns.IapId].ToString();
        }
        #endregion

        #region  insert
        else
        {
            ddlCategory.SelectedValue = _iapid;
            ltrTitle.Text = AdvertistmentsKeyword.ThemMoiBaiViet;
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
            Advertistments.Insert(ddlCategory.SelectedValue, _lang, txtTitle.Text, txtLink.Text, ddlOpenOption.SelectedValue, txtDesc.Text, image, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), txt_content.Text, txtOrder.Text, status);
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
            Advertistments.Update(ddlCategory.SelectedValue, _lang, txtTitle.Text, txtLink.Text, ddlOpenOption.SelectedValue, txtDesc.Text, image, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), txt_content.Text, txtOrder.Text, status, _iaid);
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật: " + HdTitle.Value, logAuthor, logCreateDate);
            #endregion
        }
        #endregion

        #region After Insert/Update
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo: " + txtTitle.Text + "', {type: 'success'});});", true);

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
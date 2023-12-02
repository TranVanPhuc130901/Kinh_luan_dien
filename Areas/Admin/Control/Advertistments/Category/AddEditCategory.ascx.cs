using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Keyword;
using Developer.Position;
using RevosJsc.AdminControl;
using RevosJsc.AdvertistmentsControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Advertising_Category_AddEditCategory : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _pic = FolderPic.Advertistments;

    private string _iapid = "";
    private string _action = "";
    private bool _insert;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (_action.Equals(TypePage.CreateCategory)) _insert = true;
        if (Request.QueryString["iapid"] != null) _iapid = Request.QueryString["iapid"];

        #region Gán app, pic cho user control upload ảnh đại diện
        UploadImage.Control = CodeApplications.Advertistments;
        UploadImage.Pic = _pic;
        UploadImage.Text = "Ảnh đại diện";
        UploadImage.LayAnhTuNoiDung = false;
        //UploadImage.TaoAnhNho = false;
        //UploadImage.HanCheKichThuoc = false;
        #endregion

        #region Gán đường dẫn cho ckeditor
        foreach (Control control in form.Controls)
        {
            var ckEditorControl = control as CKEditorControl;
            if (ckEditorControl != null) ckEditorControl.FilebrowserImageBrowseUrl = "/Ckeditor/ckfinder/ckfinder.aspx?type=Images&path=" + _pic;
        }
        #endregion

        if (IsPostBack) return;
        GetPosition();
        InitialControlsValue(_insert);
    }

    private void GetPosition()
    {
        var listModul = new AdvertistmentsPosition();
        DdlPosition.Items.Clear();
        for (var i = 0; i < listModul.Text.Length; i++)
        {
            DdlPosition.Items.Add(new ListItem(listModul.Text[i], listModul.Values[i]));
        }
    }

    public string LinkRedrect()
    {
        return LinkAdmin.GoAdminSubControl(CodeApplications.Advertistments, TypePage.Category);
    }

    private void InitialControlsValue(bool isInsert)
    {
        #region update
        if (!isInsert)
        {
            ltrTitle.Text = AdvertistmentsKeyword.CapNhatDanhMuc;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var condition = AdvertistmentPositionsTSql.GetById(_iapid);
            var dt = AdvertistmentPositions.GetData("1", "*", condition, "");

            txtTitle.Text = dt.Rows[0][AdvertistmentPositionsColumns.VapName].ToString();
            HdTitle.Value = dt.Rows[0][AdvertistmentPositionsColumns.VapName].ToString();
            UploadImage.Load(dt.Rows[0][AdvertistmentPositionsColumns.VapImage].ToString());

            txtOrder.Text = dt.Rows[0][AdvertistmentPositionsColumns.IapSortOrder].ToString();
            txtDesc.Text = dt.Rows[0][AdvertistmentPositionsColumns.VapDescription].ToString();
            txtDate.Text = ((DateTime)dt.Rows[0][AdvertistmentPositionsColumns.DapDateCreated]).ToString("yyyy-MM-ddTHH:mm");

            DdlPosition.SelectedValue = dt.Rows[0][AdvertistmentPositionsColumns.IaPosition].ToString();

            cbStatus.Checked = dt.Rows[0][AdvertistmentPositionsColumns.IapStatus].Equals(1);
        }
        #endregion

        #region  insert
        else
        {
            ltrTitle.Text = AdvertistmentsKeyword.ThemMoiDanhMuc;
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
            AdvertistmentPositions.Insert(_lang, DdlPosition.SelectedValue, txtTitle.Text, txtDesc.Text, image, status, txtOrder.Text, "", txtDate.Text.Replace("T", " "), DateTime.Now.ToString());
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
            AdvertistmentPositions.Update(_lang, DdlPosition.SelectedValue, txtTitle.Text, txtDesc.Text, image, status, txtOrder.Text, "", txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), _iapid);
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
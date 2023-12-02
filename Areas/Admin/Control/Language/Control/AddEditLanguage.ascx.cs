using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Developer.Keyword;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.LanguageControl;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Language_Control_AddEditLanguage : UserControl
{
    private readonly string _app = CodeApplications.Language;
    private readonly string _pic = FolderPic.Language;
    private string _ilnId = "";
    private bool _isInsert;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ilnId"] != null) _ilnId = Request.QueryString["ilnId"];
        if (_ilnId.Length < 1) _isInsert = true;

        #region Gán app, pic cho user control upload ảnh đại diện
        UploadImage.Control = _app;
        UploadImage.Pic = _pic;
        UploadImage.Text = "Cờ ngôn ngữ";
        UploadImage.LayAnhTuNoiDung = false;
        UploadImage.TaoAnhNho = false;
        UploadImage.HanCheKichThuoc = false;
        #endregion

        if (IsPostBack) return;
        InitialControlsValue(_isInsert);
    }

    private void InitialControlsValue(bool insert)
    {
        #region update

        if (!insert)
        {
            ltrTitle.Text = LanguageKeyword.CapNhatLanguage;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;

            var dt = LanguageNational.GetData("", "*", LanguageNationalTSql.GetById(_ilnId), "");

            txtTitle.Text = dt.Rows[0][LanguageNationalColumns.VlnName].ToString();

            #region Image

            UploadImage.Load(dt.Rows[0][LanguageNationalColumns.VlnFlag].ToString());

            #endregion

            #region status

            cbStatus.Checked = dt.Rows[0][LanguageNationalColumns.IlnStatus].Equals(1);

            #endregion
        }

        #endregion update

        #region insert

        else
        {
            ltrTitle.Text = LanguageKeyword.ThemMoiLanguage;
            btSubmit.Text = "Thêm mới";
            txtTitle.Focus();
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
            var image = UploadImage.Save(false, "");
            LanguageNational.Insert(txtTitle.Text, image, txtOrder.Text, status);
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " thêm mới ngôn ngữ: " + txtTitle.Text, logAuthor, logCreateDate);
            #endregion
        }

        #endregion

        #region Update

        else
        {
            var image = UploadImage.Save(true, "");
            LanguageNational.Update(_ilnId, txtTitle.Text, image, txtOrder.Text, status);

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật ngôn ngữ: " + txtTitle.Text, logAuthor, logCreateDate);
            #endregion
        }


        #endregion

        #region After Insert/Update

        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo ngôn ngữ: " + txtTitle.Text + "', {type: 'success'});});", true);

        if (cbContiue.Checked)
        {
            ResetControls();
        }
        else
        {
            Response.Redirect(Link.LnkMnLanguageNational());
        }

        #endregion After Insert/Update
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
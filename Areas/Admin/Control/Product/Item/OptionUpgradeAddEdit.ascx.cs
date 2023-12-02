using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Config;
using Developer.Extension;
using Developer.Keyword;
using RevosJsc.ProductControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Product_Item_OptionUpgradeAddEdit : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _app = "OptionUpgrade";
    private readonly string _pic = FolderPic.Product;

    private string _iiid = "";
    private string _action = "";
    private bool _insert;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (_action.Equals("OptionUpgradeAdd")) _insert = true;
        if (Request.QueryString["iiid"] != null) _iiid = Request.QueryString["iiid"];

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
            if (ckEditorControl != null) ckEditorControl.FilebrowserImageBrowseUrl = UrlExtension.WebsiteUrl + "ckeditor/ckfinder/ckfinder.aspx?type=Images&path=" + _pic;
        }

        #endregion Gán đường dẫn cho ckeditor

        if (IsPostBack) return;
        InitialControlsValue(_insert);
    }


    private string LinkRedrect()
    {
        return LinkAdmin.GoAdminCategory(CodeApplications.Product, "OptionUpgrade", "");
    }

    private void InitialControlsValue(bool isInsert)
    {
        #region update
        if (!isInsert)
        {
            ltrTitle.Text = "Cập nhật tùy chọn";
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var condition = DataExtension.AndConditon(
                ItemsTSql.GetByApp(_app),
                ItemsTSql.GetById(_iiid)
                );
            var dt = Items.GetData("1", "*", condition, "");
            txtTitle.Text = dt.Rows[0][ItemsColumns.ViTitle].ToString();
            HdTitle.Value = dt.Rows[0][ItemsColumns.ViTitle].ToString();
            UploadImage.Load(dt.Rows[0][ItemsColumns.ViImage].ToString());
            txtCode.Text = dt.Rows[0][ItemsColumns.ViCode].ToString();
            txtOrder.Text = dt.Rows[0][ItemsColumns.IiSortOrder].ToString();
            txtDesc.Text = dt.Rows[0][ItemsColumns.ViDescription].ToString();
            txtDate.Text = ((DateTime)dt.Rows[0][ItemsColumns.DiDateCreated]).ToString("yyyy-MM-ddTHH:mm");
            HdTotalView.Value = dt.Rows[0][ItemsColumns.IiTotalView].ToString();
            HdOldAuthor.Value = dt.Rows[0][ItemsColumns.ViAuthor].ToString();

            #region Price

            txtPriceOld.Text = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();
            ltrPriceOld.Text = NumberExtension.FormatNumber(dt.Rows[0][ItemsColumns.FiPriceOld].ToString());
            txtPriceNew.Text = dt.Rows[0][ItemsColumns.FiPriceNew].ToString();
            ltrPriceNew.Text = NumberExtension.FormatNumber(dt.Rows[0][ItemsColumns.FiPriceNew].ToString());

            #endregion

            cbStatus.Checked = dt.Rows[0][ItemsColumns.IiStatus].Equals(1);

        }
        #endregion

        #region  insert
        else
        {
            ltrTitle.Text = "Thêm mới tùy chọn";
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
            Items.Insert(_lang, _app, txtCode.Text, txtTitle.Text, txtDesc.Text, "", image, HdOldAuthor.Value, "", "", "", "", "", txtPriceOld.Text, txtPriceNew.Text, "", "0", txtOrder.Text, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), status);
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
            Items.Update(_lang, _app, txtCode.Text, txtTitle.Text, txtDesc.Text, "", image, HdOldAuthor.Value, "", "", "", "", "", txtPriceOld.Text, txtPriceNew.Text, "", HdTotalView.Value, txtOrder.Text, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), status, _iiid);
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
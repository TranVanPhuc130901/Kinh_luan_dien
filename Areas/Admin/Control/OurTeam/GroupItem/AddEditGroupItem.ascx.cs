using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Extension;
using Developer.Keyword;
using Developer.Position;
using RevosJsc.OurTeamControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_OurTeam_GroupItem_AddEditGroupItem : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _app = CodeApplications.OurTeamGroupItem;
    private readonly string _pic = FolderPic.OurTeam;

    private string _igid = "";
    private string _action = "";
    private bool _insert;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (_action.Equals(TypePage.CreateGroupItem)) _insert = true;
        if (Request.QueryString["igid"] != null) _igid = Request.QueryString["igid"];

        #region Gán app, pic cho user control upload ảnh đại diện
        UploadImage.Control = CodeApplications.OurTeam;
        UploadImage.Pic = _pic;
        UploadImage.Text = "Ảnh đại diện";
        UploadImage.LayAnhTuNoiDung = true;
        //UploadImage.TaoAnhNho = false;
        //UploadImage.HanCheKichThuoc = false;
        #endregion
        #region Gán đường dẫn cho ckeditor

        foreach (Control control in form.Controls)
        {
            var ckEditorControl = control as CKEditorControl;
            if (ckEditorControl != null) ckEditorControl.FilebrowserImageBrowseUrl = UrlExtension.WebsiteUrl + "ckeditor/ckfinder/ckfinder.aspx?type=Images&path=" + _pic;
        }

        #endregion Gán đường dẫn cho ckeditor

        if (IsPostBack) return;
        GetPosition();
        InitialControlsValue(_insert);
    }

    private void GetPosition()
    {
        var listModul = new OurTeamPosition();
        DdlPosition.Items.Clear();
        for (var i = 0; i < listModul.Text.Length; i++)
        {
            DdlPosition.Items.Add(new ListItem(listModul.Text[i], listModul.Values[i]));
        }
    }

    private string LinkRedrect()
    {
        return LinkAdmin.GoAdminSubControl(CodeApplications.OurTeam, TypePage.GroupItem);
    }

    private void InitialControlsValue(bool isInsert)
    {
        #region update
        if (!isInsert)
        {
            ltrTitle.Text = OurTeamKeyword.CapNhatNhom;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var condition = GroupsTSql.GetById(_igid);
            var dt = Groups.GetData("1", "*", condition, "");

            txtTitle.Text = dt.Rows[0][GroupsColumns.VgName].ToString();
            HdTitle.Value = dt.Rows[0][GroupsColumns.VgName].ToString();
            UploadImage.Load(dt.Rows[0][GroupsColumns.VgImage].ToString());

            txtOrder.Text = dt.Rows[0][GroupsColumns.IgSortOrder].ToString();
            txtDesc.Text = dt.Rows[0][GroupsColumns.VgDescription].ToString();
            txtDate.Text = ((DateTime)dt.Rows[0][GroupsColumns.DgDateCreated]).ToString("yyyy-MM-ddTHH:mm");
            txt_content.Text = dt.Rows[0][GroupsColumns.VgContent].ToString();
            HdOldContent.Value = txt_content.Text;
            HdTotalView.Value = dt.Rows[0][GroupsColumns.IgTotalView].ToString();

            DdlPosition.SelectedValue = dt.Rows[0][GroupsColumns.IgPosition].ToString();
            #region SEO
            txtUrl.Text = dt.Rows[0][GroupsColumns.VgLink].ToString();
            txtMetaTitle.Text = dt.Rows[0][GroupsColumns.VgMetaTitle].ToString();
            txtMetaKeyword.Text = dt.Rows[0][GroupsColumns.VgMetaKeyword].ToString();
            txtMetaDescription.Text = dt.Rows[0][GroupsColumns.VgMetaDescription].ToString();
            #endregion

            cbStatus.Checked = dt.Rows[0][GroupsColumns.IgStatus].Equals(1);
        }
        #endregion

        #region  insert
        else
        {
            ltrTitle.Text = OurTeamKeyword.ThemMoiNhom;
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            btSubmit.Text = "Thêm mới";
            txtTitle.Focus();
        }
        #endregion
    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        var contentDetail = ContentExtendtions.ProcessStringContent(txt_content.Text, HdOldContent.Value, _pic);

        #region Status
        var status = "0";
        if (cbStatus.Checked) status = "1";
        #endregion

        //#region Seo
        //if (txtUrl.Text.Trim().Equals(""))
        //{
        //    txtUrl.Text = txtTitle.Text;
        //}
        //if (StringExtension.ReplateTitle(txtUrl.Text).Equals("")) txtUrl.Text = Guid.NewGuid().ToString();
        //if (txtMetaTitle.Text.Trim().Equals(""))
        //{
        //    txtMetaTitle.Text = txtTitle.Text;
        //}
        //if (txtMetaKeyword.Text.Trim().Equals(""))
        //{
        //    txtMetaKeyword.Text = txtTitle.Text;
        //}
        //if (txtMetaDescription.Text.Trim().Equals(""))
        //{
        //    txtMetaDescription.Text = txtDesc.Text;
        //}
        //#endregion

        #region Insert
        if (_insert)
        {
            var image = UploadImage.Save(false, contentDetail);
            Groups.Insert("0", _lang, _app, txtOrder.Text, DdlPosition.SelectedValue, "0",  txtTitle.Text, txtDesc.Text, contentDetail, image, "", txtMetaTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, "", StringExtension.ReplateTitle(txtUrl.Text), txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), status);
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
            var image = UploadImage.Save(true, contentDetail);
            Groups.Update("0", _app, txtOrder.Text, DdlPosition.SelectedValue, HdTotalView.Value, txtTitle.Text, txtDesc.Text, contentDetail, image, "", txtMetaTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, "", StringExtension.ReplateTitle(txtUrl.Text), txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), status, _igid);
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
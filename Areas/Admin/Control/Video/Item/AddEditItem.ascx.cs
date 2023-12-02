using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Extension;
using Developer.Keyword;
using RevosJsc.VideoControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Video_Item_AddEditItem : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _app = CodeApplications.Video;
    private readonly string _pic = FolderPic.Video;

    private string _iiid = "";
    private string _action = "";
    private bool _insert;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (_action.Equals(TypePage.CreateItem)) _insert = true;
        if (Request.QueryString["iiid"] != null) _iiid = Request.QueryString["iiid"];

        #region Gán app, pic cho user control upload ảnh đại diện
        UploadImage.Control = _app;
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
            if (ckEditorControl != null) ckEditorControl.FilebrowserImageBrowseUrl = UrlExtension.WebsiteUrl + "ckeditor/ckfinder/ckfinder.aspx?type=Images&path=" + _pic;
        }

        #endregion Gán đường dẫn cho ckeditor

        if (IsPostBack) return;
        GetGroupInDdl();
        InitialControlsValue(_insert);
    }

    private string LinkRedrect()
    {
        return LinkAdmin.GoAdminCategory(CodeApplications.Video, TypePage.Item, ddlCategory.SelectedValue);
    }

    private void GetGroupInDdl()
    {
        var condition = DataExtension.AndConditon(GroupsTSql.GetByLang(_lang), GroupsTSql.GetByApp(_app), " igStatus <> '2' ");
        var dt = Groups.GetAllGroups("*", condition, "");
        ddlCategory.Items.Clear();
        if (dt.Rows.Count < 1)
        {
            ddlCategory.CssClass = "form-control";
            ddlCategory.Items.Add(new ListItem("Vui lòng tạo danh mục trước khi thêm bài viết", ""));
        }
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            ddlCategory.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i][GroupsColumns.IgLevel].ToString()) + dt.Rows[i][GroupsColumns.VgName], dt.Rows[i][GroupsColumns.IgId].ToString()));
        }
    }

    private void InitialControlsValue(bool isInsert)
    {
        #region update
        if (!isInsert)
        {
            ltrTitle.Text = VideoKeyword.CapNhatBaiViet;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var condition = DataExtension.AndConditon(
                GroupsTSql.GetByApp(_app),
                ItemsTSql.GetById(_iiid)
                );
            var dt = GroupItems.GetAllData("1", "*", condition, "");

            txtTitle.Text = dt.Rows[0][ItemsColumns.ViTitle].ToString();
            txtCode.Text = dt.Rows[0][ItemsColumns.ViCode].ToString();
            HdTitle.Value = dt.Rows[0][ItemsColumns.ViTitle].ToString();
            UploadImage.Load(dt.Rows[0][ItemsColumns.ViImage].ToString());

            txtOrder.Text = dt.Rows[0][ItemsColumns.IiSortOrder].ToString();
            txtDesc.Text = dt.Rows[0][ItemsColumns.ViDescription].ToString();
            txtDate.Text = ((DateTime)dt.Rows[0][ItemsColumns.DiDateCreated]).ToString("yyyy-MM-ddTHH:mm");
            txt_content.Text = dt.Rows[0][ItemsColumns.ViContent].ToString();
            HdOldContent.Value = dt.Rows[0][ItemsColumns.ViContent].ToString();
            HdTotalView.Value = dt.Rows[0][ItemsColumns.IiTotalView].ToString();

            #region SEO
            txtUrl.Text = dt.Rows[0][ItemsColumns.ViLink].ToString();
            txtMetaTitle.Text = dt.Rows[0][ItemsColumns.ViMetaTitle].ToString();
            txtMetaKeyword.Text = dt.Rows[0][ItemsColumns.ViMetaKeyword].ToString();
            txtMetaDescription.Text = dt.Rows[0][ItemsColumns.ViMetaDescription].ToString();
            txtTag.Text = dt.Rows[0][ItemsColumns.ViTag].ToString();
            #endregion

            cbStatus.Checked = dt.Rows[0][ItemsColumns.IiStatus].Equals(1);
            ddlCategory.SelectedValue = dt.Rows[0][GroupsColumns.IgId].ToString();
            HdOldIgId.Value = dt.Rows[0][GroupsColumns.IgId].ToString();
        }
        #endregion

        #region  insert
        else
        {
            ltrTitle.Text = VideoKeyword.ThemMoiBaiViet;
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

        #region Seo
        if (txtUrl.Text.Trim().Equals(""))
        {
            txtUrl.Text = txtTitle.Text;
        }
        if (StringExtension.ReplateTitle(txtUrl.Text).Equals("")) txtUrl.Text = Guid.NewGuid().ToString();
        if (txtMetaTitle.Text.Trim().Equals(""))
        {
            txtMetaTitle.Text = txtTitle.Text;
        }
        if (txtMetaKeyword.Text.Trim().Equals(""))
        {
            txtMetaKeyword.Text = txtTitle.Text;
        }
        if (txtMetaDescription.Text.Trim().Equals(""))
        {
            txtMetaDescription.Text = txtDesc.Text;
        }
        #endregion

        #region Insert
        if (_insert)
        {
            var image = UploadImage.Save(false, contentDetail);
            GroupItems.InsertItemGroupItem(_lang, _app, VideoExtension.GetVideoKey(txtEmbed.Text), txtTitle.Text, txtDesc.Text, contentDetail, image, "", txtMetaTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, txtTag.Text, StringExtension.ReplateTitle(txtUrl.Text), "0", "0", "", "0", txtOrder.Text, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), status, ddlCategory.SelectedValue, DateTime.Now.ToString(), DateTime.Now.ToString());
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
            GroupItems.UpdateItemGroupItem(_lang, _app, VideoExtension.GetVideoKey(txtEmbed.Text), txtTitle.Text, txtDesc.Text, contentDetail, image, "", txtMetaTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, txtTag.Text, StringExtension.ReplateTitle(txtUrl.Text), "0", "0", "", HdTotalView.Value, txtOrder.Text, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), status, ddlCategory.SelectedValue, DateTime.Now.ToString(), DateTime.Now.ToString(), _iiid, HdOldIgId.Value);
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
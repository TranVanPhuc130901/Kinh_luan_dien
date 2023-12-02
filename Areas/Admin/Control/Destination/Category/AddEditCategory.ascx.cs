using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Extension;
using Developer.Keyword;
using RevosJsc.DestinationControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Destination_Category_AddEditCategory : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _app = CodeApplications.Destination;
    private readonly string _pic = FolderPic.Destination;

    private string _igid = "";
    private string _parent = "0";
    private string _action = "";
    private bool _insert;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (_action.Equals(TypePage.CreateCategory)) _insert = true;
        if (Request.QueryString["igid"] != null) _igid = Request.QueryString["igid"];
        if (Request.QueryString["parent"] != null) _parent = Request.QueryString["parent"];

        #region Gán app, pic cho user control upload ảnh đại diện
        UploadImage.Control = _app;
        UploadImage.Pic = _pic;
        UploadImage.Text = "Bản đồ (540x630 px)";
        UploadImage.LayAnhTuNoiDung = true;
        //UploadImage.TaoAnhNho = false;
        //UploadImage.HanCheKichThuoc = false;
        UploadImage1.Control = _app;
        UploadImage1.Pic = _pic;
        UploadImage1.Text = "Banner đầu trang";
        UploadImage1.LayAnhTuNoiDung = false;
        UploadImage1.TaoAnhNho = false;
        UploadImage1.HanCheKichThuoc = false;
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
        return LinkAdmin.GoAdminSubControl(CodeApplications.Destination, TypePage.Category, ddlCategory.SelectedValue);
    }

    private void GetGroupInDdl()
    {
        var condition = DataExtension.AndConditon(GroupsTSql.GetByLang(_lang), GroupsTSql.GetByApp(_app), " igStatus <> '2' ", " igId <> '"+ _igid + "' ");
        var dt = Groups.GetAllGroups("*", condition, "");
        ddlCategory.Items.Clear();
        ddlCategory.Items.Add(new ListItem("Danh mục gốc", "0"));
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            ddlCategory.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i]["igLevel"].ToString()) + dt.Rows[i]["vgName"], dt.Rows[i]["igId"].ToString()));
        }
        ddlCategory.SelectedValue = _parent;
    }

    private void InitialControlsValue(bool isInsert)
    {
        #region update
        if (!isInsert)
        {
            ltrTitle.Text = DestinationKeyword.CapNhatDanhMuc;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var condition = GroupsTSql.GetById(_igid);
            var dt = Groups.GetData("1", "*", condition, "");

            txtTitle.Text = dt.Rows[0][GroupsColumns.VgName].ToString();
            HdTitle.Value = dt.Rows[0][GroupsColumns.VgName].ToString();
            UploadImage.Load(StringExtension.LayChuoi(dt.Rows[0][GroupsColumns.VgImage].ToString(), "", 1));
            UploadImage1.Load(StringExtension.LayChuoi(dt.Rows[0][GroupsColumns.VgImage].ToString(), "", 2));

            txtOrder.Text = dt.Rows[0][GroupsColumns.IgSortOrder].ToString();
            txtDesc.Text = dt.Rows[0][GroupsColumns.VgDescription].ToString();
            txtDate.Text = ((DateTime)dt.Rows[0][GroupsColumns.DgDateCreated]).ToString("yyyy-MM-ddTHH:mm");
            txt_content.Text = StringExtension.LayChuoi(dt.Rows[0][GroupsColumns.VgContent].ToString(), "", 0);
            txt_content2.Text = StringExtension.LayChuoi(dt.Rows[0][GroupsColumns.VgContent].ToString(), "", 1);
            txt_content3.Text = StringExtension.LayChuoi(dt.Rows[0][GroupsColumns.VgContent].ToString(), "", 2);
            txtLink.Text = StringExtension.LayChuoi(dt.Rows[0][GroupsColumns.VgContent].ToString(), "", 3);
            HdOldContent.Value = txt_content.Text;
            HdTotalView.Value = dt.Rows[0][GroupsColumns.IgTotalView].ToString();
            HdParam.Value = dt.Rows[0][GroupsColumns.VgParam].ToString();

            #region SEO
            txtUrl.Text = dt.Rows[0][GroupsColumns.VgLink].ToString();
            txtMetaTitle.Text = dt.Rows[0][GroupsColumns.VgMetaTitle].ToString();
            txtMetaKeyword.Text = dt.Rows[0][GroupsColumns.VgMetaKeyword].ToString();
            txtMetaDescription.Text = dt.Rows[0][GroupsColumns.VgMetaDescription].ToString();
            txtTag.Text = dt.Rows[0][GroupsColumns.VgTag].ToString();
            #endregion

            cbStatus.Checked = dt.Rows[0][GroupsColumns.IgStatus].Equals(1);
            ddlCategory.SelectedValue = dt.Rows[0][GroupsColumns.IgParentId].ToString();
        }
        #endregion

        #region  insert
        else
        {
            ltrTitle.Text = DestinationKeyword.ThemMoiDanhMuc;
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            btSubmit.Text = "Thêm mới";
            txtTitle.Focus();
        }
        #endregion
    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        var contentFull = txt_content.Text + StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + txt_content2.Text + StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + txt_content3.Text + StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + txtLink.Text;
        var contentDetail = ContentExtendtions.ProcessStringContent(contentFull, HdOldContent.Value, _pic);

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
            var image1 = UploadImage1.Save(false, contentDetail);
            Groups.Insert(ddlCategory.SelectedValue, _lang, _app, txtOrder.Text, "", "0",  txtTitle.Text, txtDesc.Text, contentDetail, StringExtension.GhepChuoi("", image, image1), "", txtMetaTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, txtTag.Text, StringExtension.ReplateTitle(txtUrl.Text), txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), status);
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
            var image1 = UploadImage1.Save(true, contentDetail);
            Groups.Update(ddlCategory.SelectedValue, _app, txtOrder.Text, "", HdTotalView.Value, txtTitle.Text, txtDesc.Text, contentDetail, StringExtension.GhepChuoi("", image, image1), HdParam.Value, txtMetaTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, txtTag.Text, StringExtension.ReplateTitle(txtUrl.Text), txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), status, _igid);
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
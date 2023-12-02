using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Config;
using Developer.Keyword;
using RevosJsc.AdminControl;
using RevosJsc.MenusControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Menu_AddEditMenu : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _pic = FolderPic.Menus;
    private readonly string _control = "Menus";

    private string _app = "";
    private string _imnId = "";
    private string _parent = "0";
    private string _action = "";
    private bool _insert;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["app"] != null) _app = Request.QueryString["app"];
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (_action.Equals(TypePage.Create)) _insert = true;
        if (Request.QueryString["imnid"] != null) _imnId = Request.QueryString["imnid"];
        if (Request.QueryString["parent"] != null) _parent = Request.QueryString["parent"];

        #region Gán app, pic cho user control upload ảnh đại diện
        UploadImage.Control = _app;
        UploadImage.Pic = _pic;
        UploadImage.Text = "Ảnh đại diện";
        UploadImage.LayAnhTuNoiDung = false;
        //UploadImage.TaoAnhNho = false;
        //UploadImage.HanCheKichThuoc = false;
        #endregion

        if (IsPostBack) return;
        GetGroupInDdl();
        InitialControlsValue(_insert);
        GetItemInDdlControl();
    }
    private void GetItemInDdlControl()
    {
        var listControl = new MenusConfig();
        ddlControl.Items.Clear();
        for (var i = 0; i < listControl.TextControl.Length; i++)
        {
            ddlControl.Items.Add(new ListItem(listControl.TextControl[i], listControl.ValuesControl[i] + "__" + listControl.AppsControl[i]));
        }
    }
    protected void ddlControl_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        txtLink.Text = ddlControl.SelectedValue.Substring(0, ddlControl.SelectedValue.IndexOf("__", StringComparison.Ordinal));
        txtTitle.Text = ddlControl.SelectedItem.Text;

        var app = ddlControl.SelectedValue.Substring(ddlControl.SelectedValue.IndexOf("__", StringComparison.Ordinal) + "__".Length);

        var dt = Groups.GetAllGroups(" * ", DataExtension.AndConditon(GroupsTSql.GetByLang(_lang), GroupsTSql.GetByApp(app), GroupsColumns.IgStatus + " <> 2 "), "");

        ddlControlCate.Items.Clear();
        if (dt.Rows.Count <= 0) return;
        ddlControlCate.Items.Add(new ListItem("Chọn danh mục", ""));
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            ddlControlCate.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i][GroupsColumns.IgLevel].ToString()) + dt.Rows[i][GroupsColumns.VgName], dt.Rows[i][GroupsColumns.IgId].ToString()));
        }
    }

    protected void ddlControlCate_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var rewrite = ddlControl.SelectedValue.Substring(0, ddlControl.SelectedValue.IndexOf("__", StringComparison.Ordinal));
        txtLink.Text = rewrite + "&page=c&igid=" + ddlControlCate.SelectedValue;

        txtTitle.Text = ddlControlCate.SelectedItem.Text.Trim('.');
        
    }
    private string LinkRedrect()
    {
        return LinkAdmin.GoAdminOption(_control, TypePage.Index, "app", _app);
    }

    private void GetGroupInDdl()
    {
        var condition = DataExtension.AndConditon(MenusTSql.GetByLang(_lang), MenusTSql.GetByApp(_app), " imnStatus <> 2 ", " imnId <> '"+ _imnId + "' ");
        var dt = Menus.GetAllMenus("*", condition, "");
        ddlCategory.Items.Clear();
        ddlCategory.Items.Add(new ListItem("Danh mục gốc", "0"));
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            ddlCategory.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i][MenusColumns.ImnLevel].ToString()) + dt.Rows[i][MenusColumns.VmnName], dt.Rows[i][MenusColumns.ImnId].ToString()));
        }
        ddlCategory.SelectedValue = _parent;
    }

    private void InitialControlsValue(bool isInsert)
    {
        #region update
        if (!isInsert)
        {
            ltrTitle.Text = MenusKeyword.CapNhat;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var condition = MenusTSql.GetById(_imnId);
            var dt = Menus.GetData("1", "*", condition, "");

            txtTitle.Text = dt.Rows[0][MenusColumns.VmnName].ToString();
            HdTitle.Value = dt.Rows[0][MenusColumns.VmnName].ToString();
            UploadImage.Load(dt.Rows[0][MenusColumns.VmnImage].ToString());
            txtLink.Text = dt.Rows[0][MenusColumns.VmnLink].ToString();
            txtOrder.Text = dt.Rows[0][MenusColumns.ImnSortOrder].ToString();
            txtDate.Text = ((DateTime)dt.Rows[0][MenusColumns.DmnDateCreated]).ToString("yyyy-MM-ddTHH:mm");
            ddlOpenOption.SelectedValue = dt.Rows[0][MenusColumns.ImnTarget].ToString();
            ddlCategory.SelectedValue = dt.Rows[0][MenusColumns.ImnParentId].ToString();

            cbStatus.Checked = dt.Rows[0][MenusColumns.ImnStatus].Equals(1);
            ddlCategory.SelectedValue = dt.Rows[0][MenusColumns.ImnParentId].ToString();
        }
        #endregion

        #region  insert
        else
        {
            ltrTitle.Text = MenusKeyword.ThemMoi;
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
            Menus.Insert(ddlCategory.SelectedValue, _lang, _app, txtOrder.Text, status, ddlOpenOption.SelectedValue, txtTitle.Text, txtLink.Text, image, "", txtDate.Text.Replace("T", " "), DateTime.Now.ToString());
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " thêm mới menu: " + txtTitle.Text, logAuthor, logCreateDate);
            #endregion
        }
        #endregion

        #region Update
        else
        {
            var image = UploadImage.Save(true, "");
            Menus.Update(ddlCategory.SelectedValue, _lang, _app, txtOrder.Text, status, ddlOpenOption.SelectedValue, txtTitle.Text, txtLink.Text, image, "", txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), _imnId);
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật menu: " + HdTitle.Value, logAuthor, logCreateDate);
            #endregion
        }
        #endregion

        #region After Insert/Update
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo menu: " + txtTitle.Text + "', {type: 'success'});});", true);

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
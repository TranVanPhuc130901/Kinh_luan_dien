using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Developer.Keyword;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.LanguageControl;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Language_Keyword_AddEditKeyword : UserControl
{
    private readonly string _app = CodeApplications.Language;
    private string _ikid = "";
    private bool _isInsert;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ikid"] != null) _ikid = Request.QueryString["ikid"];
        if (_ikid.Length < 1) _isInsert = true;

        if (IsPostBack) return;
        InitialControlsValue(_isInsert);
    }

    private void InitialControlsValue(bool insert)
    {
        #region update

        if (!insert)
        {
            ltrTitle.Text = LanguageKeyword.CapNhatTuKhoa;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;

            var dt = Keywords.GetData("", "*", KeywordsTSql.GetById(_ikid), "");

            txtTitle.Text = dt.Rows[0][KeywordsColumns.VkTitle].ToString();
        }

        #endregion update

        #region insert

        else
        {
            ltrTitle.Text = LanguageKeyword.ThemMoiTuKhoa;
            btSubmit.Text = "Thêm mới";
            txtTitle.Focus();
        }

        #endregion insert
    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        #region Insert

        if (_isInsert)
        {
            var dt = Keywords.GetDataByTitle(txtTitle.Text, "");
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener(\"DOMContentLoaded\", function () {$.bootstrapGrowl(\"Lỗi: Từ khoá <b>" + txtTitle.Text + "</b> đã tồn tại!\", {type: \"warning\"});});", true);
                return;
            }
            Keywords.Insert(txtTitle.Text, "");
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " thêm mới từ khóa: " + txtTitle.Text, logAuthor, logCreateDate);
            #endregion
        }

        #endregion

        #region Update

        else
        {
            Keywords.Update(_ikid, txtTitle.Text, "");

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật từ khóa: " + txtTitle.Text, logAuthor, logCreateDate);
            #endregion
        }


        #endregion

        #region After Insert/Update

        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo từ khóa: " + txtTitle.Text + "', {type: 'success'});});", true);

        if (cbContiue.Checked)
        {
            ResetControls();
        }
        else
        {
            Response.Redirect(Link.LnkMnKeyword());
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

        txtTitle.Focus();
    }
}
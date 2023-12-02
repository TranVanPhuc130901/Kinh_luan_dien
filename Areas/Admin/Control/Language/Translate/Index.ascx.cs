using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Language_Translate_Index : System.Web.UI.UserControl
{
    private readonly string lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        GetListKeywords();
    }

    private void GetListKeywords()
    {
        var dt = Keywords.GetData("", "*", "", KeywordsColumns.VkTitle);
        RpListLanguageNationals.DataSource = dt;
        RpListLanguageNationals.DataBind();
    }

    protected void RpListLanguageNationals_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        var c = e.CommandName.Trim();
        switch (c)
        {
            #region edit
            case "edit":
                var commandArgs = e.CommandArgument.ToString().Split(',');
                var id = commandArgs[0];
                var name = commandArgs[1];
                EditValue(id);
                break;
            #endregion
        }
    }
    private void EditValue(string id)
    {
        // Kiểm tra keyword đã có trong bảng Translate chưa
        var condition = DataExtension.AndConditon(
            TranslateTSql.GetByikId(id),
            TranslateTSql.GetByLang(lang)
            );
        var dt = Translate.GetData("", TranslateColumns.ItId, condition, "");
        // Nếu đã có trong bảng Translate thì update
        if (dt.Rows.Count > 0)
        {
            for (var i = 0; i <= RpListLanguageNationals.Items.Count - 1; i++)
            {
                var txtTranslate = (TextBox) RpListLanguageNationals.Items[i].FindControl("txtTranslate");
                var value = txtTranslate.Text;
                if (txtTranslate.ToolTip.Equals(id)) Translate.Update(id, lang, value);
            }
        }
        // Chưa có trong bảng Translate thì insert vào bảng Translate
        else
        {
            for (var i = 0; i <= RpListLanguageNationals.Items.Count - 1; i++)
            {
                var txtTranslate = (TextBox)RpListLanguageNationals.Items[i].FindControl("txtTranslate");
                var value = txtTranslate.Text;
                if (!txtTranslate.ToolTip.Equals(id) || value.Equals("")) continue;
                Translate.Insert(id, lang, value);
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);
    }
    protected string TranslateKeyword(string id)
    {
        var condition = DataExtension.AndConditon(
            TranslateTSql.GetByikId(id),
            TranslateTSql.GetByLang(lang)
        );
        var dt = Translate.GetData("1", "*", condition, "");
        return dt.Rows.Count > 0 ? dt.Rows[0][TranslateColumns.VtValue].ToString() : "";
    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        var arrayListFile = "";
        for (var i = 0; i < RpListLanguageNationals.Items.Count; i++)
        {
            var tbTitle = (TextBox)RpListLanguageNationals.Items[i].FindControl("txtTranslate");
            arrayListFile += tbTitle.ToolTip + ",";
        }
        char[] split = { ',' };
        foreach (var itemId in arrayListFile.Split(split, StringSplitOptions.RemoveEmptyEntries))
        {
            EditValue(itemId);
        }
    }
}
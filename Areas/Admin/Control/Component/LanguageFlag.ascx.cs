using System;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.LanguageControl;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Component_LanguageFlag : System.Web.UI.UserControl
{
    private readonly string _lang = Cookie.GetLanguageValueAdmin();
    private string _action = "";
    private string _control = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (Request.QueryString["control"] != null) _control = Request.QueryString["control"];
        if (IsPostBack) return;
        ltrList.Text = GetList();
    }

    private string GetList()
    {
        var s = "";
        var list = "";
        var dt = LanguageNational.GetData("", "*", LanguageNationalTSql.GetByStatus("1"), LanguageNationalColumns.IlnSortOrder);
        if (dt.Rows.Count < 1) return s;
        if (dt.Rows.Count > 1) s += "<li class='dropdown'>";
        else s += "<li>";
        list += "<ul class='dropdown-menu dropdown-custom dropdown-options'>";
        list += "<li class='dropdown-header text-center'>Chọn ngôn ngữ</li>";
        list += "<li><div class='btn-group btn-group-justified btn-group-sm'>";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            if (_lang == dt.Rows[i][LanguageNationalColumns.IlnId].ToString()) s += "<a href='javascript:void(0)' class='dropdown-toggle fs0' data-toggle='dropdown'>" + ImagesExtension.GetImage(FolderPic.Language, dt.Rows[i][LanguageNationalColumns.VlnFlag].ToString(), dt.Rows[i][LanguageNationalColumns.VlnName].ToString(), "", false, false, "") + "</a>";
            list += "<a href=\"javascript:SetLangAdmin('" + dt.Rows[i][LanguageNationalColumns.IlnId] + "','"+ _control +"','"+ _action +"');\" class='mr5'>" + ImagesExtension.GetImage(FolderPic.Language, dt.Rows[i][LanguageNationalColumns.VlnFlag].ToString(), dt.Rows[i][LanguageNationalColumns.VlnName].ToString(), "", false, false, "") + "</a>";
        }
        list += "</div></li>";
        list += "</ul>";
        s += list;
        s += "</li>";
        return s;
    }
}
using System;
using System.Text;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.LanguageControl;

public partial class Areas_Admin_Control_Language_Control_Index : System.Web.UI.UserControl
{
    private string app = CodeApplications.Language;
    private string pic = FolderPic.Language;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        GetList("");
    }


    private void GetList(string order)
    {
        var s = new StringBuilder();
        var dt = LanguageNational.GetData("", "*", "", LanguageNationalColumns.IlnSortOrder);
        if (dt.Rows.Count < 1) return;
        
        #region Lấy ra danh sách ngôn ngữ

        if (dt.Rows.Count < 1) return;
        s.Append("<tbody>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][LanguageNationalColumns.IlnId].ToString();
            var titleItem = dt.Rows[i][LanguageNationalColumns.VlnName].ToString();
            var status = dt.Rows[i][LanguageNationalColumns.IlnStatus].ToString();
            s.Append(@"
<tr id='item" + id + @"'>
    <td class='text-center'><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + @"'/></td>
    <td class='text-center'>" + ImagesExtension.GetImage(pic, dt.Rows[i][LanguageNationalColumns.VlnFlag].ToString(), titleItem, "mw50px pr5 fl", false, false, "") + " " + titleItem + @"</td>
    <td class='text-center'>"+ dt.Rows[i][LanguageNationalColumns.IlnSortOrder] + @"</td>
    <td class='text-center'><label class='switch switch-primary'><input type='checkbox' onchange='OnOffLanguage(" + id + ")' " + (status.Equals("1") ? "checked" : "") + @"><span></span></label></td>
    <td class='text-center'>
        <div class='btn-group-xs'>
            <a href='" + LinkAdmin.GoAdminOption(CodeApplications.Language, TypePage.AddEditNational, "ilnId", id) + @"' title='Chỉnh sửa' class='btn btn-default'><i class='fa fa-pencil'></i></a>
        </div>
    </td>
</tr>");
        }
        s.Append("</tbody>");

        #endregion
        ltrList.Text = s.ToString();
    }
}
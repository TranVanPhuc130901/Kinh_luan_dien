using System;
using System.Text;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.MenusControl;
using RevosJsc.TSql;

public partial class Areas_Display_Component_MenuOther : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _app = CodeApplications.MenuOther;
    private readonly string _pic = FolderPic.Menus;
    private string _igid = "";
    private string _rewrite = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["igid"] != null) _igid = Request.QueryString["igid"];
        if (Request.QueryString["rewrite"] != null) _rewrite = Request.QueryString["rewrite"];
        if (_rewrite == "" && Session["rewrite"] != null) _rewrite = StringExtension.RemoveSqlInjectionChars(Session["rewrite"].ToString());
        #region title
        if (Request.QueryString["title"] != null)
        {
            if (_igid.Length < 1 && Session["igid"] != null) _igid = Session["igid"].ToString();
        }
        #endregion title
        ltrList.Text = GetList();
        var hotline = SettingsExtension.GetSettingKey(SettingsExtension.KeyHotLine, _lang);
        ltrList.Text += "<p>Nếu bạn cần hỗ trợ thêm, vui lòng gọi: <a class='text-white' href='tel:"+ StringExtension.RemoveCharsInPhoneNumber(hotline) +"'>"+ hotline + "</a></p>";
    }

    private string GetList()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            MenusTSql.GetByApp(_app),
            MenusTSql.GetByParentId("0"),
            MenusTSql.GetByStatus("1")
        );
        var dt = Menus.GetData("", "*", condition, MenusColumns.ImnSortOrder);
        if (dt.Rows.Count < 1) return "";
        s.Append("<div class='inner'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var name = dt.Rows[i][MenusColumns.VmnName].ToString();
            //var image = dt.Rows[i][MenusColumns.VmnImage].ToString();
            //if (image.Length > 0) image = image + ".ashx?w=20";
            var link = RewriteExtension.GetLinkMenu(dt.Rows[i][MenusColumns.VmnLink].ToString());
            var target = dt.Rows[i][MenusColumns.ImnTarget].ToString().Equals("1") ? "target='_blank'" : "target='_self'";
            s.Append("<a class='btn_outline' href='" + link + "' " + target + ">" + name + "</a>");
        }
        s.Append("</div>");
        return s.ToString();
    }
}
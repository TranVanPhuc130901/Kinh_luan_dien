using System;
using System.Data;
using Developer.Extension;
using RevosJsc;
using RevosJsc.Columns;
using RevosJsc.Extension;
using RevosJsc.NewsControl;

public partial class Areas_Display_News_Control_Detail : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _app = CodeApplications.News;
    private readonly string _pic = FolderPic.News;
    private readonly string _maxItem = SettingKey.SoNewsKhacTrenMotTrang;
    private readonly string _noResultText = LanguageExtension.TranslateKeyword("Updating ...");
    private string _iiid = "";
    private string _igid = "";
    protected string LinkShare = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["iiid"] != null) _iiid = QueryStringExtension.GetQueryString("iiid");
        if (Request.QueryString["igid"] != null) _igid = QueryStringExtension.GetQueryString("igid");

        #region title
        if (Request.QueryString["title"] != null)
        {
            //Lấy igid từ session ra vì nó đã dược lưu khi xét tại Default.aspx
            if (_iiid.Length < 1 && Session["iiid"] != null) _iiid = Session["iiid"].ToString();
            if (_igid.Length < 1 && Session["igid"] != null) _igid = Session["igid"].ToString();
        }
        #endregion title
        GetDataBySession();
    }
    private void GetDataBySession()
    {
        var dt = (DataTable)Session["dataByTitle_Category"];
        var titleItem = dt.Rows[0][GroupsColumns.VgName].ToString();
        var desc = dt.Rows[0][GroupsColumns.VgDescription].ToString();
        var content = dt.Rows[0][GroupsColumns.VgContent].ToString();
        ltrInfo.Text = "<h1>" + titleItem + "</h1>";
        if (desc.Length > 0) ltrInfo.Text += "<div class='desc'>" + desc.Replace("\n","<br/>") + "</div>";
        ltrContent.Text = content.Length > 0 ? content : _noResultText;
    }
}
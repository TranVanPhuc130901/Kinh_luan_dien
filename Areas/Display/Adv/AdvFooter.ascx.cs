using System;
using System.Text;
using RevosJsc.AdvertistmentsControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Display_Adv_AdvFooter : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _pic = FolderPic.Advertistments;
    protected void Page_Load(object sender, EventArgs e)
    {
        ltrAdv.Text = GetList("5", "");
        if (ltrAdv.Text == "") Visible = false;
    }

    private string GetList(string pos, string cssClass)
    {
        var s = new StringBuilder();
        var fields = DataExtension.GetListColumns(
            AdvertistmentsColumns.VaTitle,
            AdvertistmentsColumns.VaDescription,
            AdvertistmentsColumns.VaImage,
            AdvertistmentsColumns.VaParam,
            AdvertistmentsColumns.VaLink,
            AdvertistmentsColumns.IaTarget
            );
        var condition = DataExtension.AndConditon(
            AdvertistmentPositionsTSql.GetByPosition(pos),
            AdvertistmentPositionsTSql.GetByStatus("1"),
            AdvertistmentPositionsTSql.GetByLang(_lang),
            AdvertistmentsTSql.GetByStatus("1")
            );
        var dt = Advertistments.GetAllData("", fields, condition, AdvertistmentsColumns.IaSortOrder);
        if (dt.Rows.Count <= 0) return s.ToString();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][AdvertistmentsColumns.VaTitle].ToString();
            var desc = dt.Rows[i][AdvertistmentsColumns.VaDescription].ToString();
            var image = dt.Rows[i][AdvertistmentsColumns.VaImage].ToString();
            var link = dt.Rows[i][AdvertistmentsColumns.VaLink].ToString();
            var param = dt.Rows[i][AdvertistmentsColumns.VaParam].ToString();
            if (link.Equals("")) link = "javascript://";
            var target = dt.Rows[i][AdvertistmentsColumns.IaTarget].ToString().Equals("1") ? "target='_blank'" : "target='_self'";
            s.Append(@"
<div class='card'>
    " + ImagesExtension.GetImage(_pic, image, titleItem, "", false, false, "", false) + @"
    <div class='title'>" + titleItem + @"</div>
    <div class='desc'>" + desc + @"</div>
    " + (param.Length > 0 ? "<a class='more' href='" + link + "' " + target + ">" + param + "</a>" : "") + @"
</div>");
        }
        return s.ToString();
    }
}
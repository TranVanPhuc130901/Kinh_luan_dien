using System;
using System.Text;
using RevosJsc.AdvertistmentsControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Display_Adv_AdvProductCategories : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _pic = FolderPic.Advertistments;
    protected void Page_Load(object sender, EventArgs e)
    {
        ltrAdv.Text = GetList("3", "lazyload");
        if (ltrAdv.Text == "") Visible = false;
    }

    private string GetList(string pos, string cssClass)
    {
        var s = new StringBuilder();
        var fields = DataExtension.GetListColumns(
            AdvertistmentsColumns.VaTitle,
            AdvertistmentsColumns.VaDescription,
            AdvertistmentsColumns.VaImage,
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
        s.Append("<div class='collections_list owl-carousel'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][AdvertistmentsColumns.VaTitle].ToString();
            var desc = dt.Rows[i][AdvertistmentsColumns.VaDescription].ToString();
            var image = dt.Rows[i][AdvertistmentsColumns.VaImage].ToString();
            var link = dt.Rows[i][AdvertistmentsColumns.VaLink].ToString();
            if (link.Equals("")) link = "javascript://";
            var target = dt.Rows[i][AdvertistmentsColumns.IaTarget].ToString().Equals("1") ? "target='_blank'" : "target='_self'";
            s.Append(@"
<div class='card'>
    <div class='wImage'>
        " + ImagesExtension.GetImage(_pic, image, titleItem, cssClass, false, false, "", false, true) + @"
    </div>
    <a href='" + link + "' " + target + @" class='card_link'></a>
    <div class='card_option'>
        <div class='inner'>
            <div class='card_caption'>" + titleItem + @"</div>
            <span class='btn'>Xem chi tiết</span>
        </div>
    </div>
</div>");
        }
        s.Append("</div>");
        return s.ToString();
    }
}
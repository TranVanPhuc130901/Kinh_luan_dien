using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.NewsControl;
using RevosJsc.TSql;
using FolderPic = RevosJsc.AdvertistmentsControl.FolderPic;

public partial class Areas_Display_Adv_CustomerImage : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private readonly string _pic = FolderPic.Advertistments;
    protected string Rewrite = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["rewrite"] != null) Rewrite = QueryStringExtension.GetQueryString("rewrite");
        if (Rewrite.Length < 1 && Session["rewrite"] != null) Rewrite = Session["rewrite"].ToString();
        ltrCustomer.Text = GetList("100", "lazyload");
   
    }

    private string GetList(string pos, string cssClass)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            AdvertistmentPositionsTSql.GetByPosition(pos),
            AdvertistmentPositionsTSql.GetByStatus("1"),
            AdvertistmentPositionsTSql.GetByLang(_lang),
            AdvertistmentsTSql.GetByStatus("1")
        );
        var dt = Advertistments.GetAllData("", "*", condition, AdvertistmentsColumns.IaSortOrder);

        //s.Append("<video loop muted autoplay playsinline preload=''><source src='/video/outdoorclothings.webm' type='video/webm'></video>");
        if (dt.Rows.Count <= 0) return s.ToString();
        s.Append("<h2 class='customerTitle'>"+ dt.Rows[0][AdvertistmentPositionsColumns.VapName] + "</h2>");
        s.Append("<div class='customerImage_homepage'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var titleItem = dt.Rows[i][AdvertistmentsColumns.VaTitle].ToString();
            var desc = dt.Rows[i][AdvertistmentsColumns.VaParam].ToString();
            var image = dt.Rows[i][AdvertistmentsColumns.VaImage].ToString();
            var link = dt.Rows[i][AdvertistmentsColumns.VaLink].ToString();
            if (link.Equals("")) link = "";
            var target = dt.Rows[i][AdvertistmentsColumns.IaTarget].ToString().Equals("1") ? "target='_blank'" : "target='_self'";
            s.Append("<div class='customerItem'>");
            s.Append("<div data-sku='" + link + "' class='image'>" + ImagesExtension.GetImage(_pic, image, titleItem, cssClass, false, false, "", false, true, "") + @"</div>");
            s.Append("<div class='viewCustomer'><span>Xem ảnh</span></div>");
            s.Append("</div>");
        }
        s.Append("</div>");
        if (dt.Rows.Count > 12)
        {
            s.Append("<div class='viewAll'><button>Xem thêm</button></div>");
        }
        
        return s.ToString();
    }
}
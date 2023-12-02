using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Developer.Extension;
using RevosJsc.Columns;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;

public partial class Areas_Display_Ajax_Customer : System.Web.UI.Page
{
    private string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private string _action = "";
    private readonly string _app = CodeApplications.Product;
    private readonly string _pic = FolderPic.Product;
    private readonly JavaScriptSerializer _js = new JavaScriptSerializer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (Request.Form["action"] != null) _action = Request.Form["action"];
        if (_action == "") return;
        switch (_action)
        {
            case "GetImageBySku":
                GetImageBySku();
                break;
        }
    }

    private void GetImageBySku()
    {
        var s = new StringBuilder();
        var sku = Request.Form["sku"] ?? "";

        var condition = DataExtension.AndConditon(
            ItemsTSql.GetByApp(_app),
            ItemsTSql.GetByLang(_lang),
            ItemsTSql.GetByStatus("1"),
            ItemsTSql.GetByCode(sku)
        );

         var dt = RevosJsc.Database.Items.GetData("1", "ViTitle, ViImage, ViLink, fiPriceOld", condition, "");
        if (dt.Rows.Count < 1) return;
        var titleItem = dt.Rows[0][ItemsColumns.ViTitle].ToString();
         var image = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViImage].ToString(), "", 1);
         var link = (UrlExtension.WebsiteUrl + dt.Rows[0][ItemsColumns.ViLink] + RewriteExtension.Extensions).ToLower();
         var price = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();

        s.Append(@"
        <a class='wImage' href='" + link + "' title='" + titleItem + @"'>
        " + ImagesExtension.GetImage(_pic, image, titleItem, "lazy", false, false, "", false, false, "") + @"
    </a>
    <div class='infoProductCustomer'>
        <div class='titleProductRight'>" + titleItem + @"</div>
        <div class='price'>" + NumberExtension.FormatNumber(price, true, "", "đ") + @"</div>
    <div/>
   ");
        string[] reply = { s.ToString()};
        Response.Output.Write(_js.Serialize(reply));
    }

}

using System;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.ProductControl;
using RevosJsc.TSql;

public partial class Areas_Admin_Ajax_Items_CloneProduct : System.Web.UI.Page
{
    private string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private string _igid = "";
    private string _iid = "";
    private string _app = "";
    JavaScriptSerializer _js = new JavaScriptSerializer();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) return;
        _igid = Request["igid"];
        _iid = Request["iid"];
        _app = Request["app"];
        UpdateOrder();
    }

    private void UpdateOrder()
    {
        var dt = RevosJsc.Database.Items.GetData("1", "*", ItemsTSql.GetById(_iid), "");
        if (dt.Rows.Count <= 0) return;

        var fileNotEx = Guid.NewGuid().ToString();
        var viImage = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViImage].ToString(), "", 1);
        var fileEx = viImage.Substring(viImage.LastIndexOf(".", StringComparison.Ordinal));
        var pic = ImagesExtension.GetFolderByApp(dt.Rows[0][ItemsColumns.ViApp].ToString());
        if (viImage.Length > 0)
        {
            var path = Request.PhysicalApplicationPath + "/" + pic + "/";

            #region Kiểm tra xem thư mục đã tồn tại chưa, nếu chưa -> tạo mới thư mục
            var dri = new DirectoryInfo(path);
            if (!dri.Exists) dri.Create();
            #endregion

            var image = Image.FromFile(path + viImage);
            image.Save(path + fileNotEx + fileEx);
            //ImagesExtension.SaveImageFromUrl2(path + fileNotEx + fileEx, path + viImage);
        }
        var lastIid = GroupItems.InsertItemGroupItem_returnItemsId(_lang, _app, dt.Rows[0][ItemsColumns.ViCode].ToString(), dt.Rows[0][ItemsColumns.ViTitle] + " - copy", dt.Rows[0][ItemsColumns.ViDescription].ToString(), dt.Rows[0][ItemsColumns.ViContent].ToString(), StringExtension.GhepChuoi("", fileNotEx + fileEx), dt.Rows[0][ItemsColumns.ViAuthor].ToString(), dt.Rows[0][ItemsColumns.ViMetaTitle].ToString(), dt.Rows[0][ItemsColumns.ViMetaKeyword].ToString(), dt.Rows[0][ItemsColumns.ViDescription].ToString(), dt.Rows[0][ItemsColumns.ViTag].ToString(), dt.Rows[0][ItemsColumns.ViLink] + "-copy", dt.Rows[0][ItemsColumns.FiPriceOld].ToString(), dt.Rows[0][ItemsColumns.FiPriceNew].ToString(), dt.Rows[0][ItemsColumns.ViParam].ToString(), dt.Rows[0][ItemsColumns.IiTotalView].ToString(), dt.Rows[0][ItemsColumns.IiSortOrder].ToString(), DateTime.Now.ToString(), DateTime.Now.ToString(), dt.Rows[0][ItemsColumns.IiStatus].ToString(), _igid, DateTime.Now.ToString(), DateTime.Now.ToString());

        #region Lấy ảnh thực tế

        SetSubImageOther(_iid, lastIid, pic);

        #endregion
        #region Logs
        var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
        var logCreateDate = DateTime.Now.ToString();
        Logs.Insert(LinkAdmin.GoAdminSubControl("Product", "Item"), logCreateDate + ": " + logAuthor + " tạo bản sao " + dt.Rows[0][ItemsColumns.ViTitle], logAuthor, logCreateDate);
        #endregion
        string[] reply = { "Đã tạo bản sao!", "/admin?control=Product&action=UpdateItem&iiid=" + lastIid };
        Response.Output.Write(_js.Serialize(reply));
    }

    private void SetSubImageOther(string iid, string lastIid, string pic)
    {
        var condition = DataExtension.AndConditon(
            SubItemsTSql.GetByStatus("1"),
            SubItemsTSql.GetByIiid(iid)
        );
        var dt = Subitems.GetData("", "*", condition, SubitemsColumns.IsiSortOrder);
        if (dt.Rows.Count <= 0) return;
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var fileNotEx = Guid.NewGuid().ToString();
            var viImage = dt.Rows[0][SubitemsColumns.VsiImage].ToString();
            var fileEx = viImage.Substring(viImage.LastIndexOf(".", StringComparison.Ordinal));
            if (viImage.Length > 0)
            {
                var path = Request.PhysicalApplicationPath + "/" + pic + "/";

                #region Kiểm tra xem thư mục đã tồn tại chưa, nếu chưa -> tạo mới thư mục
                var dri = new DirectoryInfo(path);
                if (!dri.Exists) dri.Create();
                #endregion

                var image = Image.FromFile(path + viImage);
                image.Save(path + fileNotEx + fileEx);
                //ImagesExtension.SaveImageFromUrl2(path + fileNotEx + fileEx, path + viImage);
            }
            Subitems.Insert(lastIid, dt.Rows[i][SubitemsColumns.VsiLang].ToString(), dt.Rows[i][SubitemsColumns.VsiApp].ToString(), dt.Rows[i][SubitemsColumns.VsiTitle].ToString(), "", "", fileNotEx + fileEx, "", "0", "0", dt.Rows[i][SubitemsColumns.IsiSortOrder].ToString(), "1", dt.Rows[i][SubitemsColumns.DsiDateCreated].ToString(), dt.Rows[i][SubitemsColumns.DsiDateModified].ToString());
        }
    }
}
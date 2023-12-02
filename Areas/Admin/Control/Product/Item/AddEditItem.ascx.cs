using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Config;
using Developer.Extension;
using Developer.Keyword;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RevosJsc.ProductControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;
using NPOI.HPSF;

public partial class Areas_Admin_Control_Product_Item_AddEditItem : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _app = CodeApplications.Product;
    private readonly string _appVariant = CodeApplications.Variant;
    private readonly string _pic = FolderPic.Product;

    private string _iiid = "";
    private string _action = "";
    private bool _insert;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null) _action = Request.QueryString["action"];
        if (_action.Equals(TypePage.CreateItem)) _insert = true;
        if (Request.QueryString["iiid"] != null) _iiid = Request.QueryString["iiid"];

        #region Gán app, pic cho user control upload ảnh đại diện

        UploadImage.Control = _app;
        UploadImage.Pic = _pic;
        UploadImage.Text = "Ảnh đại diện";
        UploadImage.LayAnhTuNoiDung = true;
        UploadImage.TaoAnhNho = false;
        UploadImage.HanCheKichThuoc = false;

        UploadImage1.Control = _app;
        UploadImage1.Pic = _pic;
        UploadImage1.Text = "Ảnh banner";
        UploadImage1.LayAnhTuNoiDung = false;
        UploadImage1.TaoAnhNho = false;
        UploadImage1.HanCheKichThuoc = false;

        #endregion

        #region Gán đường dẫn cho ckeditor

        foreach (Control control in form.Controls)
        {
            var ckEditorControl = control as CKEditorControl;
            if (ckEditorControl != null) ckEditorControl.FilebrowserImageBrowseUrl = UrlExtension.WebsiteUrl + "ckeditor/ckfinder/ckfinder.aspx?type=Images&path=" + _pic;
        }

        #endregion Gán đường dẫn cho ckeditor

        if (IsPostBack) return;
        rptOption.DataSource = new int[2];
        rptWholesale.DataSource = new int[100];
        rptOption.DataBind();
        rptWholesale.DataBind();
        for (var i = 0; i < 2; i++)
        {
            var item = rptOption.Items[i];
            item.Visible = false;
            var linkBt = item.FindControl("lbtDelete") as LinkButton;
            if (linkBt != null) linkBt.CommandArgument = i.ToString();
        }

        for (var r = 0; r < 100; r++)
        {
            var itemWholesale = rptWholesale.Items[r];
            itemWholesale.Visible = false;
            var linkBtWholesale = itemWholesale.FindControl("lbtDelete") as LinkButton;
            if (linkBtWholesale != null) linkBtWholesale.CommandArgument = r.ToString();
        }
        OnOffControls();
        GetGroupInDdl();
        GetProductProperty();
        InitialControlsValue(_insert);
    }
    private void GetProductProperty()
    {
        var top = "";
        var fields = ItemsColumns.IiId + "," + ItemsColumns.ViTitle;
        var condition = DataExtension.AndConditon(
            ItemsTSql.GetByApp("OptionUpgrade"),
            ItemsTSql.GetByStatus("1"),
            ItemsTSql.GetByLang(_lang)
        );
        var dt = Items.GetData(top, fields, condition, ItemsColumns.IiSortOrder);
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            cblOption.Items.Add(new ListItem(dt.Rows[i][ItemsColumns.ViTitle].ToString(), dt.Rows[i][ItemsColumns.IiId].ToString()));
        }
    }
    private void OnOffControls()
    {
        fsFilter.Visible = ProductConfig.ShowFilterProperties;
    }

    private string LinkRedrect()
    {
        return LinkAdmin.GoAdminCategory(CodeApplications.Product, TypePage.Item, ddlCategory.SelectedValue);
    }

    private void GetGroupInDdl()
    {
        var condition = DataExtension.AndConditon(GroupsTSql.GetByLang(_lang), GroupsTSql.GetByApp(_app), " igStatus <> '2' ");
        var dt = Groups.GetAllGroups("*", condition, "");
        ddlCategory.Items.Clear();
        if (dt.Rows.Count < 1)
        {
            ddlCategory.CssClass = "form-control";
            ddlCategory.Items.Add(new ListItem("Vui lòng tạo danh mục trước khi thêm bài viết", ""));
        }
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            ddlCategory.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i][GroupsColumns.IgLevel].ToString()) + dt.Rows[i][GroupsColumns.VgName], dt.Rows[i][GroupsColumns.IgId].ToString()));
        }
    }

    private void InitialControlsValue(bool isInsert)
    {
        #region update
        if (!isInsert)
        {
            ltrTitle.Text = ProductKeyword.CapNhatBaiViet;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var condition = DataExtension.AndConditon(
                GroupsTSql.GetByApp(_app),
                ItemsTSql.GetById(_iiid)
                );
            var dt = GroupItems.GetAllData("1", "*", condition, "");
            txtTitle.Text = dt.Rows[0][ItemsColumns.ViTitle].ToString();
            HdTitle.Value = dt.Rows[0][ItemsColumns.ViTitle].ToString();
            UploadImage.Load(StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViImage].ToString(), "", 1));
            UploadImage1.Load(StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViImage].ToString(), "", 2)); 
            txtCode.Text = dt.Rows[0][ItemsColumns.ViCode].ToString();
            txtOrder.Text = dt.Rows[0][ItemsColumns.IiSortOrder].ToString();
            txtDesc.Text = dt.Rows[0][ItemsColumns.ViDescription].ToString();
            txtDate.Text = ((DateTime)dt.Rows[0][ItemsColumns.DiDateCreated]).ToString("yyyy-MM-ddTHH:mm");
            HdOldContent.Value = dt.Rows[0][ItemsColumns.ViContent].ToString();
            txt_content.Text = dt.Rows[0][ItemsColumns.ViContent].ToString();

            // Lấy giá trị giá sỉ
            //txt_WholesalePrice.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParam].ToString(), "" , 1);
            //txt_WholesaleQuanlity.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParam].ToString(), "", 2);

            //ltrWholePrice.Text = NumberExtension.FormatNumber(StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParam].ToString(), "", 1));

            HdTotalView.Value = dt.Rows[0][ItemsColumns.IiTotalView].ToString();
            HdOldAuthor.Value = dt.Rows[0][ItemsColumns.ViAuthor].ToString();
            cbInventory.Checked = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParam].ToString(), "", 3).Equals("1");


            #region Price

            txtPriceOld.Text = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();
            ltrPriceOld.Text = NumberExtension.FormatNumber(dt.Rows[0][ItemsColumns.FiPriceOld].ToString());
            txtPriceNew.Text = dt.Rows[0][ItemsColumns.FiPriceNew].ToString();
            ltrPriceNew.Text = NumberExtension.FormatNumber(dt.Rows[0][ItemsColumns.FiPriceNew].ToString());

            #endregion

            #region SEO
            txtUrl.Text = dt.Rows[0][ItemsColumns.ViLink].ToString();
            txtMetaTitle.Text = dt.Rows[0][ItemsColumns.ViMetaTitle].ToString();
            txtMetaKeyword.Text = dt.Rows[0][ItemsColumns.ViMetaKeyword].ToString();
            txtMetaDescription.Text = dt.Rows[0][ItemsColumns.ViMetaDescription].ToString();
            txtTag.Text = dt.Rows[0][ItemsColumns.ViTag].ToString();
            #endregion

            cbStatus.Checked = dt.Rows[0][ItemsColumns.IiStatus].Equals(1);
            ddlCategory.SelectedValue = dt.Rows[0][GroupsColumns.IgId].ToString();
            HdOldIgId.Value = dt.Rows[0][GroupsColumns.IgId].ToString();

            HdPromotionEndDate.Value = dt.Rows[0][ItemsColumns.PromotionEndDate].ToString();
            HdPromotionStartDate.Value = dt.Rows[0][ItemsColumns.PromotionStartDate].ToString();
            HdVariant.Value = dt.Rows[0][ItemsColumns.Variant].ToString();
            HdWholesale.Value = dt.Rows[0][ItemsColumns.ViParam].ToString();

            #region Variants

            var splitChar = StringExtension.SpecialCharactersKeyword.ParamsSpilitRole;
            dynamic variants = HdVariant.Value.Length > 0 ? JsonConvert.DeserializeObject(HdVariant.Value) : null;
            if (variants != null && variants.Count > 0)
            {
                for (var i = 0; i < variants.Count; i++)
                {
                    var item = variants[i];
                    var groupName = rptOption.Items[i].FindControl("tbGroupName") as TextBox;
                    var className = rptOption.Items[i].FindControl("tbClass") as TextBox;
                    if (groupName == null || className == null) continue;
                    className.Attributes["placeholder"] = "";
                    groupName.Text = variants[i].label.ToString();
                    className.Text = variants[i].value.ToString();
                    rptOption.Items[i].Visible = true;
                }
                btAddOption.CommandArgument = (variants.Count + 1).ToString();
            }

            #endregion

            #region Wholesale
            var splitCharWholesale = StringExtension.SpecialCharactersKeyword.ParamsSpilitRole;
            dynamic wholesale = HdWholesale.Value.Length > 0 ? JsonConvert.DeserializeObject(HdWholesale.Value) : null;
            if (wholesale != null && wholesale.Count > 0)
            {
                for (var i = 0; i < wholesale.Count; i++)
                {
                    var item = wholesale[i];
                    var groupName = rptWholesale.Items[i].FindControl("tbGroupWholesale") as TextBox;
                    var className = rptWholesale.Items[i].FindControl("tbClassWholesale") as TextBox;
                    if (groupName == null || className == null) continue;
                    className.Attributes["placeholder"] = "";
                    groupName.Text = wholesale[i].label.ToString();
                    className.Text = wholesale[i].value.ToString();
                    rptWholesale.Items[i].Visible = true;
                }
                btAddOptionWholesale.CommandArgument = (wholesale.Count + 1).ToString();
                //ltrWholePrice.Text = NumberExtension.FormatNumber(wholesale[0].value.ToString());
            }
            #endregion

            #region Thuộc tính lọc

            GetFilterProperties();
            var filterProperties = "";
            dt = FilterItems.GetData("", "*", FilterItemsTSql.GetByItemsId(_iiid), "");
            if (dt.Rows.Count > 0) filterProperties = dt.Rows[0][FilterItemsColumns.VfiParam].ToString();
            for (var i = 0; i < rptParentFilter.Items.Count; i++)
            {
                //Đánh dấu radiobuttonlist
                var rdblListAnswer = (RadioButtonList)rptParentFilter.Items[i].FindControl("rdlAnswer");
                if (rdblListAnswer != null)
                {
                    for (var j = 0; j < rdblListAnswer.Items.Count; j++)
                    {
                        rdblListAnswer.Items[j].Selected = filterProperties.IndexOf("," + rdblListAnswer.Items[j].Value + ",", StringComparison.Ordinal) > -1;
                    }
                }

                //Đánh dấu checkboxlist
                var cblListAnswer = (CheckBoxList)rptParentFilter.Items[i].FindControl("cblAnswer");
                if (cblListAnswer == null) continue;
                {
                    for (var j = 0; j < cblListAnswer.Items.Count; j++)
                    {
                        cblListAnswer.Items[j].Selected = filterProperties.IndexOf("," + cblListAnswer.Items[j].Value + ",", StringComparison.Ordinal) > -1;
                    }
                }
            }

            #endregion

        }
        #endregion

        #region  insert
        else
        {
            ltrTitle.Text = ProductKeyword.ThemMoiBaiViet;
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            btSubmit.Text = "Thêm mới";
            txtTitle.Focus();
            GetFilterProperties();
        }
        #endregion
    }


    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        var contentDetail = ContentExtendtions.ProcessStringContent(txt_content.Text, HdOldContent.Value, _pic);

        #region Status
        var status = "0";
        if (cbStatus.Checked) status = "1";
        #endregion

        #region Seo
        if (txtUrl.Text.Trim().Equals(""))
        {
            txtUrl.Text = txtTitle.Text;
        }
        if (StringExtension.ReplateTitle(txtUrl.Text).Equals("")) txtUrl.Text = Guid.NewGuid().ToString();
        if (txtMetaTitle.Text.Trim().Equals(""))
        {
            txtMetaTitle.Text = txtTitle.Text;
        }
        if (txtMetaKeyword.Text.Trim().Equals(""))
        {
            txtMetaKeyword.Text = txtTitle.Text;
        }
        if (txtMetaDescription.Text.Trim().Equals(""))
        {
            txtMetaDescription.Text = txtDesc.Text;
        }
        #endregion

        #region Insert
        if (_insert)
        {

            var wholesale = new StringBuilder();
            var rows = rptWholesale.Items.Count;
            for (var i = 0; i < rows; i++)
            {
                var item = rptWholesale.Items[i];
                if (item.Visible == false) continue;
                var groupName = item.FindControl("tbGroupWholesale") as TextBox;
                var className = item.FindControl("tbClassWholesale") as TextBox;
                if (className == null || className.Text == "") continue;
                if (groupName == null) continue;
                wholesale.Append("{");
                wholesale.Append("\"label\":\"" + groupName.Text + "\",\"value\":\"" + className.Text + "\"");
                wholesale.Append("}");
                if (i < rows - 1) wholesale.Append(",");
            }

            if (wholesale.Length > 0)
            {
                wholesale.Insert(0, "[");
                wholesale.Append("]");
            }
            else return;

            //string[] fields = { ItemsColumns.ViParam };
            //string[] values = { wholesale.ToString() };
            //DatabaseExtension.Update(DataExtension.UpdateValues(fields, values), ItemsTSql.GetById(id), ItemsColumns.TableName);

            var image = UploadImage.Save(false, contentDetail);
            var image1 = UploadImage1.Save(false, "");
            _iiid = GroupItems.InsertItemGroupItem_returnItemsId(_lang, _app, txtCode.Text, txtTitle.Text, txtDesc.Text, contentDetail, StringExtension.GhepChuoi("", image, image1), HdOldAuthor.Value, txtMetaTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, txtTag.Text, StringExtension.ReplateTitle(txtUrl.Text), txtPriceOld.Text, txtPriceNew.Text, wholesale.ToString(), "0", txtOrder.Text, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(CultureInfo.InvariantCulture), status, ddlCategory.SelectedValue, DateTime.Now.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture));

            //#region Xử lý giá bán lẻ
            //WholesaleProcessing();
            //#endregion


            #region Xử lý biến thể
            VariantProcessing();
            #endregion
           

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " thêm mới: " + txtTitle.Text, logAuthor, logCreateDate);
            #endregion
        }
        #endregion

        #region Update
        else
        {
            var image = UploadImage.Save(true, contentDetail);
            var image1 = UploadImage1.Save(true, "");
            GroupItems.UpdateItemGroupItem(_lang, _app, txtCode.Text, txtTitle.Text, txtDesc.Text, contentDetail, StringExtension.GhepChuoi("", image, image1), HdOldAuthor.Value, txtMetaTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, txtTag.Text, StringExtension.ReplateTitle(txtUrl.Text), txtPriceOld.Text, txtPriceNew.Text, "", HdTotalView.Value, txtOrder.Text, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(CultureInfo.InvariantCulture), status, ddlCategory.SelectedValue, DateTime.Now.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture), _iiid, HdOldIgId.Value);

            #region Xử lý biến thể

            if (HdVariant.Value.Equals(""))
            {
                VariantProcessing();
            }
            else
            {
                var variants = new StringBuilder();
                var rows = rptOption.Items.Count;
                if (rows > 0)
                {
                    for (var i = 0; i < rows; i++)
                    {
                        var item = rptOption.Items[i];
                        if (item.Visible == false) continue;
                        var groupName = item.FindControl("tbGroupName") as TextBox;
                        var className = item.FindControl("tbClass") as TextBox;
                        if (className == null || className.Text == "") continue;
                        if (groupName == null) continue;
                        variants.Append("{");
                        variants.Append("\"label\":\"" + groupName.Text + "\",\"value\":\"" + className.Text + "\"");
                        variants.Append("}");
                        if (i < rows - 1) variants.Append(",");
                    }
                    if (variants.ToString().EndsWith(",")) variants.Remove(variants.Length - 1, variants.Length);
                }

                if (variants.Length > 0)
                {
                    variants.Insert(0, "[");
                    variants.Append("]");
                }
                string[] fields = { ItemsColumns.Variant };
                string[] values = { variants.ToString() };
                DatabaseExtension.Update(DataExtension.UpdateValues(fields, values), ItemsTSql.GetById(_iiid), ItemsColumns.TableName);
            }
            #endregion

            #region Xử lý giá sỉ

            if (HdWholesale.Value.Equals(""))
            {
                WholesaleProcessing();
            }
            else
            {
                var wholesale = new StringBuilder();
                var rows = rptWholesale.Items.Count;
                if (rows > 0)
                {
                    for (var i = 0; i < rows; i++)
                    {
                        var item = rptWholesale.Items[i];
                        if (item.Visible == false) continue;
                        var groupName = item.FindControl("tbGroupWholesale") as TextBox;
                        var className = item.FindControl("tbClassWholesale") as TextBox;
                        if (className == null || className.Text == "") continue;
                        if (groupName == null) continue;
                        wholesale.Append("{");
                        wholesale.Append("\"label\":\"" + groupName.Text + "\",\"value\":\"" + className.Text + "\"");
                        wholesale.Append("}");
                        if (i < rows - 1) wholesale.Append(",");
                    }
                    //if (wholesale.ToString().EndsWith(",")) wholesale.Remove(wholesale.Length - 1, wholesale.Length);
                }

                if (wholesale.Length > 0)
                {
                    wholesale.Insert(0, "[");
                    wholesale.Append("]");
                }
                string[] fields = { ItemsColumns.ViParam };
                string[] values = { wholesale.ToString() };
                DatabaseExtension.Update(DataExtension.UpdateValues(fields, values), ItemsTSql.GetById(_iiid), ItemsColumns.TableName);
            }
            #endregion

            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Logs.Insert(Request.RawUrl, logCreateDate + ": " + logAuthor + " cập nhật: " + txtTitle.Text, logAuthor, logCreateDate);
            #endregion
        }
        #endregion

        #region Thuộc tính lọc
        var filterProperties = ",";
        for (var i = 0; i < rptParentFilter.Items.Count; i++)
        {

            var rdblListAnswer = (RadioButtonList)rptParentFilter.Items[i].FindControl("rdlAnswer");
            if (rdblListAnswer != null)
            {
                if (rdblListAnswer.SelectedValue.Length > 0) filterProperties += rdblListAnswer.SelectedValue + ",";
            }

            var cblListAnswer = (CheckBoxList)rptParentFilter.Items[i].FindControl("cblAnswer");
            if (cblListAnswer == null) continue;
            for (var j = 0; j < cblListAnswer.Items.Count; j++)
            {
                if (cblListAnswer.Items[j].Selected) filterProperties += cblListAnswer.Items[j].Value + ",";
            }
        }

        var dt = FilterItems.GetData("", "*", FilterItemsTSql.GetByItemsId(_iiid), "");
        if (dt.Rows.Count > 0)
        {
            //Cap nhat
            FilterItems.Update(_iiid, filterProperties, dt.Rows[0][FilterItemsColumns.IfiId].ToString());
        }
        else
        {
            //Them moi
            FilterItems.Insert(_iiid, filterProperties);
        }
        #endregion

        #region After Insert/Update
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Đã tạo: " + txtTitle.Text + "', {type: 'success'});});", true);

        if (cbContiue.Checked)
        {
            ResetControls();
        }
        else
        {
            Response.Redirect(LinkRedrect());
        }
        #endregion
    }
    private void VariantProcessing()
    {
        //var splitChar = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        var variants = new StringBuilder();
        var rows = rptOption.Items.Count;
        for (var i = 0; i < rows; i++)
        {
            var item = rptOption.Items[i];
            if (item.Visible == false) continue;
            var groupName = item.FindControl("tbGroupName") as TextBox;
            var className = item.FindControl("tbClass") as TextBox;
            if (className == null || className.Text == "") continue;
            if (groupName == null) continue;
            variants.Append("{");
            variants.Append("\"label\":\"" + groupName.Text + "\",\"value\":\"" + className.Text + "\"");
            variants.Append("}");
            if (i < rows - 1) variants.Append(",");
        }

        if (variants.Length > 0)
        {
            variants.Insert(0, "[");
            variants.Append("]");
        }
        else return;

        string[] fields = { ItemsColumns.Variant };
        string[] values = { variants.ToString() };
        DatabaseExtension.Update(DataExtension.UpdateValues(fields, values), ItemsTSql.GetById(_iiid), ItemsColumns.TableName);

        if (variants.Length <= 0) return;
        dynamic contentJson = JsonConvert.DeserializeObject(variants.ToString());
        if (contentJson == null) return;
        int countRows = contentJson.Count;
        var x = 0;
        switch (countRows)
        {
            case 2:
            {
                var ar1 = contentJson[0].value.ToString();
                var ar2 = contentJson[1].value.ToString();
                foreach (var color in ar1.Split(','))
                {
                    foreach (var size in ar2.Split(','))
                    {
                        x += 1;
                        var variant = "[{\"label\":\"" + contentJson[0].label.ToString() + "\",\"value\":\"" + color + "\"},{\"label\":\"" + contentJson[1].label.ToString() + "\",\"value\":\"" + size + "\"}]";
                        Items.InsertProduct(_lang, _appVariant, txtCode.Text + "-" + (x + 1), txtTitle.Text, "", "", "", "", color.Trim(), "", size.Trim(), "", "", txtPriceOld.Text, txtPriceNew.Text, "", "", "", DateTime.Now.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture), "1", HdPromotionStartDate.Value, HdPromotionEndDate.Value, variant, _iiid, "0");
                    }
                }
                break;
            }
            default:
            {
                //var groupName = contentJson[0].label.ToString();
                var ar1 = contentJson[0].value.ToString();
                foreach (var color in ar1.Split(','))
                {
                    x += 1;
                    var variant = "[{\"label\":\"" + contentJson[0].label.ToString() + "\",\"value\":\"" + color + "\"}]";
                    Items.InsertProduct(_lang, _appVariant, txtCode.Text + "-" + (x + 1), txtTitle.Text, "", "", "", "", color.Trim(), "", "", "", "", txtPriceOld.Text, txtPriceNew.Text, "", "", "", DateTime.Now.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture), "1", HdPromotionStartDate.Value, HdPromotionEndDate.Value, variant, "0", _iiid);
                }
                break;
            }
        }
    }

    private void WholesaleProcessing()
    {
        //var splitChar = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        var wholesale = new StringBuilder();
        var rows = rptWholesale.Items.Count;
        for (var i = 0; i < rows; i++)
        {
            var item = rptWholesale.Items[i];
            if (item.Visible == false) continue;
            var groupName = item.FindControl("tbGroupWholesale") as TextBox;
            var className = item.FindControl("tbClassWholesale") as TextBox;
            if (className == null || className.Text == "") continue;
            if (groupName == null) continue;
            wholesale.Append("{");
            wholesale.Append("\"label\":\"" + groupName.Text + "\",\"value\":\"" + className.Text + "\"");
            wholesale.Append("}");
            if (i < rows - 1) wholesale.Append(",");
        }

        if (wholesale.Length > 0)
        {
            wholesale.Insert(0, "[");
            wholesale.Append("]");
        }
        else return;

        string[] fields = { ItemsColumns.ViParam };
        string[] values = { wholesale.ToString() };
        DatabaseExtension.Update(DataExtension.UpdateValues(fields, values), ItemsTSql.GetById(_iiid), ItemsColumns.TableName);

    }

    private string GetGenealogy(string id)
    {
        var dt = Groups.GetData("1", GroupsColumns.VgGenealogy, GroupsTSql.GetById(id), "");
        return dt.Rows[0][GroupsColumns.VgGenealogy].ToString();
    }

    private void ResetControls()
    {
        #region Reset các textbox, textbox nào có chứa css class là NotReset thì sẽ không bị reset
        foreach (Control control in form.Controls)
        {
            var textBox = control as TextBox;
            if (textBox != null) if (!textBox.CssClass.Contains("not-reset")) textBox.Text = "";
            var hiddenField = control as HiddenField;
            if (hiddenField != null) hiddenField.Value = "";
        }
        #endregion

        UploadImage.Reset();
        txtDate.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
        try
        {
            txtOrder.Text = (Convert.ToInt32(txtOrder.Text) + 1).ToString();
        }
        catch
        {
            // do nothing
        }

        txtTitle.Focus();
    }

    #region Phục vụ thuộc tính lọc

    /// <summary>
    /// Lấy danh sách các thuộc tính đã được add cho danh mục
    /// </summary>
    private void GetFilterProperties()
    {
        var condition = DataExtension.AndConditon(
            FilterTSql.GetByLang(_lang),
            FilterTSql.GetByStatus("1"),
            "ifLevel IN (1,3)",
            "charindex(','+cast(ifId as varchar(10))+',','" + GetListFilterProperties() + "') >0"
        );
        var dt = Filter.GetData("", "*", condition, "");

        rptParentFilter.DataSource = dt;
        rptParentFilter.DataBind();
    }
    /// <summary>
    /// Lấy danh igid các thuộc tính lọc đã được add vào danh mục (kết quả trả về dạng ,igid1,igid2,)
    /// </summary>
    /// <returns></returns>
    private string GetListFilterProperties()
    {
        var condition = GroupsTSql.GetById(ddlCategory.SelectedValue);
        var dt = Groups.GetData("", GroupsColumns.VgParam, condition, "");
        return dt.Rows.Count > 0 ? dt.Rows[0][GroupsColumns.VgParam].ToString() : "";
    }
    protected DataTable GetSubFilter(string id, string param, string allowMultipSelect)
    {
        if (param != allowMultipSelect) return new DataTable();
        var condition = DataExtension.AndConditon
        (
            FilterTSql.GetByParentId(id),
            FilterTSql.GetByStatus("1")
        );
        var dt = Filter.GetData("", FilterColumns.IfId + "," + FilterColumns.VfName, condition, "");
        return dt;
    }

    #endregion

    protected void btAddOption_Click(object sender, EventArgs e)
    {
        var rows = btAddOption.CommandArgument;
        if (rows.Equals("1"))
        {
            rptOption.Items[0].Visible = true;
            rptOption.Items[1].Visible = false;
            //rptOption.Items[2].Visible = false;
            btAddOption.Visible = true;
        }
        if (rows.Equals("2"))
        {
            rptOption.Items[0].Visible = true;
            rptOption.Items[1].Visible = true;
            //rptOption.Items[2].Visible = false;
            btAddOption.Visible = false;
        }
        //if (rows.Equals("3"))
        //{
        //    rptOption.Items[0].Visible = true;
        //    rptOption.Items[1].Visible = true;
        //    rptOption.Items[2].Visible = true;
        //    btAddOption.Visible = false;
        //}
        btAddOption.CommandArgument = (int.Parse(btAddOption.CommandArgument) + 1).ToString();
    }

    protected void btAddOptionWholesale_click(object sender, EventArgs e)
    {
        var rows = btAddOptionWholesale.CommandArgument;

        //if (rows.Equals("1"))
        //{
        //    //rptWholesale.Items[0].Visible = false;
        //    //rptWholesale.Items[1].Visible = false;
        //    var textNameLeft = rptWholesale.Items[0].FindControl("tbGroupWholesale") as TextBox;
        //    var textNameRight = rptWholesale.Items[0].FindControl("tbClassWholesale") as TextBox;

        //    //textName.Attributes["onkeyup"] = "HienThiNhieuGia(this,'giaSi')";
        //    if (textNameLeft != null) textNameLeft.Text = "Số lượng bán sỉ";
        //    if (textNameLeft != null) textNameLeft.ReadOnly = true;
        //    if (textNameRight != null) textNameRight.Text = "Giá bán sỉ";
        //    if (textNameRight != null) textNameRight.ReadOnly = true;


        //    btAddOptionWholesale.Visible = true;
        //}

        for (var i = 1; i < 100; i++)
        {
            if (!rows.Equals("" + i + "")) continue;
            rptWholesale.Items[i].Visible = true;
            rptWholesale.Items[i].Visible = true;
            var textNameRight = rptWholesale.Items[i].FindControl("tbClassWholesale") as TextBox;
            //if (textNameRight != null) textNameRight.Attributes["onkeyup"] = "" + i + "";
            //var textName = rptWholesale.Items[1].FindControl("tbGroupWholesale") as TextBox;
            //if (textName != null) textName.Text = "Số lượng sỉ";

            btAddOptionWholesale.Visible = true;
        }

        //btAddOptionWholesale.Visible = false;

        btAddOptionWholesale.CommandArgument = (int.Parse(btAddOptionWholesale.CommandArgument) + 1).ToString();
    }

   

    protected void rptOption_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        var c = e.CommandName;
        var p = e.CommandArgument.ToString();
        switch (c)
        {
            #region delete
            case "delete":
                rptOption.Items[int.Parse(p)].Visible = false;
                btAddOption.Visible = true;
                if (btAddOption.CommandArgument != "1") btAddOption.CommandArgument = (int.Parse(btAddOption.CommandArgument) - 1).ToString();
                break;
                #endregion
        }
    }

    protected void rptWholesale_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        var c = e.CommandName;
        var p = e.CommandArgument.ToString();
        switch (c)
        {
            #region delete
            case "delete":
                // Sửa đổi dòng sau
                // rptOption.Items[int.Parse(p)].Visible = false;
                rptWholesale.Items[int.Parse(p)].Visible = false;
                btAddOptionWholesale.Visible = true;
                if (btAddOptionWholesale.CommandArgument != "1") btAddOptionWholesale.CommandArgument = (int.Parse(btAddOptionWholesale.CommandArgument) - 1).ToString();
                break;
            #endregion
        }
        //rptWholesale.Items[0].Visible = true;
        ////rptWholesale.Items[1].Visible = false;
        //var textNameLeft = rptWholesale.Items[0].FindControl("tbGroupWholesale") as TextBox;
        //var textNameRight = rptWholesale.Items[0].FindControl("tbClassWholesale") as TextBox;

        //if (textNameLeft != null) textNameLeft.Text = "Số lượng bán sỉ";
        //if (textNameLeft != null) textNameLeft.ReadOnly = true;
        //if (textNameRight != null) textNameRight.Text = "Giá bán sỉ";
        //if (textNameRight != null) textNameRight.ReadOnly = true;

        //btAddOptionWholesale.Visible = true;
    }
}
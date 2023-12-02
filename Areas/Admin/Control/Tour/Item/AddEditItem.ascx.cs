using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;
using Developer.Config;
using Developer.Extension;
using Developer.Keyword;
using RevosJsc.TourControl;
using RevosJsc.AdminControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Tour_Item_AddEditItem : UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _app = CodeApplications.Tour;
    private readonly string _appG = CodeApplications.TourGroupItem;
    private readonly string _pic = FolderPic.Tour;

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
        UploadImage.Text = "Ảnh đại diện (300x200 px)";
        UploadImage.LayAnhTuNoiDung = true;
        //UploadImage.TaoAnhNho = false;
        //UploadImage.HanCheKichThuoc = false;

        UploadImage1.Control = _app;
        UploadImage1.Pic = _pic;
        UploadImage1.Text = "Ảnh bản đồ lịch trình (500x900 px)";
        UploadImage1.LayAnhTuNoiDung = false;
        UploadImage1.TaoAnhNho = false;
        UploadImage1.HanCheKichThuoc = false;

        UploadImage2.Control = _app;
        UploadImage2.Pic = _pic;
        UploadImage2.Text = "Banner đầu trang";
        UploadImage2.LayAnhTuNoiDung = false;
        UploadImage2.TaoAnhNho = false;
        UploadImage2.HanCheKichThuoc = false;

        UploadFile.Text = "Tệp đính kèm";
        UploadFile.Pic = _pic;

        #endregion

        #region Gán đường dẫn cho ckeditor

        foreach (Control control in form.Controls)
        {
            var ckEditorControl = control as CKEditorControl;
            if (ckEditorControl != null) ckEditorControl.FilebrowserImageBrowseUrl = UrlExtension.WebsiteUrl + "ckeditor/ckfinder/ckfinder.aspx?type=Images&path=" + _pic;
        }

        #endregion Gán đường dẫn cho ckeditor

        if (IsPostBack) return;
        OnOffControls();
        GetGroupInDdl();
        InitialControlsValue(_insert);
    }
    private void OnOffControls()
    {
        fsFilter.Visible = TourConfig.ShowFillterProperties;
    }
    private string LinkRedrect()
    {
        return LinkAdmin.GoAdminCategory(CodeApplications.Tour, TypePage.Item, ddlCategory.SelectedValue);
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
            ltrTitle.Text = TourKeyword.CapNhatBaiViet;
            btSubmit.Text = "Cập nhật";
            cbContiue.Visible = false;
            var condition = DataExtension.AndConditon(
                GroupsTSql.GetByApp(_app),
                ItemsTSql.GetById(_iiid)
                );
            var dt = GroupItems.GetAllData("1", "*", condition, "");

            txtTitle.Text = dt.Rows[0][ItemsColumns.ViTitle].ToString();
            HdTitle.Value = dt.Rows[0][ItemsColumns.ViTitle].ToString();
            var vImage = dt.Rows[0][ItemsColumns.ViImage].ToString();
            UploadImage.Load(StringExtension.LayChuoi(vImage, "", 1));
            UploadImage1.Load(StringExtension.LayChuoi(vImage, "", 2));
            UploadImage2.Load(StringExtension.LayChuoi(vImage, "", 5));
            UploadFile.Load(StringExtension.LayChuoi(vImage, "", 3), StringExtension.LayChuoi(vImage, "", 4));

            txtOrder.Text = dt.Rows[0][ItemsColumns.IiSortOrder].ToString();
            txtDesc.Text = dt.Rows[0][ItemsColumns.ViDescription].ToString();
            txtDate.Text = ((DateTime)dt.Rows[0][ItemsColumns.DiDateCreated]).ToString("yyyy-MM-ddTHH:mm");
            txt_content.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViContent].ToString(), "", 1);
            HdOldContent.Value = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViContent].ToString(), "", 1);
            txt_content2.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViContent].ToString(), "", 2);
            txt_content3.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViContent].ToString(), "", 3);
            txt_content4.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViContent].ToString(), "", 4);
            HdTotalView.Value = dt.Rows[0][ItemsColumns.IiTotalView].ToString();
            txtPlace.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParam].ToString(), "", 1);
            txtEmbed.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParam].ToString(), "", 2);
            txtPriceDescription.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParam].ToString(), "", 3);
            txtRoomDescription.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParam].ToString(), "", 4);
            HdOldHotel.Value = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParam].ToString(), "", 5);
            HdOldCruises.Value = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParam].ToString(), "", 6);
            HdOldVehicles.Value = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParam].ToString(), "", 7);

            #region Price

            txtPriceOld.Text = dt.Rows[0][ItemsColumns.FiPriceOld].ToString();
            ltrPriceOld.Text = NumberExtension.FormatNumber(dt.Rows[0][ItemsColumns.FiPriceOld].ToString(), false, "", "€");
            txtPriceNew.Text = dt.Rows[0][ItemsColumns.FiPriceNew].ToString();
            ltrPriceNew.Text = NumberExtension.FormatNumber(dt.Rows[0][ItemsColumns.FiPriceNew].ToString(), false, "", "€");

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
            ltrTitle.Text = TourKeyword.ThemMoiBaiViet;
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

        var fullContent = StringExtension.GhepChuoi("", contentDetail, HttpUtility.HtmlDecode(txt_content2.Text), HttpUtility.HtmlDecode(txt_content3.Text), HttpUtility.HtmlDecode(txt_content4.Text));

        var param = StringExtension.GhepChuoi("", txtPlace.Text, VideoExtension.GetVideoKey(txtEmbed.Text), txtPriceDescription.Text, txtRoomDescription.Text, HdOldHotel.Value, HdOldCruises.Value, HdOldVehicles.Value);

        #region Insert
        if (_insert)
        {
            var image = UploadImage.Save(false, contentDetail);
            var map = UploadImage1.Save(false, "");
            var image1 = UploadImage2.Save(false, "");
            var file = UploadFile.Save(false);
            var fullImage = StringExtension.GhepChuoi("", image, map, file, UploadFile.Link, image1);
            GroupItems.InsertItemGroupItem(_lang, _app, "", txtTitle.Text, txtDesc.Text, fullContent, fullImage, "", txtMetaTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, txtTag.Text, StringExtension.ReplateTitle(txtUrl.Text), txtPriceOld.Text, txtPriceNew.Text, param, "0", txtOrder.Text, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), status, ddlCategory.SelectedValue, DateTime.Now.ToString(), DateTime.Now.ToString());

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
            var map = UploadImage1.Save(true, "");
            var image1 = UploadImage2.Save(true, "");
            var file = UploadFile.Save(true);
            var fullImage = StringExtension.GhepChuoi("", image, map, file, UploadFile.Link, image1);
            GroupItems.UpdateItemGroupItem(_lang, _app, "", txtTitle.Text, txtDesc.Text, fullContent, fullImage, "", txtMetaTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, txtTag.Text, StringExtension.ReplateTitle(txtUrl.Text), txtPriceOld.Text, txtPriceNew.Text, param, HdTotalView.Value, txtOrder.Text, txtDate.Text.Replace("T", " "), DateTime.Now.ToString(), status, ddlCategory.SelectedValue, DateTime.Now.ToString(), DateTime.Now.ToString(), _iiid, HdOldIgId.Value);
            #region Logs
            var logAuthor = CookieExtension.GetCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount));
            var logCreateDate = DateTime.Now.ToString();
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
}
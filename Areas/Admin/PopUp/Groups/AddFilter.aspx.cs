using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_PopUp_Groups_AddFilter : System.Web.UI.Page
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private string _igid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["igid"] != null) _igid = QueryStringExtension.GetQueryString("igid");
        if (IsPostBack) return;
        GetListAdded();
        GetListNotAdded();

    }
    private string GetListFilterProperties()
    {
        var fields = DataExtension.GetListColumns(GroupsColumns.VgName, GroupsColumns.VgParam);
        var condition = GroupsTSql.GetById(_igid);
        var dt = Groups.GetData("", fields, condition, "");
        if (dt.Rows.Count <= 0) return "";
        ltrTitle.Text = "Quản lý thuộc tính lọc: " + dt.Rows[0][GroupsColumns.VgName];
        lt_cate_name.Text = "Danh mục: " + dt.Rows[0][GroupsColumns.VgName];
        return dt.Rows[0][GroupsColumns.VgParam].ToString();
    }
    /// <summary>
    /// Lấy danh sách các thuộc tính đã được add
    /// </summary>
    private void GetListAdded()
    {
        lstAdded.Items.Clear();
        var condition = DataExtension.AndConditon(
            FilterTSql.GetByLang(_lang),
            "ifStatus <> 2",
            "charindex(','+cast(" + FilterColumns.IfId + " as varchar(10))+',','" + GetListFilterProperties() + "') >0");
        var dt = Filter.GetAllData("*", condition, "");
        for (var i = 0; i < dt.Rows.Count; i++) lstAdded.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i][FilterColumns.IfLevel].ToString()) + dt.Rows[i][FilterColumns.VfName].ToString(), dt.Rows[i][FilterColumns.IfId].ToString()));
    }
    /// <summary>
    /// Lấy danh sách các thuộc tính chưa được add
    /// </summary>
    private void GetListNotAdded()
    {
        var listFilterProperties = GetListFilterProperties();
        lstNotAdded.Items.Clear();
        var condition = DataExtension.AndConditon(
            FilterTSql.GetByLang(_lang),
            "ifStatus <> 2",
            "charindex(','+cast(" + FilterColumns.IfId + " as varchar(10))+',','" + listFilterProperties + "') <1");

        var dt = Filter.GetAllData("*", condition, "");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var parentsId = "";
            var danhDauThuocChaHopLe = "";
            //Kiểm tra, chỉ hiện ra các mục mà cha nó chưa bị add
            parentsId = dt.Rows[i][FilterColumns.VfGenealogy].ToString();
            if (ParentInListFilterproperties(parentsId, listFilterProperties)) continue;
            danhDauThuocChaHopLe = ThuocTinhChaHopLe(dt.Rows[i][FilterColumns.IfId].ToString()) ? "+ " : ".";
            lstNotAdded.Items.Add(new ListItem(danhDauThuocChaHopLe + DropDownListExtension.FormatForDdl(dt.Rows[i][FilterColumns.IfLevel].ToString()) + dt.Rows[i][FilterColumns.VfName], dt.Rows[i][FilterColumns.IfId].ToString()));
        }
    }
    /// <summary>
    /// Kiểm tra xem Genealogy có chứa id đã add hay không
    /// </summary>
    /// <param name="parentsId"></param>
    /// <param name="listFilterProperties"></param>
    /// <returns></returns>
    private bool ParentInListFilterproperties(string parentsId, string listFilterProperties)
    {
        var result = false;
        char[] split = { ',' };
        foreach (var parentId in parentsId.Split(split))
            if (listFilterProperties.IndexOf("," + parentId + ",", StringComparison.Ordinal) >= 0) result = true;
        return result;
    }
    /// <summary>
    /// Kiểm tra xem thuộc tính được chọn có phải là thuộc tính cha hợp lệ không (hợp lệ: có thuộc tính con, thuộc tính con đó không có con)
    /// </summary>
    /// <param name="ifId">igid cần kiểm tra</param>
    /// <returns></returns>
    private bool ThuocTinhChaHopLe(string ifId)
    {
        var condition = FilterTSql.GetById(ifId);
        var dt = Filter.GetData("", FilterColumns.VfGenealogy, condition, "");
        var currentGenealogy = dt.Rows[0][FilterColumns.VfGenealogy].ToString();//Lấy danh sách cha của danh mục hiện tại

        //Lấy danh sách tất cả con của danh mục hiện tại
        condition = DataExtension.AndConditon(
            "charindex(','+cast(" + ifId + " as varchar(10))+','," + FilterColumns.VfGenealogy + ") >0",
            "ifStatus <> 2");
        dt = Filter.GetData("", FilterColumns.VfGenealogy, condition, "");
        var genealogy = "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            genealogy += dt.Rows[i][FilterColumns.VfGenealogy].ToString().Replace(currentGenealogy, "");
        }
        var soDauPhay = genealogy.Split(new string[] { "," }, StringSplitOptions.None).Length;
        return soDauPhay == dt.Rows.Count && dt.Rows.Count > 1;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        var listFilterProperties = GetListFilterProperties();
        if (listFilterProperties.Length < 1 || listFilterProperties == "0") listFilterProperties = ",";
        var iidArry = lstNotAdded.GetSelectedIndices();
        if (iidArry.Length <= 0) return;
        foreach (var i in iidArry)
        {
            if (ThuocTinhChaHopLe(lstNotAdded.Items[i].Value)) listFilterProperties += lstNotAdded.Items[i].Value + ",";
        }
        //Cap nhat lai danh sach thuoc tinh
        string[] fieldsEditStatus = { GroupsColumns.VgParam };
        string[] valuesEditStatus = { listFilterProperties };
        if (cbApplyToSubCategory.Checked) Groups.UpdateValues(DataExtension.UpdateValues(fieldsEditStatus, valuesEditStatus), "charindex(','+cast(" + _igid + " as varchar(10))+','," + GroupsColumns.VgGenealogy + ") >0");
        else Groups.UpdateValues(DataExtension.UpdateValues(fieldsEditStatus, valuesEditStatus), GroupsTSql.GetById(_igid));

        //Lấy lại danh sách các thuộc tính
        GetListAdded();
        GetListNotAdded();
    }

    protected void btnremove_Click(object sender, EventArgs e)
    {
        var listFilterProperties = GetListFilterProperties();
        var iidArry = lstAdded.GetSelectedIndices();
        if (iidArry.Length <= 0) return;
        foreach (var i in iidArry)
        {
            listFilterProperties = listFilterProperties.Replace("," + lstAdded.Items[i].Value + ",", ",");
        }
        //Cap nhat lai danh sach thuoc tinh
        string[] fieldsEditStatus = { GroupsColumns.VgParam };
        string[] valuesEditStatus = { listFilterProperties.Equals(",") ? "" : listFilterProperties };
        if (cbApplyToSubCategory.Checked) Groups.UpdateValues(DataExtension.UpdateValues(fieldsEditStatus, valuesEditStatus), "charindex(','+cast(" + _igid + " as varchar(10))+','," + GroupsColumns.VgGenealogy + ") >0");
        else Groups.UpdateValues(DataExtension.UpdateValues(fieldsEditStatus, valuesEditStatus), GroupsTSql.GetById(_igid));

        //Lấy lại danh sách các thuộc tính
        GetListAdded();
        GetListNotAdded();
    }
}
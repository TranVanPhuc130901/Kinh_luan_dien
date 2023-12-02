using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.LanguageControl;
using RevosJsc.TSql;

public partial class Areas_Admin_PopUp_GroupItems_AddItemsForGroup : Page
{
    private readonly string _lang = Cookie.GetLanguageValueAdmin();

    #region Params On This Page

    private string _igid = "";
    private string _genealogy = "";
    private string _control = "";
    private string _subapp = "";

    #endregion Params On This Page

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) return;
        if (Request.QueryString["subapp"] != null) _subapp = QueryStringExtension.GetQueryString("subapp");
        if (Request.QueryString["igid"] != null) _igid = QueryStringExtension.GetQueryString("igid");
        if (Request.QueryString["control"] != null) _control = QueryStringExtension.GetQueryString("control");
        if (Request.QueryString["genealogy"] != null) _genealogy = QueryStringExtension.GetQueryString("genealogy");

        if (IsPostBack || _igid.Equals("") || _control.Equals("") || _genealogy.Equals("")) return;
        GetDetailGroups();
        GetGroups();
        GetProductGroups(ddl_groups.SelectedValue);
    }

    //Lấy nhóm modul, đổ vào dropdownlist
    private void GetGroups()
    {
        var fields = DataExtension.GetListColumns(GroupsColumns.IgId, GroupsColumns.IgLevel, GroupsColumns.VgName);
        var dt = Groups.GetAllGroups(fields, DataExtension.AndConditon(GroupsTSql.GetByApp(_subapp.Length > 0 ? _subapp : _control), GroupsTSql.GetByLang(_lang), "igStatus <> 2"), "");
        ddl_groups.Items.Add(new ListItem("Chọn danh mục", string.Empty));
        for (var i = 0; i < dt.Rows.Count; i++)
            ddl_groups.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i][GroupsColumns.IgLevel].ToString()) + dt.Rows[i][GroupsColumns.VgName], dt.Rows[i][GroupsColumns.IgId].ToString()));
    }

    //Lấy thông tin của nhóm modul, in ra literal
    private void GetDetailGroups()
    {
        var dt = Groups.GetData("", GroupsColumns.VgName, GroupsTSql.GetById(_igid), "");
        if (dt.Rows.Count > 0) lt_cate_name.Text = "<div>" + dt.Rows[0][GroupsColumns.VgName] + "</div>";
    }

    private void GetProductGroups(string igidInDll)
    {
        var iidInListAdded = "";
        var fields = DataExtension.GetListColumns(GroupsColumns.VgName, "Items.iiId", ItemsColumns.ViTitle);
        var condition = GroupItemsTSql.GetItemsInGroupCondition(_igid, ItemsTSql.GetByApp(_subapp.Length > 0 ? _subapp : _control));
        var dtProductInCate = GroupItems.GetAllData("", fields, condition, GroupItemsColumns.DgiDateCreated);
        if (dtProductInCate.Rows.Count > 0)
            for (var i = 0; i < dtProductInCate.Rows.Count; i++)
            {
                lstadded.Items.Add(new ListItem(dtProductInCate.Rows[i][ItemsColumns.ViTitle].ToString(), dtProductInCate.Rows[i][ItemsColumns.IiId].ToString()));
                iidInListAdded += dtProductInCate.Rows[i][ItemsColumns.IiId].ToString();
                if (i != dtProductInCate.Rows.Count - 1) iidInListAdded += ",";
            }
        fields = DataExtension.GetListColumns("Items.iiId", ItemsColumns.ViTitle);
        var conditionItem = "";
        conditionItem = DataExtension.AndConditon(GroupsTSql.GetByLang(_lang), GroupsTSql.GetByApp(_subapp.Length > 0 ? _subapp : _control), ItemsTSql.GetByApp(_subapp.Length > 0 ? _subapp : _control));
        if (!iidInListAdded.Equals("")) conditionItem += "AND Items.iiId NOT IN (" + iidInListAdded + ")";
        if (!igidInDll.Equals("")) conditionItem += " AND " + GroupItemsTSql.GetItemsInGroupCondition(igidInDll, "") + " ";
        conditionItem += " AND igStatus <> 2 AND iiStatus <> 2 ";
        var dt = GroupItems.GetAllData("", fields, conditionItem, ItemsColumns.ViTitle);
        for (var i = 0; i < dt.Rows.Count; i++) lstnotadded.Items.Add(new ListItem(dt.Rows[i][ItemsColumns.ViTitle].ToString(), dt.Rows[i][ItemsColumns.IiId].ToString()));
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        var iidArry = lstnotadded.GetSelectedIndices();
        if (iidArry.Length <= 0) return;
        foreach (var i in iidArry)
            GroupItems.Insert(_igid, lstnotadded.Items[i].Value, _genealogy, DateTime.Now.ToString(), DateTime.Now.ToString());
        lstnotadded.Items.Clear();
        lstadded.Items.Clear();
        GetProductGroups(ddl_groups.SelectedValue);
    }

    protected void btnremove_Click(object sender, EventArgs e)
    {
        var iidArry = lstadded.GetSelectedIndices();
        if (iidArry.Length <= 0) return;
        var listIid = iidArry.Aggregate("", (current, i) => current + lstadded.Items[i].Value + ",");
        listIid = listIid.Substring(0, listIid.Length - 1);
        var condition = "igId = '" + _igid + "' AND iiId IN (" + listIid + ") ";
        GroupItems.Delete(condition);
        lstnotadded.Items.Clear();
        lstadded.Items.Clear();
        GetProductGroups(ddl_groups.SelectedValue);
    }

    protected void ddl_groups_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstnotadded.Items.Clear();
        lstadded.Items.Clear();
        GetProductGroups(ddl_groups.SelectedValue);
    }

}
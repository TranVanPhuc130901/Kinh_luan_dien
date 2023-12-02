using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_PopUp_Items_AddItemsToItem : Page
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    #region Params On This Page

    DataTable dtx = new DataTable();
    private string _iid = "";
    private string _app = "";
    //private string _control = "";

    #endregion Params On This Page

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) return;
        if (Request.QueryString["id"] != null) _iid = Request.QueryString["id"];
        if (Request.QueryString["app"] != null) _app = Request.QueryString["app"];

        dtx = RevosJsc.Database.Items.GetData("", "*", ItemsTSql.GetById(_iid), "");
        if (dtx.Rows.Count > 0) lt_cate_name.Text = "<div>" + dtx.Rows[0][ItemsColumns.ViTitle] + "</div>";
        if (IsPostBack) return;
        GetGroups();
        GetProductGroups(ddl_groups.SelectedValue);
    }

    //Lấy nhóm modul, đổ vào dropdownlist
    private void GetGroups()
    {
        var dt = Groups.GetAllGroups(" * ", DataExtension.AndConditon(GroupsTSql.GetByApp(_app), GroupsTSql.GetByLang(_lang), "IgStatus <> 2"), "");
        ddl_groups.Items.Add(new ListItem("Chọn danh mục", string.Empty));
        for (var i = 0; i < dt.Rows.Count; i++)
            ddl_groups.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i][GroupsColumns.IgLevel].ToString()) + dt.Rows[i][GroupsColumns.VgName], dt.Rows[i][GroupsColumns.IgId].ToString()));
    }

    private void GetProductGroups(string igIdInDll)
    {
        var iidInListAdded = "";
        var condition = ItemsTSql.GetById(_iid);
        var dtItem = RevosJsc.Database.Items.GetData("", "*", condition, "");
        if (dtItem.Rows.Count > 0)
        {
            if (_app == RevosJsc.HotelControl.CodeApplications.Hotel) iidInListAdded = StringExtension.LayChuoi(dtItem.Rows[0][ItemsColumns.ViParam].ToString(), "", 5);
            else if (_app == RevosJsc.CruisesControl.CodeApplications.Cruises) iidInListAdded = StringExtension.LayChuoi(dtItem.Rows[0][ItemsColumns.ViParam].ToString(), "", 6);
            else if (_app == RevosJsc.ProductControl.CodeApplications.Product) iidInListAdded = StringExtension.LayChuoi(dtItem.Rows[0][ItemsColumns.ViParam].ToString(), "", 7);
            if (iidInListAdded.Length > 0)
            {
                var dtItemAdded = RevosJsc.Database.Items.GetData("", "*", "iiId IN ("+ iidInListAdded.Substring(1) +")", "CHARINDEX(','+CAST(iiId AS VARCHAR)+',' , ','+'" + iidInListAdded.Substring(1) + "'+',')");
                if (dtItemAdded.Rows.Count > 0)
                {
                    for (var i = 0; i < dtItemAdded.Rows.Count; i++)
                    {
                        lstAdded.Items.Add(new ListItem(dtItemAdded.Rows[i][ItemsColumns.ViTitle].ToString(), dtItemAdded.Rows[i][ItemsColumns.IiId].ToString()));
                    }
                }
            }
        }

        var conditionItem = DataExtension.AndConditon(
            GroupsTSql.GetByLang(_lang), 
            GroupsTSql.GetByApp(_app), 
            ItemsTSql.GetByApp(_app)
            );
        if (!iidInListAdded.Equals("")) conditionItem += " and Items.iiId NOT IN(" + iidInListAdded.Substring(1) + ") ";

        if (!igIdInDll.Equals("")) conditionItem += " AND " + GroupItemsTSql.GetItemsInGroupCondition(igIdInDll, "") + " ";
        conditionItem += " AND IgStatus <> 2 AND IiStatus <> 2 ";
        var dt = GroupItems.GetAllData("", "*", conditionItem, ItemsColumns.ViTitle);
        for (var i = 0; i < dt.Rows.Count; i++) lstNotAdded.Items.Add(new ListItem(dt.Rows[i][ItemsColumns.ViTitle].ToString(), dt.Rows[i][ItemsColumns.IiId].ToString()));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (lstNotAdded.GetSelectedIndices().Length <= 0) return;
        var listId = "";
        // Lấy danh sách id đã add
        if (lstAdded.Items.Count > 0)
        {
            for (var x = 0; x < lstAdded.Items.Count; x++)
            {
                listId += "," + lstAdded.Items[x].Value;
            }
        }
        // Lấy danh sách id chuẩn bị add
        foreach (var i in lstNotAdded.GetSelectedIndices()) listId += "," + lstNotAdded.Items[i].Value;
        var param = dtx.Rows[0][ItemsColumns.ViParam].ToString();
        var newParam = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 1);
        newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 2);
        newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 3);
        newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 4);
        if (_app == RevosJsc.HotelControl.CodeApplications.Hotel) newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + listId;
        else newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 5);
        if (_app == RevosJsc.CruisesControl.CodeApplications.Cruises) newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + listId;
        else newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 6);
        if (_app == RevosJsc.ProductControl.CodeApplications.Product) newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + listId;
        else newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 7);
        newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        string[] fields = { ItemsColumns.ViParam };
        string[] values = { newParam.Replace("''", "\'").Replace("\'", "''") };
        RevosJsc.Database.Items.UpdateValues(DataExtension.UpdateValues(fields, values), ItemsTSql.GetById(_iid));
        lstNotAdded.Items.Clear();
        lstAdded.Items.Clear();
        GetProductGroups(ddl_groups.SelectedValue);
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (lstAdded.GetSelectedIndices().Length <= 0) return;
        var listId = "";
        for (var x = 0; x < lstAdded.Items.Count; x++)
        {
            if (!lstAdded.Items[x].Selected) listId += "," + lstAdded.Items[x].Value;
        }
        //RevosJsc.Database.Items.UpdateValues("viAuthor = N'" + listid + "'", ItemsTSql.GetById(_iid));
        var param = dtx.Rows[0][ItemsColumns.ViParam].ToString();
        var newParam = StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 1);
        newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 2);
        newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 3);
        newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 4);
        if (_app == RevosJsc.HotelControl.CodeApplications.Hotel) newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + listId;
        else newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 5);
        if (_app == RevosJsc.CruisesControl.CodeApplications.Cruises) newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + listId;
        else newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 6);
        if (_app == RevosJsc.ProductControl.CodeApplications.Product) newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + listId;
        else newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems + StringExtension.LayChuoi(param, "", 7);
        newParam += StringExtension.SpecialCharactersKeyword.ParamsSpilitItems;
        string[] fields = { ItemsColumns.ViParam };
        string[] values = { newParam.Replace("''", "\'").Replace("\'", "''") };
        RevosJsc.Database.Items.UpdateValues(DataExtension.UpdateValues(fields, values), ItemsTSql.GetById(_iid));
        lstNotAdded.Items.Clear();
        lstAdded.Items.Clear();
        GetProductGroups(ddl_groups.SelectedValue);
    }

    protected void ddl_groups_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstNotAdded.Items.Clear();
        lstAdded.Items.Clear();
        GetProductGroups(ddl_groups.SelectedValue);
    }

}
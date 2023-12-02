using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.NewsControl;
using RevosJsc.TSql;

public partial class Areas_Admin_PopUp_Items_AddNewsForItems : Page
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    private readonly string _app = CodeApplications.News;

    #region Params On This Page

    private string _iiid = "";
    private string _control = "";

    #endregion Params On This Page

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CookieExtension.CheckValidCookies(SecurityExtension.BuildPassword(UsersColumns.VuAccount))) return;
        if (Request.QueryString["iiid"] != null) _iiid = Request.QueryString["iiid"];
        if (Request.QueryString["control"] != null) _control = Request.QueryString["control"];

        if (IsPostBack) return;
        GetDetailGroups();
        GetGroups();
        GetProductGroups(ddl_groups.SelectedValue);
    }

    //Lấy nhóm modul, đổ vào dropdownlist
    private void GetGroups()
    {
        var dt = Groups.GetAllGroups(" * ", DataExtension.AndConditon(GroupsTSql.GetByApp(_app), GroupsTSql.GetByLang(_lang), "igStatus <> '2'"), "");
        ddl_groups.Items.Add(new ListItem("Chọn danh mục", string.Empty));
        for (var i = 0; i < dt.Rows.Count; i++)
            ddl_groups.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i][GroupsColumns.IgLevel].ToString()) + dt.Rows[i][GroupsColumns.VgName], dt.Rows[i][GroupsColumns.IgId].ToString()));
    }

    //Lấy thông tin của item in ra literal
    private void GetDetailGroups()
    {
        var condition = ItemsTSql.GetById(_iiid);
        var dt = RevosJsc.Database.Items.GetData("", "*", condition, "");
        if (dt.Rows.Count > 0) lt_cate_name.Text = "<div>" + dt.Rows[0][ItemsColumns.ViTitle] + "</div>";
    }

    private void GetProductGroups(string igidInDll)
    {
        var iidInListAdded = "";
        var condition = ItemsTSql.GetById(_iiid);
        var dtItem = RevosJsc.Database.Items.GetData("", "*", condition, "");
        if (dtItem.Rows.Count > 0)
        {
            iidInListAdded = dtItem.Rows[0][ItemsColumns.ViTag].ToString();
            if (iidInListAdded.Length > 0)
            {
                var dtItemAdded = RevosJsc.Database.Items.GetData("", "*", "iiId IN (" + iidInListAdded.Substring(1) + ")", "CHARINDEX(','+CAST(iiId AS VARCHAR)+',' , ','+'" + iidInListAdded.Substring(1) + "'+',')");
                if (dtItemAdded.Rows.Count > 0)
                {
                    for (var i = 0; i < dtItemAdded.Rows.Count; i++)
                    {
                        lstadded.Items.Add(new ListItem(dtItemAdded.Rows[i][ItemsColumns.ViTitle].ToString(), dtItemAdded.Rows[i][ItemsColumns.IiId].ToString()));
                    }
                }
            }
        }

        var conditionItem = DataExtension.AndConditon(
            GroupsTSql.GetByLang(_lang),
            GroupsTSql.GetByApp(_app),
            ItemsTSql.GetByApp(_app)
            );
        if (!iidInListAdded.Equals("")) conditionItem += " AND Items.iiId NOT IN(" + iidInListAdded.Substring(1) + ") ";

        if (!igidInDll.Equals("")) conditionItem += " AND " + GroupItemsTSql.GetItemsInGroupCondition(igidInDll, "") + " ";
        conditionItem += " AND igStatus <> 2 AND iiStatus <> 2 ";
        var dt = GroupItems.GetAllData("", "*", conditionItem, " iiSortOrder ASC, diDateCreated DESC ");
        for (var i = 0; i < dt.Rows.Count; i++) lstnotadded.Items.Add(new ListItem(dt.Rows[i][ItemsColumns.ViTitle].ToString(), dt.Rows[i][ItemsColumns.IiId].ToString()));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (lstnotadded.GetSelectedIndices().Length <= 0) return;
        var listid = "";
        // Lấy danh sách id đã add
        if (lstadded.Items.Count > 0)
        {
            for (var x = 0; x < lstadded.Items.Count; x++)
            {
                listid += "," + lstadded.Items[x].Value;
            }
        }
        // Lấy danh sách id chuẩn bị add
        foreach (var i in lstnotadded.GetSelectedIndices()) listid += "," + lstnotadded.Items[i].Value;
        RevosJsc.Database.Items.UpdateValues("viTag = N'" + listid + "'", ItemsTSql.GetById(_iiid));
        lstnotadded.Items.Clear();
        lstadded.Items.Clear();
        GetProductGroups(ddl_groups.SelectedValue);
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (lstadded.GetSelectedIndices().Length <= 0) return;
        var listId = "";
        for (var x = 0; x < lstadded.Items.Count; x++)
        {
            if (!lstadded.Items[x].Selected) listId += "," + lstadded.Items[x].Value;
        }
        RevosJsc.Database.Items.UpdateValues("viAuthor = N'" + listId + "'", ItemsTSql.GetById(_iiid));
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
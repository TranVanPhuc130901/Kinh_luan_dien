using System;
using System.Text;
using RevosJsc.ContactControl;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Contact_Category_RecycleCategory : System.Web.UI.UserControl
{
    private readonly string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueAdmin();
    protected readonly string Control = CodeApplications.Contact;
    private readonly string _app = CodeApplications.Contact;
    protected readonly string Pic = FolderPic.Contact;
    private readonly string _typePage = TypePage.RecycleCategory;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        ltrList.Text = GetCate();
    }
    private string GetCate()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            ContactsTSql.GetByLang(_lang),
            ContactsTSql.GetByStatus("2")
        );
        var dt = Contacts.GetData("", "*", condition, "VcGenealogy");

        if (dt.Rows.Count <= 0) return "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][ContactsColumns.IcId].ToString();
            var name = dt.Rows[i][ContactsColumns.VcName].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            s.Append("<div id='item-" + id + "' class='item' data-show='0'>");
            s.Append("<div class='inner'>");
            s.Append("<div class=\"cot1 text-center\"><input name='tick' class='cursor-pointer' id=\"cb_group-" + id + "\" type=\"checkbox\" onclick=\"checkAllCheckBox('cb_group-" + id + "',this)\" /></div>");

            s.Append("<div class=\"cot2\">");
            s.Append(dt.Rows[i][ContactsColumns.VcName]);
            s.Append("</div>");

            s.Append("<div class=\"cot3\">" + GetRoadGroup(id) + @"</div>");

            s.Append("<div class=\"cot4 text-center\">" + CountAllChildContact(id) + @"</div>");

            s.Append("<div class=\"cot5 btn-group-sm text-center\">");
            s.Append("<a href=\"javascript:RestoreContact('" + id + "','" + name + "')\"; title='Khôi phục' class='btn btn-success'><i class='gi gi-refresh'></i></a> ");
            s.Append("<a href=\"javascript:DeleteRecContact('" + id + "','" + name + "','"+ Pic + "');\" title='Xóa vĩnh viễn' class='btn btn-danger'><i class='fa fa-times'></i></a>");
            s.Append("</div>");
            s.Append("</div>");
            s.Append("</div>");

            //igidParent = id;
            //s.Append("<div id=\"" + id + "\"  style=\"display:none\">");
            //s.Append(GetSubCate(id));
            //s.Append("</div>");

        }
        return s.ToString();
    }

    public string CountAllChildContact(string id)
    {
        var dt = Contacts.GetData("", " icId ", " CHARINDEX('," + id + ",',vcGenealogy) > 0 ", "");
        return (dt.Rows.Count - 1).ToString();
    }
    private string GetRoadGroup(string igid)
    {
        var str = new StringBuilder();
        var fields = DataExtension.GetListColumns(ContactsColumns.IcParentId, ContactsColumns.VcName);
        var condition = DataExtension.AndConditon(ContactsTSql.GetById(igid));
        var current = "";
        var dt = Contacts.GetData("1", fields, condition, "");
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][ContactsColumns.IcParentId].ToString().Equals("0"))
            {
                str.Append(dt.Rows[0][ContactsColumns.VcName] + " / ");
            }
            else
            {
                str.Append(GetRoadGroup(dt.Rows[0][ContactsColumns.IcParentId].ToString()));
                current = dt.Rows[0][ContactsColumns.VcName].ToString();
            }
        }
        else
        {
            str.Append("/");
        }
        str.Append(current);
        return str.ToString();
    }
}
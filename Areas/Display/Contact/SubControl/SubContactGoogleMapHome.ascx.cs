using RevosJsc.Columns;
using RevosJsc.ContactControl;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Areas_Display_Contact_SubControl_SubContactGoogleMapHome : System.Web.UI.UserControl
{
    private string _lang = RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay();
    private string _app = CodeApplications.Contact;
    protected void Page_Load(object sender, EventArgs e)
    {
        ltrList.Text = GetAllLocations();
    }

    private string GetAllLocations()
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            ContactsTSql.GetByParentId("0"),
            ContactsTSql.GetByStatus("1"),
            ContactsTSql.GetByLang(_lang)
            );
        var dt = Contacts.GetData("", "*", condition, ContactsColumns.IcSortOrder);
        if (dt.Rows.Count < 1) return "";
        s.Append("<ul class='showroom-nav'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][ContactsColumns.IcId].ToString();
            var name = dt.Rows[i][ContactsColumns.VcName].ToString();
            s.Append("<li class='"+ (i == 0 ? "active" : "") +"'><a data-toggle='tab' href='#"+ id + "'>"+ name + "</a></li>");
        }
        s.Append("</ul>");
        s.Append("<div class='tab-content'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][ContactsColumns.IcId].ToString();
            var list = GetLv2(id);
            s.Append("<div id='"+ id + "' class='tab-pane fade in " + (i == 0 ? "active" : "") + "'>");
            s.Append(list);
            s.Append("</div>");
        }
        s.Append("</div>");
        return s.ToString();
    }

    private string GetLv2(string parent)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            ContactsTSql.GetByParentId(parent),
            ContactsTSql.GetByStatus("1"),
            ContactsTSql.GetByLang(_lang)
            );
        var dt = Contacts.GetData("", "*", condition, ContactsColumns.IcSortOrder);
        if (dt.Rows.Count < 1) return "";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][ContactsColumns.IcId].ToString();
            var name = dt.Rows[i][ContactsColumns.VcName].ToString();
            var count = 0;
            var list = GetLv3(id, ref count);
            if (list.Length < 1) continue;
            s.Append("<div class='showroom-item " + (i == 0 ? "active" : "") + "'>");
            s.Append("<div class='showroom-title'>"+ name + " ( "+ count +" )</div>");
            s.Append(list);
            s.Append("</div>");
        }

        return s.ToString();
    }

    private string GetLv3(string parent, ref int count)
    {
        var s = new StringBuilder();
        var condition = DataExtension.AndConditon(
            ContactsTSql.GetByParentId(parent),
            ContactsTSql.GetByStatus("1"),
            ContactsTSql.GetByLang(_lang)
            );
        var dt = Contacts.GetData("", "*", condition, ContactsColumns.IcSortOrder);
        if (dt.Rows.Count < 1) return "";
        count = dt.Rows.Count;
        s.Append("<div class='showroom-content'>");
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][ContactsColumns.IcId].ToString();
            var name = dt.Rows[i][ContactsColumns.VcName].ToString();
            var address = dt.Rows[i][ContactsColumns.VcAddress].ToString();
            var phone = dt.Rows[i][ContactsColumns.VcPhone].ToString();
            s.Append(@"
<div class='showroom-address'>
    <div class='showroom-address-name'><strong>"+ name + @"</strong></div>
    <div class='showroom-address-address'>
        <svg width='14' height='14' viewBox='0 0 14 14' stroke='#D3D6DA' fill='none' xmlns='http://www.w3.org/2000/svg'>
            <path d='M6.99969 7.83434C8.00485 7.83434 8.81969 7.01949 8.81969 6.01434C8.81969 5.00918 8.00485 4.19434 6.99969 4.19434C5.99453 4.19434 5.17969 5.00918 5.17969 6.01434C5.17969 7.01949 5.99453 7.83434 6.99969 7.83434Z' />
            <path d='M2.11231 4.95234C3.26148 -0.099328 10.7456 -0.0934946 11.889 4.95817C12.5598 7.92151 10.7165 10.4298 9.10064 11.9815C7.92814 13.1132 6.07314 13.1132 4.89481 11.9815C3.28481 10.4298 1.44148 7.91567 2.11231 4.95234Z' />
        </svg>
        "+ address + @"
    </div>
    <div class='showroom-address-phone'>
        <svg width='14' height='14' viewBox='0 0 14 14' stroke='#D3D6DA' fill='none' xmlns='http://www.w3.org/2000/svg'>
            <path d='M12.8152 10.6923C12.8152 10.9023 12.7685 11.1182 12.6693 11.3282C12.5702 11.5382 12.4418 11.7365 12.2727 11.9232C11.9868 12.2382 11.6718 12.4657 11.316 12.6115C10.966 12.7573 10.5868 12.8332 10.1785 12.8332C9.58352 12.8332 8.94768 12.6932 8.27685 12.4073C7.60602 12.1215 6.93518 11.7365 6.27018 11.2523C5.59935 10.7623 4.96352 10.2198 4.35685 9.619C3.75602 9.01234 3.21352 8.3765 2.72935 7.7115C2.25102 7.0465 1.86602 6.3815 1.58602 5.72234C1.30602 5.05734 1.16602 4.4215 1.16602 3.81484C1.16602 3.41817 1.23602 3.039 1.37602 2.689C1.51602 2.33317 1.73768 2.0065 2.04685 1.71484C2.42018 1.34734 2.82852 1.1665 3.26018 1.1665C3.42352 1.1665 3.58685 1.2015 3.73268 1.2715C3.88435 1.3415 4.01852 1.4465 4.12352 1.59817L5.47685 3.50567C5.58185 3.6515 5.65768 3.78567 5.71018 3.914C5.76268 4.0365 5.79185 4.159 5.79185 4.26984C5.79185 4.40984 5.75102 4.54984 5.66935 4.684C5.59352 4.81817 5.48268 4.95817 5.34268 5.09817L4.89935 5.559C4.83518 5.62317 4.80602 5.699 4.80602 5.79234C4.80602 5.839 4.81185 5.87984 4.82352 5.9265C4.84102 5.97317 4.85852 6.00817 4.87018 6.04317C4.97518 6.23567 5.15602 6.4865 5.41268 6.78984C5.67518 7.09317 5.95518 7.40234 6.25852 7.7115C6.57352 8.02067 6.87685 8.3065 7.18602 8.569C7.48935 8.82567 7.74018 9.00067 7.93852 9.10567C7.96768 9.11734 8.00268 9.13484 8.04352 9.15234C8.09018 9.16984 8.13685 9.17567 8.18935 9.17567C8.28852 9.17567 8.36435 9.14067 8.42852 9.0765L8.87185 8.639C9.01768 8.49317 9.15768 8.38234 9.29185 8.31234C9.42602 8.23067 9.56018 8.18984 9.70601 8.18984C9.81685 8.18984 9.93352 8.21317 10.0618 8.26567C10.1902 8.31817 10.3243 8.394 10.4702 8.49317L12.401 9.864C12.5527 9.969 12.6577 10.0915 12.7218 10.2373C12.7802 10.3832 12.8152 10.529 12.8152 10.6923Z' stroke-miterlimit='10' />
        </svg>
        "+ phone +@"
    </div>
    <a href='javascript:void(0);' class='showroom-address-link'>Chi tiết
		<svg width='12' height='12' viewBox='0 0 12 12' fill='#EE7D22' xmlns='http://www.w3.org/2000/svg'>
            <path d='M9.0075 5.25H0V6.75H9.0075V9L12 6L9.0075 3V5.25Z' />
        </svg>
    </a>
</div>");
        }
        s.Append("</div>");

        return s.ToString();
    }
}
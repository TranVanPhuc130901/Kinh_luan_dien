using System;
using System.Web.UI;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Home_LoadControl : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected string CountBills()
    {
        var dt = Bills.GetData("", BillsColumns.IbId, "", "");
        return NumberExtension.FormatNumber(dt.Rows.Count.ToString());
    }

    protected string CountNews()
    {
        var dt = Items.GetData("", ItemsColumns.IiId, ItemsTSql.GetByApp(RevosJsc.NewsControl.CodeApplications.News), "");
        //var s = 0;
        //for (var i = 0; i < dt.Rows.Count; i++)
        //{
        //    s += s + int.Parse(dt.Rows[i][ItemsColumns.IiTotalView].ToString());
        //}
        return NumberExtension.FormatNumber(dt.Rows.Count.ToString());
    }
}
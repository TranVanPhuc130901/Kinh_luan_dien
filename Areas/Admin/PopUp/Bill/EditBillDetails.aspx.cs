using System;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_PopUp_Bill_EditBillDetails : System.Web.UI.Page
{
    private string billId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Các querystring

        if (Request.QueryString["billId"] != null) billId = QueryStringExtension.GetQueryString("billId");

        #endregion Các querystring
        if (IsPostBack) return;
        GetList();
    }
    private void GetList()
    {
        // Lấy thông tin đơn hàng
        var dt = Bills.GetData("1", "*", BillsTSql.GetById(billId), "");
        if (dt.Rows.Count < 1) return;
        ltrTitle.Text = "Cập nhật đơn hàng - " + dt.Rows[0]["vbCode"];
        txtLink.Text = dt.Rows[0]["vbGender"].ToString();
    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        Bills.UpdateValues("vbGender = N'"+ txtLink.Text + "'", BillsTSql.GetById(billId));
    }
}
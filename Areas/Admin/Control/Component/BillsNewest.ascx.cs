using System;
using System.Text;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.ProductControl;

public partial class Areas_Admin_Control_Component_BillsNewest : System.Web.UI.UserControl
{
    private readonly string _control = CodeApplications.Product;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        ltrList.Text = GetListBills();
        if (ltrList.Text == "") Visible = false;
    }
    private string LinkBillDetails(string id)
    {
        return "/Areas/Admin/PopUp/Bill/ShowBillDetails.aspx?control=" + _control + "&iiid=" + id;
    }
    private string GetListBills()
    {
        var s = new StringBuilder();
        var dt = Bills.GetData("10", "*", "", BillsColumns.DbDateCreated + " DESC");
        #region Lấy ra danh sách bài viết

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i][BillsColumns.IbId].ToString();
            var status = dt.Rows[i][BillsColumns.IbStatus].ToString();
            var itemTitle = dt.Rows[i][BillsColumns.VbCode].ToString();
            var comment = dt.Rows[i]["vbComment"].ToString();
            var payment = StringExtension.LayChuoi(dt.Rows[i]["vbParam"].ToString(), "", 1);
            var address = dt.Rows[i]["vbAddress"].ToString();
            s.Append("<div id='item" + id + "' class='item inner'>");
            s.Append("<div class=\"cot1 text-center\"><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + "' /></div>");
            s.Append("<div class=\"cot2\">");
            s.Append("<b>Mã đơn hàng</b>: " + itemTitle + "<br/>");
            s.Append("<b>Họ tên</b>: " + dt.Rows[i][BillsColumns.VbName].ToString().Replace("-/-", " ") + "<br/>");
            s.Append("<b>Email</b>: " + dt.Rows[i][BillsColumns.VbEmail] + "<br/>");
            s.Append("<b>Điện thoại</b>: " + dt.Rows[i][BillsColumns.VbPhone] + "<br/>");
            s.Append("<b>Địa chỉ</b>: " + StringExtension.LayChuoi(address, "", 1) + " - " + StringExtension.LayChuoi(address, "", 2) + " - " + StringExtension.LayChuoi(address, "", 3) + "<br/>");
            s.Append("</div>");
            s.Append("<div class=\"cot2\">");
            s.Append("<b></b> " + payment + "<br/>");
            s.Append(comment.Length > 0 ? comment.Replace("\n", "<br/>") : "");
            s.Append("</div>");
            s.Append("<div class=\"cot3 text-center\">" + ((DateTime)dt.Rows[i][BillsColumns.DbDateCreated]).ToString("dd-MM-yyyy HH:mm") + "</div>");
            s.Append("<div class=\"cot6 text-center\"><label class='switch switch-primary'><input onchange='OnOffBill(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></div>");
            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"javascript:NewWindow_('" + LinkBillDetails(id) + "','ImageList','800','500','yes','yes');\" title=\"Xem chi tiết\" class=\"btn btn-primary\"><i class=\"fa fa-info-circle\"></i></a> ");
            s.Append("<a href=\"javascript:DeleteBill('" + id + "','" + itemTitle + "')\" title=\"Xóa\" class=\"btn btn-danger\"><i class=\"fa fa-times\"></i></a>");
            s.Append("</div>");
            s.Append("</div>");
        }
        ltrList.Text = s.ToString();

        #endregion
        return s.ToString();
    }
}
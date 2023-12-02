using System;
using System.Text;
using RevosJsc.Database;

public partial class Areas_Admin_Control_Component_ContactNewest : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        ltrList.Text = GetListContact();
        if (ltrList.Text == "") Visible = false;
    }
    private string LinkContactDetail(string icdId, string subApp)
    {
        return "/Areas/Admin/PopUp/Contact/ShowContactDetail.aspx?control=Contact&subapp=" + subApp + "&id=" + icdId;
    }
    private string GetListContact()
    {
        var s = new StringBuilder();
        var dt = ContactDetails.GetData("10", "*", "", "dcdDateCreated DESC");
        #region Lấy ra danh sách bài viết

        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var id = dt.Rows[i]["icdId"].ToString();
            var itemTitle = dt.Rows[i]["vcdName"].ToString().Replace("\n", "").Replace("'", "’").Replace("\"", "’");
            var status = dt.Rows[i]["icdStatus"].ToString();
            var param = dt.Rows[i]["vcdParam"].ToString();
            var type = dt.Rows[i]["vcdSubject"].ToString();
            s.Append("<div id='item" + id + "' class='item inner'>");
            s.Append("<div class=\"cot1 text-center\"><input class='cursor-pointer' id='cb-" + id + "' name='tick' type='checkbox' value='" + id + "' /></div>");
            s.Append("<div class=\"cot2\">");
            s.Append("<b>Họ tên</b>: " + dt.Rows[i]["vcdName"] + "<br/>");
            s.Append("<b>Email</b>: " + dt.Rows[i]["vcdEmail"] + "<br/>");
            s.Append("<b>Điện thoại</b>: " + dt.Rows[i]["vcdPhone"] + "<br/>");
            s.Append("<b>Địa chỉ</b>: " + dt.Rows[i]["vcdAddress"] + "<br/>");
            s.Append("</div>");
            s.Append("<div class=\"cot2\">");
            s.Append("<b>" + GetSubjectBySubject(type) + "</b><br/>");
            s.Append(dt.Rows[i]["vcdContent"].ToString().Replace("\n", "<br/>"));
            s.Append("</div>");
            s.Append("<div class=\"cot3 text-center\">" + ((DateTime)dt.Rows[i]["dcdDateCreated"]).ToString("dd-MM-yyyy HH:mm") + "</div>");
            s.Append("<div class=\"cot6 text-center\"><label class='switch switch-primary'><input onchange='OnOffContactDetail(" + id + ")' type='checkbox' " + (status.Equals("1") ? "checked" : "") + "><span></span></label></div>");
            s.Append("<div class=\"cot7 btn-group-sm text-center\">");
            s.Append("<a href=\"javascript:NewWindow_('" + LinkContactDetail(id, type) + "','ImageList','800','500','yes','yes');\" title='Xem chi tiết' class='btn btn-primary'><i class='fa fa-info-circle'></i></a> ");
            s.Append("<a href=\"javascript:DeleteContactDetail('" + id + "','" + itemTitle + "')\" title=\"Xóa\" class=\"btn btn-danger\"><i class=\"fa fa-times\"></i></a>");
            s.Append("</div>");
            s.Append("</div>");
        }
        ltrList.Text = s.ToString();

        #endregion
        return s.ToString();
    }
    private string GetSubjectBySubject(string subject)
    {
        if (subject.Equals("Appointment")) return "Yêu cầu tư vấn sản phẩm";
        if (subject.Equals("BookingTour")) return "Đặt tour tùy chọn";
        if (subject.Equals("RequestCallBack")) return "Yêu cầu gọi lại";
        if (subject.Equals("RequestCallBack2")) return "Yêu cầu tư vấn tour";
        return "Liên hệ - Góp ý";
    }
}
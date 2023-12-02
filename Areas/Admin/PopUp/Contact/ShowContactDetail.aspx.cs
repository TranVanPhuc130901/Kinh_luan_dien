using System;
using Developer.Extension;
using RevosJsc.ContactControl;
using RevosJsc.Database;
using RevosJsc.Extension;

public partial class Areas_Admin_PopUp_Contact_ShowContactDetail : System.Web.UI.Page
{
    private string _icdId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Các querystring

        if (Request.QueryString["id"] != null) _icdId = QueryStringExtension.GetQueryString("id");
        #endregion Các querystring

        ltrList.Text = GetList();

    }

    private string GetList()
    {
        // Lấy thông tin khách hàng
        var s = "";
        var dt = ContactDetails.GetData("1", "*", "icdId = "+ _icdId +"", "");
        if (dt.Rows.Count < 1) return s;
        var param = dt.Rows[0]["vcdParam"].ToString();
        var type = dt.Rows[0]["vcdSubject"].ToString();
        if (type.Equals("BookingTour"))
        {
            var opt1 = StringExtension.LayChuoi(param, "", 1);
            var participants = "";
            if (opt1.Equals("En Famille") || opt1.Equals("Avec des amis"))
            {
                participants = @"
            <tr>
                <td>Adultes (>12 ans)</td>
                <td>" + StringExtension.LayChuoi(param, "", 9) + @"</td>
            </tr>
            <tr>
                <td>Enfants (6 - 12 ans)</td>
                <td>" + StringExtension.LayChuoi(param, "", 10) + @"</td>
            </tr>
            <tr>
                <td>Enfants (2 - 5 ans)</td>
                <td>" + StringExtension.LayChuoi(param, "", 11) + @"</td>
            </tr>
            <tr>
                <td>Bébé (< 2 ans)</td>
                <td>" + StringExtension.LayChuoi(param, "", 12) + @"</td>
            </tr>
";

            }
            else if (opt1.Equals("Autres"))
            {
                participants = @"
<tr>
    <td>Précisez</td>
    <td>" + StringExtension.LayChuoi(param, "", 13) + @"</td>
</tr>
";
            }
            var opt2 = StringExtension.LayChuoi(param, "", 2);
            var _duration = "";
            if (opt2.Equals("Oui"))
            {
                _duration = @"
<tr>
    <td>Combien de jour(s)/semaine(s)</td>
    <td>" + StringExtension.LayChuoi(param, "", 14) + @"</td>
</tr>
";
            }
            var opt3 = StringExtension.LayChuoi(param, "", 3);
            var _date = "";
            if (opt3.Equals("Oui"))
            {
                _date = @"
<tr>
    <td>Date d’arrivée</td>
    <td>" + StringExtension.LayChuoi(param, "", 15) + @"</td>
</tr>
<tr>
    <td>Date de départ</td>
    <td>" + StringExtension.LayChuoi(param, "", 16) + @"</td>
</tr>
";
            }
            s += @"
<div style='text-align:center;font-size:24px;font-weight:bold;margin-bottom:10px'>CHI TIẾT LIÊN HỆ ĐẶT TOUR TÙY CHỌN</div>
<div style='text-align:center;font-size:12px;color:#454545;margin-bottom:20px;'>Ngày gửi: " + ((DateTime)dt.Rows[0]["dcdDateCreated"]).ToString("dd/MM/yyyy HH:mm") + @"</div>
<table>
    <tbody>
        <tr><td>Nom & prénom</td><td>" + dt.Rows[0]["vcdName"] + @"</td></tr>"+ participants + @"
        <tr><td>Nationalité</td><td>" + dt.Rows[0]["vcdAddress"] + @"</td></tr>"+ _duration + @"
        <tr><td>Numéro de téléphone</td><td>" + dt.Rows[0]["vcdPhone"] + @"</td></tr>"+ _date + @"
        <tr><td>Email</td><td>" + dt.Rows[0]["vcdEmail"] + @"</td></tr>
        <tr><td>Voyagez-vous?</td><td>" + StringExtension.LayChuoi(param, "", 1) + @"</td></tr>
        <tr><td>Connaissez-vous la durée de votre voyage?</td><td>" + StringExtension.LayChuoi(param, "", 2) + @"</td></tr>
        <tr><td>Savez-vous à quelle période ou date précise?</td><td>" + StringExtension.LayChuoi(param, "", 3) + @"</td></tr>
        <tr><td>Quel(s) pays souhaitez-vous visiter?</td><td>" + StringExtension.LayChuoi(param, "", 4) + @"</td></tr>
        <tr><td>Quels sont vos centres d'intérêt?</td><td>" + StringExtension.LayChuoi(param, "", 5) + @"</td></tr>
        <tr><td>Quel type d'hébergement souhaitez - vous?</td><td>" + StringExtension.LayChuoi(param, "", 6) + @"</td></tr>
        <tr><td>Quel est votre budget prévu pour le voyage (hors vol international depuis chez vous)?</td><td>" + StringExtension.LayChuoi(param, "", 7) + @"</td></tr>
        <tr><td>Comment nous avez-vous connu?</td><td>" + StringExtension.LayChuoi(param, "", 8) + @"</td></tr>
        <tr><td>Votre message</td><td>" + dt.Rows[0]["vcdContent"].ToString().Replace("\n", "</br>") + @"</td></tr>
        <tr><td>Source</td><td>" + StringExtension.LayChuoi(param, "", 17) + @"</td></tr>
        <tr><td>utm_medium</td><td>" + StringExtension.LayChuoi(param, "", 19) + @"</td></tr>
        <tr><td>utm_campaign</td><td>" + StringExtension.LayChuoi(param, "", 20) + @"</td></tr>
        <tr><td>Link</td><td>" + StringExtension.LayChuoi(param, "", 21) + @"</td></tr>
    </tbody>
</table>";
        }
        else if (type.Equals("RequestCallBack"))
        {
            s += @"
<div style='text-align:center;font-size:24px;font-weight:bold;margin-bottom:10px'>CHI TIẾT LIÊN HỆ YÊU CẦU GỌI LẠI</div>
<div style='text-align:center;font-size:12px;color:#454545;margin-bottom:20px;'>Ngày gửi: " + ((DateTime)dt.Rows[0]["dcdDateCreated"]).ToString("dd/MM/yyyy HH:mm") + @"</div>
<table>
    <tbody>
        <tr><td>Nom & prénom</td><td>" + dt.Rows[0]["vcdName"] + @"</td></tr>
        <tr><td>Nationalité</td><td>" + dt.Rows[0]["vcdAddress"] + @"</td></tr>
        <tr><td>Numéro de téléphone</td><td>" + dt.Rows[0]["vcdPhone"] + @"</td></tr>
        <tr><td>Email</td><td>" + dt.Rows[0]["vcdEmail"] + @"</td></tr>
        <tr><td>Adultes (>12 ans)</td><td>" + StringExtension.LayChuoi(param, "", 1) + @"</td></tr>
        <tr><td>Enfants (6 - 12 ans)</td><td>" + StringExtension.LayChuoi(param, "", 2) + @"</td></tr>
        <tr><td>Enfants (2 - 5 ans)</td><td>" + StringExtension.LayChuoi(param, "", 3) + @"</td></tr>
        <tr><td>Bébé (< 2 ans)</td><td>" + StringExtension.LayChuoi(param, "", 4) + @"</td></tr>
        <tr><td>Arrival date</td><td>" + StringExtension.LayChuoi(param, "", 5) + @"</td></tr>
        <tr><td>Departure date</td><td>" + StringExtension.LayChuoi(param, "", 7) + @"</td></tr>
        <tr><td>Comment nous avez-vous connu?</td><td>" + StringExtension.LayChuoi(param, "", 6) + @"</td></tr>
        <tr><td>Votre message</td><td>" + dt.Rows[0]["vcdContent"].ToString().Replace("\n", "</br>") + @"</td></tr>
        <tr><td>Source</td><td>" + StringExtension.LayChuoi(param, "", 8) + @"</td></tr>
        <tr><td>utm_medium</td><td>" + StringExtension.LayChuoi(param, "", 10) + @"</td></tr>
        <tr><td>utm_campaign</td><td>" + StringExtension.LayChuoi(param, "", 11) + @"</td></tr>
        <tr><td>Link</td><td>" + StringExtension.LayChuoi(param, "", 12) + @"</td></tr>
    </tbody>
</table>";
        }
        else if (type.Equals("RequestCallBack2"))
        {
            s += @"
<div style='text-align:center;font-size:24px;font-weight:bold;margin-bottom:10px'>CHI TIẾT LIÊN HỆ YÊU CẦU TƯ VẤN TOUR</div>
<div style='text-align:center;font-size:12px;color:#454545;margin-bottom:20px;'>Ngày gửi: " + ((DateTime)dt.Rows[0]["dcdDateCreated"]).ToString("dd/MM/yyyy HH:mm") + @"</div>
<table>
    <tbody>
        <tr><td>Nom & prénom</td><td>" + dt.Rows[0]["vcdName"] + @"</td></tr>
        <tr><td>Nationalité</td><td>" + dt.Rows[0]["vcdAddress"] + @"</td></tr>
        <tr><td>Numéro de téléphone</td><td>" + dt.Rows[0]["vcdPhone"] + @"</td></tr>
        <tr><td>Email</td><td>" + dt.Rows[0]["vcdEmail"] + @"</td></tr>
        <tr><td>Comment nous avez-vous connu?</td><td>" + StringExtension.LayChuoi(param, "", 1) + @"</td></tr>
        <tr><td>De quels renseignements/conseils avez-vous besoin?</td><td>" + StringExtension.LayChuoi(dt.Rows[0]["vcdContent"].ToString(), "", 1).Replace("\n", "</br>") + @"</td></tr>
        <tr><td>Si vous avez déjà une idée du voyage, décrivez-nous brièvement votre projet!</td><td>" + StringExtension.LayChuoi(dt.Rows[0]["vcdContent"].ToString(), "", 2).Replace("\n", "</br>") + @"</td></tr>
        <tr><td>Source</td><td>" + StringExtension.LayChuoi(param, "", 2) + @"</td></tr>
        <tr><td>utm_medium</td><td>" + StringExtension.LayChuoi(param, "", 4) + @"</td></tr>
        <tr><td>utm_campaign</td><td>" + StringExtension.LayChuoi(param, "", 5) + @"</td></tr>
    </tbody>
</table>";
        }
        else if (type.Equals("Contact"))
        {
            s += @"
<div style='text-align:center;font-size:24px;font-weight:bold;margin-bottom:10px'>CHI TIẾT LIÊN HỆ</div>
<div style='text-align:center;font-size:12px;color:#454545;margin-bottom:20px;'>Ngày gửi: " + ((DateTime)dt.Rows[0]["dcdDateCreated"]).ToString("dd/MM/yyyy HH:mm") + @"</div>
<table>
    <tbody>
        <tr><td>Họ tên</td><td>" + dt.Rows[0]["vcdName"] + @"</td></tr>
        <tr><td>Email</td><td>" + dt.Rows[0]["vcdEmail"] + @"</td></tr>
        <tr><td>Điện thoại</td><td>" + dt.Rows[0]["vcdPhone"] + @"</td></tr>
        <tr><td>Nội dung</td><td>" + dt.Rows[0]["vcdContent"].ToString().Replace("\n", "</br>") + @"</td></tr>
        <tr><td>Source</td><td>" + StringExtension.LayChuoi(param, "", 1) + @"</td></tr>
        <tr><td>utm_source</td><td>" + StringExtension.LayChuoi(param, "", 2) + @"</td></tr>
        <tr><td>utm_medium</td><td>" + StringExtension.LayChuoi(param, "", 3) + @"</td></tr>
        <tr><td>utm_campaign</td><td>" + StringExtension.LayChuoi(param, "", 4) + @"</td></tr>
    </tbody>
</table>";
        }
        else if (type.Equals("RequestQuote"))
        {
            var file = StringExtension.LayChuoi(param, "", 5);
            var atFile = file.Length > 0 ? "<a href='" + UrlExtension.WebsiteUrl + FolderPic.Contact + "/" + file + @"'>" + UrlExtension.WebsiteUrl + FolderPic.Contact + "/" + file + @"</a>" : "";
            s += @"
<div style='text-align:center;font-size:24px;font-weight:bold;margin-bottom:10px'>CHI TIẾT LIÊN HỆ</div>
<div style='text-align:center;font-size:12px;color:#454545;margin-bottom:20px;'>Ngày gửi: " + ((DateTime)dt.Rows[0]["dcdDateCreated"]).ToString("dd/MM/yyyy HH:mm") + @"</div>
<table>
    <tbody>
        <tr><td>Họ tên</td><td>" + dt.Rows[0]["vcdName"] + @"</td></tr>
        <tr><td>Địa chỉ</td><td>" + dt.Rows[0]["vcdAddress"] + @"</td></tr>
        <tr><td>Email</td><td>" + dt.Rows[0]["vcdEmail"] + @"</td></tr>
        <tr><td>Điện thoại</td><td>" + dt.Rows[0]["vcdPhone"] + @"</td></tr>
        <tr><td>Nội dung</td><td>" + dt.Rows[0]["vcdContent"].ToString().Replace("\n", "</br>") + @"</td></tr>
        <tr><td>Tệp đính kèm</td><td>" + (file.Length > 0 ? atFile : "") + @"</td></tr>
        <tr><td>Source</td><td>" + StringExtension.LayChuoi(param, "", 1) + @"</td></tr>
        <tr><td>utm_source</td><td>" + StringExtension.LayChuoi(param, "", 2) + @"</td></tr>
        <tr><td>utm_medium</td><td>" + StringExtension.LayChuoi(param, "", 3) + @"</td></tr>
        <tr><td>utm_campaign</td><td>" + StringExtension.LayChuoi(param, "", 4) + @"</td></tr>
    </tbody>
</table>";
        }
        else if (type.Equals("Contact2"))
        {
            s += @"
<div style='text-align:center;font-size:24px;font-weight:bold;margin-bottom:10px'>CHI TIẾT LIÊN HỆ</div>
<div style='text-align:center;font-size:12px;color:#454545;margin-bottom:20px;'>Ngày gửi: " + ((DateTime)dt.Rows[0]["dcdDateCreated"]).ToString("dd/MM/yyyy HH:mm") + @"</div>
<table>
    <tbody>
        <tr><td>Nom & prénom</td><td>" + dt.Rows[0]["vcdName"] + @"</td></tr>
        <tr><td>Email</td><td>" + dt.Rows[0]["vcdEmail"] + @"</td></tr>
        <tr><td>Votre message</td><td>" + dt.Rows[0]["vcdContent"].ToString().Replace("\n", "</br>") + @"</td></tr>
        <tr><td>Source</td><td>" + StringExtension.LayChuoi(param, "", 2) + @"</td></tr>
        <tr><td>utm_medium</td><td>" + StringExtension.LayChuoi(param, "", 4) + @"</td></tr>
        <tr><td>utm_campaign</td><td>" + StringExtension.LayChuoi(param, "", 5) + @"</td></tr>
        <tr><td>Link</td><td>" + StringExtension.LayChuoi(param, "", 6) + @"</td></tr>
    </tbody>
</table>";
        }
        return s;
    }


}
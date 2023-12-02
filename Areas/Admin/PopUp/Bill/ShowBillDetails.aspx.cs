using System;
using System.Globalization;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Admin_PopUp_Bill_ShowBillDetails : System.Web.UI.Page
{
    private string _iiid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Các querystring

        if (Request.QueryString["iiid"] != null) _iiid = QueryStringExtension.GetQueryString("iiid");
        #endregion Các querystring

        ltrList.Text = GetList();

    }

    private string GetList()
    {
        // Lấy thông tin khách hàng
        var s = "";
        var dt = Bills.GetData("1", "*", BillsTSql.GetById(_iiid), "");
        if (dt.Rows.Count < 1) return s;
        var ibid = dt.Rows[0]["ibid"].ToString();
        var dtBillDetail = BillDetails.GetData("1", "*", BillDetailsTSql.GetByIbId(ibid), "");
        if (dtBillDetail.Rows.Count < 1) return s;
        var size = StringExtension.LayChuoi(dtBillDetail.Rows[0]["vbdParam"].ToString(), "", 1);
        var color = StringExtension.LayChuoi(dtBillDetail.Rows[0]["vbdParam"].ToString(), "", 2);
        var city = StringExtension.LayChuoi(dt.Rows[0]["vbParam"].ToString(), "", 1);
        var referredBy = StringExtension.LayChuoi(dt.Rows[0]["vbParam"].ToString(), "", 2);
        var address = StringExtension.LayChuoi(dt.Rows[0]["vbAddress"].ToString(), "", 1);
        var status = dt.Rows[0]["ibStatus"].ToString();
        var tt = status.Equals("1") ? " - <span style='color:green'>Đã thanh toán</span>" : " - <span style='color:orange'>Chưa thanh toán</span>";
        s += @"
<div style='text-align:center;font-size:24px;font-weight:bold;margin-bottom:10px'>CHI TIẾT ĐƠN HÀNG</div>
<div style='text-align:center;font-size:12px;color:#454545;margin-bottom:20px;'>Mã số: " + dt.Rows[0][BillsColumns.VbCode] + @" - Ngày tạo: " + ((DateTime)dt.Rows[0][BillsColumns.DbDateCreated]).ToString("dd/MM/yyyy") + tt + @"</div>
<div style='line-height:20px'>
    Họ tên khách hàng: <b>" + dt.Rows[0][BillsColumns.VbName].ToString().Replace("-/-", " ") + @"</b><br/>
    Điện thoại: <b>" + dt.Rows[0][BillsColumns.VbPhone] + @"</b><br/>
    Email: <b>" + dt.Rows[0][BillsColumns.VbEmail] + @"</b><br/>
    Địa chỉ: <b>" + address + @"</b><br/>
    Phương thức thanh toán: <b>" + @"</b><br/>
    Nguồn: <b>" + referredBy + @"</b><br/>
    Size: <b>" + size + @"</b><br/>
    Màu: <b>" + color + @"</b><br/>
    Ghi chú: <b>" + dt.Rows[0][BillsColumns.VbComment].ToString().Replace("\n", "</br>") + @"</b><br/>
</div>
<br/>
";
        s += LayDanhSachSp(_iiid);
        return s;
    }

    private string LayDanhSachSp(string id)
    {
        var s = "";
        double total = 0;
        var dt = BillDetails.GetData("", "*", BillDetailsTSql.GetByIbId(id), "");
        if (dt.Rows.Count < 1) return s;

        s += "<table style='border-collapse:collapse;width:100%'>";
        s += @"
<tr>
    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>STT</th>
    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>Tên sản phẩm</th> 
    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>Số lượng</th>
    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>Size</th>
    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>Màu Sắc</th>
    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>Đơn giá</th>
    <th style='border:solid 1px #000;padding:5px 0;background:#f2f2f2'>Thành tiền</th>
</tr>";
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            var size = StringExtension.LayChuoi(dt.Rows[i]["vbdParam"].ToString(), "", 1);
            var color = StringExtension.LayChuoi(dt.Rows[i]["vbdParam"].ToString(), "", 2);
            var price = dt.Rows[i][BillDetailsColumns.FbdPriceNew].ToString().Equals("0") ? dt.Rows[i][BillDetailsColumns.FbdPriceOld].ToString() : dt.Rows[i][BillDetailsColumns.FbdPriceNew].ToString();
            var subTotal = int.Parse(dt.Rows[i][BillDetailsColumns.IbdQuantity].ToString()) * double.Parse(price);
            total += subTotal;
            s += @"
<tr>
    <td style='border:solid 1px #000;padding:5px 0;text-align:center'>" + (i + 1) + @"</td>
    <td style='border:solid 1px #000;padding:5px 0'><span style='padding-left:10px'>" + dt.Rows[i][BillDetailsColumns.VbdTitle] + @"</td> 
    <td style='border:solid 1px #000;padding:5px 0;text-align:center'>" + NumberExtension.FormatNumber(dt.Rows[i][BillDetailsColumns.IbdQuantity].ToString()) + @"</td>
    <td style='border:solid 1px #000;padding:5px 0;text-align:center'>" + size + @"</td>
    <td style='border:solid 1px #000;padding:5px 0;text-align:center'>" + color + @"</td>
    <td style='border:solid 1px #000;padding:5px 0;text-align:right'><span style='padding-right:10px'>" + NumberExtension.FormatNumber(price, true, "Liên hệ", "đ") + @"</span></td>
    <td style='border:solid 1px #000;padding:5px 0;text-align:right'><span style='padding-right:10px'>" + ThanhTien(dt.Rows[i][BillDetailsColumns.IbdQuantity].ToString(), price) + @"</span></td>
</tr>
";
        }

        s += @"
<tr>
    <td style='border:solid 1px #000;padding:5px 0;text-align:center' colspan='2'><b>Tổng cộng</b></td>
<td style='border:solid 1px #000;padding:5px 10px;text-align:right' colspan='5'><b>" +
             NumberExtension.FormatNumber(total.ToString(CultureInfo.InvariantCulture), true, "Liên hệ", "đ") + @"</b></td>
</tr>
";
        s += "</table>";
        s += "<div style='font:bold 12px Arial;padding:10px 0'>Bằng chữ: " + NumberExtension.ReadNumber(total.ToString(CultureInfo.InvariantCulture)) + @"</div>";
        return s;
    }

    private string ThanhTien(string soLuong, string donGia)
    {
        double soluong = 0;
        double dongia = 0;
        try
        {
            soluong = double.Parse(soLuong);
        }
        catch
        {
            // do nothing
        }

        try
        {
            dongia = double.Parse(donGia);
        }
        catch
        {
            // do nothing
        }
        dongia *= soluong;
        return NumberExtension.FormatNumber(dongia.ToString(CultureInfo.InvariantCulture), true, "Liên hệ", "đ");
    }
}
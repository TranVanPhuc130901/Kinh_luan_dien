using System;
using System.EnterpriseServices;
using System.Globalization;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

public partial class Areas_Display_Product_Control_Success : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ltrContent.Text = SettingsExtension.GetSettingKey("KeyThongBaoHoanThanhDangKyDungThu", RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay());
        if (ltrContent.Text == "") ltrContent.Text = "<div class='text_success'>Đặt hàng thành công</div><div>Chúng tôi sẽ liên hệ lại với bạn sau khi nhận được đơn hàng này</div>" +
                                                     "<div>Mọi thăc mắc xin vui lòng liên hệ <a href='tel: 0961969891'>0961969891</a></div>";

    }
   
}
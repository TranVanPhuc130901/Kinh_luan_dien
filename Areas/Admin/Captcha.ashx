<%@ WebHandler Language="C#" Class="Areas_Admin_Captcha" %>

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Web;
using System.Web.SessionState;

public class Areas_Admin_Captcha : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string[] fonts = { "Arial Black", "Tahoma" };

        const byte length = 4;

        // chuỗi để lấy các kí tự sẽ sử dụng cho captcha
        //const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        //const string chars = "36KL01!@ABC78%^&*9GHIJ452MNtuXPQRSTfnUVghijkDEFlmWabopqrsYZOyzvc#$(_+dewx";
        //const string chars = "19CPQRSGH56DE2348AB0FLMNO7IKTUVWXYZJ";
        const string chars = "19cpqrsgh56de2348ab0flmno7iktuvwxyxj";
        using (var bmp = new Bitmap(90, 30))
        {
            using (var g = Graphics.FromImage(bmp))
            {
                // Tạo nền nhiễu cho ảnh
                var brush = new HatchBrush(HatchStyle.LargeGrid, Color.DarkSeaGreen, Color.ForestGreen);

                g.FillRegion(brush, g.Clip);

                // Lưu chuỗi captcha trong quá trình tạo
                var strCaptcha = new StringBuilder();

                var rand = new Random();
                float curX = 0;//Đánh dấu vị trí x hiện tại đã vẽ đến
                for (var i = 0; i < length; i++)
                {
                    // Lấy kí tự ngẫu nhiên từ mảng chars                    
                    var str = chars[rand.Next(chars.Length)].ToString();
                    strCaptcha.Append(str);

                    // Tạo font với tên font ngẫu nhiên chọn từ mảng fonts
                    var font = new Font(fonts[rand.Next(fonts.Length)], rand.Next(14, 16), FontStyle.Bold | FontStyle.Regular);

                    // Lấy kích thước của kí tự
                    var size = g.MeasureString(str, font);

                    // Vẽ kí tự đó ra ảnh tại vị trí x theo vị trí hiện tại đã vẽ đến, vị trí y ngẫu nhiên
                    g.DrawString(str, font, Brushes.WhiteSmoke, curX + 5, rand.Next(0, 2));
                    curX += size.Width;//Cộng thêm độ dộng của ký tự vừa viết vào vị trí x hiện tại (đảm bảo các ký tự vẽ ra không đè lên nhau)
                    font.Dispose();
                }

                // Lưu captcha vào session
                context.Session["captchaAdminLogin"] = strCaptcha.ToString();

                // Ghi ảnh trực tiếp ra luồng xuất theo định dạng gif
                context.Response.ContentType = "image/GIF";
                bmp.Save(context.Response.OutputStream, ImageFormat.Gif);
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
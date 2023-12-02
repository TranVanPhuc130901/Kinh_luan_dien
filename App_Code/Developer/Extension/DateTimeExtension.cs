using System;

namespace Developer.Extension
{
    public class DateTimeExtension
    {
        /// <summary>
        /// Lấy khoảng cách từ ngày tháng được truyền vào tới ngày hiện tại theo kiểu: 1 giờ trước, vài giây trước...
        /// Khi di vào chuỗi sẽ hiện lên ngày tháng thật theo định dạng mặc định của hệ thống
        /// Kết quả:
        /// Trong khoảng vài giây: vài giây trước
        /// Trong khoảng 1-10 phút: vài phút trước
        /// Trong khoảng 11-60 phút: 11-60 phút trước
        /// Trong khoảng 1-24 giờ: 1-24 giờ trước
        /// Trong khoảng 25-48 giờ: hôm qua lúc + giờ:phút
        /// Trong khoảng 49-168 giờ: thứ... lúc + giờ:phút
        /// Trên 168 giờ hiện ngày tháng
        /// </summary>
        /// <param name="sourceDate"></param>
        /// <returns></returns>
        public static string GetTimeDistance(DateTime sourceDate)
        {
            var s = "";
            var dNow = DateTime.Now;
            var distance = dNow - sourceDate;
            var totalSecond = distance.TotalSeconds;
            if (totalSecond >= 168 * 60 * 60) s = sourceDate.ToString("dd/MM/yyyy - HH:mm");
            else if (totalSecond >= 48 * 60 * 60) s = GetDayOfWeek(sourceDate) + " lúc " + sourceDate.ToString("HH:mm");
            else if (totalSecond >= 24 * 60 * 60) s = "Hôm qua lúc " + sourceDate.ToString("HH:mm");
            else if (totalSecond >= 1 * 60 * 60) s = (int)distance.TotalHours + " giờ trước";
            else if (totalSecond >= 10 * 60) s = (int)distance.TotalMinutes + " phút trước";
            else if (totalSecond >= 60) s = "vài phút trước";
            else s = "vài giây trước";
            //s = "<span title='" + sourceDate.ToString("dd/MM/yyyy - HH:mm") + "'>" + s + "</span>";
            return s;
        }
        public static string GetDayOfWeek(DateTime dateTime)
        {
            var s = "";
            var day = dateTime.DayOfWeek;
            switch (day)
            {
                case DayOfWeek.Monday:
                    s = "Thứ Hai";
                    break;
                case DayOfWeek.Tuesday:
                    s = "Thứ Ba";
                    break;
                case DayOfWeek.Wednesday:
                    s = "Thứ Tư";
                    break;
                case DayOfWeek.Thursday:
                    s = "Thứ Năm";
                    break;
                case DayOfWeek.Friday:
                    s = "Thứ Sáu";
                    break;
                case DayOfWeek.Saturday:
                    s = "Thứ Bảy";
                    break;
                case DayOfWeek.Sunday:
                    s = "Chủ Nhật";
                    break;
            }
            return s;
        } 
    }
}
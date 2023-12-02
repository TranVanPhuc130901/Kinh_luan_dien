namespace Developer.Position
{
    public class AdvertistmentsPosition
    {
        private string[] values;
        private string[] text;

        public AdvertistmentsPosition()
        {
            text = new[]
            {
                "Logo svg màu đen",
                "Banner đầu trang chủ",
                "Danh mục sản phẩm trang chủ",
                "Banner giữa trang chủ",
                "Dẫn link bài viết footer",
                "Logo chân trang",
                "Thẻ thanh toán",
                "Banner trang Sản phẩm",
                "Hình ảnh thực tế",
                "Banner khuyến mãi đầu trang",
                "Banner đầu trang chủ moblie"

            };
            values = new[]
            {
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "100",
                "9",
                "10"

            };
        }

        public string[] Text
        {
            get { return text; }
        }

        public string[] Values
        {
            get { return values; }
        }
    }
}
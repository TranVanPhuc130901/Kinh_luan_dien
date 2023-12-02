namespace Developer.Position
{
    public class TourPosition
    {
        private string[] values;
        private string[] text;

        public TourPosition()
        {
            text = new[] { "Nhóm tour nổi bật - Trang chủ", "Nhóm tour theo loại hình", "Nhóm tour trang Blog" };
            values = new[] { "1", "2", "3" };
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

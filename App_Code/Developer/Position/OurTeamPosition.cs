namespace Developer.Position
{
    public class OurTeamPosition
    {
        private string[] values;
        private string[] text;

        public OurTeamPosition()
        {
            text = new[] { "Nhóm trang chủ", "Hỗ trợ trực tuyến", "Nhóm trang Giới thiệu" };
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

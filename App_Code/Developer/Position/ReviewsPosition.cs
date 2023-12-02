namespace Developer.Position
{
    public class ReviewsPosition
    {
        private string[] values;
        private string[] text;

        public ReviewsPosition()
        {
            text = new[]
            {
                "Nhóm trang chủ"
            };
            values = new[]
            {
                "1"
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

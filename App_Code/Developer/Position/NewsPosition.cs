
namespace Developer.Position
{
    public class NewsPosition
    {
        private string[] values;
        private string[] text;

        public NewsPosition()
        {
            text = new[] { "Tin tức trang chủ", "Tin tức nổi bật" };
            values = new[] { "1", "2" };
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
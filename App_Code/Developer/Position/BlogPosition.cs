namespace Developer.Position
{
    public class BlogPosition
    {
        private string[] values;
        private string[] text;

        public BlogPosition()
        {
            text = new[] { "Bài viết nổi bật", "Nhóm bên phải" };
            values = new[] { "1","2" };
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

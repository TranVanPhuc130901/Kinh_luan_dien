namespace Developer.Position
{
    public class ProjectPosition
    {
        private string[] values;
        private string[] text;

        public ProjectPosition()
        {
            text = new[] { "Nhóm trang chủ", "Nhóm 2" };
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

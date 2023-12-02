namespace Developer.Position
{
    public class AboutUsPosition
    {
        private string[] values;
        private string[] text;

        public AboutUsPosition()
        {
            text = new[] { "Nhóm 1" };
            values = new[] { "1" };
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

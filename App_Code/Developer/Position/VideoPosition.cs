namespace Developer.Position
{
    public class VideoPosition
    {
        private string[] values;
        private string[] text;

        public VideoPosition()
        {
            text = new[] { "Video nổi bật" };
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

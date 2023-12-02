namespace Developer.Position
{
    public class DestinationPosition
    {
        private string[] values;
        private string[] text;

        public DestinationPosition()
        {
            text = new[] { "Nhóm điểm đến" };
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
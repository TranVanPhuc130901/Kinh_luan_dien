namespace Developer.Position
{
    public class CustomerPosition
    {
        private string[] values;
        private string[] text;

        public CustomerPosition()
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

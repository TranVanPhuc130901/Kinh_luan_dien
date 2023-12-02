namespace Developer.Position
{
    public class ServicePosition
    {
        private string[] values;
        private string[] text;

        public ServicePosition()
        {
            text = new[] { "Nhóm dịch vụ bên trái website" };
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

namespace Developer.Position
{
    public class FAQPosition
    {
        private string[] values;
        private string[] text;

        public FAQPosition()
        {
            text = new[] { "Nhóm ý kiến khách hàng trang chủ" };
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

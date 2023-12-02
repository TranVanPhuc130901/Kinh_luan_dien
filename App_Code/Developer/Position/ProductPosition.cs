namespace Developer.Position
{
    public class ProductPosition
    {
        private string[] values;
        private string[] text;

        public ProductPosition()
        {
            text = new[] { "Nhóm sản phẩm hot", "Nhóm sản phẩm khác" };
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

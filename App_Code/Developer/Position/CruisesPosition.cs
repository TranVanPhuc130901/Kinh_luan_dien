namespace Developer.Position
{
    public class CruisesPosition
    {
        private string[] values;
        private string[] text;

        public CruisesPosition()
        {
            text = new[]
            {
                "Nhóm tour giảm giá - trang chủ"
                ,"Nhóm tour Hạ Long - trang chủ"
                ,"Nhóm tàu bên trái"
            };
            values = new[]
            {
                "1"
                ,"2"
                ,"3"
            };
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

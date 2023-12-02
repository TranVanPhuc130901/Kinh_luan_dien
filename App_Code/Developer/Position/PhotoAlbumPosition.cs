namespace Developer.Position
{
    public class PhotoAlbumPosition
    {
        private string[] values;
        private string[] text;

        public PhotoAlbumPosition()
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

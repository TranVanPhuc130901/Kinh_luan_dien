
namespace Developer
{
    /// <summary>
    /// Summary description for JsonExt
    /// </summary>
    public class JsonProduct
    {
        public int id;
        public string title;
        public string handle;
        public string published_at;
        public string created_at;
        public string vendor;
        public string type;
        public string[] tags;
        public float price;
        public float price_min;
        public float price_max;
        public bool available;
        public bool price_varies;
        public float compare_at_price;
        public float compare_at_price_min;
        public float compare_at_price_max;
        public bool compare_at_price_varies;
        public string[] variants;
        public string[] images;
        public string featured_image;
        public string[] options;
    }
    public class JsonProductData
    {
        public JsonProduct JsonProductDataOpt(int id, string title, string handle, string published_at, string created_at, string vendor, string type, string [] tags, float price, float price_min, float price_max, bool available, bool price_varies, float compare_at_price, float compare_at_price_min, float compare_at_price_max, bool compare_at_price_varies, string [] variants, string [] images, string featured_image, string [] options)
        {
            var EmpObj = new JsonProduct();
            EmpObj.id = id;
            EmpObj.title = title;
            EmpObj.handle = handle;
            EmpObj.published_at = published_at;
            EmpObj.created_at = created_at;
            EmpObj.vendor = vendor;
            EmpObj.type = type;
            EmpObj.tags = tags;
            EmpObj.price = price;
            EmpObj.price_min = price_min;
            EmpObj.price_max = price_max;
            EmpObj.available = available;
            EmpObj.price_varies = price_varies;
            EmpObj.compare_at_price = compare_at_price;
            EmpObj.compare_at_price_min = compare_at_price_min;
            EmpObj.compare_at_price_max = compare_at_price_max;
            EmpObj.compare_at_price_varies = compare_at_price_varies;
            EmpObj.variants = variants;
            EmpObj.images = images;
            EmpObj.featured_image = featured_image;
            EmpObj.options = options;
            return EmpObj;
        }
    }
}
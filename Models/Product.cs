namespace ProductCoreAPI.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string  Name { get; set; }
        public string  Category { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
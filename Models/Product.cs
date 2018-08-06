using System.ComponentModel.DataAnnotations;
namespace ProductCoreAPI.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required(ErrorMessage="Product Name is required")]
        public string  Name { get; set; }
        public string  Category { get; set; }
        [Required]          
        public string Price { get; set; }
        [Required]       
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
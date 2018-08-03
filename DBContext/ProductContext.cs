using Microsoft.EntityFrameworkCore;
using ProductCoreAPI.Models;
namespace ProductCoreAPI.DBContext
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        public DbSet<Product> Product { get; set; }
    }

}
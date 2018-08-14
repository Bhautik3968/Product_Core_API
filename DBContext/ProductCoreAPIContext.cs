using Microsoft.EntityFrameworkCore;
using ProductCoreAPI.Models;
namespace ProductCoreAPI.DBContext
{
    public class ProductCoreAPIContext : DbContext
    {
        public ProductCoreAPIContext(DbContextOptions<ProductCoreAPIContext> options) : base(options)
        {

        }
        public DbSet<Error> API_Errors { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> tblUsers { get; set; }
    }
}

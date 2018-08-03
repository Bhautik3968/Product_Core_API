using Microsoft.EntityFrameworkCore;
using ProductCoreAPI.Models;
namespace ProductCoreAPI.DBContext
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User> tblUsers { get; set; }
    }

}
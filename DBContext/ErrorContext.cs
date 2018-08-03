using Microsoft.EntityFrameworkCore;
using ProductCoreAPI.Models;
namespace ProductCoreAPI.DBContext
{
    public class DbErrorContext : DbContext
    {
        public DbErrorContext(DbContextOptions<DbErrorContext> options) : base(options)
        {
        }
        public DbSet<Error> API_Errors { get; set; }
    }

}
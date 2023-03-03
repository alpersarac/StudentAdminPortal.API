
using Microsoft.EntityFrameworkCore;
using Rentiva.Models;

namespace Rentiva.DataAccess
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products{ get; set; }
    }
}

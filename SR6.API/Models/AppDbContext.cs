using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SR6.DataLayer;

namespace SR6.API.Models
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Brand> Brand { get; set; }
    }
}

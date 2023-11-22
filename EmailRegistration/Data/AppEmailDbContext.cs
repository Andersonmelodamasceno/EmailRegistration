using EmailRegistration.Dto;
using Microsoft.EntityFrameworkCore;

namespace EmailRegistration.Data
{
    public class AppEmailDbContext : DbContext
    {
        public AppEmailDbContext(DbContextOptions<AppEmailDbContext> options) : base(options) { }



        public DbSet<Feed> Feed { get; set; }

    }
}

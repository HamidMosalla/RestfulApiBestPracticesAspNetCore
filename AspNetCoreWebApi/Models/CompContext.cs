using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApi.Models
{
    public class CompContext : DbContext
    {
        public CompContext(DbContextOptions<CompContext> options) : base(options) { }

        public DbSet<Camper> Campers { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
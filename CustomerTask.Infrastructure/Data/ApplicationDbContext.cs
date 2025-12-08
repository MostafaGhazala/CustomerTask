using System.Reflection;
using CustomerTask.Core.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomerTask.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Numbers> Numbers{ get; set; }
        public DbSet<ReservedNumbers> ReservedNumbers{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

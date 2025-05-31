using CourierManagementsystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace CourierManagementsystem.Data
{
    public class CourierDbContext : IdentityDbContext<IdentityUser>
    {
        public CourierDbContext(DbContextOptions<CourierDbContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Receivers> Receivers { get; set; }
        // Other DbSet properties for additional entities

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserRole<string>>();
            builder.Entity<IdentityUserLogin<string>>();
          //  builder.Entity<IdentityUserLogin<string>>()
          //.HasKey(login => new { login.LoginProvider, login.ProviderKey });
        }
    }

}

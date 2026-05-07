using CourierManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourierManagement.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ParcelType> ParcelTypes => Set<ParcelType>();
    public DbSet<Parcel> Parcels => Set<Parcel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ParcelType>(builder =>
        {
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(80).IsRequired();
            builder.Property(x => x.BaseRate).HasPrecision(18, 2);
            builder.Property(x => x.PerKgRate).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Parcel>(builder =>
        {
            builder.HasIndex(x => x.TrackingId).IsUnique();
            builder.Property(x => x.TrackingId).HasMaxLength(32).IsRequired();

            builder.Property(x => x.SenderName).HasMaxLength(120).IsRequired();
            builder.Property(x => x.SenderPhone).HasMaxLength(30).IsRequired();
            builder.Property(x => x.SenderAddress).HasMaxLength(300);

            builder.Property(x => x.ReceiverName).HasMaxLength(120).IsRequired();
            builder.Property(x => x.ReceiverPhone).HasMaxLength(30).IsRequired();
            builder.Property(x => x.ReceiverAddress).HasMaxLength(300).IsRequired();

            builder.Property(x => x.WeightKg).HasPrecision(18, 3);
            builder.Property(x => x.DeliveryCharge).HasPrecision(18, 2);
            builder.Property(x => x.TotalPayable).HasPrecision(18, 2);
            builder.Property(x => x.CodAmount).HasPrecision(18, 2);

            builder.HasOne(x => x.ParcelType)
                .WithMany()
                .HasForeignKey(x => x.ParcelTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}


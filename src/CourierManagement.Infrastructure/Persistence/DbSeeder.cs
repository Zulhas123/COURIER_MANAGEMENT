using CourierManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourierManagement.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext dbContext, CancellationToken cancellationToken = default)
    {
        if (await dbContext.ParcelTypes.AnyAsync(cancellationToken)) return;

        dbContext.ParcelTypes.AddRange(
            new ParcelType { Name = "Document", Description = "Letters, papers, files", BaseRate = 60, PerKgRate = 20, IsActive = true },
            new ParcelType { Name = "Fragile", Description = "Handle with care", BaseRate = 80, PerKgRate = 30, IsActive = true },
            new ParcelType { Name = "Electronics", Description = "Gadgets, devices", BaseRate = 90, PerKgRate = 35, IsActive = true },
            new ParcelType { Name = "Food", Description = "Perishable items", BaseRate = 70, PerKgRate = 25, IsActive = true }
        );

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}


using CourierManagement.Application.Abstractions;
using CourierManagement.Domain.Entities;
using CourierManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CourierManagement.Infrastructure.Repositories;

public sealed class ParcelRepository : EfRepository<Parcel>, IParcelRepository
{
    private readonly AppDbContext _dbContext;

    public ParcelRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Parcel?> GetByTrackingIdAsync(string trackingId, CancellationToken cancellationToken = default)
        => _dbContext.Parcels.AsNoTracking()
            .Include(x => x.ParcelType)
            .FirstOrDefaultAsync(x => x.TrackingId == trackingId, cancellationToken);

    public override Task<Parcel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _dbContext.Parcels.AsNoTracking()
            .Include(x => x.ParcelType)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public override async Task<IReadOnlyList<Parcel>> ListAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Parcels.AsNoTracking()
            .Include(x => x.ParcelType)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
}

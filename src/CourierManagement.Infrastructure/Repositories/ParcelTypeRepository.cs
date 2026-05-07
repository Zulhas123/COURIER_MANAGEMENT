using CourierManagement.Application.Abstractions;
using CourierManagement.Domain.Entities;
using CourierManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CourierManagement.Infrastructure.Repositories;

public sealed class ParcelTypeRepository : EfRepository<ParcelType>, IParcelTypeRepository
{
    private readonly AppDbContext _dbContext;

    public ParcelTypeRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ParcelType?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        => _dbContext.ParcelTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

    public async Task<IReadOnlyList<ParcelType>> ListActiveAsync(CancellationToken cancellationToken = default)
        => await _dbContext.ParcelTypes.AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
}


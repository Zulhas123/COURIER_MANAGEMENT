using CourierManagement.Domain.Entities;

namespace CourierManagement.Application.Abstractions;

public interface IParcelTypeRepository : IRepository<ParcelType>
{
    Task<ParcelType?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ParcelType>> ListActiveAsync(CancellationToken cancellationToken = default);
}


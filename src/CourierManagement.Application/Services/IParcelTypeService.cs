using CourierManagement.Domain.Entities;

namespace CourierManagement.Application.Services;

public interface IParcelTypeService
{
    Task<IReadOnlyList<ParcelType>> ListAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ParcelType>> ListActiveAsync(CancellationToken cancellationToken = default);
    Task<ParcelType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ParcelType> CreateAsync(ParcelType parcelType, CancellationToken cancellationToken = default);
    Task UpdateAsync(ParcelType parcelType, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}


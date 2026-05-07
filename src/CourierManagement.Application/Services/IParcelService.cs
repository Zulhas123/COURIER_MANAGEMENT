using CourierManagement.Domain.Entities;
using CourierManagement.Application.Models;

namespace CourierManagement.Application.Services;

public interface IParcelService
{
    Task<Parcel> CreateAsync(ParcelCreateModel model, CancellationToken cancellationToken = default);
    Task<Parcel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Parcel?> TrackAsync(string trackingId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Parcel>> ListAsync(CancellationToken cancellationToken = default);
}


using CourierManagement.Domain.Entities;

namespace CourierManagement.Application.Abstractions;

public interface IParcelRepository : IRepository<Parcel>
{
    Task<Parcel?> GetByTrackingIdAsync(string trackingId, CancellationToken cancellationToken = default);
}


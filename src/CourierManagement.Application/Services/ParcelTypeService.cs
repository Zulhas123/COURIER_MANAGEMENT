using CourierManagement.Application.Abstractions;
using CourierManagement.Domain.Entities;

namespace CourierManagement.Application.Services;

public sealed class ParcelTypeService : IParcelTypeService
{
    private readonly IParcelTypeRepository _parcelTypes;
    private readonly IUnitOfWork _unitOfWork;

    public ParcelTypeService(IParcelTypeRepository parcelTypes, IUnitOfWork unitOfWork)
    {
        _parcelTypes = parcelTypes;
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<ParcelType>> ListAsync(CancellationToken cancellationToken = default)
        => _parcelTypes.ListAsync(cancellationToken);

    public Task<IReadOnlyList<ParcelType>> ListActiveAsync(CancellationToken cancellationToken = default)
        => _parcelTypes.ListActiveAsync(cancellationToken);

    public Task<ParcelType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _parcelTypes.GetByIdAsync(id, cancellationToken);

    public async Task<ParcelType> CreateAsync(ParcelType parcelType, CancellationToken cancellationToken = default)
    {
        var existing = await _parcelTypes.GetByNameAsync(parcelType.Name, cancellationToken);
        if (existing is not null)
        {
            throw new InvalidOperationException("A parcel type with the same name already exists.");
        }

        await _parcelTypes.AddAsync(parcelType, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return parcelType;
    }

    public async Task UpdateAsync(ParcelType parcelType, CancellationToken cancellationToken = default)
    {
        parcelType.UpdatedAtUtc = DateTime.UtcNow;
        await _parcelTypes.UpdateAsync(parcelType, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _parcelTypes.GetByIdAsync(id, cancellationToken);
        if (entity is null) return;
        await _parcelTypes.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}


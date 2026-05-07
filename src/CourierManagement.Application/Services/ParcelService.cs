using CourierManagement.Application.Abstractions;
using CourierManagement.Application.Models;
using CourierManagement.Domain.Entities;
using CourierManagement.Domain.Enums;

namespace CourierManagement.Application.Services;

public sealed class ParcelService : IParcelService
{
    private readonly IParcelRepository _parcels;
    private readonly IParcelTypeRepository _parcelTypes;
    private readonly ITrackingIdGenerator _trackingIdGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public ParcelService(
        IParcelRepository parcels,
        IParcelTypeRepository parcelTypes,
        ITrackingIdGenerator trackingIdGenerator,
        IUnitOfWork unitOfWork)
    {
        _parcels = parcels;
        _parcelTypes = parcelTypes;
        _trackingIdGenerator = trackingIdGenerator;
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<Parcel>> ListAsync(CancellationToken cancellationToken = default)
        => _parcels.ListAsync(cancellationToken);

    public Task<Parcel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _parcels.GetByIdAsync(id, cancellationToken);

    public Task<Parcel?> TrackAsync(string trackingId, CancellationToken cancellationToken = default)
        => _parcels.GetByTrackingIdAsync(trackingId, cancellationToken);

    public async Task<Parcel> CreateAsync(ParcelCreateModel model, CancellationToken cancellationToken = default)
    {
        if (model.WeightKg <= 0) throw new ArgumentOutOfRangeException(nameof(model.WeightKg));
        if (model.PaymentMethod == PaymentMethod.CashOnDelivery && (model.CodAmount is null || model.CodAmount <= 0))
        {
            throw new InvalidOperationException("COD amount is required for Cash on Delivery.");
        }

        var parcelType = await _parcelTypes.GetByIdAsync(model.ParcelTypeId, cancellationToken)
                         ?? throw new InvalidOperationException("Parcel type not found.");

        var trackingId = _trackingIdGenerator.Generate();

        var deliveryCharge = CalculateDeliveryCharge(parcelType, model.WeightKg, model.DeliverySpeed, model.DeliveryPriority);
        var totalPayable = model.PaymentMethod == PaymentMethod.CashOnDelivery
            ? deliveryCharge + (model.CodAmount ?? 0)
            : deliveryCharge;

        var parcel = new Parcel
        {
            TrackingId = trackingId,
            ParcelTypeId = model.ParcelTypeId,
            SenderName = model.SenderName.Trim(),
            SenderPhone = model.SenderPhone.Trim(),
            SenderAddress = string.IsNullOrWhiteSpace(model.SenderAddress) ? null : model.SenderAddress.Trim(),
            ReceiverName = model.ReceiverName.Trim(),
            ReceiverPhone = model.ReceiverPhone.Trim(),
            ReceiverAddress = model.ReceiverAddress.Trim(),
            WeightKg = model.WeightKg,
            DeliverySpeed = model.DeliverySpeed,
            DeliveryPriority = model.DeliveryPriority,
            PaymentMethod = model.PaymentMethod,
            CodAmount = model.PaymentMethod == PaymentMethod.CashOnDelivery ? model.CodAmount : null,
            DeliveryCharge = deliveryCharge,
            TotalPayable = totalPayable,
            Status = ParcelStatus.Pending
        };

        await _parcels.AddAsync(parcel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return parcel;
    }

    private static decimal CalculateDeliveryCharge(ParcelType parcelType, decimal weightKg, DeliverySpeed speed, DeliveryPriority priority)
    {
        var charge = parcelType.BaseRate + (parcelType.PerKgRate * weightKg);

        charge *= speed switch
        {
            DeliverySpeed.SameDay => 1.25m,
            _ => 1.0m
        };

        charge *= priority switch
        {
            DeliveryPriority.Urgent => 1.2m,
            DeliveryPriority.High => 1.1m,
            _ => 1.0m
        };

        return Math.Round(charge, 2, MidpointRounding.AwayFromZero);
    }
}


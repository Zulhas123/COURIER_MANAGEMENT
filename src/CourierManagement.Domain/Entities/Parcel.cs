using CourierManagement.Domain.Enums;

namespace CourierManagement.Domain.Entities;

public sealed class Parcel : BaseEntity
{
    public string TrackingId { get; set; } = string.Empty;

    public Guid ParcelTypeId { get; set; }
    public ParcelType? ParcelType { get; set; }

    public string SenderName { get; set; } = string.Empty;
    public string SenderPhone { get; set; } = string.Empty;
    public string? SenderAddress { get; set; }

    public string ReceiverName { get; set; } = string.Empty;
    public string ReceiverPhone { get; set; } = string.Empty;
    public string ReceiverAddress { get; set; } = string.Empty;

    public decimal WeightKg { get; set; }
    public DeliverySpeed DeliverySpeed { get; set; } = DeliverySpeed.NextDay;
    public DeliveryPriority DeliveryPriority { get; set; } = DeliveryPriority.Normal;

    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Prepaid;
    public decimal? CodAmount { get; set; }

    public decimal DeliveryCharge { get; set; }
    public decimal TotalPayable { get; set; }

    public ParcelStatus Status { get; set; } = ParcelStatus.Pending;
}


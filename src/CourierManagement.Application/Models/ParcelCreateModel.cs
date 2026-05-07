using CourierManagement.Domain.Enums;

namespace CourierManagement.Application.Models;

public sealed class ParcelCreateModel
{
    public Guid ParcelTypeId { get; set; }

    public string SenderName { get; set; } = string.Empty;
    public string SenderPhone { get; set; } = string.Empty;
    public string? SenderAddress { get; set; }

    public string ReceiverName { get; set; } = string.Empty;
    public string ReceiverPhone { get; set; } = string.Empty;
    public string ReceiverAddress { get; set; } = string.Empty;

    public decimal WeightKg { get; set; }
    public DeliverySpeed DeliverySpeed { get; set; }
    public DeliveryPriority DeliveryPriority { get; set; }

    public PaymentMethod PaymentMethod { get; set; }
    public decimal? CodAmount { get; set; }
}


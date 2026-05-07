using System.ComponentModel.DataAnnotations;
using CourierManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourierManagement.Web.Models;

public sealed class ParcelCreateVm
{
    [Required]
    public Guid ParcelTypeId { get; set; }

    [Required, StringLength(120)]
    public string SenderName { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string SenderPhone { get; set; } = string.Empty;

    [StringLength(300)]
    public string? SenderAddress { get; set; }

    [Required, StringLength(120)]
    public string ReceiverName { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string ReceiverPhone { get; set; } = string.Empty;

    [Required, StringLength(300)]
    public string ReceiverAddress { get; set; } = string.Empty;

    [Range(0.001, 9999)]
    public decimal WeightKg { get; set; }

    public DeliverySpeed DeliverySpeed { get; set; } = DeliverySpeed.NextDay;
    public DeliveryPriority DeliveryPriority { get; set; } = DeliveryPriority.Normal;

    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Prepaid;

    [Range(0, 999999)]
    public decimal? CodAmount { get; set; }

    public List<SelectListItem> ParcelTypes { get; set; } = [];
}


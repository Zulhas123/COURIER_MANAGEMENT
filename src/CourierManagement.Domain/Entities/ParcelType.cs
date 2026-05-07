namespace CourierManagement.Domain.Entities;

public sealed class ParcelType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Pricing model: base + (perKg * weightKg)
    public decimal BaseRate { get; set; }
    public decimal PerKgRate { get; set; }

    public bool IsActive { get; set; } = true;
}


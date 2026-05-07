using System.ComponentModel.DataAnnotations;

namespace CourierManagement.Web.Models;

public sealed class ParcelTypeFormVm
{
    public Guid? Id { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [StringLength(250)]
    public string? Description { get; set; }

    [Range(0, 999999)]
    public decimal BaseRate { get; set; }

    [Range(0, 999999)]
    public decimal PerKgRate { get; set; }

    public bool IsActive { get; set; } = true;
}


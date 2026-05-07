using System.ComponentModel.DataAnnotations;

namespace CourierManagement.Web.Models;

public sealed class TrackVm
{
    [Required, StringLength(32)]
    public string TrackingId { get; set; } = string.Empty;
}


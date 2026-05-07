using CourierManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourierManagement.Web.Controllers.Api;

[ApiController]
[Route("api/tracking")]
[Authorize]
public sealed class TrackingController : ControllerBase
{
    private readonly IParcelService _parcels;

    public TrackingController(IParcelService parcels)
    {
        _parcels = parcels;
    }

    [HttpGet("{trackingId}")]
    public async Task<IActionResult> Get(string trackingId, CancellationToken cancellationToken)
    {
        trackingId = trackingId.Trim();
        if (string.IsNullOrWhiteSpace(trackingId)) return BadRequest();

        var parcel = await _parcels.TrackAsync(trackingId, cancellationToken);
        if (parcel is null) return NotFound();

        return Ok(new
        {
            parcel.TrackingId,
            parcel.Status,
            parcel.CreatedAtUtc,
            parcel.ReceiverName,
            parcel.ReceiverPhone,
            parcel.DeliveryCharge,
            parcel.TotalPayable,
            parcel.PaymentMethod
        });
    }
}


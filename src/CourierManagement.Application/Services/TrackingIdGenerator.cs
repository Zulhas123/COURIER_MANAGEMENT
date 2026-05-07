using System.Security.Cryptography;

namespace CourierManagement.Application.Services;

public sealed class TrackingIdGenerator : ITrackingIdGenerator
{
    public string Generate()
    {
        // Example: CM-20260507-A1B2C3
        var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
        var randomPart = Convert.ToHexString(RandomNumberGenerator.GetBytes(3));
        return $"CM-{datePart}-{randomPart}";
    }
}

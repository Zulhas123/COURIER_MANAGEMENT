using NuGet.Protocol.Plugins;

namespace CourierManagementsystem.Models
{
    public class Customer
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public int? phoneNo { get; set; }
        public string? address { get; set; }

        // Other customer-related properties
        public Shipper Shipper { get; set; }
        public Receivers Receiver { get; set; }
        public Shipment Shipment { get; set; }
    }
}

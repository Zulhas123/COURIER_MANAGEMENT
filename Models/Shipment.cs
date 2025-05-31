namespace CourierManagementsystem.Models
{
    public class Shipment
    {
        public int Id { get; set; }

        // Shipper Information
        public string ShipperName { get; set; }
        public string? ShipperAddress { get; set; }
        public string? ShipperContactNumber { get; set; }

        // Receiver Information
        public string ReceiverName { get; set; }
        public string? ReceiverAddress { get; set; }
        public string ReceiverContactNumber { get; set; }

        // Shipment Details
        public string ConsignmentNumber { get; set; }
        public DateTime PickupDateTime { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string Status { get; set; } // Delivered, In Transit, etc.

        // Other relevant properties based on your requirements

        // Foreign key relationships (if any)
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }

}

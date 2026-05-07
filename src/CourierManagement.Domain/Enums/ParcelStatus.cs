namespace CourierManagement.Domain.Enums;

public enum ParcelStatus
{
    Pending = 0,
    PickupAssigned = 1,
    PickedUp = 2,
    InTransit = 3,
    HubReceived = 4,
    OutForDelivery = 5,
    Delivered = 6,
    FailedDelivery = 7,
    ReturnInitiated = 8,
    Returned = 9
}


using CourierManagementsystem.Models;

namespace CourierManagementsystem.Interface.Manager
{
    interface IShipmentManager:ICommonManager<Shipment>
    {
        Shipment GetById(int id);
        IEnumerable<Shipment> GetAll();
        IEnumerable<Shipment> GetShipments(int page, string search);
        Shipment GetShipmentByConsignmentNumber(string consignmentNumber);
    }
}

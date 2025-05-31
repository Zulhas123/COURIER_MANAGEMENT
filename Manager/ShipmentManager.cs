using CourierManagementsystem.Data;
using CourierManagementsystem.Interface.Manager;
using CourierManagementsystem.Models;
using CourierManagementsystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace CourierManagementsystem.Manager
{
    public class ShipmentManager:CommonManager<Shipment>,IShipmentManager
    {
        public ShipmentManager(CourierDbContext db) : base(new CommonRepository<Shipment>(db))
        {
        }

        public Shipment GetById(int id)
        {
            return GetFirstOrDefault(c => c.Id == id);
        }

        // Fix for GetAll method
        public IEnumerable<Shipment> GetAll()
        {
            return base.GetAll();
        }

        public IEnumerable<Shipment> GetShipments(int page, string search)
        {
            const int pageSize = 10; // Adjust the page size as needed

            var query = GetAll(); // Use the GetAll method from the base class

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s =>
                    s.ShipperName.Contains(search) ||
                    s.ReceiverName.Contains(search) ||
                    s.ConsignmentNumber.Contains(search) ||
                    s.Status.Contains(search));
            }

            // Calculate total number of items for paging
            var totalItems = query.Count();

            // Calculate the number of skip items based on the page and page size
            var skip = (page - 1) * pageSize;

            // Retrieve the shipments for the current page
            var shipments = query
                .OrderByDescending(s => s.PickupDateTime)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return shipments;
        }


        public Shipment GetShipmentByConsignmentNumber(string consignmentNumber)
        {
            return GetFirstOrDefault(s => s.ConsignmentNumber == consignmentNumber);
        }

       
    }
}

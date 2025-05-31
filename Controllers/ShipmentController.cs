using CourierManagementsystem.Data;
using CourierManagementsystem.Interface.Manager;
using CourierManagementsystem.Manager;
using Microsoft.AspNetCore.Mvc;

namespace CourierManagementsystem.Controllers
{
    public class ShipmentController : Controller
    {
        private readonly IShipmentManager _shipmentManager;
        public ShipmentController(CourierDbContext db) 
        {
            _shipmentManager= new ShipmentManager(db);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListShipments(int page = 1, string search = null)
        {
            var shipments = _shipmentManager.GetShipments(page, search);
            return View(shipments);
        }
    }
}

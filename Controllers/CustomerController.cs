using CourierManagementsystem.Data;
using CourierManagementsystem.Interface.Manager;
using CourierManagementsystem.Manager;
using CourierManagementsystem.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace CourierManagementsystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IShipmentManager _shipmentManager;
        private readonly ICustomerManager _customerManager;
      
        public CustomerController(CourierDbContext db)
        {
            _shipmentManager = new ShipmentManager(db);
            _customerManager = new CustomerManager(db);

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TrackShipment(string consignmentNumber)
        {
            // Retrieve the shipment based on the provided consignment number
            var shipment = _shipmentManager.GetShipmentByConsignmentNumber(consignmentNumber);

            if (shipment != null)
            {
                // Display shipment details
                return View("ShipmentDetails", shipment);
            }

            // Shipment not found
            ModelState.AddModelError("consignmentNumber", "Invalid consignment number");
            return View("TrackShipment");
        }

        public IActionResult Details(int id)
        {
            var customer = _customerManager.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerManager.Add(customer);
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            var customer = _customerManager.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _customerManager.Update(customer);
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        public IActionResult Delete(int id)
        {
            var customer = _customerManager.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _customerManager.GetById(id);
            _customerManager.Delete(customer);
            return RedirectToAction(nameof(Index));
        }



        public IActionResult AddShipper(int customerId)
        {
            var customer = _customerManager.GetById(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            var shipper = new Shipper();
            customer.Shipper = shipper;

            return View("AddShipper", customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveShipper(int customerId, Shipper shipper)
        {
            var customer = _customerManager.GetById(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Shipper = shipper;
            //_shipperManager.Add(shipper);

            return RedirectToAction("Edit", new { id = customerId });
        }

        public IActionResult AddReceiver(int customerId)
        {
            var customer = _customerManager.GetById(customerId);

            if (customer == null)
            {
                return NotFound();
            }

           // var receiver = new Receiver();
           // customer.Receiver = receiver;

            return View("AddReceiver", customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveReceiver(int customerId, Receiver receiver)
        {
            var customer = _customerManager.GetById(customerId);

            if (customer == null)
            {
                return NotFound();
            }

           // customer.Receiver = receiver;
            //_receiverManager.Add(receiver);

            return RedirectToAction("Edit", new { id = customerId });
        }

        public IActionResult AddShipment(int customerId)
        {
            var customer = _customerManager.GetById(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            var shipment = new Shipment();
            customer.Shipment = shipment;

            return View("AddShipment", customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveShipment(int customerId, Shipment shipment)
        {
            var customer = _customerManager.GetById(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Shipment = shipment;
            _shipmentManager.Add(shipment);

            return RedirectToAction("Edit", new { id = customerId });
        }
    }
}

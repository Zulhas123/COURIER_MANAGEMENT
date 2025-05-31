using CourierManagementsystem.Data;
using CourierManagementsystem.Interface.Manager;
using CourierManagementsystem.Manager;
using CourierManagementsystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourierManagementsystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IShipmentManager _shipmentManager;

        public AdminController(CourierDbContext db)
        {
            _shipmentManager = new ShipmentManager(db);

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            // Add authentication logic here (check credentials, set authentication cookie, etc.)
            // For simplicity, let's assume a hardcoded admin user for demonstration purposes

            if (model.Username == "admin" && model.Password == "adminpassword")
            {
                // Successful login
                return RedirectToAction("ManageShipments");
            }

            // Invalid credentials
            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        public IActionResult ManageShipments()
        {
            // Retrieve a list of shipments from the database
            var shipments = _shipmentManager.GetAll();
            return View(shipments);
        }
    }
}

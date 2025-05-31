using CourierManagementsystem.Data;
using CourierManagementsystem.Interface.Manager;
using CourierManagementsystem.Models;
using CourierManagementsystem.Repository;

namespace CourierManagementsystem.Manager
{
    public class CustomerManager:CommonManager<Customer>, ICustomerManager
    {
        public CustomerManager(CourierDbContext db) : base(new CommonRepository<Customer>(db))
        {
        }
        public Customer GetById(int id)
        {
            return GetFirstOrDefault(c => c.Id == id);
        }
    }
}

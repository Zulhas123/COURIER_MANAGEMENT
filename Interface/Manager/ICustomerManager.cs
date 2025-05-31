using CourierManagementsystem.Models;

namespace CourierManagementsystem.Interface.Manager
{
    interface ICustomerManager:ICommonManager<Customer>
    {
        Customer GetById(int id);
    }
}

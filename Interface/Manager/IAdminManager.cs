using CourierManagementsystem.Models;

namespace CourierManagementsystem.Interface.Manager
{
    interface IAdminManager:ICommonManager<Admin>
    {
        Admin GetById(int id);
        Admin GetAdminByUsernameAndPassword(string username, string password);
    }
}

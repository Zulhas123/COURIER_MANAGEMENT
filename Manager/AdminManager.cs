using CourierManagementsystem.Data;
using CourierManagementsystem.Interface.Manager;
using CourierManagementsystem.Models;
using CourierManagementsystem.Repository;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace CourierManagementsystem.Manager
{
    public class AdminManager:CommonManager<Admin>,IAdminManager
    {
        public AdminManager(CourierDbContext db): base(new CommonRepository<Admin>(db))
        {
        }

        public Admin GetById(int id)
        {
            return GetFirstOrDefault(c => c.Id == id);
        }

        public Admin GetAdminByUsernameAndPassword(string username, string password)
        {
            return GetFirstOrDefault(a => a.Username == username && a.Password == password);
        }
    }

}

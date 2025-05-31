using Microsoft.AspNetCore.Identity;

namespace CourierManagementsystem.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string country { get; set; }
        public string City { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Models.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string City { set; get; }
    }
}
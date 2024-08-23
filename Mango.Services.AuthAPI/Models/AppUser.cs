using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; } 
    }
}

using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password  { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public string? Role { get; set; }
    }
}

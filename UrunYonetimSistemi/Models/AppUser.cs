using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UrunYonetimSistemi.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [Display(Name = "İsim")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; } = string.Empty;
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace simpleCommerce_Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required(ErrorMessage ="Name is a required field")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is a required field")]
        public string Surname { get; set; }
    }
}

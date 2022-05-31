using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simpleCommerce_Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [RegularExpression(@"^5(0[5-7]|[3-5]\d) \d{3} \d{2} \d{2}$", ErrorMessage ="Not a valid phone number")]
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumer { get; set; }
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
            + "@"
            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$",
            ErrorMessage ="Not a valid Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Address is required")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Province is required")]
        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public Province? Province { get; set; }
        [Required(ErrorMessage = "Address Line 1 is required")]
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; }
        [DataType(DataType.MultilineText)]
        public string? OrderNote { get; set; }
        public DateOnly? CreationDate { get; set; }
    }
}
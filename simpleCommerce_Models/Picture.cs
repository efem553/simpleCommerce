using System.ComponentModel.DataAnnotations;

namespace simpleCommerce_Models
{
    public class Picture
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? ImageBase64 { get; set; }

    }
}

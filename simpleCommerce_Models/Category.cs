using System.ComponentModel.DataAnnotations;

namespace simpleCommerce_Models
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? FilterName { get; set; }
    }
}

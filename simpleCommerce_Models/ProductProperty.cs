using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simpleCommerce_Models
{
    [Index(nameof(ProductId), nameof(PropertyId), IsUnique = true)]
    public class ProductProperty
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        public Guid PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        public string? Value { get; set; }
    }
}

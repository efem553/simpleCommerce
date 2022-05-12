using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simpleCommerce_Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required for Product")]
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required for Product")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Category Is Required")]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        [Display(Name = "Is In Stock")]
        public bool IsInStock { get; set; }
        [Required(ErrorMessage = "Stock Count Is Required")]
        [Display(Name = "Stock Count")]
        public int StockCount { get; set; }
    }
}

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

        public bool IsInStock { get; set; }
        public Guid PictureId { get; set; }
        [ForeignKey("PictureId")]
        public List<Picture>? Pictures { get; set; }
        public List<Property>? Properties  { get; set; }
        public List<Tag>? Tags { get; set; }


    }
}

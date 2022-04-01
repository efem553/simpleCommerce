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

        [Required(ErrorMessage = "You need to add a picture at least for Product")]
        public Guid PictureId { get; set; }
        [ForeignKey("PictureId")]
        public IList<Picture>? Pictures { get; set; }

    }
}

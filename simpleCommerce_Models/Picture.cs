using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simpleCommerce_Models
{

    public class Picture
    {
        public Guid PictureId { get; set; }
        [Required(ErrorMessage = "Picture cant be null")]

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public byte[]? ImageData { get; set; }
    }
}

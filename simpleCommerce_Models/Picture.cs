using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simpleCommerce_Models
{

    public class Picture
    {
        public Guid PictureId { get; set; }
        

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        [Required(ErrorMessage = "Picture cant be null")]
        public string? ImageData { get; set; }
    }
}

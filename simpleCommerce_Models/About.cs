

using System.ComponentModel.DataAnnotations;

namespace simpleCommerce_Models
{
    public class About
    {
        [Key]
        public Guid AboutArticleId { get; set; }

        public string? BaseIconBase64 { get; set; }
        [Required]
        [Display(Name ="Store Name")]
        public string? StoreName { get; set; }
        [Required]
        [Display(Name ="Article 1 Header")]
        public string Article1Header { get; set; }
        [Required]
        [Display(Name ="Article 1 Content")]
        public string Article1Content { get; set; }
    }
}

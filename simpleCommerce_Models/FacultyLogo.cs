using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models
{
    public class FacultyLogo
    {
        [Key]
        public Guid FacultyLogoId { get; set; }
        [Required]
        public string LogoBase64 { get; set; }
        [Required]
        public string FacultyName { get; set; }
        public Guid AboutArticleId { get; set; }
        [ForeignKey("AboutArticleId")]
        public About About { get; set; }
    }
}

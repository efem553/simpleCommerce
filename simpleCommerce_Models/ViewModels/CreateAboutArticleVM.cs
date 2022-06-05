using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models.ViewModels
{
    [MetadataType(typeof(About))]
    public class CreateAboutArticleVM
    {
        public About About { get; set; }
        [Required]
        [Display(Name ="Store Logo")]
        public IFormFile? StoreLogoFile { get; set; }
        [Required]
        [Display(Name = "Faculty Logos")]
        public List<IFormFile>? FacultyLogoFiles { get; set; }
    }
}

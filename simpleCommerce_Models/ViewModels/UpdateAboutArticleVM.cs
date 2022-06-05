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
    public class UpdateAboutArticleVM
    {
        public About About { get; set; }

        public IFormFile? StoreLogoFile { get; set; }

        public List<IFormFile>? FacultyLogoFiles { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models.ViewModels
{
    public class CreateProductVM
    {
        public Product? Product { get; set; }

        public IEnumerable<SelectListItem>? CategorySelectList{ get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}

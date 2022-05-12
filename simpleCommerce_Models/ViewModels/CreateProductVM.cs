using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace simpleCommerce_Models.ViewModels
{
    public class CreateProductVM
    {
        public Product? Product { get; set; }

        public IEnumerable<SelectListItem>? CategorySelectList { get; set; }
        public IEnumerable<SelectListItem>? PropertySelectList { get; set;}
        public IEnumerable<SelectListItem>? TagSelectList { get; set; }
        public string? PropertyJSON { get; set; }
        public string? TagJSON { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}

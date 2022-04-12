using Microsoft.AspNetCore.Mvc;
using simpleCommerce_Models;

namespace simpleCommerce.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Product product)
        {
            
            return View();
        }
    }
}

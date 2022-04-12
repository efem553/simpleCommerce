using Microsoft.AspNetCore.Mvc;

namespace simpleCommerce.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

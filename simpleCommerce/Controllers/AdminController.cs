using Microsoft.AspNetCore.Mvc;

namespace simpleCommerce.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

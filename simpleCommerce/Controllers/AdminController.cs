using eCommerce_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace simpleCommerce.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

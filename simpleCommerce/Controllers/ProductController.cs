using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;
using simpleCommerce_Models.ViewModels;

namespace simpleCommerce.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _prodRepo;
        private ICategoryRepository _catRepo;
        public ProductController(IProductRepository prodRepo,ICategoryRepository catRepo)
        {
            _prodRepo = prodRepo;
            _catRepo = catRepo; 
        }
        public IActionResult Index()
        {
            CreateProductVM createProductVM = new CreateProductVM();
            createProductVM.CategorySelectList = _catRepo.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = Convert.ToString(x.Id)
            }).OrderBy(x => x.Text);
            return View(createProductVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Product product)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreateProductVM productVM)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Index", productVM);
            }
            
        }
    }
}

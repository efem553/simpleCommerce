using Microsoft.AspNetCore.Mvc;
using simpleCommerce_DataAccess.Repository;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;

namespace simpleCommerce.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository _catRepo;
        public CategoryController(ICategoryRepository catRepo)
        {
            _catRepo=catRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category category)
        {
            if(ModelState.IsValid)
            {
                _catRepo.Add(category);
                _catRepo.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid CategoryId)
        {
            Category category;
            category=_catRepo.Find(CategoryId);

            return View("Edit", category);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetCategoryList()
        {
            return Json(new { data = _catRepo.GetAll() });
        }
        #endregion
    }
}

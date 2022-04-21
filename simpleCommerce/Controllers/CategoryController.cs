using eCommerce_Utility;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = WC.AdminRole)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = WC.AdminRole)]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = WC.AdminRole)]
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

        [Authorize(Roles = WC.AdminRole)]
        public IActionResult Edit(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                Category category;
                category = _catRepo.Find(Guid.Parse(id));

                return View("Index", category);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = WC.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _catRepo.Update(category);
                _catRepo.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [Authorize(Roles = WC.AdminRole)]
        [HttpGet]
        public IActionResult GetCategoryList()
        {
            return Json(new { data = _catRepo.GetAll() });
        }

        [Authorize(Roles = WC.AdminRole)]
        [HttpGet]
        public IActionResult DeleteCategory(Guid id)
        {
            if(id!=Guid.Empty)
            {
                _catRepo.Remove(_catRepo.Find(id));
                _catRepo.Save();
                return Ok();
            }
            return NoContent();
        }
        #endregion
    }
}

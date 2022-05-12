using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;
using simpleCommerce_Utility;

namespace simpleCommerce.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class CategoryController : Controller
    {
        private ICategoryRepository _catRepo;
        public CategoryController(ICategoryRepository catRepo)
        {
            _catRepo = catRepo;
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
            if (ModelState.IsValid)
            {
                _catRepo.Add(category);
                _catRepo.Save();
                TempData[WC.Success] = "Category Created Successfully";
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                Category category;
                category = _catRepo.Find(guid);

                return View("Index", category);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _catRepo.Update(category);
                _catRepo.Save();
                TempData[WC.Success] = "Category Updated Successfully";
            }
            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetCategoryList()
        {
            return Json(new { data = _catRepo.GetAll() });
        }

        [HttpGet]
        public IActionResult DeleteCategory(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                _catRepo.Remove(_catRepo.Find(guid));
                _catRepo.Save();
                return Ok();
            }
            return NoContent();
        }
        #endregion
    }
}

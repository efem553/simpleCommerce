using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;
using simpleCommerce_Utility;

namespace simpleCommerce.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepo;
        public TagController(ITagRepository tagRepo)
        {
            _tagRepo = tagRepo;
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
        public IActionResult Add(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _tagRepo.Add(tag);
                _tagRepo.Save();
                TempData[WC.Success] = "Tag Created Successfully";
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                Tag tag;
                tag = _tagRepo.Find(guid);

                return View("Index", tag);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _tagRepo.Update(tag);
                _tagRepo.Save();
                TempData[WC.Success] = "Tag Updated Successfully";
            }
            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetTagList()
        {
            return Json(new { data = _tagRepo.GetAll() });
        }

        [HttpGet]
        public IActionResult DeleteTag(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                _tagRepo.Remove(_tagRepo.Find(guid));
                _tagRepo.Save();
                return Ok();
            }
            return NoContent();
        }
        #endregion
    }
}

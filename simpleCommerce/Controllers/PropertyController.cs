using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;
using simpleCommerce_Utility;

namespace simpleCommerce.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class PropertyController : Controller
    {
        private IPropertyRepository _propRep;

        public PropertyController(IPropertyRepository propRep)
        {
            _propRep = propRep;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Property property)
        {
            if (ModelState.IsValid)
            {
                _propRep.Add(property);
                _propRep.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Index", property);
            }
        }

        [HttpPost]
        public IActionResult Update(Property property)
        {
            if (ModelState.IsValid)
            {
                _propRep.Update(property);
                _propRep.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Index", property);
            }
        }

        public IActionResult Edit(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                Property property;
                property = _propRep.Find(Guid.Parse(id));

                return View("Index", property);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Property property)
        {
            if (ModelState.IsValid)
            {
                _propRep.Update(property);
                _propRep.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetPropertyList()
        {
            return Json(new { data = _propRep.GetAll() });
        }
        [HttpGet]
        public IActionResult DeleteProperty(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                _propRep.Remove(_propRep.Find(guid));
                _propRep.Save();
                return Ok();
            }
            return NoContent();
        }
        #endregion
    }
}

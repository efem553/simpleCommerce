using eCommerce_Utility;
using Microsoft.AspNetCore.Authorization;
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
        private IPictureRepository _pictRepo;
        public ProductController(IProductRepository prodRepo,ICategoryRepository catRepo, IPictureRepository pictRepo)
        {
            _prodRepo = prodRepo;
            _catRepo = catRepo; 
            _pictRepo = pictRepo;
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
            if (ModelState.IsValid)
            {
                
                
                _prodRepo.Add(productVM.Product);
                _prodRepo.Save();

                
                if (Request.Form.Files.Count() > 0)
                {
                    List<Picture> pictureList = new List<Picture>();
                    foreach (var file in Request.Form.Files)
                    {
                        byte[] imageBase64Data;

                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);
                        imageBase64Data = ms.ToArray();

                        ms.Close();
                        ms.Dispose();
                        pictureList.Add(new Picture
                        {
                            PictureId = Guid.NewGuid(),
                            ProductId = productVM.Product.Id,
                            ImageData = imageBase64Data
                        });

                    }
                    _pictRepo.AddRange(pictureList);
                    _pictRepo.Save();

                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Index", productVM);
            }
            
        }

        #region API CALLS
        [Authorize(Roles = WC.AdminRole)]
        [HttpGet]
        public IActionResult GetProductList()
        {
            return Json(new { data = _catRepo.GetAll() });
        }

        [Authorize(Roles = WC.AdminRole)]
        [HttpGet]
        public IActionResult DeleteProduct(Guid id)
        {
            if (id != Guid.Empty)
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

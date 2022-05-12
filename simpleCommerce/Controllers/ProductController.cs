using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;
using simpleCommerce_Models.ViewModels;
using simpleCommerce_Utility;
using System.Text.Json;

namespace simpleCommerce.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ProductController : Controller
    {
        private IProductRepository  _prodRepo;
        private ICategoryRepository _catRepo;
        private IPictureRepository  _pictRepo;
        private IPropertyRepository _propRepo;
        private ITagRepository      _tagRepo;
        private IProductPropertyRepository _productPropertyRepo;
        private IProductTagRepository _productTagRepo;
        public ProductController(IProductRepository prodRepo, ICategoryRepository catRepo, IPictureRepository pictRepo, IPropertyRepository propRepo, ITagRepository tagRepo, IProductPropertyRepository productPropertyRepo, IProductTagRepository productTagRepo)
        {
            _prodRepo   = prodRepo;
            _catRepo    = catRepo;
            _pictRepo   = pictRepo;
            _propRepo   = propRepo;
            _tagRepo    = tagRepo;
            _productPropertyRepo = productPropertyRepo;
            _productTagRepo = productTagRepo;
        }
        public IActionResult Index()
        {
            CreateProductVM createProductVM = new CreateProductVM();
            createProductVM.CategorySelectList = _catRepo.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = Convert.ToString(x.Id)
            }).OrderBy(x => x.Text);
            createProductVM.PropertySelectList = _propRepo.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = Convert.ToString(x.PropertyId)
            }).OrderBy(x => x.Text);

            createProductVM.TagSelectList = _tagRepo.GetAll().Select(x=> new SelectListItem
            {
                Text=x.Name,
                Value=Convert.ToString(x.TagId)
            }).OrderBy(x => x.Text);
            return View(createProductVM);
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
                    TempData[WC.Success] = "Product Created Successfully";
                }
                if(!string.IsNullOrEmpty(productVM.PropertyJSON))
                {
                    IList<ProductProperty> productProperties= JsonObjects.ConvertToProductProperty(productVM.Product.Id, productVM.PropertyJSON);
                    _productPropertyRepo.AddRange(productProperties);
                    _productPropertyRepo.Save();
                }
                if (!string.IsNullOrEmpty(productVM.TagJSON))
                {
                    IList<ProductTag> productTags = JsonObjects.ConvertToProductTag(productVM.Product.Id, productVM.TagJSON);
                    _productTagRepo.AddRange(productTags);
                    _productTagRepo.Save();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Index", productVM);
            }

        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Guid productId = id;
            if (productId!=Guid.Empty)
            {
                CreateProductVM createProductVM = new CreateProductVM();
                createProductVM.Product = _prodRepo.Find(productId);
                createProductVM.CategorySelectList = _catRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = Convert.ToString(x.Id)
                }).OrderBy(x => x.Text);
                createProductVM.PropertySelectList = _propRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = Convert.ToString(x.PropertyId)
                }).OrderBy(x => x.Text);

                createProductVM.TagSelectList = _tagRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = Convert.ToString(x.TagId)
                }).OrderBy(x => x.Text);
                return View("Index", createProductVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(CreateProductVM createProductVM)
        {
            if (createProductVM.Product.Id != Guid.Empty)
            {
                if (ModelState.IsValid)
                {


                    _prodRepo.Update(createProductVM.Product);
                    _prodRepo.Save();

                    if (Request.Form.Files.Count() > 0)
                    {
                        _pictRepo.RemoveRange(_pictRepo.GetAll(x => x.ProductId == createProductVM.Product.Id));
                        _pictRepo.Save();
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
                                ProductId = createProductVM.Product.Id,
                                ImageData = imageBase64Data
                            });

                        }
                        _pictRepo.AddRange(pictureList);
                        _pictRepo.Save();
                        TempData[WC.Success] = "Product Created Successfully";
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData[WC.Error] = "Something went wrong while updating Product";
                return RedirectToAction(nameof(Index));
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetProductList()
        {
            var a = Json(new { data = _prodRepo.GetAll(includeProperties: "Category") });
            return a;
        }

        [HttpGet]
        public IActionResult DeleteProduct(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                _prodRepo.Remove(_prodRepo.Find(guid));
                _prodRepo.Save();
                return Ok();
            }
            return NoContent();
        }
        [HttpGet]
        public IActionResult GetProductProperty(string id)
        {
            Guid productId;
            if (Guid.TryParse(id, out productId))
            {
                List<JsonObjects.PropertyJSON> propertyJSON = new List<JsonObjects.PropertyJSON>();
                foreach (var item in _productPropertyRepo.GetAll(filter:x => x.ProductId == productId,includeProperties:"Property"))
                {
                    propertyJSON.Add(new JsonObjects.PropertyJSON
                    {
                        propertyId = item.Property.PropertyId,
                        name = item.Property.Name,
                        value = item.Value,
                        button=item.Id
                    });
                }
                return Json(new {data= propertyJSON });
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult GetProductTag(string id)
        {
            Guid productId;
            if (Guid.TryParse(id, out productId))
            {
                List<JsonObjects.TagJSON> tagJSON = new List<JsonObjects.TagJSON>();
                foreach (var item in _productTagRepo.GetAll(filter:x => x.ProductId == productId,includeProperties:"Tag"))
                {
                    tagJSON.Add(new JsonObjects.TagJSON
                    {
                        tagId=item.TagId,
                        name=item.Tag.Name,
                        button = item.Id
                    });
                }
                return Json(new {data= tagJSON });
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult DeleteProductProperty(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                _productPropertyRepo.Remove(_productPropertyRepo.Find(guid));
                _productPropertyRepo.Save();
                return Ok();
            }
            return NoContent();
        }

        [HttpGet]
        public IActionResult DeleteProductTag(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                _productTagRepo.Remove(_productTagRepo.Find(guid));
                _productTagRepo.Save();
                return Ok();
            }
            return NoContent();
        }

        [HttpGet]
        public IActionResult AddProductTag(string productId,string tagId)
        {
            Guid gProductId= new Guid();
            Guid gTagId= new Guid();
            if (Guid.TryParse(productId, out gProductId) && Guid.TryParse(tagId, out gTagId))
            {
                ProductTag productTag = new ProductTag
                {
                    Id=Guid.NewGuid(),
                    TagId=gTagId,
                    ProductId=gProductId,
                };
                _productTagRepo.Add(productTag);
                _productTagRepo.Save();
                return Ok();
            }
            return NoContent();
        }

        [HttpGet]
        public IActionResult AddProductProperty(string productId, string propertyId,string value)
        {
            Guid gProductId = new Guid();
            Guid gPropertyId = new Guid();
            if (Guid.TryParse(productId, out gProductId) && Guid.TryParse(propertyId, out gPropertyId))
            {
                ProductProperty productProperty = new ProductProperty
                {
                    Id = Guid.NewGuid(),
                    PropertyId=gPropertyId,
                    ProductId = gProductId,
                    Value=value
                };
                _productPropertyRepo.Add(productProperty);
                _productPropertyRepo.Save();
                return Ok();
            }
            return NoContent();
        }
        #endregion
    }
}

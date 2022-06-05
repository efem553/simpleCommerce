using Microsoft.AspNetCore.Mvc;
using simpleCommerce.Models;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;
using simpleCommerce_Models.ViewModels;
using simpleCommerce_Utility;
using System.Diagnostics;

namespace simpleCommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _prodRepo;
        private readonly ICategoryRepository _catRepo;
        private readonly ITagRepository _tagRepo;
        private readonly IPictureRepository _pictRepo;
        private readonly IProductTagRepository _productTagRepo;
        private readonly IProductPropertyRepository _productPropRepo;
        private IWebHostEnvironment _webHostEnvironment;


        public HomeController(ILogger<HomeController> logger, IProductRepository prodRepo, ICategoryRepository catRepo, ITagRepository tagRepo, IPictureRepository pictRepo, IProductTagRepository productTagRepo, IProductPropertyRepository productPropRepo, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _prodRepo = prodRepo;
            _catRepo = catRepo;
            _tagRepo = tagRepo;
            _pictRepo = pictRepo;
            _productTagRepo = productTagRepo;
            _productPropRepo = productPropRepo;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index(Guid category, Guid tag, string price, string order, string searchFilter)
        {
            HomePageVM homePageVM = new HomePageVM();
            IEnumerable<ProductTag> productTags = _productTagRepo.GetAll().ToList();
            IQueryable<Product> products = _prodRepo.GetAllQuery(includeProperties: "Category");
            IEnumerable<Tag> tags = _tagRepo.GetAll();
            //Filter for Category
            if (category != Guid.Empty)
            {
                if (products.Where(x => x.CategoryId == category).Any())
                {
                    products = products.Where(x => x.CategoryId == category);
                    homePageVM.FilterProperties.CategoryId = category;
                }
            }
            //Filter for Tag
            if (tag != Guid.Empty)
            {

                Guid[] filteredProductIds = new Guid[0];
                foreach (var item in productTags.Where(t => t.TagId == tag))
                {
                    Array.Resize(ref filteredProductIds, filteredProductIds.Length + 1);
                    filteredProductIds[filteredProductIds.GetUpperBound(0)] = item.ProductId;
                }

                products = products.Where(x => filteredProductIds.Contains(x.Id));

                homePageVM.FilterProperties.TagId = tag;

            }
            //Filter for price with range
            if (!String.IsNullOrEmpty(price) || price?.Split(',').Count() == 2)
            {
                decimal lowestPrice = Convert.ToDecimal(String.Format("{0:0.##}", price.Split(',')[0]));
                decimal highestPrice = Convert.ToDecimal(String.Format("{0:0.##}", price.Split(',')[1]));
                products = products.Where(x => x.Price <= highestPrice && x.Price >= lowestPrice);
                homePageVM.FilterProperties.LowestPrice = lowestPrice;
                homePageVM.FilterProperties.HighestPrice = highestPrice;
            }
            //Product Name filtering
            if (!String.IsNullOrEmpty(searchFilter))
            {
                products = products.Where(x => (x.Name == null ? "" : x.Name).Contains(searchFilter));
                homePageVM.FilterProperties.SearchText = searchFilter;
            }
            //Lets fill Products
            homePageVM.Products = products.Select(x => new ProductCardVM
            {
                ProductId = x.Id,
                CategoryName = x.Category.Name,
                Price = String.Format("{0:0.00}", x.Price),
                ProductName = x.Name
            }).ToList();
            //Ordering Products
            if (!String.IsNullOrEmpty(order))
            {
                short orderBy;
                if (Int16.TryParse(order, out orderBy))
                {
                    if (orderBy == 1)
                    {
                        products = products.OrderBy(x => x.Price);
                    }
                    else if (orderBy == 2)
                    {
                        products = products.OrderByDescending(x => x.Price);
                    }
                    else
                    {
                        //Nothing to do here
                    }
                    homePageVM.FilterProperties.OrderNo = orderBy;
                }
            }

            IQueryable<Picture> pictures = _pictRepo.GetAllQuery();
            //Fetching the first picture of product.
            //If we have any Picture record with productId in item down below.
            foreach (var item in homePageVM.Products)
            {
                if (pictures.Where(x => x.ProductId == item.ProductId).Any())
                {
                    item.Picture = pictures.Where(x => x.ProductId == item.ProductId).First()?.ImageData;
                }
                else
                {
                    //If we dont have any picture with that productId, fetching default 
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string finalImagePath;
                    finalImagePath = webRootPath + WC.NoImageFilePath;
                    string imageBase64;
                    string imageExtension;
                    if (!String.IsNullOrEmpty(finalImagePath))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        using (FileStream file = new FileStream(finalImagePath, FileMode.Open, FileAccess.Read))
                        {
                            byte[] bytes = new byte[file.Length];
                            file.Read(bytes, 0, (int)file.Length);
                            ms.Write(bytes, 0, (int)file.Length);
                            imageBase64 = Convert.ToBase64String(ms.ToArray());
                            imageExtension = String.Format("data:image/{0};base64,", Path.GetExtension(finalImagePath).Split(".")[1]);
                        }
                        if (!String.IsNullOrEmpty(imageBase64) || !String.IsNullOrEmpty(imageExtension))
                        {
                            item.Picture = imageExtension + imageBase64;
                        }
                    }

                }

            }
            //Adding All item here
            homePageVM.Categories = new Category[]
            {
                new Category{
                Name = "All",
                FilterName = "all",
                Id= Guid.Empty }
            };
            //Adding all categories
            foreach (var item in _catRepo.GetAll())
            {
                //This is probably worst way to add item to IEnumerable
                homePageVM.Categories = homePageVM.Categories.Concat(new[] { item });
            }
            //Adding All tag item
            homePageVM.Tags = new Tag[]
            {
                new Tag
                {
                    FilterName = "all",
                    Name = "All",
                    TagId = Guid.Empty
                }
            };
            //Adding all tags
            foreach (var item in _tagRepo.GetAll())
            {
                homePageVM.Tags = homePageVM.Tags.Concat(new[] { item });
            }
            //Returning to Index View with ModelView
            return View(homePageVM);
        }

        public IActionResult SingleProduct(Guid productId)
        {
            if (productId != null && productId != Guid.Empty)
            {
                if (_prodRepo.GetAll(x => x.Id == productId).Any())
                {
                    SingleProductVM singleProductVM = new SingleProductVM();
                    Product product = _prodRepo.GetAll(x => x.Id == productId, includeProperties: "Category").First();
                    singleProductVM.Product = product;
                    if (_productTagRepo.GetAll(x => x.ProductId == product.Id).Any())
                    {
                        List<Tag> productTags = new List<Tag>();
                        foreach (var item in _productTagRepo.GetAll(x => x.ProductId == product.Id, includeProperties: "Tag"))
                        {
                            productTags.Add(item.Tag);
                        }
                        singleProductVM.Tags = productTags;
                    }

                    if (_productPropRepo.GetAll(x => x.ProductId == product.Id).Any())
                    {
                        List<ProductProperty> productProperties = new List<ProductProperty>();
                        foreach (var item in _productPropRepo.GetAll(x => x.ProductId == product.Id, includeProperties: "Property"))
                        {
                            productProperties.Add(item);
                        }
                        singleProductVM.Properties = productProperties;
                    }

                    if (_pictRepo.GetAll(x => x.ProductId == product.Id).Any())
                    {
                        List<string> picturesBase64 = new List<string>();
                        foreach (var item in _pictRepo.GetAll(x => x.ProductId == product.Id))
                        {
                            if (!String.IsNullOrEmpty(item.ImageData))
                                picturesBase64.Add(item.ImageData);
                        }
                        singleProductVM.Pictures = picturesBase64;
                    }

                    return View(singleProductVM);
                }
                else
                {
                    TempData[WC.Error] = "Sorry but we cant show you details of Product";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData[WC.Error] = "Bad Request";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
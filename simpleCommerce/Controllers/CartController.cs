using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;
using simpleCommerce_Models.ViewModels;
using simpleCommerce_Utility;
using System.Security.Claims;
using static simpleCommerce_Utility.JsonObjects;

namespace simpleCommerce.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;
        private readonly ICartItemRepository _cartItemRepo;
        private readonly IProductRepository _prodRepo;
        private readonly IPictureRepository _pictRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderLineRepository _orderLineRepo;
        private readonly IProvinceRepository _provinceRepo;
        private readonly UserManager<IdentityUser> _userManager;


        public CartController(ICartRepository cartRepo, ICartItemRepository cartItemRepo, IProductRepository prodRepo, IPictureRepository pictRepo, IOrderRepository orderRepo, IOrderLineRepository orderLineRepo, IProvinceRepository provinceRepo, UserManager<IdentityUser> userManager)
        {
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _prodRepo = prodRepo;
            _userManager = userManager;
            _pictRepo = pictRepo;
            _orderRepo = orderRepo;
            _orderLineRepo = orderLineRepo;
            _provinceRepo = provinceRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            decimal subtotal = 0;
            CartVM cartVM = new CartVM();
            //Takes User Id and checks it
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                //If there any cart for user, takes Id down below
                if (_cartRepo.GetAll(x => x.UserId == userId).Count() > 0)
                {
                    Guid cartId = _cartRepo.FirstOrDefault(x => x.UserId == userId).CartId;
                    //If CartId is not null, creates a List of CarItemLine and adds items with required values here
                    if (cartId != null && cartId != Guid.Empty)
                    {
                        List<CartItemLineVM> lines = new List<CartItemLineVM>();
                        //Grabs all items in cart
                        foreach (var item in _cartItemRepo.GetAll(x => x.CartId == cartId, includeProperties: "Product"))
                        {
                            string pictureBase64 = "";
                            //Grabs first picture of Product.
                            if (_pictRepo.GetAll(x => x.ProductId == item.ProductId).Any())
                            {
                                //In ProductController there is logically need to be at least one record.(No Image)
                                pictureBase64 = _pictRepo.FirstOrDefault(x => x.ProductId == item.ProductId).ImageData;
                            }
                            //Adds line to list. it calculates line total 
                            lines.Add(new CartItemLineVM
                            {
                                CartItemId = item.CartItemId,
                                ProductId = item.ProductId,
                                ProductName = item.Product.Name,
                                Price = item.Product.Price,
                                Quantity = item.Count,
                                LineTotal = item.Count * item.Product.Price,
                                Picture = pictureBase64
                            });
                            //Sums every single line for subtotal
                            subtotal = subtotal + (item.Count * item.Product.Price);
                        }
                        cartVM.CartItems = lines;
                        cartVM.Subtotal = subtotal;
                        return View(cartVM);
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }


        #region API Calls
        [HttpGet]
        public IActionResult Additem(Guid ProductId, int Count)
        {
            //Checks if cart id null or equals to empty or count greater than 0
            if (ProductId != null && ProductId != Guid.Empty && Count > 0)
            {
                //Checks is there any product with this Id
                if (_prodRepo.GetAll(x => x.Id == ProductId).Any())
                {
                    //Takes User Id and checks it
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (userId != null)
                    {
                        Guid CartId;
                        //Checks is there any cart for user. If there is no cart for user creates it.
                        if (_cartRepo.GetAll(x => x.UserId == userId).Any())
                        {
                            CartId = _cartRepo.FirstOrDefault(x => x.UserId == userId).CartId;
                        }
                        else
                        {
                            Cart cart = new Cart
                            {
                                CartId = Guid.NewGuid(),
                                UserId = userId
                            };
                            CartId = cart.CartId;
                            _cartRepo.Add(cart);
                            _cartRepo.Save();
                        }
                        //If cartId is not null creates item
                        if (CartId != null && CartId != Guid.Empty)
                        {
                            //Check is there any cartItem with that productId
                            if (_cartItemRepo.GetAll(x => x.CartId == CartId && x.ProductId == ProductId).Count() == 1)
                            {
                                CartItem cartItem = _cartItemRepo.FirstOrDefault(x => x.CartId == CartId && x.ProductId == ProductId);
                                cartItem.Count = cartItem.Count + Count;
                                _cartItemRepo.Update(cartItem);
                                _cartItemRepo.Save();
                            }
                            //If there is not creates new one
                            else
                            {
                                CartItem cartItem = new CartItem
                                {
                                    CartItemId = Guid.NewGuid(),
                                    CartId = CartId,
                                    ProductId = ProductId,
                                    Count = Count
                                };
                                _cartItemRepo.Add(cartItem);
                                _cartItemRepo.Save();
                            }

                        }
                        return Ok();
                    }
                }
            }
            return NotFound();
        }
        [HttpGet]
        public int GetCartItemCount()
        {
            //Takes User Id and checks it
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                Guid CartId;
                //Checks is there any cart for user. If there is no cart for user creates it.
                if (_cartRepo.GetAll(x => x.UserId == userId).Any())
                {
                    CartId = _cartRepo.FirstOrDefault(x => x.UserId == userId).CartId;
                    if (CartId != null && CartId != Guid.Empty)
                    {
                        return _cartItemRepo.GetAll(x => x.CartId == CartId).Count();
                    }
                }
            }
            return 0;
        }

        [HttpGet]
        public IActionResult Update(string cartItemsJSON)
        {
            if (!String.IsNullOrEmpty(cartItemsJSON))
            {
                List<CartItemJSON> cartItems = JsonObjects.ConvertToCartItem(cartItemsJSON);
                if (cartItems.Count > 0)
                {
                    if (cartItems.Where(x => x.quantity == 0).Any())
                    {
                        foreach (var item in cartItems.Where(x => x.quantity == 0))
                        {
                            var cartItem = _cartItemRepo.Find(item.cartItemId);
                            if (cartItem != null)
                            {
                                _cartItemRepo.Remove(cartItem);
                            }
                        }
                        _cartItemRepo.Save();
                    }
                    if (cartItems.Where(x => x.quantity > 0).Any())
                    {
                        foreach (var item in cartItems.Where(x => x.quantity != 0))
                        {
                            var cartItem = _cartItemRepo.Find(item.cartItemId);
                            if (cartItem != null && item.quantity > 0)
                            {
                                cartItem.Count = item.quantity;
                                _cartItemRepo.Update(cartItem);
                            }
                        }
                        _cartItemRepo.Save();
                    }
                }
                return Ok();
            }

            return BadRequest();
        }
        #endregion

        [HttpGet]
        public IActionResult ProceedToCheckOut()
        {
            decimal subtotal = 0;
            CartVM cartVM = new CartVM();
            //Takes User Id and checks it
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                //If there any cart for user, takes Id down below
                if (_cartRepo.GetAll(x => x.UserId == userId).Count() > 0)
                {
                    Guid cartId = _cartRepo.FirstOrDefault(x => x.UserId == userId).CartId;
                    //If CartId is not null, creates a List of CarItemLine and adds items with required values here
                    if (cartId != null && cartId != Guid.Empty)
                    {
                        if (!_cartItemRepo.GetAll().Any())
                        {
                            TempData[WC.Error] = "Your Cart is empty!";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            IEnumerable<CheckoutLineVM> checkoutLines = _cartItemRepo.GetAll(x => x.CartId == cartId, includeProperties: "Product").Select(x => new CheckoutLineVM
                            {
                                ProductName = x.Product.Name,
                                Quantity = x.Count,
                                LineTotal = x.Product.Price * x.Count
                            });

                            CheckoutTotalVM checktoutTotal = new CheckoutTotalVM
                            {
                                Items = checkoutLines,
                                Total = checkoutLines.Sum(x => x.LineTotal)

                            };

                            CheckoutVM checkoutVM = new CheckoutVM
                            {
                                Order = new Order() { UserId = userId },
                                CheckoutTotalVM = checktoutTotal,
                                ProvinceSelectList = _provinceRepo.GetAll().Select(x => new SelectListItem
                                {
                                    Text = x.Name,
                                    Value = x.Id.ToString()
                                })
                            };

                            return View("Checkout", checkoutVM);
                        }
                    }
                }
            }
            return RedirectToAction("Checkout");
        }
        [HttpPost]
        public IActionResult Checkout(CheckoutVM checkoutVM)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                if (userId == checkoutVM.Order.UserId)
                {
                    if (ModelState.IsValid)
                    {
                        checkoutVM.Order.CreationDate = DateOnly.FromDateTime(DateTime.Now);
                        _orderRepo.Add(checkoutVM.Order);
                        _orderRepo.Save();
                    }
                    if (checkoutVM.Order.OrderId != null && checkoutVM.Order.OrderId != Guid.Empty)
                    {
                        Guid CartId = _cartRepo.GetAll(x => x.UserId == userId).First().CartId;
                        if (CartId != null && CartId != Guid.Empty)
                        {
                            IEnumerable<CartItem> cartItems = _cartItemRepo.GetAll(x => x.CartId == CartId, includeProperties: "Product");
                            if (cartItems != null)
                            {
                                _orderLineRepo.AddRange(cartItems.Select(x => new OrderLine
                                {
                                    OrderId = checkoutVM.Order.OrderId,
                                    ProductId = x.ProductId,
                                    Price = x.Product.Price,
                                    ProductName = x.Product.Name,
                                    Quantity = x.Count
                                }));
                                _orderLineRepo.Save();

                                _cartItemRepo.RemoveRange(cartItems);
                                _cartItemRepo.Save();
                                return RedirectToAction("Confirmation", new
                                {
                                    orderId = checkoutVM.Order.OrderId
                                });
                            }

                        }
                    }
                }
            }

            return View(checkoutVM);
        }
        [HttpGet]
        public IActionResult Confirmation(Guid orderId){
            if (orderId!=null && orderId!=Guid.Empty)
            {
                IEnumerable<CheckoutLineVM> checkoutLines = _orderLineRepo.GetAll(x => x.OrderId == orderId, includeProperties: "Product").Select(x => new CheckoutLineVM
                {
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    LineTotal = x.Price * x.Quantity
                });

                CheckoutTotalVM checktoutTotal = new CheckoutTotalVM
                {
                    Items = checkoutLines,
                    Total = checkoutLines.Sum(x => x.LineTotal)

                };

                ConfirmationVM confirmationVM = new ConfirmationVM
                {
                    Order = _orderRepo.Find(orderId),
                    CheckoutTotalVM = checktoutTotal
                };
                return View(confirmationVM);
            }
            return RedirectToAction("Index", "Home");

        }
    }
}

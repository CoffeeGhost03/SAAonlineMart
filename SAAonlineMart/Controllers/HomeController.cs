using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SAAonlineMart.Data;
using SAAonlineMart.Models;
using System.Diagnostics;

namespace SAAonlineMart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SAAonlineMartContext _context;

        public HomeController(ILogger<HomeController> logger, SAAonlineMartContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var product = _context.Products.Find(productId);

            if (product != null)
            {
                var cart = GetCartItems();
                var cartItem = cart.SingleOrDefault(c => c.CProductId == productId);

                if (cartItem == null)
                {
                    cart.Add(new CartItemViewModel
                    {
                        CProductId = product.ProductId,
                        CProductName = product.ProductName,
                        CProductPrice = product.ProductPrice,
                        Quantity = 1
                    });
                }
                else
                {
                    cartItem.Quantity++;
                }

                SaveCartItems(cart);
            }

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult UpdateCart(int productId, int quantity)
        {
            var cart = GetCartItems();
            var cartItem = cart.SingleOrDefault(c => c.CProductId == productId);

            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.Quantity = quantity;
                }
                else
                {
                    cart.Remove(cartItem);
                }

                SaveCartItems(cart);
            }

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCartItems();
            var cartItem = cart.SingleOrDefault(c => c.CProductId == productId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                SaveCartItems(cart);
            }

            return RedirectToAction("Cart");
        }

        public IActionResult Cart()
        {
            var cart = GetCartItems();
            return View(cart);
        }

        public IActionResult Checkout()
        {
            var cart = GetCartItems();
            if (cart.Count == 0)
            {
                return RedirectToAction("Index");
            }

            // Create and save order
            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = cart.Sum(item => item.CProductPrice * item.Quantity),
                OrderItems = cart.Select(item => new OrderItem
                {
                    ProductId = item.CProductId,
                    Quantity = item.Quantity,
                    Price = item.CProductPrice
                }).ToList()
            };

            _context.Order.Add(order);
            _context.SaveChanges();

            // Clear the cart after checkout
            HttpContext.Session.Remove("Cart");

            // Redirect to confirmation page with order details
            return View(new OrderViewModel
            {
                OrderId = order.OrderId,
                Items = cart,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate
            });
        }


        private List<CartItemViewModel> GetCartItems()
        {
            var sessionCart = HttpContext.Session.GetString("Cart");
            return sessionCart == null ? new List<CartItemViewModel>() : JsonConvert.DeserializeObject<List<CartItemViewModel>>(sessionCart);
        }

        private void SaveCartItems(List<CartItemViewModel> cart)
        {
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
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

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

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var products = _context.Products.ToList();
            var product = products.Find(p => p.ProductId == productId);

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

        public IActionResult Cart()
        {
            var cart = GetCartItems();
            return View(cart);
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

        private List<Products> products = new List<Products>();
        
        public IActionResult Details(int id)
        {
            
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

      
    }
}

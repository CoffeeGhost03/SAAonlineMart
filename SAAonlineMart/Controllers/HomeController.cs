using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var product = products.Find(p => p.ProductId == id);
            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            // Simulate adding the product to the cart (you would normally store this in session or a database)
            var product = products.Find(p => p.ProductId == productId);
            if (product != null)
            {
                // For now, just redirect to the cart view (you'll need to implement this view and logic)
                return RedirectToAction("Cart");
            }

            return RedirectToAction("Details", new { id = productId });
        }

        public IActionResult Cart()
        {
            // You would return the cart view here, showing all items added to the cart
            return View();
        }
    }
}

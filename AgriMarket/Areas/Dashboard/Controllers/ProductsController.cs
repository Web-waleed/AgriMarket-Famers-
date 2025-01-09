using AgriMarket.Data;
using AgriMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriMarket.Areas.Dashboard.Controllers
{
    [Area("Dashboard")] 
    public class ProductsController : Controller
    {
        private  AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            
            var pendingProducts = _context.products
                .Where(o => o.Status == FarmerProductStatus.Pending)
                .ToList();
           var acceptedProducts = _context.products
                .Where(o => o.Status == FarmerProductStatus.Accepted)
                .ToList();
            var rejectedProducts = _context.products
                .Where(o => o.Status == FarmerProductStatus.Rejected)
                .ToList();

            ViewBag.PendingProducts = pendingProducts;
            ViewBag.AcceptedProducts = acceptedProducts;
            ViewBag.RejectedProducts = rejectedProducts;

            return View();
        }
        public async Task<IActionResult> Pend(int id)
        {
            var Product = await _context.products.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }

            Product.Status = FarmerProductStatus.Pending;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Accept(int id)
        {
            var Product = await _context.products.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }

            Product.Status = FarmerProductStatus.Accepted;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Reject(int id)
        {
            var Product = _context.products.Find(id);
            if (Product != null)
            {
                Product.Status = FarmerProductStatus.Rejected;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var Product = _context.products.Find(id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }
    }
}

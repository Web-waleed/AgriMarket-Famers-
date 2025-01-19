using AgriMarket.Data;
using AgriMarket.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriMarket.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Dashboard")]
    public class OrderDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public OrderDetailsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Orders()
        {
            var orders = _context.OrderProducts
                .Include(o => o.Products)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderDetailViewModel
                {
                    Id = o.Id,
                    FullName = o.FullName,
                    Email = o.Email,
                    PhoneNumber = o.PhoneNumber,
                    Address = o.Address,
                    OrderDate = o.OrderDate,
                    Status = o.Status, 
                    Products = o.Products.Select(p => new OrderDetailViewModel.Product
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Quantity = p.Quantity
                    }).ToList()
                }).ToList();

            return View(orders);
        }
        [HttpPost]
        public IActionResult UpdateStatus(int orderId, string status)
        {
            var order = _context.OrderProducts.Find(orderId);
            if (order != null)
            {
                order.Status = status;
                _context.SaveChanges();
            }

            return RedirectToAction("Orders");
        }
        [HttpPost]
        public IActionResult DeleteOrder(int orderId)
        {
            var order = _context.OrderProducts
                .Include(o => o.Products) 
                .FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {

                _context.OrderProducts.Remove(order);
                _context.SaveChanges();
            }

            return RedirectToAction("Orders");
        }
    }
}

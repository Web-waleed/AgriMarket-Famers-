using AgriMarket.Data;
using AgriMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AgriMarket.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _Context;
        private UserManager<IdentityUser> _UserManager;
        private RoleManager<IdentityRole> _RoleManager;
        public HomeController(AppDbContext Context, UserManager<IdentityUser> UserManager , RoleManager<IdentityRole> RoleManager)
        {
            _Context = Context;
            _UserManager= UserManager;
            _RoleManager= RoleManager;
        }
        public IActionResult Index()
        {
            ViewBag.TotalProduct= _Context.Products.Count();
            ViewBag.TotalUser= _Context.Users.Count();
            ViewBag.TotalFeedback=_Context.contactIUs.Count();
            ViewBag.TotalSales=_Context.productItems.Sum(p=>p.Quantity*p.Price);
            ViewBag.TotalFarmers = _Context.UserRoles.Where(f => f.RoleId == "69e6f5da-e45d-4edf-b9d0-7855673d4e18") .Count();
            return View();
        }
        public IActionResult SubmitProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubmitProduct(Product products)
        {
            if (ModelState.IsValid)
            {
                products.Status = FarmerProductStatus.Pending;

                if (products.ImageFile != null && products.ImageFile.Length > 0)
                {
                    // Define the path to save the image
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var fileName = Path.GetFileName(products.ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Ensure the uploads folder exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await products.ImageFile.CopyToAsync(fileStream);
                    }

                    // Set the image path in the model (you may want to adjust this based on your requirements)
                    products.ProductImg = $"/images/{fileName}";


                    _Context.Products.Add(products);
                    await _Context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                return BadRequest(new { message = "No file uploaded." });
            }

            return View(products);
        }
    }
}

using AgriMarket.Data;
using AgriMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriMarket.Controllers
{
    [Authorize(Roles = "Farmer,Admin")]
    public class ProductController : Controller
    {
        private AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string farmerEmail = User.Identity.Name;
            var farmer = _context.Farmers.FirstOrDefault(f => f.FarmerEmail == farmerEmail);
            var products = _context.Products
                .Where(p => p.FarmerId == farmer.FarmerId && p.Status == FarmerProductStatus.Accepted)
                .ToList();

            return View(products);
        }
        [AllowAnonymous] 
        public IActionResult Details(int id)
        {
            
            var product = _context.Products
                .Include(p => p.Farmer) 
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product); 
        }

        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Product product)
        {
            if (ModelState.IsValid)
            {
                
                string farmerEmail = User.Identity.Name;

                
                var farmer = _context.Farmers.FirstOrDefault(f => f.FarmerEmail == farmerEmail);
                if (farmer == null)
                {
                    return Unauthorized("Farmer not found for the logged-in user.");
                }

               
                product.FarmerId = farmer.FarmerId;

                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    try
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                      
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await product.ImageFile.CopyToAsync(fileStream);
                        }

                        
                        product.ProductImg = $"/images/{fileName}";
                    }
                    catch (Exception ex)
                    {
                        
                        Console.WriteLine($"Error uploading file: {ex.Message}");
                        ModelState.AddModelError(string.Empty, "An error occurred while uploading the file. Please try again.");
                        return View(product);
                    }
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Please upload a valid image file.");
                    return View(product);
                }

                
                _context.Add(product);
                await _context.SaveChangesAsync();

                TempData["PendingAlert"] = true;
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var organization = await _context.Products.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            return View(organization);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }


            string farmerEmail = User.Identity.Name;

            
            var farmer = _context.Farmers.FirstOrDefault(f => f.FarmerEmail == farmerEmail);
            if (farmer == null)
            {
                return Unauthorized("Farmer not found for the logged-in user.");
            }

            
            product.FarmerId = farmer.FarmerId;

            if (ModelState.IsValid)
            {
                try
                {
                    
                    if (product.ImageFile != null && product.ImageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await product.ImageFile.CopyToAsync(fileStream);
                        }

                        product.ProductImg = $"/images/{fileName}";
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Organizations/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
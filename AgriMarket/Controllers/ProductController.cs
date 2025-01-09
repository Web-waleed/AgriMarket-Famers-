using AgriMarket.Data;
using AgriMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriMarket.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            var products=_context.products
                .Where(x => x.Status == FarmerProductStatus.Accepted).ToList();
            
            return View(products);
        }
        public IActionResult Details(int id)
        {
            var products = _context.products.Find(id);

            if (products == null)
            {
                return NotFound();
            }

            if (products.Status == FarmerProductStatus.Accepted)
            {
                return View(products);
            }
            else if (products.Status == FarmerProductStatus.Pending)
            {
                return RedirectToAction("PendingMessage");
            }
            else if (products.Status == FarmerProductStatus.Rejected)
            {
                return RedirectToAction("RejectedMessage");
            }

            return View(products);
        }
       
        public IActionResult Create()
        {

            return View();
        }

        // POST: Organizations/Create
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    try
                    {

                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        var fileName = Path.GetFileName(product.ImageFile.FileName);
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


                        _context.Add(product);
                        await _context.SaveChangesAsync();


                        TempData["PendingAlert"] = true;


                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError(string.Empty, "An error occurred while uploading the file.");
                    }
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Please upload a valid image file.");
                }
            }


            return View(product);
        }


        // GET: Organizations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var organization = await _context.products.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            var product = await _context.products.FindAsync(id);
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
            var product = await _context.products.FindAsync(id);
            if (product != null)
            {
                _context.products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.products.Any(e => e.ProductId == id);
        }
    }
}
  
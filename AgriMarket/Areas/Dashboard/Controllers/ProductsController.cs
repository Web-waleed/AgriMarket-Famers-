using AgriMarket.Data;
using AgriMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriMarket.Areas.Dashboard.Controllers
{
    [Area("Dashboard")] 
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.products.ToListAsync());
        }

        // GET: Dashboard/products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Product = await _context.products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (Product == null)
            {
                return NotFound();
            }

            return View(Product);
        }

        // GET: Dashboard/products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product products)
        {
            if (ModelState.IsValid)
            {
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

                    // Save to the database
                    _context.products.Add(products);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                return BadRequest(new { message = "No file uploaded." });
            }

            return View(products);
        }

        // GET: Dashboard/products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Product = await _context.products.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }

            return View(Product);
        }

        // POST: Dashboard/products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product products)
        {
            if (id != products.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing BestSeller from the database
                    var existingproducts = await _context.products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
                    if (existingproducts == null)
                    {
                        return NotFound();
                    }

                    // Check if an image file has been uploaded
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

                        // Save the new image file to the server
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await products.ImageFile.CopyToAsync(fileStream);
                        }

                        // Update the image path in the model
                        products.ProductImg = $"/images/{fileName}";
                    }
                    else
                    {
                        // If no new file is uploaded, retain the existing image path
                        products.ProductImg = existingproducts.ProductImg;
                    }

                    // Update the product in the database
                    _context.Entry(products).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductId))
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

            return View(products);
        }
        // GET: Dashboard/BestSeller/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Dashboard/BestSeller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.products.FindAsync(id);
            if (products != null)
            {
                _context.products.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.products.Any(e => e.ProductId == id);
        }
    }
}

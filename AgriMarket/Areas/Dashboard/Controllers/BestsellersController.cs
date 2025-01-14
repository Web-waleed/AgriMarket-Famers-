using AgriMarket.Data;
using AgriMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriMarket.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin")]
    public class BestsellersController : Controller
    {
        private readonly AppDbContext _context;
        public BestsellersController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.BestSeller.ToListAsync());
        }

        // GET: Dashboard/BestSeller/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var BestSeller = await _context.BestSeller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (BestSeller == null)
            {
                return NotFound();
            }

            return View(BestSeller);
        }

        // GET: Dashboard/BestSeller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/BestSeller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BestSeller BestSeller)
        {
            if (ModelState.IsValid)
            {
                if (BestSeller.ImageFile != null && BestSeller.ImageFile.Length > 0)
                {
                    // Define the path to save the image
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var fileName = Path.GetFileName(BestSeller.ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Ensure the uploads folder exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await BestSeller.ImageFile.CopyToAsync(fileStream);
                    }

                    // Set the image path in the model (you may want to adjust this based on your requirements)
                    BestSeller.ProductImg = $"/images/{fileName}";

                    // Save to the database
                    _context.BestSeller.Add(BestSeller);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                return BadRequest(new { message = "No file uploaded." });
            }

            return View(BestSeller);
        }

        // GET: Dashboard/BestSeller/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var BestSeller = await _context.BestSeller.FindAsync(id);
            if (BestSeller == null)
            {
                return NotFound();
            }

            return View(BestSeller);
        }

        // POST: Dashboard/Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BestSeller BestSeller)
        {
            if (id != BestSeller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing BestSeller from the database
                    var existingBestSeller = await _context.BestSeller.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                    if (existingBestSeller == null)
                    {
                        return NotFound();
                    }

                    // Check if an image file has been uploaded
                    if (BestSeller.ImageFile != null && BestSeller.ImageFile.Length > 0)
                    {
                        // Define the path to save the image
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        var fileName = Path.GetFileName(BestSeller.ImageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        // Ensure the uploads folder exists
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Save the new image file to the server
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await BestSeller.ImageFile.CopyToAsync(fileStream);
                        }

                        // Update the image path in the model
                        BestSeller.ProductImg = $"/images/{fileName}";
                    }
                    else
                    {
                        // If no new file is uploaded, retain the existing image path
                        BestSeller.ProductImg = existingBestSeller.ProductImg;
                    }

                    // Update the product in the database
                    _context.Entry(BestSeller).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestSellerExists(BestSeller.Id))
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

            return View(BestSeller);
        }
        // GET: Dashboard/BestSeller/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var BestSeller = await _context.BestSeller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (BestSeller == null)
            {
                return NotFound();
            }

            return View(BestSeller);
        }

        // POST: Dashboard/BestSeller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var BestSeller = await _context.BestSeller.FindAsync(id);
            if (BestSeller != null)
            {
                _context.BestSeller.Remove(BestSeller);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestSellerExists(int id)
        {
            return _context.BestSeller.Any(e => e.Id == id);
        }
    }
}

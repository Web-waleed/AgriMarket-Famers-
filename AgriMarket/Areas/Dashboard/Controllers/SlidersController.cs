using AgriMarket.Data;
using AgriMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgriMarket.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin")]
    public class SlidersController : Controller
    {
        private readonly AppDbContext _context;

        public SlidersController(AppDbContext context)
        {
            _context = context;
        }
        // GET: Dashboard/Sliders
        public async Task<IActionResult> Index()
        {
            var sliders = _context.sliders
        .Include(s => s.Product) 
        .ToList();
            return View(sliders);
        }

        // GET: Dashboard/Sliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.sliders.Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.SliderID == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Dashboard/Sliders/Create
        public IActionResult Create()
        {
           
            ViewBag.GetProducts = new SelectList(_context.Products, "ProductId", "ProductName");
           
            return View();
           
        }

        // POST: Dashboard/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider Slider)
        {
            if (ModelState.IsValid)
            {
                if (Slider.ImageFile != null && Slider.ImageFile.Length > 0)
                {
                    // Define the path to save the image
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var fileName = Path.GetFileName(Slider.ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Ensure the uploads folder exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Slider.ImageFile.CopyToAsync(fileStream);
                    }

                    // Set the image path in the model (you may want to adjust this based on your requirements)
                    Slider.Sliderimg = $"/images/{fileName}";

                    // Save to the database
                    _context.sliders.Add(Slider);
                    await _context.SaveChangesAsync();
                    ViewBag.GetProducts = new SelectList(_context.Products, "ProductId", "ProductName" );
                    return RedirectToAction(nameof(Index));
                }

                return BadRequest(new { message = "No file uploaded." });
            }

            return View(Slider);
        }

        // GET: Dashboard/Slider/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.sliders.Include(s => s.Product).FirstOrDefaultAsync(s => s.SliderID == id);
            if (slider == null)
            {
                return NotFound();
            }
            ViewBag.GetProducts = new SelectList(_context.Products, "ProductId", "ProductName");
            return View(slider);
        }

        // POST: Dashboard/Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Slider slider)
        {
            if (id != slider.SliderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing product from the database
                    var existingSlider = await _context.sliders.Include(s => s.Product) .AsNoTracking().FirstOrDefaultAsync(p => p.SliderID == id);
                    if (existingSlider == null)
                    {
                        return NotFound();
                    }

                    // Check if an image file has been uploaded
                    if (slider.ImageFile != null && slider.ImageFile.Length > 0)
                    {
                        // Define the path to save the image
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        var fileName = Path.GetFileName(slider.ImageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        // Ensure the uploads folder exists
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Save the new image file to the server
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await slider.ImageFile.CopyToAsync(fileStream);
                        }

                        // Update the image path in the model
                        slider.Sliderimg = $"/images/{fileName}";
                    }
                    else
                    {
                        // If no new file is uploaded, retain the existing image path
                        slider.Sliderimg = existingSlider.Sliderimg;
                    }

                    // Update the product in the database
                    _context.Entry(slider).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.SliderID))
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

            return View(slider);
        }
        // GET: Dashboard/Sliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.sliders.Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.SliderID == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Dashboard/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slider = await _context.sliders.FindAsync(id);
            if (slider != null)
            {
                _context.sliders.Remove(slider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
            return _context.sliders.Include(s => s.Product).Any(e => e.SliderID == id);
        }
    }
}
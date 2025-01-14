using AgriMarket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriMarket.Models.ViewComponents
{
    public class SliderViewComponent :ViewComponent
    {
        private AppDbContext _context;
        public SliderViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var sliders = _context.sliders
               .Include(s => s.Product) 
               .ToList();

            return View(sliders);
        }
    }
}

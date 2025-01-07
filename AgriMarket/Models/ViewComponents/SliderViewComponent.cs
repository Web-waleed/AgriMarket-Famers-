using AgriMarket.Data;
using Microsoft.AspNetCore.Mvc;

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
            return View(_context.sliders.ToList());
        }
    }
}

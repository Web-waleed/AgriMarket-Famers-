using AgriMarket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AgriMarket.Models.ViewComponents
{
    public class ContactViewComponent : ViewComponent
    {
        private AppDbContext _context;
        public ContactViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.contactIUs.ToList());
        }

    }
}

using AgriMarket.Data;
using Microsoft.AspNetCore.Mvc;

namespace AgriMarket.Models.ViewComponents
{
    public class BestSellerViewComponent: ViewComponent
    {
        private AppDbContext context;
        public BestSellerViewComponent(AppDbContext context)
        {
            this.context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(context.BestSeller.ToList());
        }
    }
}

using AgriMarket.Data;
using Microsoft.AspNetCore.Mvc;

namespace AgriMarket.Models.ViewComponents 
{
    public class ProductViewComponent: ViewComponent
    {
        private AppDbContext context;
        public ProductViewComponent(AppDbContext context)
        {
            this.context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(context.products.ToList());
        }
    }
}

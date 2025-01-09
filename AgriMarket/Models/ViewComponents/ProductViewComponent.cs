using AgriMarket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriMarket.Models.ViewComponents 
{
    public class ProductViewComponent: ViewComponent
    {
        private AppDbContext context;
        public ProductViewComponent(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            var acceptedProducts = await context.products
                .Where(p => p.Status == FarmerProductStatus.Accepted)
                .ToListAsync();

            return View(acceptedProducts);
        }
        
    }
}

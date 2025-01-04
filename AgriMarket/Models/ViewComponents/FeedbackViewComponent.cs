using AgriMarket.Data;
using Microsoft.AspNetCore.Mvc;

namespace AgriMarket.Models.ViewComponents
{
    public class FeedbackViewComponent :ViewComponent
    {
        private AppDbContext context;
        public FeedbackViewComponent(AppDbContext context)
        {
            this.context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(context.feedBacks.ToList());
        }

    }
}

using Microsoft.AspNetCore.Mvc;

namespace AgriMarket.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}

using AgriMarket.Data;
using AgriMarket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(ILogger<HomeController> logger, AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    private string GetUserId()
    {
       
        return _httpContextAccessor.HttpContext.User.Identity.Name; 
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.CartItemCount = GetCartItemCount();
        return View();
    }

    public int GetCartItemCount()
    {
        var userId = GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return 0;
        }

        var cartItems = _context.CartItems.Where(c => c.UserId == userId).ToList();
        return cartItems.Sum(item => item.Quantity);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
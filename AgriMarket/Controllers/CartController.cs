using AgriMarket.Data;
using AgriMarket.Models.ViewModels;
using AgriMarket.Models;
using Microsoft.AspNetCore.Mvc;

public class CartController : Controller
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    

    public IActionResult Index()
    {
         
        var userId = _httpContextAccessor.HttpContext?.User.Identity.Name;
        var cartItems = _context.CartItems.Where(c => c.UserId == userId).ToList();
        ViewBag.CartItemCount = cartItems.Sum(item => item.ProductId);
        return View(cartItems);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId, string productName, decimal price, string imageUrl)
    {
        var userId = _httpContextAccessor.HttpContext?.User.Identity.Name;
        var existingItem = _context.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            _context.CartItems.Add(new CartItem
            {
                ProductId = productId,
                ProductName = productName,
                Price = price,
                ImageUrl = imageUrl,
                Quantity = 1,
                UserId = userId
            });
        }

        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int productId)
    {
        var userId = _httpContextAccessor.HttpContext?.User.Identity.Name;
        var itemToRemove = _context.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

        if (itemToRemove != null)
        {
            _context.CartItems.Remove(itemToRemove);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ClearCart()
    {
        var userId = _httpContextAccessor.HttpContext?.User.Identity.Name;
        var cartItems = _context.CartItems.Where(c => c.UserId == userId).ToList();

        if (cartItems.Any())
        {
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int productId, int quantity)
    {
        var userId = _httpContextAccessor.HttpContext.User.Identity.Name;
        var itemToUpdate = _context.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

        if (itemToUpdate != null && quantity > 0)
        {
            itemToUpdate.Quantity = quantity;
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SubmitOrder(OrderViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = _httpContextAccessor.HttpContext.User.Identity.Name;
            var cartItems = _context.CartItems.Where(c => c.UserId == userId).ToList();

            var order = new OrderProduct
            {
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                PaymentMethod = model.PaymentMethod,
                OrderDate = DateTime.Now,
                UserId = userId
            };

            foreach (var item in cartItems)
            {
                var productItem = new ProductItem
                {
                    ProductName = item.ProductName,
                    Price = item.Price,
                    ImageUrl = item.ImageUrl,
                    Quantity = item.Quantity
                };
                order.Products.Add(productItem);
            }

            _context.OrderProducts.Add(order);
            _context.CartItems.RemoveRange(cartItems); 
            _context.SaveChanges();

            TempData["ConfirmationMessage"] = "Thank you! Your order has been placed successfully.";
            return RedirectToAction("OrderConfirmation");
        }

        return View(model);
    }

    public IActionResult OrderConfirmation()
    {
        return View();
    }
}
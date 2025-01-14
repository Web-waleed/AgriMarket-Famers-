using AgriMarket.Data;
using AgriMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Area("Dashboard")]
[Authorize(Roles = "Admin")]
public class ContactsController : Controller
{
    private readonly AppDbContext _context;
    public ContactsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Index(ContactIUS contact)
    {
        if (ModelState.IsValid)
        {
            _context.contactIUs.Add(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Success));
        }

        return View(contact);
    }

    public async Task<IActionResult> Messages()
    {
        var contacts = await _context.contactIUs.OrderByDescending(c => c.DateSubmitted).ToListAsync();
        return View(contacts);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var contact = await _context.contactIUs.FindAsync(id);
        if (contact == null)
        {
            return NotFound();
        }

        _context.contactIUs.Remove(contact);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Messages));
    }

    [AllowAnonymous]
    public IActionResult Success()
    {
        return View();
    }
}


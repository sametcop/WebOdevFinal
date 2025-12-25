using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrunYonetimSistemi.Models;
using UrunYonetimSistemi.ViewModels;

namespace UrunYonetimSistemi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.OrderByDescending(p => p.CreatedDate).Take(8).ToListAsync();
        var sliders = await _context.SliderItems.OrderBy(s => s.Order).ToListAsync();
        
        var viewModel = new HomeViewModel
        {
            Products = products,
            Sliders = sliders
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Support()
    {
        return View();
    }

    public async Task<IActionResult> Search(string q, string sortOrder)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return RedirectToAction(nameof(Index));
        }

        var productsQuery = _context.Products
            .Include(p => p.Category)
            .Where(p => p.Name.Contains(q) || (p.Category != null && p.Category.Name.Contains(q)));

        switch (sortOrder)
        {
            case "price_asc":
                productsQuery = productsQuery.OrderBy(p => p.Price);
                break;
            case "price_desc":
                productsQuery = productsQuery.OrderByDescending(p => p.Price);
                break;
            default: // Newest
                productsQuery = productsQuery.OrderByDescending(p => p.CreatedDate);
                break;
        }

        var results = await productsQuery.ToListAsync();

        ViewBag.SearchQuery = q;
        ViewBag.CurrentSort = sortOrder;
        return View(results);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

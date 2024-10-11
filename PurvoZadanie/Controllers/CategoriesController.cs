using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PurvoZadanie.Data;
using PurvoZadanie.Models;
using System.Linq;
using System.Threading.Tasks;

public class CategoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Categories
    public async Task<IActionResult> Index()
    {
        return View(await _context.Categories.ToListAsync());
    }

    // GET: Categories/Create
    public IActionResult Create()
    {
        // Извличане на всички категории
        var categories = _context.Categories.ToList();
        ViewBag.Categories = new SelectList(categories, "Id", "Name"); // Създаване на SelectList
        return View();
    }

    // POST: Categories/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,DisplayOrder")] Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        var categories = _context.Categories.ToList();
        ViewBag.Categories = new SelectList(categories, "Id", "Name"); // Предаване на категориите отново
        return View(category);
    }
}

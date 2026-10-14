
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lesson9_Lab.Models;

[Area("Admin")]
public class BannerController : Controller
{
    private readonly Lesson9DbContext _context;

    public BannerController(Lesson9DbContext context)
    {
        _context = context;
    }

    // GET: BANNERS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Banners.ToListAsync());
    }

    // GET: BANNERS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var banner = await _context.Banners
            .FirstOrDefaultAsync(m => m.Id == id);
        if (banner == null)
        {
            return NotFound();
        }

        return View(banner);
    }

    // GET: BANNERS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: BANNERS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Status,Prioty,Image,Description")] Banner banner)
    {
        if (ModelState.IsValid)
        {
            _context.Add(banner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(banner);
    }

    // GET: BANNERS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var banner = await _context.Banners.FindAsync(id);
        if (banner == null)
        {
            return NotFound();
        }
        return View(banner);
    }

    // POST: BANNERS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Status,Prioty,Image,Description")] Banner banner)
    {
        if (id != banner.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(banner);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannerExists(banner.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(banner);
    }

    // GET: BANNERS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var banner = await _context.Banners
            .FirstOrDefaultAsync(m => m.Id == id);
        if (banner == null)
        {
            return NotFound();
        }

        return View(banner);
    }

    // POST: BANNERS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var banner = await _context.Banners.FindAsync(id);
        if (banner != null)
        {
            _context.Banners.Remove(banner);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BannerExists(int? id)
    {
        return _context.Banners.Any(e => e.Id == id);
    }
}

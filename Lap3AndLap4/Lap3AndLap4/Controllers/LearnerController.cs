using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lap3AndLap4.Models; // Đảm bảo đúng namespace

public class LearnerController : Controller
{
    private readonly DBContext _context;

    public LearnerController(DBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Learners.Include(l => l.Major).ToListAsync());
    }

    public async Task<IActionResult> Details(int? learnerid)
    {
        if (learnerid == null) return NotFound();

        var learner = await _context.Learners
            .Include(l => l.Major)
            .FirstOrDefaultAsync(m => m.LearnerID == learnerid);

        if (learner == null) return NotFound();
        return View(learner);
    }

    public IActionResult Create()
    {
        // Truyền danh sách Major sang View để làm thẻ Select
        ViewBag.MajorID = new SelectList(_context.Majors, "MajorID", "MajorName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("LearnerID,LastName,FirstMidName,EnrollmentDate,MajorID")] Learner learner)
    {
        ModelState.Remove("Major");
        ModelState.Remove("Enrollments");

        if (ModelState.IsValid)
        {
            _context.Add(learner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.MajorID = new SelectList(_context.Majors, "MajorID", "MajorName", learner.MajorID);
        return View(learner);
    }

    public async Task<IActionResult> Edit(int? learnerid)
    {
        if (learnerid == null) return NotFound();

        var learner = await _context.Learners.FindAsync(learnerid);
        if (learner == null) return NotFound();

        ViewBag.MajorID = new SelectList(_context.Majors, "MajorID", "MajorName", learner.MajorID);
        return View(learner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? learnerid, [Bind("LearnerID,LastName,FirstMidName,EnrollmentDate,MajorID")] Learner learner)
    {
        if (learnerid != learner.LearnerID) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(learner);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LearnerExists(learner.LearnerID)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.MajorID = new SelectList(_context.Majors, "MajorID", "MajorName", learner.MajorID);
        return View(learner);
    }

    public async Task<IActionResult> Delete(int? learnerid)
    {
        if (learnerid == null) return NotFound();

        var learner = await _context.Learners
            .Include(l => l.Major)
            .FirstOrDefaultAsync(m => m.LearnerID == learnerid);

        if (learner == null) return NotFound();
        return View(learner);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? learnerid)
    {
        var learner = await _context.Learners.FindAsync(learnerid);
        if (learner != null)
        {
            _context.Learners.Remove(learner);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LearnerExists(int? learnerid)
    {
        return _context.Learners.Any(e => e.LearnerID == learnerid);
    }
}
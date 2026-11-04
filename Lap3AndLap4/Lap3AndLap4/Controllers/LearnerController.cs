using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lap3AndLap4.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using System;

public class LearnerController : Controller
{
    private readonly DBContext _context;
    private int pageSize = 3;

    public LearnerController(DBContext context)
    {
        _context = context;
    }


    public async Task<IActionResult> Index(int? id)
    {
        var query = _context.Learners.Include(l => l.Major).AsQueryable();

        if (id.HasValue)
        {
            query = query.Where(l => l.MajorID == id.Value);
        }

        int totalItems = await query.CountAsync();
        int pageNumber = (int)Math.Ceiling(totalItems / (double)pageSize);

        ViewBag.pageNumber = pageNumber;

        var result = await query.Take(pageSize).ToListAsync();

        return View(result);
    }

    public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
    {
        var learners = _context.Learners.AsQueryable();

        int page = pageIndex == null || pageIndex <= 0 ? 1 : pageIndex.Value;

        if (mid != null)
        {
            learners = learners.Where(l => l.MajorID == mid);
            ViewBag.mid = mid;
        }

        if (keyword != null)
        {
            learners = learners.Where(l => l.FirstMidName.ToLower().Contains(keyword.ToLower()));
            ViewBag.keyword = keyword;
        }

        int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
        ViewBag.pageNum = pageNum;

        var result = learners.Skip(pageSize * (page - 1))
                             .Take(pageSize)
                             .Include(m => m.Major);

        return PartialView("LearnerTable", result);
    }

    public IActionResult LearnerByMajorID(int? id)
    {
        if (id == null)
        {
            var allLearners = _context.Learners.Include(l => l.Major).ToList();
            return PartialView("LearnerTable", allLearners);
        }

        var learners = _context.Learners
            .Where(l => l.MajorID == id)
            .Include(l => l.Major)
            .ToList();

        return PartialView("LearnerTable", learners);
    }

  
    public async Task<IActionResult> Details(int? id, int? learnerid)
    {
        var targetId = id ?? learnerid;
        if (targetId == null) return NotFound();

        var learner = await _context.Learners
            .Include(l => l.Major)
            .FirstOrDefaultAsync(m => m.LearnerID == targetId);

        if (learner == null) return NotFound();
        return View(learner);
    }

    public IActionResult Create()
    {
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

    public async Task<IActionResult> Edit(int? id, int? learnerid)
    {
        var targetId = id ?? learnerid;
        if (targetId == null) return NotFound();

        var learner = await _context.Learners.FindAsync(targetId);
        if (learner == null) return NotFound();

        ViewBag.MajorID = new SelectList(_context.Majors, "MajorID", "MajorName", learner.MajorID);
        return View(learner);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("LearnerID,LastName,FirstMidName,EnrollmentDate,MajorID")] Learner learner)
    {
        if (id != learner.LearnerID) return NotFound();

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

    public async Task<IActionResult> Delete(int? id, int? learnerid)
    {
        var targetId = id ?? learnerid;
        if (targetId == null) return NotFound();

        var learner = await _context.Learners
            .Include(l => l.Major)
            .FirstOrDefaultAsync(m => m.LearnerID == targetId);

        if (learner == null) return NotFound();
        return View(learner);
    }


    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id, int? learnerid)
    {
        var targetId = id ?? learnerid;
        var learner = await _context.Learners.FindAsync(targetId);
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
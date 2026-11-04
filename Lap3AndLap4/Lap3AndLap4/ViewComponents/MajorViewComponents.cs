using Lap3AndLap4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;

namespace Lap3AndLap4.ViewComponents
{
    public class MajorViewComponent : ViewComponent
    {
        private readonly DBContext _context;
        public List<Major> _majors;

        public MajorViewComponent(DBContext context)
        {
            _context = context;
            _majors = _context.Majors.ToList();
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderMajor", _majors);
        }

    }
}

using Microsoft.AspNetCore.Mvc;

namespace lesson9_Lab.Areas.Admins.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}

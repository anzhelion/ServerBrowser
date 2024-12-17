using Microsoft.AspNetCore.Mvc;
using ServerBrowser.Data;

namespace ServerBrowser.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerBrowser.Data;
using System.Security.Claims;

namespace ServerBrowser.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Wipe()
        {
            string? UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (UserId != null && UserId == "71fd9927-6777-4724-8793-7e1b5e957c87")
            {
                await _context.Database.EnsureDeletedAsync();
            }

            return View(nameof(Index));
        }
    }
}

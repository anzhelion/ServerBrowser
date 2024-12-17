using Microsoft.AspNetCore.Mvc;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;

namespace ServerBrowser.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            List<ReviewViewModel> models = _context.ServerReviews
            .Select(model => new ReviewViewModel
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                PublisherId = model.PublisherId,
                AddedOn = model.AddedOn,
                ServerId = model.ServerId
            })
            .ToList();

            return View(models);
        }
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ReviewViewModel model)
        {
            bool IsValid = true;

            // Validation
            if (!ModelState.IsValid)
            {
                IsValid = false;
            }

            int Count = _context.Servers.Where(x => x.Id == model.ServerId).Count();
            if (Count != 1)
            {
                IsValid = false;
            }

            if (IsValid)
            {
                var data = new ServerReview
                {
                    Title = model.Title,
                    Description = model.Description,
                    PublisherId = model.PublisherId ?? User.FindFirstValue(ClaimTypes.NameIdentifier),
                    AddedOn = model.AddedOn,
                    ServerId = model.ServerId
                };

                _context.ServerReviews.Add(data);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

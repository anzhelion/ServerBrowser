using Microsoft.AspNetCore.Mvc;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;

namespace ServerBrowser.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly ApplicationDbContext context;

        public AnnouncementController(ApplicationDbContext context_)
        {
            this.context = context_;
        }

        public IActionResult Index()
        {
            List<ReviewViewModel> models = context.ServerReviews
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

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ReviewViewModel model)
        {
            var data = new ServerReview
            {
                Title = model.Title,
                Description = model.Description,
                PublisherId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                AddedOn = model.AddedOn,
                ServerId = model.ServerId
            };

            context.ServerReviews.Add(data);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

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
            List<AnnouncementViewModel> models = context.Announcements
            .Select(model => new AnnouncementViewModel
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
            bool IsValid = true;

            // Validation
            if (model.Title?.Length > 64 ||
                model.Description?.Length > 128)
            {
                IsValid = false;
            }

            if (model.Title == null || model.Description == null)
            {
                IsValid = false;
            }

            if (model.Title?.Length < 1 ||
                model.Description?.Length < 1)
            {
                IsValid = false;
            }

            int Count = context.Servers.Where(x => x.Id == model.ServerId).Count();
            if (Count != 1)
            {
                IsValid = false;
            }

            if (IsValid)
            {

                var data = new Announcement
                {
                    Title = model.Title,
                    Description = model.Description,
                    PublisherId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    AddedOn = model.AddedOn,
                    ServerId = model.ServerId
                };

                context.Announcements.Add(data);
                context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

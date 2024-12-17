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
                ServerIds = string.Join(" ", model.ServerIds),
                Severity = model.Severity,
            })
            .ToList();

            // Reverse the list to get new announcements first
            models.Reverse();

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
        public IActionResult Add(AnnouncementViewModel model)
        {
            bool IsValid = true;

            // Validation
            if (!ModelState.IsValid)
            {
                IsValid = false;
            }

            int[] Ids = null;
            if (model.ServerIds != null)
            {
                Ids = model.ServerIds.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                foreach (var Id in Ids)
                {
                    int Count = context.Servers.Where(x => x.Id == Id).Count();
                    if (Count != 1)
                    {
                        IsValid = false;
                    }
                }
            }
            else
            {
                IsValid = false;
            }

            if (IsValid)
            {

                var data = new Announcement
                {
                    Title = model.Title,
                    Description = model.Description,
                    PublisherId = model.PublisherId ?? User.FindFirstValue(ClaimTypes.NameIdentifier),
                    AddedOn = model.AddedOn,
                    ServerIds = Ids,
                    Severity = model.Severity
                };

                context.Announcements.Add(data);
                context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

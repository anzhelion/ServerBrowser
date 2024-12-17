using Microsoft.AspNetCore.Mvc;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using ServerBrowser.Services.Contracts;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;

namespace ServerBrowser.Controllers
{
    public class AnnouncementController : BaseController
    {
        private readonly IAnnouncementService _service;

        public AnnouncementController(IAnnouncementService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<AnnouncementViewModel> models = await _service.GetAllAnnouncements();

            // Reverse the list to get new announcements first
            models.Reverse();

            return View(models);
        }
        public async Task<IActionResult> About()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AnnouncementViewModel model)
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
                    int Count = await _service.GetServerIdUsedCount(Id);
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
                await _service.AddAnnouncement(GetUserId(), Ids, model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

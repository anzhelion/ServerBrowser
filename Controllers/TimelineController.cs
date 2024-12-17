using Microsoft.AspNetCore.Mvc;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;
using ServerBrowser.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ServerBrowser.Controllers
{
    public class TimelineController : BaseController
    {
        private readonly ITimelineService _service;

        public TimelineController(ITimelineService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<TimelineViewModel> models = await _service.GetAllTimelineModels();

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
        public async Task<IActionResult> Add(TimelineViewModel model)
        {
            bool IsValid = true;

            if (model.DescriptionsInput != null)
            {
                model.EventDescription = model.DescriptionsInput.Split(";", StringSplitOptions.None);
            }
            
            if (model.InputAddedOn != null)
            {
                string[] Parts = model.InputAddedOn.Split(";");
                model.AddedOn = new DateTime?[Parts.Length];
                for (int i = 0; i < Parts.Length; ++i)
                {
                    if (DateTime.TryParse(Parts[i], out DateTime date))
                    {
                        model.AddedOn[i] = date;
                    }
                    else
                    {
                        model.AddedOn[i] = null;
                    }
                }
            }

            // Validation
            if (!ModelState.IsValid)
            {
                IsValid = false;
            }

            int Count = await _service.GetServerIdUsedCount(model.ServerId);
            if (Count != 1)
            {
                IsValid = false;
            }

            if (IsValid)
            {
                await _service.AddTimeline(model);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}

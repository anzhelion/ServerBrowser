using Microsoft.AspNetCore.Mvc;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;
using ServerBrowser.Services.Contracts;

namespace ServerBrowser.Controllers
{
    public class ServerController : BaseController
    {
        private readonly IServerService _service;

        public ServerController(IServerService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<string> types = await _service.GetServerTypes();

            List<ServerViewModel> models = await _service.GetAllServerModels(types);

            return View(models);
        }
        public async Task<IActionResult> About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ServerViewModel model = await _service.GetServerById(id) ?? new();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> FullDetails(int id)
        {
            FullDetailsViewModel model = new();

            // Server
            model.serverViewModel = await _service.GetServerById(id);

            // Reviews
            model.reviewViewModels = await _service.GetReviewsByServerId(id);

            // Lists
            model.listViewModels = await _service.GetListsByServerId(id);

            // Announcements
            model.announcementViewModels = await _service.GetAnnouncementsByServerId(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ServerViewModel model)
        {
            bool IsValid = true;

            // Validation
            if (!ModelState.IsValid)
            {
                IsValid = false;
            }

            if (IsValid)
            {
                await _service.AddServer(GetUserId(), model);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}

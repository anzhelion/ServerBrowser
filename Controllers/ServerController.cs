using Microsoft.AspNetCore.Mvc;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;

namespace ServerBrowser.Controllers
{
    public class ServerController : Controller
    {
        private readonly ApplicationDbContext context;

        public ServerController(ApplicationDbContext context_)
        {
            this.context = context_;
        }

        public IActionResult Index()
        {
            List<ServerViewModel> models = context.Servers
        .Select(model => new ServerViewModel
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            PublisherId = model.PublisherId,
            AddedOn = model.AddedOn,
            ServerType = model.ServerType,
            IPAddress = model.IPAddress,
            LastLiveOn = model.LastLiveOn,
            ActiveTimeStart = model.ActiveTimeStart,
            ActiveTimeEnd = model.ActiveTimeEnd,
            PeakConcurrentUsers = model.PeakConcurrentUsers,
            IsPrivate = model.IsPrivate,
            IsDedicated = model.IsDedicated,
            IsOfficial = model.IsOfficial,
            IsRemoved = model.IsRemoved,
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
        public IActionResult Add(ServerViewModel model)
        {
            var data = new Server
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PublisherId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                AddedOn = model.AddedOn,
                ServerType = model.ServerType,
                IPAddress = model.IPAddress,
                LastLiveOn = model.LastLiveOn,
                ActiveTimeStart = model.ActiveTimeStart,
                ActiveTimeEnd = model.ActiveTimeEnd,
                PeakConcurrentUsers = model.PeakConcurrentUsers,
                IsDedicated = model.IsDedicated,
                IsOfficial = model.IsOfficial,
                IsRemoved = model.IsRemoved,
                IsPrivate= model.IsPrivate,
            };

            context.Servers.Add(data);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

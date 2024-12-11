using Microsoft.AspNetCore.Mvc;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using System.Linq;
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
            List<string> types = context.ServerTypes
        .Select(model => model.Name)
        .ToList();

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
            TypeString = types[model.ServerType - 1], // minus 1 for 0 index null type
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
            bool IsValid = true;

            // Validation
            if (model.Title?.Length > 64 ||
                model.Description?.Length > 64 ||
                model.ImageUrl?.Length > 128 ||
                model.IPAddress?.Length > 128)
            {
                IsValid = false;
            }

            if (model.Title == null || model.Description == null ||
                model.ImageUrl == null || model.IPAddress == null)
            {
                IsValid = false;
            }

            if (model.Title?.Length < 1 ||
                model.Description?.Length < 1 ||
                model.ImageUrl?.Length < 1 ||
                model.IPAddress?.Length < 1)
            {
                IsValid = false;
            }

            if (IsValid)
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
                    IsPrivate = model.IsPrivate,
                };

                context.Servers.Add(data);
                context.SaveChanges();
            }


            return RedirectToAction(nameof(Index));
        }
    }
}

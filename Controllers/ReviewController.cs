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
        private readonly ApplicationDbContext context;

        public ReviewController(ApplicationDbContext context_)
        {
            this.context = context_;
        }

        public IActionResult Index()
        {
            List<ListViewModel> models = context.ServerLists
            .Select(model => new ListViewModel
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                PublisherId = model.PublisherId,
                AddedOn = model.AddedOn,
                ServerIds = string.Join(" ", model.ServerIds)
                    
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
        public IActionResult Add(ListViewModel model)
        {
            int[] Ids = model.ServerIds.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

            var data = new ServerList
            {
                Title = model.Title,
                Description = model.Description,
                PublisherId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                AddedOn = model.AddedOn,
                ServerIds = Ids
            };

            context.ServerLists.Add(data);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using ServerBrowser.Services.Contracts;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;

namespace ServerBrowser.Controllers
{
    public class ListController : BaseController
    {
        private readonly IListService _service;

        public ListController(IListService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<ListViewModel> models = await _service.GetAllLists();

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
        public async Task<IActionResult> Add(ListViewModel model)
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
                await _service.AddList(GetUserId(), Ids, model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

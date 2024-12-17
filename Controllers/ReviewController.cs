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
    public class ReviewController : BaseController
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<ReviewViewModel> models = await _service.GetAllReviews();

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
        public async Task<IActionResult> Add(ReviewViewModel model)
        {
            bool IsValid = true;

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
                await _service.AddReview(GetUserId(), model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

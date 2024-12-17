using Microsoft.EntityFrameworkCore;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using ServerBrowser.Services.Contracts;
using System.Security.Claims;

namespace ServerBrowser.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<List<ReviewViewModel>> GetAllReviews()
        {
            List<ReviewViewModel> models = await _context.ServerReviews
                .Select(model => new ReviewViewModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    PublisherId = model.PublisherId,
                    AddedOn = model.AddedOn,
                    ServerId = model.ServerId
                })
                .ToListAsync();

            return models;
        }

        public async Task AddReview(string UserId, ReviewViewModel model)
        {
            var data = new ServerReview
            {
                Title = model.Title,
                Description = model.Description,
                PublisherId = model.PublisherId ?? UserId,
                AddedOn = model.AddedOn,
                ServerId = model.ServerId
            };

            await _context.ServerReviews.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetServerIdUsedCount(int id)
        {
            int Count = await _context.Servers.Where(x => x.Id == id).CountAsync();

            return Count;
        }
    }
}

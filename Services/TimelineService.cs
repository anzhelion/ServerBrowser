using Microsoft.EntityFrameworkCore;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using ServerBrowser.Services.Contracts;
using System.Security.Claims;

namespace ServerBrowser.Services
{
    public class TimelineService : ITimelineService
    {
        private readonly ApplicationDbContext _context;

        public TimelineService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<List<TimelineViewModel>> GetAllTimelineModels()
        {
            List<TimelineViewModel> models = await _context.Timelines
                .Select(model => new TimelineViewModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    EventDescription = model.EventDescription.Split(";", StringSplitOptions.None),
                    AddedOn = model.AddedOn
                })
                .ToListAsync();

            return models;
        }

        public async Task AddTimeline(TimelineViewModel model)
        {
            var data = new Timeline
            {
                Name = model.Name,
                EventDescription = string.Join(";", model.EventDescription),
                AddedOn = model.AddedOn,
            };

            await _context.Timelines.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetServerIdUsedCount(int id)
        {
            int Count = await _context.Servers.Where(x => x.Id == id).CountAsync();

            return Count;
        }
    }
}

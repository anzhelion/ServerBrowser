using Microsoft.EntityFrameworkCore;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using ServerBrowser.Services.Contracts;
using System.Security.Claims;

namespace ServerBrowser.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<List<AnnouncementViewModel>> GetAllAnnouncements()
        {
            List<AnnouncementViewModel> models = await _context.Announcements
                .Select(model => new AnnouncementViewModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    PublisherId = model.PublisherId,
                    AddedOn = model.AddedOn,
                    ServerIds = string.Join(" ", model.ServerIds),
                    Severity = model.Severity,
                })
                .ToListAsync();

            return models;
        }

        public async Task AddAnnouncement(string UserId, int[] Ids, AnnouncementViewModel model)
        {
            var data = new Announcement
            {
                Title = model.Title,
                Description = model.Description,
                PublisherId = model.PublisherId ?? UserId,
                AddedOn = model.AddedOn,
                ServerIds = Ids,
                Severity = model.Severity
            };

            await _context.Announcements.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetServerIdUsedCount(int id)
        {
            int Count = await _context.Servers.Where(x => x.Id == id).CountAsync();

            return Count;
        }
    }
}

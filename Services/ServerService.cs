using Microsoft.EntityFrameworkCore;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using ServerBrowser.Services.Contracts;
using System.Security.Claims;

namespace ServerBrowser.Services
{
    public class ServerService : IServerService
    {
        private readonly ApplicationDbContext _context;

        public ServerService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<List<string>> GetServerTypes()
        {
            List<string> types = await _context.ServerTypes
                .Select(model => model.Name)
                .ToListAsync();

            return types;
        }

        public async Task<List<ServerViewModel>> GetAllServerModels(List<string> types)
        {
            List<ServerViewModel> models = await _context.Servers
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
                    TypeString = types[(model.ServerType ?? 1) - 1], // minus 1 for 0 index null type
                })
                .ToListAsync();

            return models;
        }

        public async Task<ServerViewModel?> GetServerById(int id)
        {
            ServerViewModel? model = await _context.Servers
                .Where(x => x.Id == id)
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
                    IsRemoved = model.IsRemoved
                }).FirstOrDefaultAsync();

            return model;
        }

        public async Task<List<ReviewViewModel>> GetReviewsByServerId(int id)
        {
            List<ReviewViewModel> models =  await _context.ServerReviews
                .Where(x => x.ServerId == id)
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

        public async Task<List<ListViewModel>> GetListsByServerId(int id)
        {
            List<ListViewModel> models = await _context.ServerLists
                .Where(x => x.ServerIds.Contains(id))
                .Select(model => new ListViewModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    PublisherId = model.PublisherId,
                    AddedOn = model.AddedOn,
                    ServerIds = string.Join(" ", model.ServerIds)

                })
                .ToListAsync();

            return models;
        }

        public async Task<List<AnnouncementViewModel>> GetAnnouncementsByServerId(int id)
        {
            List<AnnouncementViewModel> models = await _context.Announcements
                .Where(x => x.ServerIds.Contains(id))
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

        public async Task AddServer(string UserId, ServerViewModel model)
        {
            var data = new Server
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PublisherId = model.PublisherId ?? UserId,
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

            await _context.Servers.AddAsync(data);
            await _context.SaveChangesAsync();
        }
    }
}

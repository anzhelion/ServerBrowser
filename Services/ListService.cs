using Microsoft.EntityFrameworkCore;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using ServerBrowser.Services.Contracts;
using System.Security.Claims;

namespace ServerBrowser.Services
{
    public class ListService : IListService
    {
        private readonly ApplicationDbContext _context;

        public ListService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<List<ListViewModel>> GetAllLists()
        {
            List<ListViewModel> models = _context.ServerLists
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

            return models;
        }

        public async Task AddList(string UserId, int[] Ids, ListViewModel model)
        {
            var data = new ServerList
            {
                Title = model.Title,
                Description = model.Description,
                PublisherId = model.PublisherId ?? UserId,
                AddedOn = model.AddedOn,
                ServerIds = Ids
            };

            await _context.ServerLists.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetServerIdUsedCount(int id)
        {
            int Count = await _context.Servers.Where(x => x.Id == id).CountAsync();

            return Count;
        }
    }
}

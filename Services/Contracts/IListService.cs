using ServerBrowser.Models;

namespace ServerBrowser.Services.Contracts
{
    public interface IListService
    {
        Task<List<ListViewModel>> GetAllLists();

        Task AddList(string UserId, int[] Ids, ListViewModel model);

        Task<int> GetServerIdUsedCount(int id);
    }
}

using ServerBrowser.Models;

namespace ServerBrowser.Services.Contracts
{
    public interface IServerService
    {
        Task<List<string>> GetServerTypes();
        Task<List<ServerViewModel>> GetAllServerModels(List<string> types);

        Task<ServerViewModel?> GetServerById(int id);

        Task<List<ReviewViewModel>> GetReviewsByServerId(int id);

        Task<List<ListViewModel>> GetListsByServerId(int id);

        Task<List<AnnouncementViewModel>> GetAnnouncementsByServerId(int id);

        Task AddServer(string UserId, ServerViewModel model);

        Task<int> GetServerIdUsedCount(int id);

        Task<List<ServerViewModel>> GetServersByStartName(string Name);

    }
}

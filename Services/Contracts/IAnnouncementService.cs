using ServerBrowser.Models;

namespace ServerBrowser.Services.Contracts
{
    public interface IAnnouncementService
    {
        Task<List<AnnouncementViewModel>> GetAllAnnouncements();

        Task AddAnnouncement(string UserId, int[] Ids, AnnouncementViewModel model);

        Task<int> GetServerIdUsedCount(int id);
    }
}

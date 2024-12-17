using ServerBrowser.Models;

namespace ServerBrowser.Services.Contracts
{
    public interface ITimelineService
    {
        Task<List<TimelineViewModel>> GetAllTimelineModels();

        Task AddTimeline(TimelineViewModel model);

        Task<int> GetServerIdUsedCount(int id);
    }
}

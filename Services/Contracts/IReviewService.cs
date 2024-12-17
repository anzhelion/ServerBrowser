using ServerBrowser.Models;

namespace ServerBrowser.Services.Contracts
{
    public interface IReviewService
    {
        Task<List<ReviewViewModel>> GetAllReviews();

        Task AddReview(string UserId, ReviewViewModel model);

        Task<int> GetServerIdUsedCount(int id);
    }
}

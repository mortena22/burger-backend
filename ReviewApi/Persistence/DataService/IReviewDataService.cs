using ReviewApi.Domain.Model;

namespace ReviewApi.Persistence.DataService
{
  public interface IReviewDataService
  {
    Task<IEnumerable<ReviewReadDto>> GetReviewsForRastaurantAsync(int rastaurantId);
    Task<int?> CreateReviewFormRastaurantAsync(ReviewCreateDto review, int rastaurantId, int userId);
  }
}
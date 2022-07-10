using ReviewApi.Domain.Model;

namespace ReviewApi.Persistence.DataService
{
  public interface IReviewDataService
  {
    Task<IEnumerable<ReviewReadDto>> GetReviewsForRestaurantAsync(int restaurantId);
    Task<int?> CreateReviewFormRestaurantAsync(ReviewCreateDto review, int restaurantId, int userId);
  }
}
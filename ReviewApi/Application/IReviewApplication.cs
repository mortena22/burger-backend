using ReviewApi.Domain.Model;

namespace ReviewApi.Application
{
  public interface IReviewApplication
  {
    Task<int?> CreateReviewForRestaurantAsync(ReviewCreateDto dto, int restaurantId, int userId);
    Task<IEnumerable<ReviewReadDto>> GetReviewsForRestaurantAsync(int restaurantId);
  }
}
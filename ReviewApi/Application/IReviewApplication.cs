using ReviewApi.Domain.Model;

namespace ReviewApi.Application
{
  public interface IReviewApplication
  {
    Task<int?> CreateReviewForRastaurantAsync(ReviewCreateDto dto, int rastaurantId, int userId);
    Task<IEnumerable<ReviewReadDto>> GetReviewsForRastaurantAsync(int rastaurantId);
  }
}
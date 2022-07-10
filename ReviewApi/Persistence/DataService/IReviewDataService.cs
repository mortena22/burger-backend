using ReviewApi.Model;

namespace ReviewApi.Persistence.DataService
{
  public interface IReviewDataService
  {
    Task<IEnumerable<ReviewReadDto>> GetReviewsForRastaurantAsync(int rastaurantId);
  }
}
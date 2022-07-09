using ReviewApi.Model;

namespace ReviewApi.Persistence.DataService
{
  public interface IReviewDataService
  {
    IEnumerable<ReviewReadDto> GetReviewsForRestaurant(int restaurantId);
  }
}
using ReviewApi.Domain.Model;
using ReviewApi.Persistence.DataService;

namespace ReviewApi.Application
{
  public class ReviewApplication : IReviewApplication
  {
    private readonly IReviewDataService _dataService;
    private readonly ILogger<ReviewApplication> _logger;

    public ReviewApplication(IReviewDataService dataService, ILogger<ReviewApplication> logger)
    {
      _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<int?> CreateReviewForRestaurantAsync(ReviewCreateDto dto, int restaurantId, int userId)
    {
      _logger.LogInformation("Method: {methodName} called with restaurantId: {restaurantId} and userId: {userId}", nameof(CreateReviewForRestaurantAsync), restaurantId, userId);

      return await _dataService.CreateReviewFormRestaurantAsync(dto, restaurantId, userId);
    }

    public async Task<IEnumerable<ReviewReadDto>> GetReviewsForRestaurantAsync(int restaurantId)
    {
      _logger.LogInformation("Method: {methodName} called with restaurantId: {restaurantId}", nameof(GetReviewsForRestaurantAsync), restaurantId);

      return await _dataService.GetReviewsForRestaurantAsync(restaurantId);
    }
  }
}
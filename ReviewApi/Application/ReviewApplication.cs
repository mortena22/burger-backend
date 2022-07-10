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

    public async Task<int?> CreateReviewForRastaurantAsync(ReviewCreateDto dto, int rastaurantId, int userId)
    {
      _logger.LogInformation("Method: {methodName} called with rastaurantId: {rastaurantId} and userId: {userId}", nameof(CreateReviewForRastaurantAsync), rastaurantId, userId);

      return await _dataService.CreateReviewFormRastaurantAsync(dto, rastaurantId, userId);
    }

    public async Task<IEnumerable<ReviewReadDto>> GetReviewsForRastaurantAsync(int rastaurantId)
    {
      _logger.LogInformation("Method: {methodName} called with rastaurantId: {rastaurantId}", nameof(GetReviewsForRastaurantAsync), rastaurantId);

      return await _dataService.GetReviewsForRastaurantAsync(rastaurantId);
    }
  }
}
using Microsoft.AspNetCore.Mvc;
using ReviewApi.Model;
using ReviewApi.Persistence.DataService;

namespace ReviewApi.Controllers
{
  [ApiController]
  [Route("api/v1/review-management/")]
  public class ReviewController : Controller
  {
    private readonly IReviewDataService _dataService;
    private readonly ILogger<ReviewController> _logger;

    public ReviewController(IReviewDataService dataService, ILogger<ReviewController> logger)
    {
      _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Route("rastaurants/{rastaurant-id:int}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewReadDto>>> GetReviewsForRastaurantAsync([FromRoute(Name = "rastaurant-id")] int rastaurantId)
    {
      _logger.LogInformation("Method: {methodName} called with rastaurantId: {rastaurantId}", nameof(GetReviewsForRastaurantAsync), rastaurantId);

      return Ok(await _dataService.GetReviewsForRastaurantAsync(rastaurantId));
    }
  }
}
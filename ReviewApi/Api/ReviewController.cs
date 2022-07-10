using Microsoft.AspNetCore.Mvc;
using ReviewApi.Application;
using ReviewApi.Domain.Model;

namespace ReviewApi.Api
{
  [ApiController]
  [Route("api/v1/review-management/")]
  public class ReviewController : Controller
  {
    private readonly IReviewApplication _application;
    private readonly ILogger<ReviewController> _logger;

    public ReviewController(IReviewApplication application, ILogger<ReviewController> logger)
    {
      _application = application ?? throw new ArgumentNullException(nameof(application));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    [Route("rastaurants/{rastaurant-id:int}/reviews")]
    public async Task<ActionResult<int>> CreateReviewsForRastaurantAsync([FromBody] ReviewCreateDto dto, [FromRoute(Name = "rastaurant-id")] int rastaurantId)
    {
      _logger.LogInformation("Method: {methodName} called with rastaurantId: {rastaurantId}", nameof(CreateReviewsForRastaurantAsync), rastaurantId);

      var userId = Request.Headers["UserId"].FirstOrDefault();

      if (string.IsNullOrEmpty(userId))
      {
        _logger.LogCritical("Method: {methodName} header value userId is null", nameof(CreateReviewsForRastaurantAsync));

        return Unauthorized();
      }

      var result = await _application.CreateReviewForRastaurantAsync(dto, rastaurantId, Convert.ToInt32(userId));

      if (result is null) return BadRequest("User has allready created a review for that rastuarant");
      else return Ok(result);
    }

    [HttpGet]
    [Route("rastaurants/{rastaurant-id:int}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewReadDto>>> GetReviewsForRastaurantAsync([FromRoute(Name = "rastaurant-id")] int rastaurantId)
    {
      _logger.LogInformation("Method: {methodName} called with rastaurantId: {rastaurantId}", nameof(GetReviewsForRastaurantAsync), rastaurantId);

      return Ok(await _application.GetReviewsForRastaurantAsync(rastaurantId));
    }
  }
}
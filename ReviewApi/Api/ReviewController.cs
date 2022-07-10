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
    [Route("restaurants/{restaurant-id:int}/reviews")]
    public async Task<ActionResult<int>> CreateReviewsForRestaurantAsync([FromBody] ReviewCreateDto dto, [FromRoute(Name = "restaurant-id")] int restaurantId)
    {
      _logger.LogInformation("Method: {methodName} called with restaurantId: {restaurantId}", nameof(CreateReviewsForRestaurantAsync), restaurantId);

      var userId = Request.Headers["UserId"].FirstOrDefault();

      if (string.IsNullOrEmpty(userId))
      {
        _logger.LogCritical("Method: {methodName} header value userId is null", nameof(CreateReviewsForRestaurantAsync));

        return Unauthorized();
      }

      var result = await _application.CreateReviewForRestaurantAsync(dto, restaurantId, Convert.ToInt32(userId));

      if (result is null) return BadRequest("User has allready created a review for that rastuarant");
      else return Ok(result);
    }

    [HttpGet]
    [Route("restaurants/{restaurant-id:int}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewReadDto>>> GetReviewsForRestaurantAsync([FromRoute(Name = "restaurant-id")] int restaurantId)
    {
      _logger.LogInformation("Method: {methodName} called with restaurantId: {restaurantId}", nameof(GetReviewsForRestaurantAsync), restaurantId);

      return Ok(await _application.GetReviewsForRestaurantAsync(restaurantId));
    }
  }
}
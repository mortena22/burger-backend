using Microsoft.EntityFrameworkCore;
using ReviewApi.Model;
using ReviewApi.Persistence.Context;

namespace ReviewApi.Persistence.DataService
{
  public class ReviewDataService : IReviewDataService
  {
    private readonly IDbContextFactory<ReviewContext> _contextFactory;
    private readonly ILogger<ReviewDataService> _logger;

    public ReviewDataService(IDbContextFactory<ReviewContext> contextFactory, ILogger<ReviewDataService> logger)
    {
      _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<ReviewReadDto>> GetReviewsForRastaurantAsync(int rastaurantId)
    {
      _logger.LogInformation("Method: {methodName} called with rastaurantId: {rastaurantId}", nameof(GetReviewsForRastaurantAsync), rastaurantId);

      var result = new List<ReviewReadDto>();

      using var context = _contextFactory.CreateDbContext();

      foreach (var review in context.Reviews.Where(a => a.RastaurantReviewed == rastaurantId))
      {
        var scores = await context.ReviewScores.Where(a => a.ReviewId == review.Id).Select(a => new ReviewScoreDto
        {
          Score = a.Score,
          Category = (ReviewScoreCategory)a.CategoryId
        }).ToListAsync();

        result.Add(new ReviewReadDto(review.Description, scores)
        {
          ImageName = review.ImageName
        });
      }

      return result;
    }
  }
}

//EF Migration
//Docker Image
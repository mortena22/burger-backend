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

      using var context = _contextFactory.CreateDbContext();

      return await (from review in context.Reviews
                    where review.RastaurantReviewed == rastaurantId

                    let reviewScores = (from score in review.Scores
                                        select new ReviewScoreDto
                                        {
                                          Score = score.Score,
                                          Category = (ReviewScoreCategory)score.CategoryId
                                        })

                    let reviewDto = new ReviewReadDto(review.Id, review.Description, review.ImageName, reviewScores)

                    select reviewDto).ToListAsync();
    }
  }
}

//Docker Image
using Microsoft.EntityFrameworkCore;
using ReviewApi.Domain.Model;
using ReviewApi.Persistence.Context;
using ReviewApi.Persistence.Model;

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
                                          Category = (ReviewApi.Domain.Model.ReviewScoreCategory)score.CategoryId
                                        })

                    let reviewDto = new ReviewReadDto(review.Id, review.Description, review.ImageName, reviewScores)

                    select reviewDto).ToListAsync();
    }

    public async Task<int?> CreateReviewFormRastaurantAsync(ReviewCreateDto review, int rastaurantId, int userId)
    {
      _logger.LogInformation("Method: {methodName} called with rastaurantId: {rastaurantId}, userId: {userId}", nameof(CreateReviewFormRastaurantAsync), rastaurantId, userId);

      using var context = _contextFactory.CreateDbContext();

      var existingReview = await context.Reviews.FirstOrDefaultAsync(a => a.RastaurantReviewed == rastaurantId && a.CreatedByUser == userId);

      if (existingReview is not null)
      {
        _logger.LogError("Method: {methodName}. Review allready for rastaurant: {rastaurantId} written existed by: {userId}", nameof(CreateReviewFormRastaurantAsync), rastaurantId, userId);

        return null;
      }

      var newEntity = Map(review, rastaurantId, userId);

      context.Reviews.Add(newEntity);
      await context.SaveChangesAsync();

      return newEntity.Id;
    }

    private Review Map(ReviewCreateDto dto, int rastaurantId, int userId)
    {
      return new Review
      {
        Description = dto.Description,
        ImageName = dto.ImageName,
        RastaurantReviewed = rastaurantId,
        CreatedByUser = userId,
        CreationDate = DateTime.Now,
        Scores = dto.Scores.Select(a => new ReviewScore
        {
          CategoryId = (int)a.Category,
          Score = a.Score
        }).ToList()
      };
    }
  }
}
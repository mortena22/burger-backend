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

    public async Task<IEnumerable<ReviewReadDto>> GetReviewsForRestaurantAsync(int restaurantId)
    {
      _logger.LogInformation("Method: {methodName} called with restaurantId: {restaurantId}", nameof(GetReviewsForRestaurantAsync), restaurantId);

      using var context = _contextFactory.CreateDbContext();

      return await (from review in context.Reviews
                    where review.RestaurantReviewed == restaurantId

                    let reviewScores = (from score in review.Scores
                                        select new ReviewScoreDto
                                        {
                                          Score = score.Score,
                                          Category = (ReviewApi.Domain.Model.ReviewScoreCategory)score.CategoryId
                                        })

                    let reviewDto = new ReviewReadDto(review.Id, review.Description, review.ImageName, reviewScores)

                    select reviewDto).ToListAsync();
    }

    public async Task<int?> CreateReviewFormRestaurantAsync(ReviewCreateDto review, int restaurantId, int userId)
    {
      _logger.LogInformation("Method: {methodName} called with restaurantId: {restaurantId}, userId: {userId}", nameof(CreateReviewFormRestaurantAsync), restaurantId, userId);

      using var context = _contextFactory.CreateDbContext();

      var existingReview = await context.Reviews.FirstOrDefaultAsync(a => a.RestaurantReviewed == restaurantId && a.CreatedByUser == userId);

      if (existingReview is not null)
      {
        _logger.LogError("Method: {methodName}. Review allready for restaurant: {restaurantId} written existed by: {userId}", nameof(CreateReviewFormRestaurantAsync), restaurantId, userId);

        return null;
      }

      var newEntity = Map(review, restaurantId, userId);

      context.Reviews.Add(newEntity);
      await context.SaveChangesAsync();

      return newEntity.Id;
    }

    private Review Map(ReviewCreateDto dto, int restaurantId, int userId)
    {
      return new Review
      {
        Description = dto.Description,
        ImageName = dto.ImageName,
        RestaurantReviewed = restaurantId,
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
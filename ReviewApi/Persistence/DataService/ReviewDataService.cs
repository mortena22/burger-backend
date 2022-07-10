using Microsoft.EntityFrameworkCore;
using ReviewApi.Model;
using ReviewApi.Persistence.Context;

namespace ReviewApi.Persistence.DataService
{
  public class ReviewDataService : IReviewDataService
  {
    private readonly IDbContextFactory<ReviewContext> _contextFactory;

    public ReviewDataService(IDbContextFactory<ReviewContext> contextFactory)
    {
      _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
    }

    public async Task<IEnumerable<ReviewReadDto>> GetReviewsForRastaurantAsync(int rastaurantId)
    {
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
//Controller
//Logning DataService
//UnitTest
//Docker Image
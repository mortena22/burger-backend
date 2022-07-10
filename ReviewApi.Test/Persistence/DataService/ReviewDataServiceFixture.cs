using Microsoft.EntityFrameworkCore;
using ReviewApi.Persistence.Context;
using ReviewApi.Persistence.DataService;
using ReviewApi.Persistence.Model;
using ReviewApi.Test.Persistence.Context;

namespace ReviewApi.Test.Persistence.DataService
{
  public class ReviewDataServiceFixture
  {
    private readonly IDbContextFactory<ReviewContext> _dbContextFactory;

    public ReviewDataServiceFixture()
    {
      _dbContextFactory = new MockDbContextFactory<ReviewContext>();
    }

    public IReviewDataService CreateSut()
    {
      return new ReviewDataService(_dbContextFactory);
    }

    public void CreateReviewsForCompany(int rastaurantId)
    {
      using var context = _dbContextFactory.CreateDbContext();

      foreach (var review in AddReviews(context, rastaurantId))
      {
        AddReviewScore(context, 1, review.Id);
        AddReviewScore(context, 2, review.Id);
        AddReviewScore(context, 3, review.Id);
      }

      context.SaveChanges();
    }

    public void InitTestDatabase()
    {
      using var context = _dbContextFactory.CreateDbContext();

      //Create database
      context.Database.EnsureDeleted();
      context.Database.EnsureCreated();

      //Add ReviewScoreCategories
      context.ReviewScoreCategories.Add(new ReviewScoreCategory
      {
        Id = 1,
        Description = "Taste"
      });

      context.ReviewScoreCategories.Add(new ReviewScoreCategory
      {
        Id = 2,
        Description = "Texture"
      });

      context.ReviewScoreCategories.Add(new ReviewScoreCategory
      {
        Id = 3,
        Description = "VisualPresentation"
      });

      context.SaveChanges();
    }

    public void Cleanup()
    {
      using var context = _dbContextFactory.CreateDbContext();

      context.Database.EnsureDeleted();
    }

    private void AddReviewScore(ReviewContext context, int category, int reviewId)
    {
      context.ReviewScores.Add(new ReviewScore
      {
        CategoryId = category,
        Score = Random.Shared.Next(1, 5),
        ReviewId = reviewId
      });
    }

    private List<Review> AddReviews(ReviewContext context, int rastaurantId)
    {
      var reviews = new List<Review>
      {
        new Review
        {
          Description = "A very tasty burger",
          ImageName = "stock_image",
          CreationDate = DateTime.Now,
          CreatedByUser = 10,
          RastaurantReviewed = rastaurantId
        },
        new Review
        {
          Description = "A very tasty burger, not good looking",
          ImageName = "nasty_burger_image",
          CreationDate = DateTime.Now,
          CreatedByUser = 27,
          RastaurantReviewed = rastaurantId
        }
      };

      context.Reviews.AddRange(reviews);
      context.SaveChanges();

      return reviews;
    }
  }
}
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReviewApi.Domain.Model;
using ReviewApi.Persistence.Context;
using ReviewApi.Persistence.DataService;
using ReviewApi.Persistence.Model;
using ReviewApi.Test.Persistence.Context;

namespace ReviewApi.Test.Persistence.DataService
{
  public class ReviewDataServiceFixture
  {
    private readonly IDbContextFactory<ReviewContext> _dbContextFactory;
    private readonly ILogger<ReviewDataService> _logger;

    public ReviewDataServiceFixture()
    {
      _dbContextFactory = new MockDbContextFactory<ReviewContext>();
      _logger = A.Fake<ILogger<ReviewDataService>>(); 
    }

    public IReviewDataService CreateSut()
    {
      return new ReviewDataService(_dbContextFactory, _logger);
    }

    public void CreateReviewsForRestaurant(int restaurantId)
    {
      using var context = _dbContextFactory.CreateDbContext();

      foreach (var review in AddReviews(context, restaurantId))
      {
        AddReviewScore(context, 1, review.Id);
        AddReviewScore(context, 2, review.Id);
        AddReviewScore(context, 3, review.Id);
      }

      context.SaveChanges();
    }

    public void CreateReviewForRestaurant(int restaurantId, int userId)
    {
      using var context = _dbContextFactory.CreateDbContext();

      var review = new Review
      {
        Description = "A very tasty burger",
        ImageName = "stock_image",
        CreationDate = DateTime.Now,
        CreatedByUser = userId,
        RestaurantReviewed = restaurantId
      };
      context.Reviews.Add(review);
      context.SaveChanges();

      AddReviewScore(context, 1, review.Id);
      AddReviewScore(context, 2, review.Id);
      AddReviewScore(context, 3, review.Id);

      context.SaveChanges();
    }

    public ReviewCreateDto CreateReviewCreateDto()
    {
      var scores = CreateReviewScoreDtos();

      return new ReviewCreateDto("A Burger", scores);
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

    private List<Review> AddReviews(ReviewContext context, int restaurantId)
    {
      var reviews = new List<Review>
      {
        new Review
        {
          Description = "A very tasty burger",
          ImageName = "stock_image",
          CreationDate = DateTime.Now,
          CreatedByUser = 10,
          RestaurantReviewed = restaurantId
        },
        new Review
        {
          Description = "A very tasty burger, not good looking",
          ImageName = "nasty_burger_image",
          CreationDate = DateTime.Now,
          CreatedByUser = 27,
          RestaurantReviewed = restaurantId
        }
      };

      context.Reviews.AddRange(reviews);
      context.SaveChanges();

      return reviews;
    }

    private List<ReviewScoreDto> CreateReviewScoreDtos()
    {
      return new List<ReviewScoreDto>
      {
        new ReviewScoreDto{Category = Domain.Model.ReviewScoreCategory.Taste, Score = 2},
        new ReviewScoreDto{Category = Domain.Model.ReviewScoreCategory.Texture, Score = 3},
        new ReviewScoreDto{Category = Domain.Model.ReviewScoreCategory.VisualPresentation, Score = 5}
      };
    }
  }
}
using FakeItEasy;
using Microsoft.Extensions.Logging;
using ReviewApi.Controllers;
using ReviewApi.Model;
using ReviewApi.Persistence.DataService;

namespace ReviewApi.Test.Controller
{
  public class ReviewControllerFixture
  {
    private readonly ILogger<ReviewController> _logger;

    public ReviewControllerFixture()
    {
      DataService = A.Fake<IReviewDataService>();
      _logger = A.Fake<ILogger<ReviewController>>();
    }

    public IReviewDataService DataService { get; }

    public ReviewController CreateSut()
    {
      return new ReviewController(DataService, _logger);
    }

    public IList<ReviewReadDto> CreateReviews()
    {
      return new List<ReviewReadDto>
      {
        CreateRandomReview(),
        CreateRandomReview(),
        CreateRandomReview()
      };
    }

    private ReviewReadDto CreateRandomReview()
    {
      var reviewScores = new List<ReviewScoreDto>
      { CreateReviewScore(ReviewScoreCategory.Taste),
        CreateReviewScore(ReviewScoreCategory.Taste),
        CreateReviewScore(ReviewScoreCategory.Taste)
      };

      return new ReviewReadDto(1, CreateRandomText(50), CreateRandomText(10), reviewScores);
    }

    private ReviewScoreDto CreateReviewScore(ReviewScoreCategory category)
    {
      return new ReviewScoreDto
      {
        Category = category,
        Score = Random.Shared.Next(1, 5)
      };
    }

    private string CreateRandomText(int length)
    {
      var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ";

      return new string(Enumerable.Range(1, length).Select(_ => chars[Random.Shared.Next(chars.Length)]).ToArray());
    }
  }
}
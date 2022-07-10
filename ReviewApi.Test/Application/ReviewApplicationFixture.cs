using FakeItEasy;
using Microsoft.Extensions.Logging;
using ReviewApi.Application;
using ReviewApi.Domain.Model;
using ReviewApi.Persistence.DataService;

namespace ReviewApi.Test.Application
{
  public class ReviewApplicationFixture
  {
    private readonly ILogger<ReviewApplication> _logger;

    public ReviewApplicationFixture()
    {
      DataService = A.Fake<IReviewDataService>();
      _logger = A.Fake<ILogger<ReviewApplication>>();
    }

    public IReviewDataService DataService { get; }

    public ReviewApplication CreateSut()
    {
      return new ReviewApplication(DataService, _logger);
    }

    public ReviewCreateDto CreateReviewCreateDto()
    {
      var scores = new List<ReviewScoreDto>
      {
        CreateReviewScore(ReviewScoreCategory.Taste),
        CreateReviewScore(ReviewScoreCategory.Texture),
        CreateReviewScore(ReviewScoreCategory.VisualPresentation)
      };

      return new ReviewCreateDto(CreateRandomText(50), scores);
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
      {
        CreateReviewScore(ReviewScoreCategory.Taste),
        CreateReviewScore(ReviewScoreCategory.Texture),
        CreateReviewScore(ReviewScoreCategory.VisualPresentation)
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
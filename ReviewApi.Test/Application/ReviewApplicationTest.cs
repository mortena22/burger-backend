using FakeItEasy;
using ReviewApi.Domain.Model;

namespace ReviewApi.Test.Application
{
  [TestClass]
  public class ReviewApplicationTest
  {
    private readonly ReviewApplicationFixture _fixture;

    public ReviewApplicationTest()
    {
      _fixture = new ReviewApplicationFixture();
    }

    [TestMethod, TestCategory("UnitTest")]
    public async Task CreateReviewsForRastaurant_ReturnsIdDataService_Await()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var rastaurantId = 10;
      var userId = 25;
      var reviewToBeCreated = _fixture.CreateReviewCreateDto();
      var expectedResult = 100;

      A.CallTo(() => _fixture.DataService.CreateReviewFormRastaurantAsync(reviewToBeCreated, rastaurantId, userId))
        .Returns(Task.FromResult<int?>(expectedResult));

      //Act
      var actualResult = await sut.CreateReviewForRastaurantAsync(reviewToBeCreated, rastaurantId, userId);

      //Assert
      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod, TestCategory("UnitTest")]
    public async Task GetReviewsForRastaurant_ReturnsReviewFromDataService_Await()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var rastaurantId = 10;
      var expectedResult = _fixture.CreateReviews();

      A.CallTo(() => _fixture.DataService.GetReviewsForRastaurantAsync(rastaurantId))
        .Returns(Task.FromResult<IEnumerable<ReviewReadDto>>(expectedResult));

      //Act
      var actualResult = await sut.GetReviewsForRastaurantAsync(rastaurantId);

      //Assert
      Assert.AreEqual(expectedResult.Count, actualResult.Count());
    }
  }
}
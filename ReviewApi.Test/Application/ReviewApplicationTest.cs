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
    public async Task CreateReviewsForRestaurant_ReturnsIdDataService_Await()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var restaurantId = 10;
      var userId = 25;
      var reviewToBeCreated = _fixture.CreateReviewCreateDto();
      var expectedResult = 100;

      A.CallTo(() => _fixture.DataService.CreateReviewFormRestaurantAsync(reviewToBeCreated, restaurantId, userId))
        .Returns(Task.FromResult<int?>(expectedResult));

      //Act
      var actualResult = await sut.CreateReviewForRestaurantAsync(reviewToBeCreated, restaurantId, userId);

      //Assert
      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod, TestCategory("UnitTest")]
    public async Task GetReviewsForRestaurant_ReturnsReviewFromDataService_Await()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var restaurantId = 10;
      var expectedResult = _fixture.CreateReviews();

      A.CallTo(() => _fixture.DataService.GetReviewsForRestaurantAsync(restaurantId))
        .Returns(Task.FromResult<IEnumerable<ReviewReadDto>>(expectedResult));

      //Act
      var actualResult = await sut.GetReviewsForRestaurantAsync(restaurantId);

      //Assert
      Assert.AreEqual(expectedResult.Count, actualResult.Count());
    }
  }
}
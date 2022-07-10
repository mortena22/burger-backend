using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using ReviewApi.Model;

namespace ReviewApi.Test.Controller
{
  [TestClass]
  public class ReviewControllerTest
  {
    private readonly ReviewControllerFixture _fixture;

    public ReviewControllerTest()
    {
      _fixture = new ReviewControllerFixture();
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
      Assert.AreEqual(expectedResult.Count, GetValueFromActionResult(actualResult)?.Count());
    }

    private T GetValueFromActionResult<T>(ActionResult<T> actionResult)
    {
      var result = actionResult.Result as OkObjectResult;

      return (T)result?.Value!;
    }
  }
}
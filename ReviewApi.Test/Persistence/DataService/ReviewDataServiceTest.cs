namespace ReviewApi.Test.Persistence.DataService
{
  [TestClass]
  public class ReviewDataServiceTest : IntegrationTestBase
  {
    private readonly ReviewDataServiceFixture _fixture;

    public ReviewDataServiceTest()
    {
      _fixture = new ReviewDataServiceFixture();
    }

    [TestMethod, TestCategory("IntegrationTest")]
    public async Task GetReviewsForRastaurant_NoReviewsForRastaurant_ReturnsEmptyCollection_Async()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var rastaurantId = 10;

      //Act
      var actual = await sut.GetReviewsForRastaurantAsync(rastaurantId);

      //Assert
      Assert.IsFalse(actual.Any());
    }

    [TestMethod, TestCategory("IntegrationTest")]
    public async Task GetReviewsForRastaurant_ReviewsFound_ReturnsCollectionWithReviws_Async()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var rastaurantId = 10;

      _fixture.CreateReviewsForRastaurant(rastaurantId);

      //Act
      var actual = await sut.GetReviewsForRastaurantAsync(rastaurantId);

      //Assert
      Assert.IsTrue(actual.Any());
    }

    [TestMethod, TestCategory("IntegrationTest")]
    public async Task CreateReviewFormRastaurant_ReviewAllreadyExistWithThatUserAndRasturant_ReturnsNull_Async()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var rastaurantId = 10;
      var userId = 2;

      var review = _fixture.CreateReviewCreateDto();
      _fixture.CreateReviewForRastaurant(rastaurantId, userId);

      //Act
      var actual = await sut.CreateReviewFormRastaurantAsync(review, rastaurantId, userId);

      //Assert
      Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("IntegrationTest")]
    public async Task CreateReviewFormRastaurant_NoReviewExistWithThatUserAndRasturant_ReturnsReviewId_Async()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var rastaurantId = 10;
      var userId = 2;

      var review = _fixture.CreateReviewCreateDto();

      //Act
      var actual = await sut.CreateReviewFormRastaurantAsync(review, rastaurantId, userId);

      //Assert
      Assert.IsNotNull(actual);
    }
  }
}


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
    public async Task GetReviewsForRestaurant_NoReviewsForRestaurant_ReturnsEmptyCollection_Async()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var restaurantId = 10;

      //Act
      var actual = await sut.GetReviewsForRestaurantAsync(restaurantId);

      //Assert
      Assert.IsFalse(actual.Any());
    }

    [TestMethod, TestCategory("IntegrationTest")]
    public async Task GetReviewsForRestaurant_ReviewsFound_ReturnsCollectionWithReviws_Async()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var restaurantId = 10;

      _fixture.CreateReviewsForRestaurant(restaurantId);

      //Act
      var actual = await sut.GetReviewsForRestaurantAsync(restaurantId);

      //Assert
      Assert.IsTrue(actual.Any());
    }

    [TestMethod, TestCategory("IntegrationTest")]
    public async Task CreateReviewFormRestaurant_ReviewAllreadyExistWithThatUserAndRasturant_ReturnsNull_Async()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var restaurantId = 10;
      var userId = 2;

      var review = _fixture.CreateReviewCreateDto();
      _fixture.CreateReviewForRestaurant(restaurantId, userId);

      //Act
      var actual = await sut.CreateReviewFormRestaurantAsync(review, restaurantId, userId);

      //Assert
      Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("IntegrationTest")]
    public async Task CreateReviewFormRestaurant_NoReviewExistWithThatUserAndRasturant_ReturnsReviewId_Async()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var restaurantId = 10;
      var userId = 2;

      var review = _fixture.CreateReviewCreateDto();

      //Act
      var actual = await sut.CreateReviewFormRestaurantAsync(review, restaurantId, userId);

      //Assert
      Assert.IsNotNull(actual);
    }
  }
}


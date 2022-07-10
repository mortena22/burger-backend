namespace ReviewApi.Test.Persistence.DataService
{
  [TestClass]
  public class ReviewDataServiceTest
  {
    private readonly ReviewDataServiceFixture _fixture;

    public ReviewDataServiceTest()
    {
      _fixture = new ReviewDataServiceFixture();
    }

    [TestInitialize()]
    public void TestInit()
    {
      _fixture.InitTestDatabase();
    }

    [TestCleanup()]
    public void TestCleanup()
    {
      _fixture.Cleanup();
    }

    [TestMethod()]
    public async Task GetReviewsForRestaurant_NoReviewsForRestaurant_ReturnsEmptyCollection_Async()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var rastaurantId = 10;

      //Act
      var actual = await sut.GetReviewsForRastaurantAsync(rastaurantId);

      //Assert
      Assert.IsFalse(actual.Any());
    }

    [TestMethod()]
    public async Task GetReviewsForRestaurant_ReviewsFound_ReturnsCollectionWithReviws_Async()
    {
      //Arrange
      var sut = _fixture.CreateSut();
      var rastaurantId = 10;

      _fixture.CreateReviewsForCompany(rastaurantId);

      //Act
      var actual = await sut.GetReviewsForRastaurantAsync(rastaurantId);

      //Assert
      Assert.IsTrue(actual.Any());
    }
  }
}


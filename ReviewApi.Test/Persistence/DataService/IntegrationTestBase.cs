using Microsoft.EntityFrameworkCore;
using ReviewApi.Persistence.Context;
using ReviewApi.Test.Persistence.Context;

namespace ReviewApi.Test.Persistence.DataService
{
  public class IntegrationTestBase
  {
    private readonly IDbContextFactory<ReviewContext> _dbContextFactory;

    public IntegrationTestBase()
    {
      _dbContextFactory = new MockDbContextFactory<ReviewContext>();
    }

    [TestInitialize]
    public void InitTestDatabase()
    {
      using var context = _dbContextFactory.CreateDbContext();

      //Create database
      context.Database.EnsureDeleted();
      context.Database.EnsureCreated();
    }

    [TestCleanup]
    public void Cleanup()
    {
      using var context = _dbContextFactory.CreateDbContext();

      context.Database.EnsureDeleted();
    }
  }
}
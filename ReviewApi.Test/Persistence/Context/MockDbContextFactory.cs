using Microsoft.EntityFrameworkCore;

namespace ReviewApi.Test.Persistence.Context
{
  public class MockDbContextFactory<T> : IDbContextFactory<T> where T : DbContext
  {
    #nullable disable

    private readonly string _connectionString;

    public MockDbContextFactory()
    {
      _connectionString = Environment.GetEnvironmentVariable("connection_string") ?? "Data Source=ReviewTest.db";
    }

    public T CreateDbContext()
    {
      var dbContext = Activator.CreateInstance(typeof(T), new object[] { new DbContextOptionsBuilder<T>().UseSqlite(_connectionString).Options }) as T;

      return dbContext;
    }
  }
}
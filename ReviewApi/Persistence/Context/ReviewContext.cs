using Microsoft.EntityFrameworkCore;
using ReviewApi.Persistence.Model;

namespace ReviewApi.Persistence.Context
{
  public class ReviewContext : DbContext
  {
    public ReviewContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<ReviewScore> ReviewScores => Set<ReviewScore>();
    public DbSet<ReviewScoreCategory> ReviewScoreCategories => Set<ReviewScoreCategory>();
  }
}
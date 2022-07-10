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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<ReviewScoreCategory>().HasData(
        new ReviewScoreCategory
        {
          Id = 1,
          Description = "Taste"
        },
        new ReviewScoreCategory
        {
          Id = 2,
          Description = "Texture"
        },
        new ReviewScoreCategory
        {
          Id = 3,
          Description = "VisualPresentation"
        });
    }
  }
}
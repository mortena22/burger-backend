using Microsoft.EntityFrameworkCore;
using ReviewApi.Persistence.Model;

namespace ReviewApi.Persistence.Context
{
  public interface IReviewContext
  {
    DbSet<Review> Reviews { get; }
    DbSet<ReviewScore> ReviewScores { get; }
    DbSet<ReviewScoreCategory> ReviewScoreCategories { get; }
  }
}
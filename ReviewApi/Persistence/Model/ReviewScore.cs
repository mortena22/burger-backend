namespace ReviewApi.Persistence.Model
{
  public class ReviewScore
  {
    public ReviewScore(ReviewScoreCategory category)
    {
      Category = category;
    }

    public int Id { get; set; }
    public ReviewScoreCategory Category { get; set; }
    public decimal Score { get; set; }
  }
}
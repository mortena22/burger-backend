namespace ReviewApi.Domain.Model
{
  public class ReviewScoreDto
  {
    public ReviewScoreCategory Category { get; set; }
    public decimal Score { get; set; }
  }
}
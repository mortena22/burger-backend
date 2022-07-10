namespace ReviewApi.Persistence.Model
{
  #nullable disable

  public class ReviewScore
  {
    public int Id { get; set; }
    public decimal Score { get; set; }

    public int ReviewId { get; set; }
    public Review Review { get; set; }

    public int CategoryId { get; set; }
  }
}
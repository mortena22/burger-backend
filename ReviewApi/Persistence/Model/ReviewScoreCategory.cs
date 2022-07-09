namespace ReviewApi.Persistence.Model
{
  public class ReviewScoreCategory
  {
    public ReviewScoreCategory(string description)
    {
      Description = description;
    }

    public int Id { get; set; }
    public string Description { get; set; }
  }
}
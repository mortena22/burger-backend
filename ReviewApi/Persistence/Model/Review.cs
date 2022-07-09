namespace ReviewApi.Persistence.Model
{
  public class Review
  {
    public Review(string description, string imageName, IEnumerable<ReviewScore> scores)
    {
      Description = description;
      Scores = scores;
      ImageName = imageName;
      CreationDate = DateTime.Now;
    }

    public int Id { get; set; }

    public string Description { get; set; }

    public string ImageName { get; set; }

    public IEnumerable<ReviewScore> Scores { get; set; }

    public int CreatedByUser { get; set; }

    public DateTime CreationDate { get; set; }

    public int ResturantReviewed { get; set; }
  }
}
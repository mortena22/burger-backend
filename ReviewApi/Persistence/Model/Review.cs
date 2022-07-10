namespace ReviewApi.Persistence.Model
{
  #nullable disable

  public class Review
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public string ImageName { get; set; }
    public int CreatedByUser { get; set; }
    public DateTime CreationDate { get; set; }
    public int RastaurantReviewed { get; set; }

    public List<ReviewScore> Scores { get; set; }
  }
}
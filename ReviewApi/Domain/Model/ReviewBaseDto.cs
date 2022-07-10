namespace ReviewApi.Domain.Model
{
  public class ReviewBaseDto
  {
    public ReviewBaseDto(string description, IEnumerable<ReviewScoreDto> scores) : this(description, null, scores)
    { }

    public ReviewBaseDto(string description, string? imageName, IEnumerable<ReviewScoreDto> scores)
    {
      Description = description;
      ImageName = imageName ?? "Stock_Image";
      Scores = scores;
    }

    public string Description { get; set; }

    public string ImageName { get; set; }

    public IEnumerable<ReviewScoreDto> Scores { get; set; }

    public decimal AvargeScore => Scores.Average(a => a.Score);
  }
}
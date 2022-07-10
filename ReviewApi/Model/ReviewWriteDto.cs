namespace ReviewApi.Model
{
  public class ReviewWriteDto
  {
    public ReviewWriteDto(string description, string imageName, IEnumerable<ReviewScoreDto> scores)
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
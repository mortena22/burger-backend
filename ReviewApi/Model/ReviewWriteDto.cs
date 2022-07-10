namespace ReviewApi.Model
{
  public class ReviewWriteDto
  {
    public ReviewWriteDto(string description, IEnumerable<ReviewScoreDto> scores)
    {
      Description = description;
      Scores = scores;
      ImageName = "Stock_Image";
    }

    public string Description { get; set; }

    public string ImageName { get; set; }

    public IEnumerable<ReviewScoreDto> Scores { get; set; }

    public decimal AvargeScore => Scores.Average(a => a.Score);
  }
}
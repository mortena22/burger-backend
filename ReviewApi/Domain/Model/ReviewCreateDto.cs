namespace ReviewApi.Domain.Model
{
  public class ReviewCreateDto : ReviewBaseDto
  {
    public ReviewCreateDto() : this("", new List<ReviewScoreDto>())
    { }

    public ReviewCreateDto(string description, IEnumerable<ReviewScoreDto> scores) : this(description, null, scores)
    { }

    public ReviewCreateDto(string description, string? imageName, IEnumerable<ReviewScoreDto> scores) : base(description, imageName, scores)
    { }
  }
}
namespace ReviewApi.Domain.Model
{
  public class ReviewReadDto : ReviewBaseDto
  {
    public ReviewReadDto(int id, string description, IEnumerable<ReviewScoreDto> scores) : this(id, description, null, scores)
    { }

    public ReviewReadDto(int id, string description, string? imageName, IEnumerable<ReviewScoreDto> scores) : base(description, imageName, scores)
    {
      Id = id;
    }

    public int Id { get; }
  }
}
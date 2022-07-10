namespace ReviewApi.Model
{
  public class ReviewReadDto : ReviewWriteDto
  {
    public ReviewReadDto(int id, string description, string imageName, IEnumerable<ReviewScoreDto> scores) : base(description, imageName, scores)
    {
      Id = id;
    }

    public int Id { get; }
  }
}
namespace ReviewApi.Model
{
  public class ReviewReadDto : ReviewWriteDto
  {
    public ReviewReadDto(string description, IEnumerable<ReviewScoreDto> scores) : base(description, scores)
    {

    }

    public int Id { get; set; }
  }
}
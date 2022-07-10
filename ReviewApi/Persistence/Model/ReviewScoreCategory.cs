using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewApi.Persistence.Model
{
  #nullable disable

  public class ReviewScoreCategory
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Description { get; set; }
  }
}
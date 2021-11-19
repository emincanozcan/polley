namespace Polley.WebApi.Data;

public class Poll : BaseEntity
{
    public Question Question { get; set; }
    public ICollection<Answer> Answers { get; set; }
}
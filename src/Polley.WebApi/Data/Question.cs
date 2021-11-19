namespace Polley.WebApi.Data;

public class Question : BaseEntity
{
    public string Content { get; set; }

    public int PollId { get; set; }

    public Poll Poll { get; set; }
}
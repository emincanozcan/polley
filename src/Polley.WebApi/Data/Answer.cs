namespace Polley.WebApi.Data;

public class Answer : BaseEntity
{
    public string Content { get; set; }
    
    public int SelectedCount { get; set; }
    
    public int PollId { get; set; }
    
    public Poll Poll { get; set; }
}
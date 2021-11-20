namespace Polley.WebApi.DTOs.Request;

public class VoteCreateRequestDto
{
    public int PollId { get; set; }
    public int AnswerId { get; set; }
}
using Polley.WebApi.Data;

namespace Polley.WebApi.DTOs;

public class VoteDto
{
    public int PollId { get; set; }
    public int AnswerId { get; set; }
}
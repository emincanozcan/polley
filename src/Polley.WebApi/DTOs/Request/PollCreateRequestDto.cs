namespace Polley.WebApi.DTOs.Request;

public class PollCreateRequestDto
{
    public string Question { get; set; }
    public List<string> Answers { get; set; }
}
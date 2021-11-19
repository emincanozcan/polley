namespace Polley.WebApi.DTOs;

public class PollCreateDto
{
    public string Question { get; set; }
    public List<string> Answers { get; set; }
}
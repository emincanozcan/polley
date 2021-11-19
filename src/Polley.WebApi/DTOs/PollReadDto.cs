using Polley.WebApi.Data;

namespace Polley.WebApi.DTOs;

public class PollReadDto
{
    public int Id { get; set; }
    public QuestionReadDto question { get; set; }
    public ICollection<AnswerReadDto> answers { get; set; }
}
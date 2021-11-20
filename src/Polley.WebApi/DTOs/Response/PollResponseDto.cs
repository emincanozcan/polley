namespace Polley.WebApi.DTOs.Response;

public class PollResponseDto
{
    public int Id { get; set; }
    public QuestionResponseDto question { get; set; }
    public ICollection<AnswerResponseDto> answers { get; set; }
}
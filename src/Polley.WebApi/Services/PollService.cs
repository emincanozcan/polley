using Microsoft.EntityFrameworkCore;
using Polley.WebApi.Context;
using Polley.WebApi.Data;
using Polley.WebApi.DTOs.Request;
using Polley.WebApi.DTOs.Response;

namespace Polley.WebApi.Services;

public class PollService : IPollService
{
    private readonly PolleyDbContext _dbContext;

    public PollService(PolleyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PollResponseDto> CreatePoll(PollCreateRequestDto pollCreateRequestDto)
    {
        var question = new Question {Content = pollCreateRequestDto.Question};
        List<Answer> answers = pollCreateRequestDto.Answers.Select(c => new Answer {Content = c, SelectedCount = 0}).ToList();

        Poll poll = new Poll
        {
            Answers = answers,
            Question = question
        };

        _dbContext.Polls.Add(poll);
        await _dbContext.SaveChangesAsync();

        return new PollResponseDto
        {
            Id = poll.Id,
            question = new QuestionResponseDto
            {
                Id = poll.Question.Id,
                Content = poll.Question.Content
            },
            answers = poll.Answers.Select(answer => new AnswerResponseDto
                {Id = answer.Id, Content = answer.Content, SelectedCount = answer.SelectedCount}).ToList()
        };
    }

    public async Task<PollResponseDto> GetPollById(int id)
    {
        var poll = await _dbContext.Polls
            .Where(poll => poll.Id == id)
            .Include(poll => poll.Question)
            .Include(poll => poll.Answers)
            .FirstOrDefaultAsync();

        if (poll == null)
        {
            throw new Exception("Poll not found");
        }

        return new PollResponseDto
        {
            Id = poll.Id,
            question = new QuestionResponseDto
            {
                Id = poll.Question.Id,
                Content = poll.Question.Content
            },
            answers = poll.Answers.Select(answer => new AnswerResponseDto
                {Id = answer.Id, Content = answer.Content, SelectedCount = answer.SelectedCount}).ToList()
        };
    }

    public async Task<bool> SaveVote(VoteCreateRequestDto voteCreateRequest)
    {
        // Note:
        // This logic is a little bit problematic, open to conflicts.
        // It fetches data and increases column, after that updates the record at Database.
        // A better approach might be doing this increment at database `SET SelectedCount=SelectedCount + 1` 
        // Another solution is using locks ofc but I think it should be last option.

        var answer = await _dbContext.Answers
            .Where(a => a.PollId == voteCreateRequest.PollId && a.Id == voteCreateRequest.AnswerId)
            .FirstOrDefaultAsync();

        if (answer == null)
        {
            throw new Exception("Provided data is invalid.");
        }

        answer.SelectedCount += 1;

        await _dbContext.SaveChangesAsync();

        return true;
    }
}
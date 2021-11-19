using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Polley.WebApi.Context;
using Polley.WebApi.Data;
using Polley.WebApi.DTOs;

namespace Polley.WebApi.Services;

public class PollService : IPollService
{
    private readonly PolleyDbContext _dbContext;

    public PollService(PolleyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PollReadDto> CreatePoll(PollCreateDto pollCreateDto)
    {
        var question = new Question {Content = pollCreateDto.Question};
        List<Answer> answers = pollCreateDto.Answers.Select(c => new Answer {Content = c, SelectedCount = 0}).ToList();

        Poll poll = new Poll
        {
            Answers = answers,
            Question = question
        };

        _dbContext.Polls.Add(poll);
        await _dbContext.SaveChangesAsync();

        return new PollReadDto
        {
            Id = poll.Id,
            question = new QuestionReadDto
            {
                Id = poll.Question.Id,
                Content = poll.Question.Content
            },
            answers = poll.Answers.Select(answer => new AnswerReadDto
                {Id = answer.Id, Content = answer.Content, SelectedCount = answer.SelectedCount}).ToList()
        };
    }

    public async Task<PollReadDto> GetPollById(int id)
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

        return new PollReadDto
        {
            Id = poll.Id,
            question = new QuestionReadDto
            {
                Id = poll.Question.Id,
                Content = poll.Question.Content
            },
            answers = poll.Answers.Select(answer => new AnswerReadDto
                {Id = answer.Id, Content = answer.Content, SelectedCount = answer.SelectedCount}).ToList()
        };
    }
}
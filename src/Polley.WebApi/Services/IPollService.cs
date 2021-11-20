using Polley.WebApi.DTOs;

namespace Polley.WebApi.Services;

public interface IPollService
{
    public Task<PollReadDto> CreatePoll(PollCreateDto pollCreateDto);
    public Task<PollReadDto> GetPollById(int id);
    public Task<bool> SaveVote(VoteDto vote);
}
using Polley.WebApi.DTOs.Request;
using Polley.WebApi.DTOs.Response;

namespace Polley.WebApi.Services;

public interface IPollService
{
    public Task<PollResponseDto> CreatePoll(PollCreateRequestDto pollCreateRequestDto);
    public Task<PollResponseDto> GetPollById(int id);
    public Task<bool> SaveVote(VoteCreateRequestDto voteCreateRequest);
}
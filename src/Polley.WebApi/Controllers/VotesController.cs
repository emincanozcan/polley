using Microsoft.AspNetCore.Mvc;
using Polley.WebApi.DTOs.Request;
using Polley.WebApi.Services;

namespace Polley.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VotesController : ControllerBase
{
    private readonly IPollService _pollService;

    public VotesController(IPollService pollService)
    {
        _pollService = pollService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VoteCreateRequestDto voteCreateRequestDto)
    {
        try
        {
            await _pollService.SaveVote(voteCreateRequestDto);
        }
        catch (Exception e)
        {
            return NotFound();
        }

        return StatusCode(201, new
        {
            Message = "Vote is added."
        });
    }
}
using Microsoft.AspNetCore.Mvc;
using Polley.WebApi.DTOs;
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
    public async Task<IActionResult> Create([FromBody] VoteDto voteDto)
    {
        try
        {
            await _pollService.SaveVote(voteDto);
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
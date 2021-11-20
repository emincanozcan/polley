using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polley.WebApi.DTOs.Request;
using Polley.WebApi.Services;

namespace Polley.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController : ControllerBase
{
    private readonly IPollService _pollService;

    public PollsController(IPollService pollService)
    {
        _pollService = pollService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] PollCreateRequestDto pollCreateRequestDto)
    {
        var obj = await _pollService.CreatePoll(pollCreateRequestDto);

        return CreatedAtAction(nameof(Show), new {id = obj.Id},obj );
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Show(int id)
    {
        var poll = await _pollService.GetPollById(id);
        return Ok(poll);
    }
}
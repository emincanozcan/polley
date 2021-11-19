using Microsoft.AspNetCore.Mvc;
using Polley.WebApi.DTOs;
using Polley.WebApi.Services;

namespace Polley.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollController : ControllerBase
{
    private readonly IPollService _pollService;

    public PollController(IPollService pollService)
    {
        _pollService = pollService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PollCreateDto pollCreateDto)
    {
        var obj = await _pollService.CreatePoll(pollCreateDto);

        return CreatedAtAction(nameof(Show), new {id = obj.Id},obj );
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Show(int id)
    {
        var poll = await _pollService.GetPollById(id);
        return Ok(poll);
    }
}
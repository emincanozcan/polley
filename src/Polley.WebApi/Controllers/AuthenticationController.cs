using Microsoft.AspNetCore.Mvc;
using Polley.WebApi.DTOs.Request;
using Polley.WebApi.Services;

namespace Polley.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthenticationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto data)
    {
        try
        {
            var r = await _userService.Login(data);
            return Ok(r);
        }
        catch (Exception e)
        {
            return BadRequest(new {Message = e.Message});
        }
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserCreateRequestDto data)
    {
        try
        {
            var r = await _userService.Register(data);
            return Ok(r);
        }
        catch (Exception e)
        {
            return BadRequest(new {Message = e.Message});
        }
    }
}
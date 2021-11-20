using Polley.WebApi.DTOs.Request;
using Polley.WebApi.DTOs.Response;

namespace Polley.WebApi.Services;

public interface IUserService
{
    public Task<UserLoginResponseDto> Login(UserLoginRequestDto data);
    public Task<UserLoginResponseDto> Register(UserCreateRequestDto data);
}
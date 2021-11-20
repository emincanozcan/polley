namespace Polley.WebApi.DTOs.Response;

public class UserLoginResponseDto
{
    public string Token { get; set; }
    public DateTime TokenExpireDate { get; set; }
    public UserResponseDto User { get; set; }
}
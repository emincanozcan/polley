using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Polley.WebApi.Data;
using Polley.WebApi.DTOs.Request;
using Polley.WebApi.DTOs.Response;

namespace Polley.WebApi.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<UserLoginResponseDto> Login(UserLoginRequestDto data)
    {
        var user = await _userManager.FindByEmailAsync(data.Email);
        if (user == null)
        {
            throw new Exception("User is not found");
        }

        if (!(await _userManager.CheckPasswordAsync(user, data.Password)))
        {
            throw new Exception("Wrong password");
        }

        return await GetUserLoginResponseDto(user);
    }

    public async Task<UserLoginResponseDto> Register(UserCreateRequestDto data)
    {
        if ((await _userManager.FindByEmailAsync(data.Email)) != null)
        {
            throw new Exception("Email address is already registered");
        }

        var user = new User
        {
            UserName = data.Email,
            Email = data.Email,
            FirstName = data.FirstName,
            LastName = data.LastName,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await _userManager.CreateAsync(user, data.Password);
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.ToString());
        }

        if (!await _roleManager.RoleExistsAsync("default"))
            await _roleManager.CreateAsync(new IdentityRole("default"));

        await _userManager.AddToRoleAsync(user, "default");


        return await GetUserLoginResponseDto(user);
    }


    private async Task<UserLoginResponseDto> GetUserLoginResponseDto(User user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        authClaims.AddRange(
            userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole))
        );

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new UserLoginResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            TokenExpireDate = token.ValidTo,
            User = new UserResponseDto
            {
                Id = Guid.Parse(user.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            }
        };
    }
}
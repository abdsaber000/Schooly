using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Authentication.Commands.Login;

public class LoginUserCommand : IRequest<Result<string>>
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public LoginUserCommandHandler(UserManager<ApplicationUser> userManager , IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _userManager.FindByEmailAsync(request.Email);
        if (existUser == null)
        {
            return Result<string>.Failure("Invalid Credentials.");
        }
        
        var isCorrectPassword = await _userManager.CheckPasswordAsync(existUser, request.Password);
        if (!isCorrectPassword)
        {
            return Result<string>.Failure("Invalid Credentials.");
        }
        
        return Result<string>.Success("" , await GenerateToken(existUser));
    }
    
    private async Task<string> GenerateToken(ApplicationUser user)
    {
        var claims = new List<Claim>();

        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
        }

        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        SigningCredentials signingCred = new(
            securityKey,
            SecurityAlgorithms.HmacSha256
        );

        JwtSecurityToken myToken = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signingCred
        );

        return new JwtSecurityTokenHandler().WriteToken(myToken);
    }
}
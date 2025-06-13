using System.ComponentModel.DataAnnotations;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.Authentication.Dtos;
using SchoolManagement.Application.Services.TokenService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Authentication.Commands.Login;

public class LoginUserCommand : IRequest<Result<LoginDto>>
{
    [Required(ErrorMessage =  "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; } = true;
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IStringLocalizer<LoginUserCommandHandler> _localizer;
    public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService, IStringLocalizer<LoginUserCommandHandler> localizer)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _localizer = localizer;
    }
    public async Task<Result<LoginDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _userManager.FindByEmailAsync(request.Email);
        var isCorrectPassword = existUser != null 
                                && await _userManager.CheckPasswordAsync(existUser, request.Password);
        
        if (existUser is null || !isCorrectPassword )
        {
            return Result<LoginDto>.Failure(_localizer["Invalid Credentials."] , HttpStatusCode.Unauthorized);
        }
        var token = await _tokenService.GenerateToken(existUser, request.RememberMe);
        
        var loginDto = existUser.ToLoginDto();
        
        return Result<LoginDto>.Success(loginDto, token);
    }
}
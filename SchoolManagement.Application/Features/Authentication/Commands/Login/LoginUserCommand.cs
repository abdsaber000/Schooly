using System.ComponentModel.DataAnnotations;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.Authentication.Dtos;
using SchoolManagement.Application.Services.TokenService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Features.Authentication.Commands.Login;

public class LoginUserCommand : IRequest<Result<LoginDto>>
{
    [Required(ErrorMessage =  "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IStringLocalizer<LoginUserCommandHandler> _localizer;
    private readonly string _urlPrefix;
    public LoginUserCommandHandler(
        UserManager<ApplicationUser> userManager, 
        ITokenService tokenService, 
        IStringLocalizer<LoginUserCommandHandler> localizer,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _localizer = localizer;
        _urlPrefix = configuration["Url:UploadPrefix"] ?? throw new ArgumentNullException(nameof(configuration));
    }
    public async Task<Result<LoginDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _userManager.FindByEmailAsync(request.Email);
        var isCorrectPassword = existUser != null 
                                && await _userManager.CheckPasswordAsync(existUser, request.Password);
        
        if (existUser is null || !isCorrectPassword )
        {
            return Result<LoginDto>.Failure(_localizer["Invalid Credentials."] , HttpStatusCode.Forbidden);
        }
        var token = await _tokenService.GenerateToken(existUser, true);
        
        var loginDto = existUser.ToLoginDto();
        HandlePictureUrl(loginDto);
        return Result<LoginDto>.Success(loginDto, token);
    }

    private void HandlePictureUrl(LoginDto result)
    {
        if (result.ProfilePictureUrl != null)
        {
            result.ProfilePictureUrl = _urlPrefix + result.ProfilePictureUrl;
        }
    }
}
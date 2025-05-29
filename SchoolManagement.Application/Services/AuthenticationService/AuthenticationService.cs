using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.AuthenticationService;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UserAuthenticationService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;   
    }
    public async Task<ApplicationUser> GetCurrentUserAsync(IHttpContextAccessor contextAccessor)
    {
        if (contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated != true)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var userId = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedAccessException("User ID not found in claims.");
        }

        var user = await _userManager.FindByIdAsync(userId) ?? throw new UnauthorizedAccessException("User is not found.");
        return user;
    }
}

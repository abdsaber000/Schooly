using System;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.AuthenticationService;

public interface IUserAuthenticationService
{
    Task<ApplicationUser> GetCurrentUserAsync(IHttpContextAccessor contextAccessor);
}

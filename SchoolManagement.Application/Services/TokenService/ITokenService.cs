using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.TokenService;

public interface ITokenService
{
    Task<string> GenerateToken(ApplicationUser user, bool rememberMe);
}
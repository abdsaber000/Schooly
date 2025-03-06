using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.PasswordReset.Command.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Result<string>>
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
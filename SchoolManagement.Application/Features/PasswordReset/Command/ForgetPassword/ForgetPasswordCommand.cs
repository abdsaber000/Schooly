using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.PasswordReset.Command.ForgetPassword
{
    public class ForgetPasswordCommand : IRequest<Result<string>>
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
    }
}
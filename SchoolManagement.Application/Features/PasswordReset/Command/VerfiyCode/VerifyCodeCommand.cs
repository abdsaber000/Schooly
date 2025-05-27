using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.PasswordReset.Command.VerifyCode
{
    public class VerifyCodeCommand : IRequest<Result<string>>
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Verification code is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Verification code must be exactly 6 digits")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Verification code must contain only numbers")]
        public string Code { get; set; } = string.Empty;
    }
}

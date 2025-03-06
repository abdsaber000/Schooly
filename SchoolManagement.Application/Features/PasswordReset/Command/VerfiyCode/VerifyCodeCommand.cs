using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.PasswordReset.Command.VerifyCode
{
    public class VerifyCodeCommand : IRequest<Result<string>>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(6, MinimumLength = 6)]
        [RegularExpression(@"^\d{6}$")]
        public string Code { get; set; } = string.Empty;
    }
}
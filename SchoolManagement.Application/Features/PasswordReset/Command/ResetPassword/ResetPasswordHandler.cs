using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.PasswordReset.Command.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IResetCodeRepository _resetCodeRepository;
        private readonly IStringLocalizer<ResetPasswordHandler> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordHandler(
            IStudentRepository studentRepository,
            IResetCodeRepository resetCodeRepository,
            IStringLocalizer<ResetPasswordHandler> localizer,
            UserManager<ApplicationUser> userManager)
        {
            _studentRepository = studentRepository;
            _resetCodeRepository = resetCodeRepository;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var student = await _studentRepository.GetStudentByEmail(request.Email);
                if (student == null)
                {
                    return Result<string>.Failure(_localizer["NoStudentFoundForEmail"], HttpStatusCode.NotFound);
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(student);
                var result = await _userManager.ResetPasswordAsync(student, token, request.NewPassword);

                if (!result.Succeeded)
                {
                    return Result<string>.Failure(_localizer["PasswordResetFailed"], HttpStatusCode.InternalServerError);
                }

                var resetCode = await _resetCodeRepository.GetResetCodeByEmailAsync(request.Email, cancellationToken);
                if (resetCode != null)
                {
                    await _resetCodeRepository.RemoveResetCodeAsync(resetCode, cancellationToken);
                }

                return Result<string>.SuccessMessage(_localizer["PasswordResetSuccessful"]);
            }
            catch (Exception)
            {
                return Result<string>.Failure(_localizer["InternalServerError"], HttpStatusCode.InternalServerError);
            }
        }
    }
}
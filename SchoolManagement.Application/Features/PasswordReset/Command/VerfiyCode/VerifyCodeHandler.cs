using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.PasswordReset.Command.VerifyCode
{
    public class VerifyCodeHandler : IRequestHandler<VerifyCodeCommand, Result<string>>
    {
        private readonly IResetCodeRepository _resetCodeRepository;
        private readonly IStringLocalizer<VerifyCodeHandler> _localizer;

        public VerifyCodeHandler(IResetCodeRepository resetCodeRepository, IStringLocalizer<VerifyCodeHandler> localizer)
        {
            _resetCodeRepository = resetCodeRepository;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(VerifyCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return Result<string>.Failure(_localizer["EmailRequired"], HttpStatusCode.BadRequest);
                }

                if (string.IsNullOrWhiteSpace(request.Code) || request.Code.Length != 6 || !int.TryParse(request.Code, out _))
                {
                    return Result<string>.Failure(_localizer["InvalidCodeFormat"], HttpStatusCode.BadRequest);
                }

                var resetCode = await _resetCodeRepository.GetResetCodeByEmailAsync(request.Email, cancellationToken);
                if (resetCode == null)
                {
                    return Result<string>.Failure(_localizer["NoResetCodeFound"], HttpStatusCode.NotFound);
                }

                if (resetCode.Code != request.Code)
                {
                    return Result<string>.Failure(_localizer["IncorrectVerificationCode"], HttpStatusCode.Unauthorized);
                }

                if (resetCode.ExpirationTime < DateTime.UtcNow)
                {
                    return Result<string>.Failure(_localizer["VerificationCodeExpired"], HttpStatusCode.Gone);
                }

                return Result<string>.SuccessMessage(_localizer["VerificationSuccessful"]);
            }
            catch (Exception)
            {
                return Result<string>.Failure(_localizer["InternalServerError"], HttpStatusCode.InternalServerError);
            }
        }
    }
}
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.Email.Command.SendEmail;
using SchoolManagement.Application.Services.EmailService;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.Email.Command.SendEmail
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result<string>>
    {
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<SendEmailCommandHandler> _localizer;

        public SendEmailCommandHandler(IEmailService emailService, IStringLocalizer<SendEmailCommandHandler> localizer)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _emailService.SendEmailAsync(request.ToEmail, request.Subject, request.Body, request.Attachments);
                return Result<string>.SuccessMessage(_localizer["Email.Success"]);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(_localizer["Email.Failed", ex.Message], HttpStatusCode.InternalServerError);
            }
        }
    }
}

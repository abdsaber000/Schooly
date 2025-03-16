using System;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Services.EmailService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.PasswordReset.Command.ForgetPassword
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, Result<string>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IResetCaodeRepository _resetCodeRepository;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<ForgetPasswordCommandHandler> _localizer;

        public ForgetPasswordCommandHandler(
            IStudentRepository studentRepository,
            IResetCodeRepository resetCodeRepository,
            IEmailService emailService,
            IStringLocalizer<ForgetPasswordCommandHandler> localizer)
        {
            _studentRepository = studentRepository;
            _resetCodeRepository = resetCodeRepository;
            _emailService = emailService;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _studentRepository.GetStudentByEmail(request.Email);
                if (user == null)
                {
                    return Result<string>.Failure(_localizer["This email isn't linked to a student account."], HttpStatusCode.NotFound);
                }

                var verificationCode = GenerateVerificationCode();

                var expirationTime = DateTime.UtcNow.AddMinutes(10);

                var existingCode = await _resetCodeRepository.GetResetCodeByEmailAsync(request.Email, cancellationToken);
                if (existingCode != null)
                {
                    await _resetCodeRepository.RemoveResetCodeAsync(existingCode, cancellationToken);
                }

                var resetCode = new ResetCode
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    Code = verificationCode,
                    ExpirationTime = expirationTime
                };

                await _resetCodeRepository.AddResetCodeAsync(resetCode, cancellationToken);

                string subject = "Password Reset Request";
                string body = GenerateEmailBody(user.Name, request.Email, verificationCode);

                await _emailService.SendEmailAsync(request.Email, subject, body);

                return Result<string>.SuccessMessage(_localizer["Password reset email sent successfully"]);
            }
            catch (Exception)
            {
                return Result<string>.Failure(_localizer["An error occurred while processing your request."], HttpStatusCode.InternalServerError);
            }
        }

        private string GenerateVerificationCode()
        {
            var verificationCodeBytes = new byte[4];
            RandomNumberGenerator.Fill(verificationCodeBytes);
            var verificationCodeNumber = BitConverter.ToInt32(verificationCodeBytes) % 1000000;
            if (verificationCodeNumber < 0) verificationCodeNumber *= -1;
            return verificationCodeNumber.ToString("D6");
        }

        private string GenerateEmailBody(string userName, string email, string verificationCode)
        {
            return $@"
     <div style='font-family: Arial, sans-serif; padding: 20px; max-width: 600px; margin: 0 auto; border: 1px solid #e0e0e0; border-radius: 8px; background-color: #ffffff;'>
         <div style='text-align: center; padding: 20px; background-color: #007bff; border-radius: 8px 8px 0 0;'>
             <h2 style='color: #ffffff; margin: 0;'>Password Reset Request</h2>
         </div>

         <div style='padding: 20px;'>
             <p style='font-size: 16px; color: #333;'>
                 Hello <strong>{userName}</strong>,
             </p>

             <p style='font-size: 16px; color: #333;'>
                 You recently requested to reset your password for your account 
                 <strong style='color: #007bff;'>{email}</strong>.
             </p>

             <div style='text-align: center; margin: 20px 0; padding: 15px; background-color: #f8f9fa; border-radius: 8px;'>
                 <p style='font-size: 18px; font-weight: bold; color: #007bff; margin: 0;'>
                     Your verification code:
                 </p>
                 <p style='font-size: 28px; font-weight: bold; color: #28a745; margin: 10px 0;'>
                     {verificationCode}
                 </p>
             </div>

             <p style='font-size: 16px; color: #333;'>
                 This code is valid for <strong>10 minutes</strong>. Please use it to reset your password.
             </p>

             <p style='font-size: 16px; color: #333;'>
                 If you didn't request this, you can safely ignore this email.
             </p>
         </div>

         <div style='text-align: center; padding: 20px; background-color: #f8f9fa; border-radius: 0 0 8px 8px;'>
             <p style='font-size: 14px; color: #777; margin: 0;'>
                 Best regards,<br/>
                 <strong style='color: #007bff;'>School Management Team</strong>
             </p>
         </div>
     </div>";
        }
    }
}
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Services.EmailService;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body, List<IFormFile> attachments = null);
}
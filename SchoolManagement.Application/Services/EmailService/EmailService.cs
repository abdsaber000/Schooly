using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;

namespace SchoolManagement.Application.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromName;
        private readonly string _fromAddress;

        public EmailService(IConfiguration configuration)
        {
            _smtpServer = configuration["EmailSettings:SmtpServer"] ?? throw new ArgumentNullException("SMTP server is not configured.");
            _smtpPort = configuration.GetValue<int>("EmailSettings:SmtpPort");
            _smtpUsername = configuration["EmailSettings:SmtpUsername"] ?? throw new ArgumentNullException("SMTP username is not configured.");
            _smtpPassword = configuration["EmailSettings:SmtpPassword"] ?? throw new ArgumentNullException("SMTP password is not configured.");
            _fromName = configuration["EmailSettings:FromName"] ?? "";
            _fromAddress = configuration["EmailSettings:FromAddress"] ?? throw new ArgumentNullException("Sender email address is not configured.");
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body, List<IFormFile> attachments = null)
        {
            if (string.IsNullOrWhiteSpace(toEmail) || !IsValidEmail(toEmail))
                throw new ArgumentException("Invalid recipient email address.", nameof(toEmail));

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_fromName, _fromAddress));
            email.To.Add(new MailboxAddress(toEmail, toEmail));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            AddAttachments(bodyBuilder, attachments);
            email.Body = bodyBuilder.ToMessageBody();

            using var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
                await client.SendAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to send email: " + ex.Message, ex);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }

        private void AddAttachments(BodyBuilder bodyBuilder, List<IFormFile> attachments)
        {
            if (attachments == null) return;

            foreach (var file in attachments)
            {
                if (file.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    file.CopyTo(memoryStream);
                    bodyBuilder.Attachments.Add(file.FileName, memoryStream.ToArray(), ContentType.Parse(file.ContentType));
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

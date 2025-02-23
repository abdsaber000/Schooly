using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.Email.Command.SendEmail

{
    public class SendEmailCommand : IRequest<Result<string>>
    {
        [Required(ErrorMessage = "Validation.RecipientRequired")]
        [EmailAddress(ErrorMessage = "Validation.InvalidEmail")]
        public string ToEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Validation.SubjectRequired")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Validation.BodyRequired")]
        public string Body { get; set; } = string.Empty;

        public List<IFormFile>? Attachments { get; set; }
    }
}

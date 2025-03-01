using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using MimeKit.Encodings;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.HomeWorke.Commands.AddHomeworke;

public class AddHomeWorkCommands : IRequest<Result<string>>
{
    [Required]
    public Guid lessonId { get; set; }
    [Required]
    public Guid classRoomId { get; set; }
    [Required]    
    public IFormFile file { get; set; }
}

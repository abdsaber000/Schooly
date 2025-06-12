using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.HomeWork.Commands.AddHomeWork;

public class AddHomeWorkCommands : IRequest<Result<string>>
{
    [Required]
    public Guid lessonId { get; set; }

    [Required]
    public DateTime ToDate { get; set; }
    [Required]    
    public string FileUrl { get; set; }
}

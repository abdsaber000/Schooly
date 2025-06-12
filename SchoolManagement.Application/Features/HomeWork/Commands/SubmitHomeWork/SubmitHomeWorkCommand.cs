using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.HomeWork.Commands.SubmitHomeWork;

public class SubmitHomeWorkCommand : IRequest<Result<string>>
{
    [Required]
    public Guid HomeWorkId { get; set; }
    
    [Required]
    public string FileUrl { get; set; }
}

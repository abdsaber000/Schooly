using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.ClassRoom.Command.AssignTeacerToClassroom;

public class AssignTeacherToClassroomCommand : IRequest<Result<string>>
{
    
   [Required] public string TeacherId { get; set; }
   [Required]  public Guid ClassRoomId { get; set; }
}
using System;
using MediatR;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Teacher.Command.UpdateTeacher;

public class UpdateTeacherCommandRequest : IRequest<Result<UpdateTeacherCommandDto>>
{
    public required string Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public Gender? Gender { get; set; }
    
}

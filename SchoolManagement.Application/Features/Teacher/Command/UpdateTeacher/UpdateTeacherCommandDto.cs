using System;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Teacher.Command.UpdateTeacher;

public class UpdateTeacherCommandDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public Gender? Gender { get; set; }
}

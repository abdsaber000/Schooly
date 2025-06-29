using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Teacher.Dtos;

public class TeacherDto
{
    public string Id { get; set; }
    public string Name { get; set;}
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string PhotoUrl { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
}
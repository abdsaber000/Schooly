using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Student.Dtos;

public class StudentDto
{
    public string Id { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public Role Role { get; set; }
    public Department Department { get; set; }
    public Grade Grade { get; set; }
    public string ParentName { get; set; } = string.Empty;
    public string ParentPhone1 { get; set; } = string.Empty;
    public DateOnly DateOfJoining { get; set; }
}

public static class StudentDtoExtensionMethold
{
    public static StudentDto ToStudentDto(this Domain.Entities.Student student)
    {
        return new StudentDto()
        {
            Id = student.Id,
            StudentName = student.Name,
            ProfilePictureUrl = student.ProfilePictureUrl,
            Email = student.Email,
            DateOfBirth = student.DateOfBarith,
            Gender = student.Gender,
            PhoneNumber = student.PhoneNumber,
            Role = student.Role,
            Department = student.Department,
            Grade = student.Grade,
            ParentName = student.Parent is null ? "" : student.Parent.ParentName,
            ParentPhone1 = student.Parent is null ? "" : student.Parent.Phone1,
            DateOfJoining = student.DateOfJoining
        };
    }
}
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Student.Dtos;

public class StudentDto
{
    public string Id { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
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
            StudentId = student.StudentId,
            Department = student.Department,
            Grade = student.Grade,
            ParentName = student.Parent.ParentName,
            ParentPhone1 = student.Parent.Phone1,
            DateOfJoining = student.DateOfJoining
        };
    }
}
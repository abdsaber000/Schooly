using SchoolManagement.Application.Features.Student.Commands.UpdateStudent;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Student.Dtos;
using Student = Domain.Entities.Student;
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
    public string ParentPhone2 { get; set; } = string.Empty;
    public string ParentJob { get; set; } = string.Empty;
    public DateOnly DateOfJoining { get; set; }
    public string Address { get; set; } = string.Empty;
    public Relation ParentRelation { get; set; }
}

public static class StudentDtoExtensionMethold
{
    private static readonly string _domainPrefix = "https://schoolly.runasp.net/api/upload/";
    private static string? HandleUrl(string? imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
        {
            return imageUrl;
        }
        return _domainPrefix + imageUrl;
    }
    public static StudentDto ToStudentDto(this Student student)
    {
        return new StudentDto()
        {
            Id = student.Id,
            StudentName = student.Name,
            ProfilePictureUrl = HandleUrl(student.ProfilePictureUrl),
            Email = student.Email,
            DateOfBirth = student.DateOfBarith,
            Gender = student.Gender,
            PhoneNumber = student.PhoneNumber,
            Role = student.Role,
            Department = student.Department,
            Grade = student.Grade,
            ParentName = student.Parent is null ? "" : student.Parent.ParentName,
            ParentPhone1 = student.Parent is null ? "" : student.Parent.Phone1,
            DateOfJoining = student.DateOfJoining,
            Address = student.Address,
            ParentJob = student.Parent.Job,
            ParentPhone2 = student.Parent.Phone2,
            ParentRelation = student.Parent.Relation
        };
    }

    public static UpdateStudentCommandDto ToUpdateStudentDto(this Student student)
    {
        return new UpdateStudentCommandDto()
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            ProfilePictureUrl = HandleUrl(student.ProfilePictureUrl),
            Address = student.Address,
            DateOfJoining = student.DateOfJoining,
            Department = student.Department,
            Grade = student.Grade,
            Gender = student.Gender,
            Parent = new UpdateParentDto()
            {
                Id = student.Parent?.ParentId,
                ParentName = student.Parent?.ParentName,
                Relation = student.Parent?.Relation,
                Job = student.Parent?.Job,
                Phone1 = student.Parent?.Phone1,
                Phone2 = student.Parent?.Phone2
            }
        };
    }
}
using SchoolManagement.Application.Features.Authentication.Commands.Register;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Authentication.Dtos;

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
    public static long GenerateUnique16DigitNumber()
    {
        Guid guid = Guid.NewGuid();
        long unique16DigitNumber = BitConverter.ToInt64(guid.ToByteArray(), 0);
        
        return Math.Abs(unique16DigitNumber % 9999999999999999L) + 1000000000000000L;
    }
    public static Domain.Entities.Student ToStudent(this RegisterStudentCommand command)
    {
        string unique16DigitNumber = GenerateUnique16DigitNumber().ToString();
        var uniqString = Guid.NewGuid().ToString();
        var parent = new Parent()
        {
            ParentId = uniqString,
            ParentName = command.ParentName,
            Job = command.ParentJob,
            Relation = command.ParentRelation,
            Phone1 = command.ParentPhone1,
            Phone2 = command.ParentPhone2
        };
        return new Domain.Entities.Student()
        {
            StudentId = unique16DigitNumber,
            ParentId = parent.ParentId,
            Name = command.StudentName,
            Email = command.StudentEmail,
            UserName = command.StudentEmail,
            Address = command.Address,
            Department = command.Department,
            Gender = command.Gender,
            Grade = command.Grade,
            DateOfJoining = command.DateOfJoining,
            DateOfBarith = command.DateOfBirth,
            Parent = parent,
        };
    }

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
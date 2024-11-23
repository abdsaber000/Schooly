using SchoolManagement.Application.Features.Student.Commands.AddStudent;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Student.Dtos;

public class StudentDtos
{
    
}

public static class StudentDtoExtensionMethold
{
    public static Domain.Entities.Student ToStudent(this AddStudentCommand command)
    {
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
            ParentId = parent.ParentId,
            Name = command.StudentName,
            Address = command.Address,
            Department = command.Department,
            Gender = command.Gender,
            Grade = command.Grade,
            DateOfJoining = command.DateOfJoining,
            DateOfBarith = command.DateOfBirth,
            Parent = parent,
            Email = uniqString,
            UserName = uniqString
        };
    }
}
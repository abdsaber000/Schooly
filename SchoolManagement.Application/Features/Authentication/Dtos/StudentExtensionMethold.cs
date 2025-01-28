using SchoolManagement.Application.Features.Authentication.Commands.Register;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Authentication.Dtos;

public static class StudentExtensionMetholdauth
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
}
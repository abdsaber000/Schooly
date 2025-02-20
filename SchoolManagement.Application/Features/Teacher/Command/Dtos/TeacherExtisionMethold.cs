using Microsoft.Identity.Client;

namespace SchoolManagement.Application.Features.Teacher.Command.Dtos;

public static class TeacherExtisionMethold
{
    public static Domain.Entities.Teacher ToTeacher(this AddTeacherCommand.AddTeacherCommand command)
    {
        return new Domain.Entities.Teacher()
        {
            Email = command.Email,
            UserName = command.Email,
            Name = command.name,
            Gender = command.Gender,
            DateOfBarith = command.DateOfBirth
        };
    }
}
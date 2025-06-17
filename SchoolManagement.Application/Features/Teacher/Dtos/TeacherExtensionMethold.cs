namespace SchoolManagement.Application.Features.Teacher.Dtos;

public static class TeacherExtensionMethold
{
    public static TeacherDto ToTeacherDto(this Domain.Entities.Teacher command)
    {
        return new TeacherDto()
        {
            Id = command.Id,
            Name = command.Name,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber
        };
    }
}
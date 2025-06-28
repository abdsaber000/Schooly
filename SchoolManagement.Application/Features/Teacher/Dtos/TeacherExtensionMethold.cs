namespace SchoolManagement.Application.Features.Teacher.Dtos;

using SchoolManagement.Application.Features.Teacher.Command.UpdateTeacher;
using Teacher = Domain.Entities.Teacher;
public static class TeacherExtensionMethold
{
    public static TeacherDto ToTeacherDto(this Teacher command)
    {
        return new TeacherDto()
        {
            Id = command.Id,
            Name = command.Name,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            PhotoUrl = command.ProfilePictureUrl
        };
    }

    public static UpdateTeacherCommandDto ToUpdateTeacher(this Teacher teacher)
    {
        return new UpdateTeacherCommandDto()
        {
            Id = teacher.Id,
            Name = teacher.Name,
            Email = teacher.Email,
            PhoneNumber = teacher.PhoneNumber,
            ProfilePictureUrl = teacher.ProfilePictureUrl,
            Gender = teacher.Gender
        };
    }
}
namespace SchoolManagement.Application.Features.Teacher.Dtos;

using SchoolManagement.Application.Features.Teacher.Command.UpdateTeacher;
using Teacher = Domain.Entities.Teacher;
public static class TeacherExtensionMethold
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
    public static TeacherDto ToTeacherDto(this Teacher command)
    {
        return new TeacherDto()
        {
            Id = command.Id,
            Name = command.Name,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            PhotoUrl = HandleUrl(command.ProfilePictureUrl),
            DateOfBirth = command.DateOfBarith,
            Gender = command.Gender
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
            ProfilePictureUrl = HandleUrl(teacher.ProfilePictureUrl),
            Gender = teacher.Gender
        };
    }
}
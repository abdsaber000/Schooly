using SchoolManagement.Application.Features.Authentication.Commands.Login;
using SchoolManagement.Application.Features.Authentication.Commands.Register;
using SchoolManagement.Application.Features.FaceRecognition.VerifyFace;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Authentication.Dtos;

public static class StudentExtensionMetholdauth
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
    public static Domain.Entities.Student ToStudent(this RegisterStudentCommand command)
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
            Email = command.StudentEmail,
            UserName = command.StudentEmail,
            PhoneNumber = command.PhoneNumber,
            Address = command.Address,
            Department = command.Department,
            Gender = command.Gender,
            Grade = command.Grade,
            DateOfJoining = command.DateOfJoining,
            DateOfBarith = command.DateOfBirth,
            Parent = parent,
        };
    }

    public static LoginDto ToLoginDto(this ApplicationUser user)
    {
        return new LoginDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            ProfilePictureUrl = HandleUrl(user.ProfilePictureUrl),
            Gender = user.Gender
        };
    }

    public static VerifyFaceCommandDto ToVerifyFaceCommandDto(this ApplicationUser user)
    {
        return new VerifyFaceCommandDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            ProfilePictureUrl = HandleUrl(user.ProfilePictureUrl),
            Gender = user.Gender
        };
    }
    
}
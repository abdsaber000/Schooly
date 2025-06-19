using System;
using SchoolManagement.Application.Features.Profile.Commands.UpdateProfile;
using SchoolManagement.Application.Features.Profile.Queries.GetProfile;
using SchoolManagement.Application.Features.Profile.Queries.GetUserInfo;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Profile.Dtos;
using Student = Domain.Entities.Student;
static public class ProfileExtensionMethod
{
    public static GetProfileQueryDto ToProfileQueryDto(this ApplicationUser user)
    {
        return new GetProfileQueryDto()
        {
            Name = user.Name,
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBarith,
            Role = user.Role,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Gender = user.Gender
        };
    }

    public static GetProfileQueryDto ToProfileQueryDto(this Student student)
    {
        return new GetProfileQueryDto()
        {
            Name = student.Name,
            Id = student.Id,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            DateOfBirth = student.DateOfBarith,
            Role = student.Role,
            ProfilePictureUrl = student.ProfilePictureUrl,
            Gender = student.Gender,
            StudentExtra = student.ToGetStudentExtraDto()
        };
    }

    public static UpdateProfileCommandDto ToProfileUpdateDto(this Student student)
    {
        return new UpdateProfileCommandDto()
        {
            Name = student.Name,
            Id = student.Id,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            DateOfBirth = student.DateOfBarith,
            Role = student.Role,
            ProfilePictureUrl = student.ProfilePictureUrl,
            Gender = student.Gender,
            StudentExtra = student.ToUpdateStudentExtraDto()
        };
    }

    public static UpdateProfileCommandDto ToProfileUpdateDto(this ApplicationUser student)
    {
        return new UpdateProfileCommandDto()
        {
            Name = student.Name,
            Id = student.Id,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            DateOfBirth = student.DateOfBarith,
            Role = student.Role,
            ProfilePictureUrl = student.ProfilePictureUrl,
            Gender = student.Gender,
        };
    }

    public static GetStudentExtraDto ToGetStudentExtraDto(this Student student)
    {
        return new GetStudentExtraDto()
        {
            Parent = student.Parent,
            Address = student.Address,
            DateOfJoining = student.DateOfJoining,
            Department = student.Department,
            Grade = student.Grade
        };
    }

    public static UpdateStudentExtraDto ToUpdateStudentExtraDto(this Student student)
    {
        return new UpdateStudentExtraDto()
        {
            Parent = student.Parent,
            Address = student.Address,
            DateOfJoining = student.DateOfJoining,
            Department = student.Department,
            Grade = student.Grade
        };
    }

    public static GetUserInfoQueryDto ToUserInfoQueryDto(this ApplicationUser user)
    {
        return new GetUserInfoQueryDto()
        {
            Id = user.Id,
            Name = user.Name,
            Role = user.Role,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Gender = user.Gender
        };
    }
}

using System;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
using SchoolManagement.Domain.Enums.User;

namespace SchoolManagement.Application.Features.Profile.Commands.UpdateProfile;
public class UpdateStudentExtraDto
{
    public Parent Parent { get; set; } = new Parent();
    public string Address { get; set; } = string.Empty;
     public DateOnly DateOfJoining { get; set; }
    public Department Department { get; set; }
    public Grade Grade { get; set; }

}
public class UpdateProfileCommandDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public Role Role { get; set; }
    public UpdateStudentExtraDto? StudentExtra { get; set; }
}

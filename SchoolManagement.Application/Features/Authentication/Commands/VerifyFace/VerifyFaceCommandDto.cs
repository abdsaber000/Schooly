using System;
using SchoolManagement.Domain.Enums;
using SchoolManagement.Domain.Enums.User;

namespace SchoolManagement.Application.Features.Authentication.Commands.VerifyFace;

public class VerifyFaceCommandDto
{
    public string UserId { get; set; } = string.Empty;
    public string Name {get; set;} = string.Empty;
    public string? Email {get; set;}
    public Gender? Gender {get; set;}
    public Role Role {get; set;}
}

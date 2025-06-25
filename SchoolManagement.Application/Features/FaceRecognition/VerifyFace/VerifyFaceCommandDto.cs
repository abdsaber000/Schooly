using System;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.FaceRecognition.VerifyFace;

public class VerifyFaceCommandDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public Role Role { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public Gender? Gender { get; set; }
}

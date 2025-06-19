using System;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Profile.Queries.GetUserInfo;

public class GetUserInfoQueryDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Role Role { get; set; }
    public Gender? Gender { get; set; }
    public string? ProfilePictureUrl { get; set; }
}

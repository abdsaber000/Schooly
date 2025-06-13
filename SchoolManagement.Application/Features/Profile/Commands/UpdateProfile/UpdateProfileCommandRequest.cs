using System;
using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.Profile.Commands.UpdateProfile;

public class UpdateProfileCommandRequest : IRequest<Result<UpdateProfileCommandDto>>
{
    public string? ProfilePictureUrl { get; set; }
}

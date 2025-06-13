using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.Profile.Commands.UpdateProfile;

public class UpdateProfileCommandRequest : IRequest<Result<UpdateProfileCommandDto>>
{
    public string? ProfilePictureUrl { get; set; }
    public string Name { get; set; } = string.Empty;
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }  
}

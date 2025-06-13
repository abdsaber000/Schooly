using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Services.AuthenticationService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums.User;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Application.Features.Profile.Dtos;
namespace SchoolManagement.Application.Features.Profile.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommandRequest, Result<UpdateProfileCommandDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserAuthenticationService _authenticationService;
    private readonly IStudentRepository _studentRepository;
    public UpdateProfileCommandHandler(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserAuthenticationService authenticationService,
        IStudentRepository studentRepository)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
        _authenticationService = authenticationService;
        _studentRepository = studentRepository;
    }
    public async Task<Result<UpdateProfileCommandDto>> Handle(UpdateProfileCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _authenticationService.GetCurrentUserAsync(_contextAccessor);
        if (request.ProfilePictureUrl != null)
        {
            user.ProfilePictureUrl = request.ProfilePictureUrl;
        }
        if (request.Name != null)
        {
            user.Name = request.Name;
        }
        if (request.Email != null)
        {
            user.Email = request.Email;
            var emailResult = await _userManager.SetEmailAsync(user, request.Email);
            if (!emailResult.Succeeded)
            {
                return Result<UpdateProfileCommandDto>.Failure("Failed to update email.");
            }
        }
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return Result<UpdateProfileCommandDto>.Failure("Failed to update profile.");
        }

        if (user.Role == Role.Student)
        {
            var student = await _studentRepository.GetByIdAsync(user.Id);
            if (student == null)
            {
                return Result<UpdateProfileCommandDto>.Failure("Student not found.");
            }
            return Result<UpdateProfileCommandDto>.Success(student.ToProfileUpdateDto());
        }
        return Result<UpdateProfileCommandDto>.Success(user.ToProfileUpdateDto());
    }
}

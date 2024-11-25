using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Authentication.Commands.Register;

public class RegisterUserCommand : IRequest<Result<string>>
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public DateOnly Birthday { get; set; } 
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _userManager.FindByEmailAsync(request.Email);
        if (existUser != null)
        {
            return Result<string>.Failure("Email already exists");
        }

        ApplicationUser user = new()
        {
            UserName = request.Email,
            Email = request.Email,
            Gender = request.Gender,
          //  DateOfBirth = request.Birthday
        };
        var createdUser = await _userManager.CreateAsync(user , request.Password);
        return createdUser.Succeeded ? Result<string>.Success(user.Id) : Result<string>.Failure("Failed to create user");
    }
}
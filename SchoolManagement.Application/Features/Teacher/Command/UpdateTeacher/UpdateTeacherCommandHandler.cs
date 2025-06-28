using System;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Features.Teacher.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Teacher.Command.UpdateTeacher;

using Teacher = Domain.Entities.Teacher;
public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommandRequest, Result<UpdateTeacherCommandDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITeacherRepository _teacherRepository;
    public UpdateTeacherCommandHandler(
        UserManager<ApplicationUser> userManager,
        ITeacherRepository teacherRepository)
    {
        _userManager = userManager;
        _teacherRepository = teacherRepository;
    }
    public async Task<Result<UpdateTeacherCommandDto>> Handle(UpdateTeacherCommandRequest request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(request.Id);
        if (teacher == null)
        {
            return Result<UpdateTeacherCommandDto>.Failure("User not found.", HttpStatusCode.NotFound);
        }
        await MapRequestToUser(request, teacher);
        await _teacherRepository.Update(teacher);
        return Result<UpdateTeacherCommandDto>.Success(teacher.ToUpdateTeacher());
    }

    private async Task MapRequestToUser(UpdateTeacherCommandRequest request, Teacher teacher)
    {
        if (request.Name != null)
        {
            teacher.Name = request.Name;
        }
        if (request.Email != null)
        {
            teacher.Email = request.Email;
            teacher.UserName = request.Email;
            await _userManager.SetEmailAsync(teacher, request.Email);
            await _userManager.SetUserNameAsync(teacher, request.Email);
        }
        if (request.PhoneNumber != null)
        {
            teacher.PhoneNumber = request.PhoneNumber;
        }
        if (request.ProfilePictureUrl != null)
        {
            teacher.ProfilePictureUrl = request.ProfilePictureUrl;
        }
        if (request.Gender != null)
        {
            teacher.Gender = request.Gender;
        }
    }
}

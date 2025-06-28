using System;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Teacher.Command.DeleteTeacher;

public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeaacherCommandRequest, Result<string>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    public DeleteTeacherCommandHandler(
        ITeacherRepository teacherRepository,
        UserManager<ApplicationUser> userManager)
    {
        _teacherRepository = teacherRepository;
        _userManager = userManager;
    }
    public async Task<Result<string>> Handle(DeleteTeaacherCommandRequest request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(request.Id);
        if (teacher == null)
        {
            return Result<string>.Failure("User is not found.", HttpStatusCode.NotFound);
        }
        await _teacherRepository.RemoveTeacher(teacher);
        
        return Result<string>.SuccessMessage("Teacher deleted successfully.");
    }
}

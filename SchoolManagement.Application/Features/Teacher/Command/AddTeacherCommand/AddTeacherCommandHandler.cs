using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.Teacher.Command.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Teacher.Command.AddTeacherCommand;

public class AddTeacherCommandHandler : IRequestHandler<AddTeacherCommand , Result<string>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer<AddTeacherCommand> _localizer;
    
    public AddTeacherCommandHandler(ITeacherRepository teacherRepository, UserManager<ApplicationUser> userManager, IStringLocalizer<AddTeacherCommand> localizer)
    {
        _teacherRepository = teacherRepository;
        _userManager = userManager;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(AddTeacherCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _userManager.FindByEmailAsync(request.Email);
        if (existUser != null)
        {
            return Result<string>.Failure(_localizer["Email already exists"]);
        }

        var teacher = request.ToTeacher();
        await _teacherRepository.AddTeacher(teacher, request.Password);
        return Result<string>.SuccessMessage(_localizer["Teacher created successfully"]);
    }
}
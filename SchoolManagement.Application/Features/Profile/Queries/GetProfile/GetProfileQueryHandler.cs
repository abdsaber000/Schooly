using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Services.AuthenticationService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Application.Features.Profile.Dtos;
using SchoolManagement.Domain.Enums;
using SchoolManagement.Domain.Interfaces.IRepositories;
using Microsoft.Extensions.Configuration;


namespace SchoolManagement.Application.Features.Profile.Queries.GetProfile;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQueryRequest, Result<GetProfileQueryDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserAuthenticationService _authenticationService;
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly string _urlPrefix;
    public GetProfileQueryHandler(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserAuthenticationService authenticationService,
        IStudentRepository studentRepository,
        ITeacherRepository teacherRepository,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
        _authenticationService = authenticationService;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
        _urlPrefix = configuration["Url:UploadPrefix"] ?? throw new ArgumentNullException(nameof(configuration));

    }
    public async Task<Result<GetProfileQueryDto>> Handle(GetProfileQueryRequest request, CancellationToken cancellationToken)
    {
        var user = await _authenticationService.GetCurrentUserAsync(_contextAccessor);
        if (user.Role == Role.Student)
        {
            return await HandleStudent(user.Id);
        }
        var result = user.ToProfileQueryDto();
        HandlePictureUrl(result);
        return Result<GetProfileQueryDto>.Success(result);
    }

    private async Task<Result<GetProfileQueryDto>> HandleStudent(string id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null)
        {
            return Result<GetProfileQueryDto>.Failure("Student not found.");
        }
        var result = student.ToProfileQueryDto();
        HandlePictureUrl(result);
        return Result<GetProfileQueryDto>.Success(result);
    }

    private void HandlePictureUrl(GetProfileQueryDto result)
    {
        if (result.ProfilePictureUrl != null)
        {
            result.ProfilePictureUrl = _urlPrefix + result.ProfilePictureUrl;
        }
    }

    private async Task<Result<GetProfileQueryDto>> HandleTeacher(string id)
    {
        var teacher = await _teacherRepository.GetByIdAsync(id);
        if (teacher == null)
        {
            return Result<GetProfileQueryDto>.Failure("Teacher not found.");
        }
        return Result<GetProfileQueryDto>.Success(teacher.ToProfileQueryDto());
    }
}

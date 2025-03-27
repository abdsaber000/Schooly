using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.HomeWork.Dtos;
using SchoolManagement.Application.Features.HomeWorke.Commands.AddHomeworke;
using SchoolManagement.Application.Services.FileService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Commands.AddHomeWork;

public class AddHomeWorkCommandsHandler : IRequestHandler<AddHomeWorkCommands , Result<string>>
{
    private readonly IFileService _fileService;
    private readonly IHomeWorkRepository _homeWorkRepository;
    private readonly IStringLocalizer<AddHomeWorkCommandsHandler> _localizer;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILessonRepository _lessonRepository;
    private readonly IClassRoomRepository _classRoomRepository;
    private readonly IUploadedFileRepositry _uploadedFileRepositry;

    public AddHomeWorkCommandsHandler(IHomeWorkRepository homeWorkRepository, IStringLocalizer<AddHomeWorkCommandsHandler> localizer, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IClassRoomRepository classRoomRepository, ILessonRepository lessonRepository, IUploadedFileRepositry uploadedFileRepositry)
    {
        _homeWorkRepository = homeWorkRepository;
        _localizer = localizer;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _classRoomRepository = classRoomRepository;
        _lessonRepository = lessonRepository;
        _uploadedFileRepositry = uploadedFileRepositry;
    }
    
    public async Task<Result<string>> Handle(AddHomeWorkCommands request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.lessonId);
        var classRoom = await _classRoomRepository.GetByIdAsync(request.classRoomId);
        if (lesson is null)
        {
            return Result<string>.Failure(_localizer["Lesson not found."]);
        }
        if (classRoom is null)
        {
            return Result<string>.Failure(_localizer["Classroom not found"]);
        }
        var teacher = _userManager.GetUserAsync(_httpContextAccessor?.HttpContext.User).Result;
        var homeWork = request.ToHomeWork(teacher);
        var file = await _uploadedFileRepositry.GetFileByName(request.FileUrl);
        homeWork.fileName = file.FileName;
        await _homeWorkRepository.AddAsync(homeWork);
        return Result<string>.SuccessMessage(_localizer["Homework added Successfully"]);
    }
}
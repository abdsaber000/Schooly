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

    public AddHomeWorkCommandsHandler(IFileService fileService, IHomeWorkRepository homeWorkRepository, IStringLocalizer<AddHomeWorkCommandsHandler> localizer, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
    {
        _fileService = fileService;
        _homeWorkRepository = homeWorkRepository;
        _localizer = localizer;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }
    
    public async Task<Result<string>> Handle(AddHomeWorkCommands request, CancellationToken cancellationToken)
    {
        var fileName = await _fileService.UploadFile(request.file);
        var teacher = _userManager.GetUserAsync(_httpContextAccessor?.HttpContext.User).Result;
        var homeWork = request.ToHomeWork(teacher, fileName);
        await _homeWorkRepository.AddHomeWork(homeWork);
        return Result<string>.SuccessMessage(_localizer["Homework added Successfully"]);
    }
}
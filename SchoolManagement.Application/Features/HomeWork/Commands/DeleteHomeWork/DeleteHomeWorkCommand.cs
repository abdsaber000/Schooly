using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Services.FileService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Commands.DeleteHomeWork;

public class DeleteHomeWorkCommand : IRequest<Result<string>>
{
    [Required]
    public string fileName;

    public DeleteHomeWorkCommand(string fileName)
    {
        this.fileName = fileName;
    }
}

public class DeleteHomeWorkCommandHandler : IRequestHandler<DeleteHomeWorkCommand, Result<string>>
{
    private readonly IHomeWorkRepository _homeWorkRepository;
    private readonly IStringLocalizer<DeleteHomeWorkCommand> _localizer;
    private readonly IFileService _fileService;
    public DeleteHomeWorkCommandHandler(IHomeWorkRepository homeWorkRepository, IStringLocalizer<DeleteHomeWorkCommand> localizer, IFileService fileService)
    {
        _homeWorkRepository = homeWorkRepository;
        _localizer = localizer;
        _fileService = fileService;
    }
    public async Task<Result<string>> Handle(DeleteHomeWorkCommand request, CancellationToken cancellationToken)
    {
        var homeWork = await _homeWorkRepository.GetHomeWorkByFileName(request.fileName);
        if (homeWork is null)
        {
            return Result<string>.Failure(_localizer["HomeWork not found"]);
        }

        await _fileService.DeleteFileAsync(homeWork.fileName);
        await _homeWorkRepository.Delete(homeWork);
        return Result<string>.SuccessMessage(_localizer["HomeWork deleted successfully"]);
    }
}
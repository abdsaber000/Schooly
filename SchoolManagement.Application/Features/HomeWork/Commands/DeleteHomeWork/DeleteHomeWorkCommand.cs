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
    public string fileUrl;

    public DeleteHomeWorkCommand(string fileUrl)
    {
        this.fileUrl = fileUrl;
    }
}

public class DeleteHomeWorkCommandHandler : IRequestHandler<DeleteHomeWorkCommand, Result<string>>
{
    private readonly IHomeWorkRepository _homeWorkRepository;
    private readonly IStringLocalizer<DeleteHomeWorkCommand> _localizer;
    private readonly IFileService _fileService;
    private readonly IUploadedFileRepositry _uploadedFileRepositry;
    public DeleteHomeWorkCommandHandler(IHomeWorkRepository homeWorkRepository, IStringLocalizer<DeleteHomeWorkCommand> localizer, IFileService fileService, IUploadedFileRepositry uploadedFileRepositry)
    {
        _homeWorkRepository = homeWorkRepository;
        _localizer = localizer;
        _fileService = fileService;
        _uploadedFileRepositry = uploadedFileRepositry;
    }
    public async Task<Result<string>> Handle(DeleteHomeWorkCommand request, CancellationToken cancellationToken)
    {
        var homeWork = await _homeWorkRepository.GetHomeWorkByFileUrl(request.fileUrl);
        if (homeWork is null)
        {
            return Result<string>.Failure(_localizer["HomeWork not found"]);
        }
        await _homeWorkRepository.Delete(homeWork);
        var moreHomeWorkWithSameName = await _homeWorkRepository.GetHomeWorkByFileUrl(request.fileUrl);
        if (moreHomeWorkWithSameName is null)
        {
            await _fileService.DeleteFileAsync(homeWork.FileUrl);
        }
        return Result<string>.SuccessMessage(_localizer["HomeWork deleted successfully"]);
    }
}
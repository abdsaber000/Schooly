using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Services.FileService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Query.GetHomeWork;

public class GetHomeWorkQuery : IRequest<IActionResult>
{
    public string fileName;
    public GetHomeWorkQuery(string fileName)
    {
        this.fileName = fileName;
    }
}

public class GetHomeWorkQueryHandler : IRequestHandler<GetHomeWorkQuery, IActionResult>
{
    private readonly IHomeWorkRepository _homeWorkRepository;
    private readonly IFileService _fileService;
    public GetHomeWorkQueryHandler(IHomeWorkRepository homeWorkRepository, IFileService fileService)
    {
        _homeWorkRepository = homeWorkRepository;
        _fileService = fileService;
    }
    public async Task<IActionResult> Handle(GetHomeWorkQuery request, CancellationToken cancellationToken)
    {
        var homeWork = await _homeWorkRepository.GetHomeWorkByFileUrl(request.fileName);
        if (homeWork is null)
        {
            return new NotFoundResult();
        }

        var file = await _fileService.GetFileAsync(homeWork.FileUrl);
        return file;
    }
}
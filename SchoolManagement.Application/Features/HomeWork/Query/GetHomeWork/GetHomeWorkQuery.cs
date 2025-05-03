using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.HomeWork.Dtos;
using SchoolManagement.Application.Services.FileService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Query.GetHomeWork;

public class GetHomeWorkQuery : IRequest<Result<HomeWorkDto>>
{
    public string fileName;
    public GetHomeWorkQuery(string fileName)
    {
        this.fileName = fileName;
    }
}

public class GetHomeWorkQueryHandler : IRequestHandler<GetHomeWorkQuery, Result<HomeWorkDto>>
{
    private readonly IHomeWorkRepository _homeWorkRepository;
    private readonly IFileService _fileService;
    private readonly IStringLocalizer<GetHomeWorkQueryHandler> _localizer;
    public GetHomeWorkQueryHandler(IHomeWorkRepository homeWorkRepository, IFileService fileService, IStringLocalizer<GetHomeWorkQueryHandler> localizer)
    {
        _homeWorkRepository = homeWorkRepository;
        _fileService = fileService;
        _localizer = localizer;
    }
    public async Task<Result<HomeWorkDto>> Handle(GetHomeWorkQuery request, CancellationToken cancellationToken)
    {
        var homeWork = await _homeWorkRepository.GetHomeWorkByFileUrl(request.fileName);
        if (homeWork is null)
        {
            return Result<HomeWorkDto>.Failure(_localizer["HomeWork not found"]);
        }

        return Result<HomeWorkDto>.Success(homeWork.ToHomeWorkDto());
    }
}
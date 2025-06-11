using System;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Services.FileService;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Upload.Queries;

public class GetFileQuery : IRequest<IActionResult>
{
    public GetFileQuery(string fileUrl)
    {
        FileUrl = fileUrl;
    }

    public string FileUrl { get; set; }
   
}

public class GetFileQueryHandler : IRequestHandler<GetFileQuery, IActionResult>
{

    private readonly IFileService _fileService;
    private readonly IStringLocalizer<GetFileQueryHandler> _localizer;

    public GetFileQueryHandler(IStringLocalizer<GetFileQueryHandler> localizer, IFileService fileService)
    {
        _localizer = localizer;
        _fileService = fileService;
    }

    public async Task<IActionResult> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        var (stream, contentType, downloadName, errorKey) = await _fileService.GetFileAsync(request.FileUrl);

        if (errorKey != null)
        {
            return errorKey switch
            {
                "InvalidFileName" => new BadRequestObjectResult(_localizer[errorKey]),
                _ => new NotFoundObjectResult(_localizer[errorKey])
            };
        }
        return new FileStreamResult(stream, contentType) 
        { 
            FileDownloadName = downloadName 
        };
    }
}

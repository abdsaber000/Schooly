using System;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
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
    private readonly IUploadedFileRepositry _uploadedFileRepositry;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IFileService _fileService;
    public GetFileQueryHandler(IUploadedFileRepositry uploadedFileRepositry
        , IWebHostEnvironment webHostEnvironment, IFileService fileService)
    {
        _uploadedFileRepositry = uploadedFileRepositry;
        _webHostEnvironment = webHostEnvironment;
        _fileService = fileService;
    }
    public async Task<IActionResult> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        return await _fileService.GetFileAsync(request.FileUrl);
    }
}

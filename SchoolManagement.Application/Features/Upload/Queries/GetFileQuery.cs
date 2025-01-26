using System;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Upload.Queries;

public class GetFileQuery : IRequest<IActionResult>
{
    public string FileName { get; set; } = string.Empty;
    public GetFileQuery(string fileName)
    {
        FileName = fileName;
    }
}

public class GetFileQueryHandler : IRequestHandler<GetFileQuery, IActionResult>
{
    private readonly IUploadedFileRepositry _uploadedFileRepositry;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public GetFileQueryHandler(IUploadedFileRepositry uploadedFileRepositry
        , IWebHostEnvironment webHostEnvironment)
    {
        _uploadedFileRepositry = uploadedFileRepositry;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<IActionResult> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        var file = await _uploadedFileRepositry.GetFileByName(request.FileName);
        if (file == null)
        {
            return new NotFoundResult();
        }
        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", file.StoredFileName);

        using FileStream fileStream = new(path, FileMode.Open);

        // MemoryStream memoryStream = new();

        // await fileStream.CopyToAsync(memoryStream);
        // memoryStream.Position = 0;

        // return new FileStreamResult(memoryStream, file.ContentType)
        // {
        //     FileDownloadName = file.FileName
        // };

        return new PhysicalFileResult(path , file.ContentType){
            FileDownloadName = file.FileName
        };
    }
}

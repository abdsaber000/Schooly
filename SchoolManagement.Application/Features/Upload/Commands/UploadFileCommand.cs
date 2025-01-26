using System;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Upload.Commands;

public class UploadFileCommand : IRequest<Result<UploadedFile>>
{
    required public IFormFile formFile {get; set;}
}

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Result<UploadedFile>>
{
    private readonly IUploadedFileRepositry _uploadedFileRepositry;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public UploadFileCommandHandler(IUploadedFileRepositry uploadedFileRepositry
        , IWebHostEnvironment webHostEnvironment)
    {
        _uploadedFileRepositry = uploadedFileRepositry;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<Result<UploadedFile>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var file = request.formFile;
        var fakeFileName = Path.GetRandomFileName();
        var uploadedFile = new UploadedFile
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            StoredFileName = fakeFileName
        };
        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", fakeFileName);

        using FileStream fileStream = new(path, FileMode.Create);

        await file.CopyToAsync(fileStream);
        
        await _uploadedFileRepositry.AddFile(uploadedFile);
        return Result<UploadedFile>.Success(uploadedFile);
    }
}
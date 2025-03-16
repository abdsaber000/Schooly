using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Services.FileService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Upload.Commands;

public class UploadFileCommand : IRequest<Result<UploadedFile>>
{
    [Required] public IFormFile formFile {get; set;}
}

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Result<UploadedFile>>
{
    private readonly IFileService _fileService;
    public UploadFileCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }
    public async Task<Result<UploadedFile>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var uploadedFile = await _fileService.UploadFile(request.formFile);
        return Result<UploadedFile>.Success(uploadedFile);
    }
}
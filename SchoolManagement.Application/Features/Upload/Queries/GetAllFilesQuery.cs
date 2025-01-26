using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Upload.Queries;

public class GetAllFilesQuery : IRequest<Result<List<UploadedFile>>>
{
    
}

public class GetAllFilesQueryHandler : IRequestHandler<GetAllFilesQuery, Result<List<UploadedFile>>>
{
    private readonly IUploadedFileRepositry _uploadedFileRepositry;
    public GetAllFilesQueryHandler(IUploadedFileRepositry uploadedFileRepositry)
    {
        _uploadedFileRepositry = uploadedFileRepositry;
    }
    public async Task<Result<List<UploadedFile>>> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
    {
        return Result<List<UploadedFile>>.Success(await _uploadedFileRepositry.GetAllFiles());
    }
}
